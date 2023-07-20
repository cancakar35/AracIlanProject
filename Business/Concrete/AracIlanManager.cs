using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Business.Concrete
{
    public class AracIlanManager : IAracIlanService
    {
        private IAracIlanDal _aracIlanDal;
        private IAracService _aracService;

        public AracIlanManager(IAracIlanDal aracIlanDal, IAracService aracService)
        {
            _aracIlanDal = aracIlanDal;
            _aracService = aracService;
        }
        public async Task<IResult> Add(AddIlanDto addIlanDto, Arac arac, int userId)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    await _aracService.Add(arac);
                    Ilan newIlan = new Ilan
                    {
                        UserId = userId,
                        AracId = arac.Id,
                        TelefonNo = addIlanDto.TelefonNo,
                        Aciklama = addIlanDto.Aciklama,
                        Tarih = DateTime.Now,
                        Il = addIlanDto.Il,
                        Ilce = addIlanDto.Ilce,
                        Mahalle = addIlanDto.Mahalle,
                        IsActive = true
                    };
                    await _aracIlanDal.Add(newIlan);
                    scope.Complete();
                }
                catch {
                    scope.Dispose();
                    return new ErrorResult(Messages.IlanEklemeBasarisiz);
                }
            }
            return new SuccessResult();
        }

        public async Task<IDataResult<List<IlanDto>>> GetAllIlanDetails()
        {
            return new SuccessDataResult<List<IlanDto>>(await _aracIlanDal.GetAllIlanDetails());
        }

        public async Task<IDataResult<IlanDto>> GetIlanDetailById(int id)
        {
            try
            {
                IlanDto? ilan = await _aracIlanDal.GetIlanDetailById(id);
                if (ilan == null)
                {
                    return new ErrorDataResult<IlanDto>(Messages.IlanBulunamadi);
                }
                return new SuccessDataResult<IlanDto>(ilan);
            }
            catch
            {
                return new ErrorDataResult<IlanDto>("Hata oluştu");
            }
        }

        public async Task<IResult> Remove(int id, int userId)
        {
            Ilan? ilan = await _aracIlanDal.Get(x => x.Id == id);
            if (ilan == null)
            {
                return new ErrorResult(Messages.IlanBulunamadi);
            }
            if (ilan.UserId != userId)
            {
                return new ErrorResult("Bu işlem için yetkiniz yok!");
            }
            ilan.IsActive = false;
            await _aracIlanDal.Update(ilan);
            return new SuccessResult();
        }

        public async Task<IResult> Update(int id, AddIlanDto addIlanDto, int userId)
        {
            Ilan? ilan = await _aracIlanDal.Get(x => x.Id == id);
            if (ilan == null)
            {
                return new ErrorResult(Messages.IlanBulunamadi);
            }
            if (ilan.UserId != userId)
            {
                return new ErrorResult("Bu işlem için yetkiniz yok!");
            }
            ilan.TelefonNo = addIlanDto.TelefonNo;
            ilan.Aciklama = addIlanDto.Aciklama;
            ilan.Il = addIlanDto.Il;
            ilan.Ilce = addIlanDto.Ilce;
            ilan.Mahalle = addIlanDto.Mahalle;
            await _aracIlanDal.Update(ilan);
            return new SuccessResult();
        }
    }
}

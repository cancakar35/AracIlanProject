using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
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
        private IAracResimService _aracResimService;

        public AracIlanManager(IAracIlanDal aracIlanDal, IAracService aracService,
            IAracResimService aracResimService)
        {
            _aracIlanDal = aracIlanDal;
            _aracService = aracService;
            _aracResimService = aracResimService;
        }
        public async Task<IResult> Add(AddIlanDto addIlanDto, Arac arac, IFormFileCollection fileCollection, int userId)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    var aracResult = await _aracService.Add(arac);
                    if (!aracResult.Success)
                    {
                        throw new Exception();
                    }
                    await _aracResimService.Add(fileCollection, aracResult.Data.Id);
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

        public async Task<IDataResult<List<AracIlanDto>>> GetAllIlanDetails()
        {
            return new SuccessDataResult<List<AracIlanDto>>(await _aracIlanDal.GetAllIlanDetails());
        }

        public async Task<IDataResult<AracIlanDto>> GetIlanDetailById(int id)
        {
            try
            {
                AracIlanDto? ilan = await _aracIlanDal.GetIlanDetailById(id);
                if (ilan == null)
                {
                    return new ErrorDataResult<AracIlanDto>(Messages.IlanBulunamadi);
                }
                return new SuccessDataResult<AracIlanDto>(ilan);
            }
            catch
            {
                return new ErrorDataResult<AracIlanDto>("Hata oluştu");
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

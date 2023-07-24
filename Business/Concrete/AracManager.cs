using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AracManager : IAracService
    {
        private IAracDal _aracDal;
        public AracManager(IAracDal aracDal)
        {
            _aracDal = aracDal;
        }
        public async Task<IDataResult<Arac>> Add(Arac arac)
        {
            try
            {
                Arac? addArac = await _aracDal.Add(arac);
                if (addArac == null)
                {
                    return new ErrorDataResult<Arac>("Hata oluştu!");
                }
                return new SuccessDataResult<Arac>(addArac);
            }
            catch
            {
                return new ErrorDataResult<Arac>(Messages.AracEklemeBasarisiz);
            }
        }

        public async Task<IDataResult<List<AracDto>>> GetAllDetailed()
        {
            return new SuccessDataResult<List<AracDto>>(await _aracDal.GetAllAracDetails());
        }

        public async Task<IDataResult<AracDto>> GetAracDetailById(int id)
        {
            var arac = await _aracDal.GetAracDetails(id);
            if (arac == null)
            {
                return new ErrorDataResult<AracDto>(Messages.AracBulunamadi);
            }
            return new SuccessDataResult<AracDto>(arac);
        }

        public async Task<IDataResult<Arac>> Update(Arac arac)
        {
            try
            {
                Arac? updateArac = await _aracDal.Update(arac);
                if (updateArac == null)
                {
                    return new ErrorDataResult<Arac>("Hata oluştu!");
                }
                await _aracDal.Update(arac);
                return new SuccessDataResult<Arac>(updateArac);
            }
            catch
            {
                return new ErrorDataResult<Arac>(Messages.GuncellemeBasarisiz);
            }
        }
    }
}

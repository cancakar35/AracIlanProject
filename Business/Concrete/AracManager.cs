using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public async Task<IResult> Add(Arac arac)
        {
            try
            {
                await _aracDal.Add(arac);
                return new SuccessResult();
            }
            catch
            {
                return new ErrorResult(Messages.AracEklemeBasarisiz);
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

        public async Task<IResult> Update(Arac arac)
        {
            try
            {
                await _aracDal.Update(arac);
                return new SuccessResult();
            }
            catch
            {
                return new ErrorResult(Messages.GuncellemeBasarisiz);
            }
        }
    }
}

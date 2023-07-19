using Business.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AracIlanManager : IAracIlanService
    {
        public Task<IDataResult<Ilan>> Add(AddIlanDto addIlanDto)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<List<IlanDto>>> GetAllIlanDetails()
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<IlanDto>> GetIlanDetailById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<Ilan>> Update(int id, AddIlanDto addIlanDto)
        {
            throw new NotImplementedException();
        }
    }
}

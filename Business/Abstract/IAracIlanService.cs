using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAracIlanService
    {
        Task<IDataResult<Ilan>> Add(AddIlanDto addIlanDto);
        Task<IDataResult<Ilan>> Update(int id, AddIlanDto addIlanDto);
        Task<IResult> Remove(int id);
        Task<IDataResult<IlanDto>> GetIlanDetailById(int id);
        Task<IDataResult<List<IlanDto>>> GetAllIlanDetails();
    }
}

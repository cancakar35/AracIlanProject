using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAracIlanService
    {
        Task<IResult> Add(AddIlanDto addIlanDto, Arac arac, IFormFileCollection fileCollection, int userId);
        Task<IResult> Update(int id, AddIlanDto addIlanDto, int userId);
        Task<IResult> Remove(int id, int userId);
        Task<IDataResult<AracIlanDto>> GetIlanDetailById(int id);
        Task<IDataResult<List<AracIlanDto>>> GetAllIlanDetails();
    }
}

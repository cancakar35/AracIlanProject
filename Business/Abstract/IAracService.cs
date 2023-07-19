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
    public interface IAracService
    {
        Task<IResult> Add(Arac arac);
        Task<IResult> Update(Arac arac);
        Task<IDataResult<List<AracDto>>> GetAllDetailed();
        Task<IDataResult<AracDto>> GetAracDetailById(int id);
    }
}

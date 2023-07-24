using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAracResimService
    {
        Task<IDataResult<List<AracResim>>> GetAll(Expression<Func<AracResim, bool>>? expr = null);
        Task<IDataResult<List<AracResim>>> GetByAracId(int aracId);
        Task<IDataResult<AracResim>> GetById(int id);
        Task<IResult> Add(IFormFileCollection fileCollection, int aracId);
        Task<IResult> Update(IFormFile file, int id);
        Task<IResult> Delete(int id);
    }
}

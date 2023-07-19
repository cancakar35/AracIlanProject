using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IAracIlanDal : IEntityRepository<Ilan>
    {
        Task<IlanDto?> GetIlanDetailById(int id);
        Task<List<IlanDto>> GetAllIlanDetails(Expression<Func<Ilan,bool>>? expr=null);
    }
}

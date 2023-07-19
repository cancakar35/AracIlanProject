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
    public interface IAracDal : IEntityRepository<Arac>
    {
        Task<List<AracDto>> GetAllAracDetails(Expression<Func<Arac, bool>>? expr);
        Task<AracDto?> GetAracDetails(int id);
    }
}

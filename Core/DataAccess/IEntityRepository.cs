using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        Task<List<T>> GetAll(Expression<Func<T,bool>>? expr);
        Task<T?> Get(Expression<Func<T, bool>> expr);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}

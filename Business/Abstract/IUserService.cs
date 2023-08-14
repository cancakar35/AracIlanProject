using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        Task Add(User user);
        Task Update(User user);
        Task Delete(User user);
        Task<User?> GetById(int id);
        Task<User?> GetByMail(string mail);
        Task<List<OperationClaim>> GetClaims(User user);
    }
}

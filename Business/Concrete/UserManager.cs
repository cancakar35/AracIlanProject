using Business.Abstract;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public async Task Add(User user)
        {
            await _userDal.Add(user);
        }

        public async Task Delete(User user)
        {
            await _userDal.Delete(user);
        }

        public async Task<User?> GetByMail(string mail)
        {
            return await _userDal.Get(u => u.Email == mail);
        }

        public async Task<List<OperationClaim>> GetClaims(User user)
        {
            return await _userDal.GetClaims(user);
        }

        public async Task Update(User user)
        {
            await _userDal.Update(user);
        }
    }
}

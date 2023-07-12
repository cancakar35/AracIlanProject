using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Login(UserLoginDto userLoginDto);
        IDataResult<User> Register(UserRegisterDto userRegisterDto);
        IDataResult<AccessToken> CreateAccessToken(User user);
        IResult UserExist(string email);
    }
}

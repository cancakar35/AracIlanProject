using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        Task<IDataResult<User>> Login(UserLoginDto userLoginDto);
        Task<IDataResult<User>> Register(UserRegisterDto userRegisterDto);
        Task<IDataResult<Tokens>> CreateAccessToken(User user);
        Task<IDataResult<Tokens>> RefreshToken(string accessToken, string refreshToken);
        Task<IResult> UserExist(string email);
    }
}

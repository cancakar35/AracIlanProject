using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        IUserService _userService;
        ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;

        }
        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            try
            {
                var claims = _userService.GetClaims(user);
                return new SuccessDataResult<AccessToken>(_tokenHelper.CreateToken(user, claims.Result));
            }
            catch
            {
                return new ErrorDataResult<AccessToken>();
            }
        }

        public IDataResult<User> Login(UserLoginDto userLoginDto)
        {
            User? userToLogin = _userService.GetByMail(userLoginDto.Email).Result;
            if (userToLogin == null)
            {
                return new ErrorDataResult<User>(Messages.KullaniciHataliGiris);
            }
            if (HashingHelper.VerifyPasswordHash(userLoginDto.Password, userToLogin.PasswordHash, userToLogin.PasswordSalt))
            {
                return new SuccessDataResult<User>(userToLogin);
            }
            return new ErrorDataResult<User>(Messages.KullaniciHataliGiris);
        }

        public IDataResult<User> Register(UserRegisterDto userRegisterDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userRegisterDto.Password, out passwordHash, out passwordSalt);
            User user = new User
            {
                FirstName = userRegisterDto.FirstName,
                LastName = userRegisterDto.LastName,
                Email = userRegisterDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            try
            {
                _userService.Add(user);
                return new SuccessDataResult<User>(user);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<User>(ex.Message);
            }
        }

        public IResult UserExist(string email)
        {
            if(_userService.GetByMail(email).Result != null)
            {
                return new ErrorResult(Messages.KullaniciMevcut);
            }
            return new SuccessResult();
        }
    }
}

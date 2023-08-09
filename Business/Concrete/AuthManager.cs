﻿using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;
using FluentValidation;
using FluentValidation.Results;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenHelper;
        private readonly IValidator<User> _userValidator;
        private readonly IValidator<UserRegisterDto> _userRegisterValidator;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper, IValidator<User> userValidator, IValidator<UserRegisterDto> userRegisterValidator)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _userValidator = userValidator;
            _userRegisterValidator = userRegisterValidator;
        }
        public async Task<IDataResult<AccessToken>> CreateAccessToken(User user)
        {
            try
            {
                var claims = await _userService.GetClaims(user);
                return new SuccessDataResult<AccessToken>(_tokenHelper.CreateToken(user, claims));
            }
            catch
            {
                return new ErrorDataResult<AccessToken>();
            }
        }

        public async Task<IDataResult<User>> Login(UserLoginDto userLoginDto)
        {
            User? userToLogin = await _userService.GetByMail(userLoginDto.Email);
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

        public async Task<IDataResult<User>> Register(UserRegisterDto userRegisterDto)
        {
            var result = await _userRegisterValidator.ValidateAsync(userRegisterDto);
            if (!result.IsValid)
            {
                return new ErrorDataResult<User>(JsonConvert.SerializeObject(result.Errors));
            }
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
                var userValidateResult = await _userValidator.ValidateAsync(user);
                if (!userValidateResult.IsValid)
                {
                    return new ErrorDataResult<User>(JsonConvert.SerializeObject(userValidateResult.Errors));
                }
                await _userService.Add(user);
                return new SuccessDataResult<User>(user);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<User>(ex.Message);
            }
        }

        public async Task<IResult> UserExist(string email)
        {
            if(await _userService.GetByMail(email) != null)
            {
                return new ErrorResult(Messages.KullaniciMevcut);
            }
            return new SuccessResult();
        }
    }
}

using Business.Abstract;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AracIlanProject.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var userToLogin = await _authService.Login(userLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }
            var result = await _authService.CreateAccessToken(userToLogin.Data);
            if (result.Success)
            {
                var refreshCookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = result.Data.RefreshToken.Expiration
                };
                Response.Cookies.Append("refreshToken", result.Data.RefreshToken.Token, refreshCookieOptions);
                return Ok(result.Data.AccessToken);
            }
            return BadRequest(result.Message);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            var checkUserExist = await _authService.UserExist(userRegisterDto.Email);
            if (!checkUserExist.Success)
            {
                return BadRequest("Bu email kullanılamaz!");
            }
            var registerResult = await _authService.Register(userRegisterDto);
            if (!registerResult.Success)
            {
                return BadRequest(registerResult.Message);
            }
            var result = await _authService.CreateAccessToken(registerResult.Data);
            if (result.Success)
            {
                var refreshCookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = result.Data.RefreshToken.Expiration
                };
                Response.Cookies.Append("refreshToken", result.Data.RefreshToken.Token, refreshCookieOptions);
                return Ok(result.Data.AccessToken);
            }
            return BadRequest(result.Message);
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken(AccessToken accessToken)
        {
            string? refreshToken;
            if (!Request.Cookies.TryGetValue("refreshToken", out refreshToken))
            {
                return BadRequest();
            }
            if (refreshToken == null) return BadRequest();
            var result = await _authService.RefreshToken(accessToken.Token, refreshToken);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            var refreshCookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = result.Data.RefreshToken.Expiration
            };
            Response.Cookies.Append("refreshToken", result.Data.RefreshToken.Token, refreshCookieOptions);
            return Ok(result.Data.AccessToken);
        }
    }
}

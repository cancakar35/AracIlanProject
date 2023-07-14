using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AracIlanProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public ActionResult Login(UserLoginDto userLoginDto)
        {
            var result = _authService.Login(userLoginDto);
            if (result.Success)
            {
                try
                {
                    return Ok(_authService.CreateAccessToken(result.Data));
                }
                catch
                {
                    return StatusCode(500);
                }
            }
            return BadRequest(result.Message);
        }

        [HttpPost("register")]
        public ActionResult Register(UserRegisterDto userRegisterDto)
        {
            var checkUserExist = _authService.UserExist(userRegisterDto.Email);
            if (!checkUserExist.Success)
            {
                return BadRequest("Bu email kullanılamaz!");
            }
            var registerResult = _authService.Register(userRegisterDto);
            if (!registerResult.Success)
            {
                return BadRequest("Kayıt başarısız!");
            }
            return Ok(_authService.CreateAccessToken(registerResult.Data));
        }
    }
}

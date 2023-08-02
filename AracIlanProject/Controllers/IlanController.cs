using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AracIlanProject.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class IlanController : ControllerBase
    {
        private readonly IAracIlanService _ilanService;
        private readonly IAracService _aracService;

        public IlanController(IAracIlanService ilanService, IAracService aracService)
        {
            _ilanService = ilanService;
            _aracService = aracService;
        }

        [AllowAnonymous]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _ilanService.GetAllIlanDetails();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var result = await _ilanService.GetIlanDetailById(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm] AddIlanDto addIlanDto, [FromForm] Arac arac, [FromForm] IFormFileCollection files)
        {
            if (addIlanDto == null || arac == null) {
                return BadRequest();
            }
            int userId;
            if (int.TryParse(User.Claims.First(i => i.Type == "UserId").Value, out userId) == false){
                return StatusCode(403);
            }
            var result = await _ilanService.Add(addIlanDto, arac, files, userId);
            if (result.Success)
            {
                return Ok();
            }
            return BadRequest(result.Message);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromForm] int id, [FromForm] AddIlanDto addIlanDto)
        {
            if (addIlanDto == null) {
                return BadRequest();
            }
            int userId;
            if (int.TryParse(User.Claims.First(i => i.Type == "UserId").Value, out userId) == false)
            {
                return StatusCode(403);
            }
            var result = await _ilanService.Update(id, addIlanDto, userId);
            if (result.Success)
            {
                return Ok();
            }
            return BadRequest(result.Message);
        }

        [HttpPost("remove")]
        public async Task<IActionResult> Remove([FromBody] int id)
        {
            int userId;
            if (int.TryParse(User.Claims.First(i => i.Type == "UserId").Value, out userId) == false)
            {
                return StatusCode(403);
            }
            var result = await _ilanService.Remove(id, userId);
            if (result.Success)
            {
                return Ok();
            }
            return BadRequest(result.Message);
        }
    }
}

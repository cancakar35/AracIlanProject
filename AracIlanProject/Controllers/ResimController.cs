using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AracIlanProject.Controllers
{
    [Authorize]
    [Route("api/resimler")]
    [ApiController]
    public class ResimController : ControllerBase
    {
        private readonly IAracResimService _resimService;

        public ResimController(IAracResimService resimService)
        {
            _resimService = resimService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllImages()
        {
            var result = await _resimService.GetAll();
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByResimId([FromRoute] int id)
        {
            var result = await _resimService.GetById(id);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetByAracId([FromQuery] int aracId)
        {
            var result = await _resimService.GetByAracId(aracId);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }
    }
}

using Business.Abstract;
using Entities.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AracIlanProject.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OzellikController<TOzellik> : ControllerBase
        where TOzellik : class, IOzellik, new()
    {
        private readonly IOzellikService<TOzellik> _ozellikService;

        public OzellikController(IOzellikService<TOzellik> ozellikService)
        {
            _ozellikService = ozellikService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _ozellikService.GetAll();
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var result = await _ozellikService.GetById(id);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }
    }
}

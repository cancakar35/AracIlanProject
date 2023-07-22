using Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AracIlanProject.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MarkaController : ControllerBase
    {
        private readonly IMarkaService _markaService;

        public MarkaController(IMarkaService markaService)
        {
            _markaService = markaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _markaService.GetMarkaList();
            if (!result.Success)
            {
                return NotFound();
            }
            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _markaService.GetMarkaById(id);
            if (!result.Success)
            {
                return NotFound();
            }
            return Ok(result.Data);
        }
    }
}

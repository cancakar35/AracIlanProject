using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
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
        [HttpGet("filtre")]
        public async Task<IActionResult> GetAllFilter([FromQuery] List<int>? kategori, [FromQuery] List<int>? markaId, [FromQuery] List<int>? renkId, [FromQuery] List<int?>? vitesTipiId,
            [FromQuery] List<int?>? yakitTipiId, [FromQuery] List<int?>? kasaTipiId, [FromQuery] List<int?>? cekisTipiId)
        {
            Expression<Func<Arac, bool>> aracExpr = arac => (kategori.Count == 0 || kategori.Contains(arac.KategoriId))
                                                        &&(markaId.Count==0 || markaId.Contains(arac.MarkaId))
                                                        && (renkId.Count == 0 || renkId.Contains(arac.RenkId))
                                                        && (vitesTipiId.Count == 0 || vitesTipiId.Contains(arac.VitesTipiId))
                                                        && (yakitTipiId.Count == 0 || yakitTipiId.Contains(arac.YakitTipiId))
                                                        && (kasaTipiId.Count == 0 || kasaTipiId.Contains(arac.KasaTipiId))
                                                        && (cekisTipiId.Count == 0 || cekisTipiId.Contains(arac.CekisTipiId));
            var result = await _ilanService.GetAllIlanDetails(aracExpr);
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

        [AllowAnonymous]
        [HttpGet("getbykategori/{kategoriId}")]
        public async Task<IActionResult> GetByKategoriId([FromRoute] int kategoriId)
        {
            var result = await _ilanService.GetAllIlanByKategoriId(kategoriId);
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

using Microsoft.AspNetCore.Mvc;
using System.Linq;
using SAV.Api.Dtos;
using SAV.Core.Interfaces.Services;

namespace SAV.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarcasController : ControllerBase
    {
        private readonly IMarcaService _service;

        public MarcasController(IMarcaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _service.ObtenerTodasLasMarcas();
            var dtos = items.Select(m => new MarcaDto(m.Id, m.Nombre));
            return Ok(dtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.ObtenerMarcaPorId(id);
            if (item is null) return NotFound();
            return Ok(new MarcaDto(item.Id, item.Nombre));
        }
    }
}

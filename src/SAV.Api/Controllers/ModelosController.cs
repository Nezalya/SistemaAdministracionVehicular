using Microsoft.AspNetCore.Mvc;
using System.Linq;
using SAV.Api.Dtos;
using SAV.Core.Interfaces.Services;

namespace SAV.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModelosController : ControllerBase
    {
        private readonly IModeloService _service;

        public ModelosController(IModeloService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _service.ObtenerTodosLosModelos();
            var dtos = items.Select(m => new ModeloDto(m.Id, m.Nombre, m.MarcaId));
            return Ok(dtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.ObtenerModeloPorId(id);
            if (item is null) return NotFound();
            return Ok(new ModeloDto(item.Id, item.Nombre, item.MarcaId));
        }
    }
}

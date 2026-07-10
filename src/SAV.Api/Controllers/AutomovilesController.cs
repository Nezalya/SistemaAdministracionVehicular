using Microsoft.AspNetCore.Mvc;
using System.Linq;
using SAV.Api.Dtos;
using SAV.Core.Entities.Inventario;
using SAV.Core.Interfaces.Services;

namespace SAV.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutomovilesController : ControllerBase
    {
        private readonly IAutomovilService _service;

        public AutomovilesController(IAutomovilService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _service.ObtenerTodosLosAutomoviles();
            var dtos = items.Select(a => new AutomovilDto(a.Id, a.ModeloId, a.NumBastidor, a.Color, a.AnoFabricacion, a.Estado));
            return Ok(dtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.ObtenerAutomovilPorId(id);
            if (item is null) return NotFound();
            var dto = new AutomovilDto(item.Id, item.ModeloId, item.NumBastidor, item.Color, item.AnoFabricacion, item.Estado);
            return Ok(dto);
        }

        [HttpGet("disponibles")]
        public async Task<IActionResult> GetDisponibles()
        {
            var items = await _service.ObtenerAutomovilesDisponibles();
            var dtos = items.Select(a => new AutomovilDto(a.Id, a.ModeloId, a.NumBastidor, a.Color, a.AnoFabricacion, a.Estado));
            return Ok(dtos);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAutomovilDto dto)
        {
            var entity = new Automovil
            {
                ModeloId = dto.ModeloId,
                NumBastidor = dto.NumBastidor,
                Color = dto.Color,
                AnoFabricacion = dto.AnoFabricacion,
                Estado = dto.Estado,
                ConcesionarioPropietarioId = dto.ConcesionarioPropietarioId
            };

            var creado = await _service.RegistrarAutomovil(entity);
            var resultDto = new AutomovilDto(creado.Id, creado.ModeloId, creado.NumBastidor, creado.Color, creado.AnoFabricacion, creado.Estado);
            return CreatedAtAction(nameof(GetById), new { id = creado.Id }, resultDto);
        }
    }
}

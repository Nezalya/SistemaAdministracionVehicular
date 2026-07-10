using Microsoft.AspNetCore.Mvc;
using SAV.Api.Dtos;
using SAV.Core.Interfaces.Services;

namespace SAV.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CaracteristicasController : ControllerBase
    {
        private readonly ICaracteristicaService _service;

        public CaracteristicasController(ICaracteristicaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _service.ObtenerTodasLasCaracteristicas();
            var dtos = items.Select(c => new CaracteristicaDto(c.Id, c.Nombre, c.Descripcion, c.Categoria));
            return Ok(dtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.ObtenerCaracteristicaPorId(id);
            if (item is null) return NotFound();
            return Ok(new CaracteristicaDto(item.Id, item.Nombre, item.Descripcion, item.Categoria));
        }
    }
}

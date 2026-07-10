using Microsoft.AspNetCore.Mvc;
using SAV.Api.Dtos;
using SAV.Core.Entities.RedDeDistribuciónYPersonal;
using SAV.Core.Interfaces.Services;

namespace SAV.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicioOficialesController : ControllerBase
    {
        private readonly IServicioOficialService _service;

        public ServicioOficialesController(IServicioOficialService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _service.ObtenerTodosLosServiciosOficiales();
            var dtos = items.Select(s => new ServicioOficialDto(s.Id, s.ConcesionarioId, s.Nombre, s.Nif, s.Domicilio, s.Telefono));
            return Ok(dtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.ObtenerServicioOficialPorId(id);
            if (item is null) return NotFound();
            return Ok(new ServicioOficialDto(item.Id, item.ConcesionarioId, item.Nombre, item.Nif, item.Domicilio, item.Telefono));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateServicioOficialDto dto)
        {
            var entity = new ServicioOficial
            {
                ConcesionarioId = dto.ConcesionarioId,
                Nombre = dto.Nombre,
                Nif = dto.Nif,
                Domicilio = dto.Domicilio,
                Telefono = dto.Telefono
            };

            var creado = await _service.CrearServicioOficial(entity);
            return CreatedAtAction(nameof(GetById), new { id = creado.Id }, new ServicioOficialDto(creado.Id, creado.ConcesionarioId, creado.Nombre, creado.Nif, creado.Domicilio, creado.Telefono));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateServicioOficialDto dto)
        {
            var entity = new ServicioOficial
            {
                ConcesionarioId = dto.ConcesionarioId,
                Nombre = dto.Nombre,
                Nif = dto.Nif,
                Domicilio = dto.Domicilio,
                Telefono = dto.Telefono
            };

            var actualizado = await _service.ActualizarServicioOficial(id, entity);
            return Ok(new ServicioOficialDto(actualizado.Id, actualizado.ConcesionarioId, actualizado.Nombre, actualizado.Nif, actualizado.Domicilio, actualizado.Telefono));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.EliminarServicioOficial(id);
            return NoContent();
        }
    }
}

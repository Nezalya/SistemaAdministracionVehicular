using Microsoft.AspNetCore.Mvc;
using SAV.Api.Dtos;
using SAV.Core.Entities.RedDeDistribuciónYPersonal;
using SAV.Core.Interfaces.Services;

namespace SAV.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendedoresController : ControllerBase
    {
        private readonly IVendedorService _service;

        public VendedoresController(IVendedorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _service.ObtenerTodosLosVendedores();
            var dtos = items.Select(v => new VendedorDto(v.Id, v.Nombre, v.Telefono ?? string.Empty, v.Email ?? string.Empty));
            return Ok(dtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.ObtenerVendedorPorId(id);
            if (item is null) return NotFound();
            return Ok(new VendedorDto(item.Id, item.Nombre, item.Telefono ?? string.Empty, item.Email ?? string.Empty));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVendedorDto dto)
        {
            var entity = new Vendedor
            {
                Nombre = dto.Nombre,
                Telefono = dto.Telefono,
                Email = dto.Email
            };

            var creado = await _service.CrearVendedor(entity);
            return CreatedAtAction(nameof(GetById), new { id = creado.Id }, new VendedorDto(creado.Id, creado.Nombre, creado.Telefono ?? string.Empty, creado.Email ?? string.Empty));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateVendedorDto dto)
        {
            var entity = new Vendedor
            {
                Nombre = dto.Nombre,
                Telefono = dto.Telefono,
                Email = dto.Email
            };

            var actualizado = await _service.ActualizarVendedor(id, entity);
            return Ok(new VendedorDto(actualizado.Id, actualizado.Nombre, actualizado.Telefono ?? string.Empty, actualizado.Email ?? string.Empty));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.EliminarVendedor(id);
            return NoContent();
        }
    }
}

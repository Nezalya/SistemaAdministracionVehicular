using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using SAV.Api.Dtos;
using SAV.Core.Entities.RedDeDistribuciónYPersonal;
using SAV.Core.Interfaces.Services;

namespace SAV.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _service;

        public ClientesController(IClienteService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _service.ObtenerTodosLosClientes();
            var dtos = items.Select(c => new ClienteDto(c.Id, c.Nombre, c.Nif, c.Domicilio, c.Telefono, c.Email, c.FechaRegistro));
            return Ok(dtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.ObtenerClientePorId(id);
            if (item is null) return NotFound();
            var dto = new ClienteDto(item.Id, item.Nombre, item.Nif, item.Domicilio, item.Telefono, item.Email, item.FechaRegistro);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClienteDto dto)
        {
            var entity = new Cliente
            {
                Nombre = dto.Nombre,
                Nif = dto.Nif,
                Domicilio = dto.Domicilio,
                Telefono = dto.Telefono,
                Email = dto.Email,
                FechaRegistro = DateTime.UtcNow
            };

            var creado = await _service.CrearCliente(entity);
            var result = new ClienteDto(creado.Id, creado.Nombre, creado.Nif, creado.Domicilio, creado.Telefono, creado.Email, creado.FechaRegistro);
            return CreatedAtAction(nameof(GetById), new { id = creado.Id }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateClienteDto dto)
        {
            var entity = new Cliente
            {
                Nombre = dto.Nombre,
                Nif = dto.Nif,
                Domicilio = dto.Domicilio,
                Telefono = dto.Telefono,
                Email = dto.Email
            };

            var actualizado = await _service.ActualizarCliente(id, entity);
            var result = new ClienteDto(actualizado.Id, actualizado.Nombre, actualizado.Nif, actualizado.Domicilio, actualizado.Telefono, actualizado.Email, actualizado.FechaRegistro);
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.EliminarCliente(id);
            return NoContent();
        }
    }
}

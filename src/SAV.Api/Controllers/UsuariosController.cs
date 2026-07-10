using Microsoft.AspNetCore.Mvc;
using System.Linq;
using SAV.Api.Dtos;
using SAV.Core.Entities.Seguridad;
using SAV.Core.Interfaces.Services;

namespace SAV.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuariosController(IUsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _service.ObtenerTodosLosUsuarios();
            var dtos = items.Select(u => new UsuarioDto(u.Id, u.Username, (int)u.Rol, u.Activo));
            return Ok(dtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.ObtenerUsuarioPorId(id);
            if (item is null) return NotFound();
            return Ok(new UsuarioDto(item.Id, item.Username, (int)item.Rol, item.Activo));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUsuarioDto dto)
        {
            var entity = new Usuario
            {
                Username = dto.Username,
                Rol = (SAV.Core.Enums.RolUsuario)dto.Rol,
                VendedorId = dto.VendedorId,
                ConcesionarioId = dto.ConcesionarioId,
                ClienteId = dto.ClienteId,
                Activo = true
            };

            var creado = await _service.CrearUsuario(entity, dto.Password);
            return CreatedAtAction(nameof(GetById), new { id = creado.Id }, new UsuarioDto(creado.Id, creado.Username, (int)creado.Rol, creado.Activo));
        }
    }
}

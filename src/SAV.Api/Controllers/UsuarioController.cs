using Microsoft.AspNetCore.Authorization;
using SAV.Api.Authorization;

namespace SAV.Api.Controllers
{
    [Authorize(Roles = Roles.Administrador)]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;
        public UsuarioController(IUsuarioService usuarioService, IMapper mapper)
        { 
            _usuarioService = usuarioService; 
            _mapper = mapper; 
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerUsuarios() 
        {
            var usuarios = await _usuarioService.ObtenerTodosLosUsuarios();
            var usuariosDto = _mapper.Map<IEnumerable<UsuarioDto>>(usuarios);
            return Ok(usuariosDto);
        }
            

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerUsuarioPorId(int id)
        {
            var usuario = await _usuarioService.ObtenerUsuarioPorId(id);
            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);
            return Ok(usuarioDto);
        }

    }
}
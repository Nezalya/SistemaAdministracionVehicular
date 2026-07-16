namespace SAV.Api.Controllers
{
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
        public async Task<IActionResult> ObtenerUsuariosPorId(int id)
        {
            var usuario = await _usuarioService.ObtenerUsuarioPorId(id);
            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);
            return Ok(usuarioDto);
        }

        [HttpPost]
        public async Task<IActionResult> CrearUsuario(CrearUsuarioDto dto)
        {
            var usuario = _mapper.Map<Usuario>(dto);
            var usuarioCreado = await _usuarioService.CrearUsuario(usuario, dto.Password);
            return CreatedAtAction(nameof(ObtenerUsuariosPorId), new { id = usuarioCreado.Id }, _mapper.Map<UsuarioDto>(usuarioCreado));
        }
    }
}
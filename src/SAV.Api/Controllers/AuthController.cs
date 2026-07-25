using SAV.Api.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace SAV.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var resultado = await _authService.Login(dto.Email, dto.Password);
            return Ok(resultado);
        }

        [Authorize(Roles = Roles.Administrador)]
        [HttpPost("registro-administrador")]
        public async Task<IActionResult> RegistroAdministrador(RegistrarAdministradorDto dto)
        {
            var usuario = await _authService.RegistrarAdministrador(dto.Username, dto.Email, dto.Password);
            return Ok(new { usuario.Id, usuario.Username, usuario.Email, usuario.FechaRegistro, rol = usuario.Rol.ToString() });
        }

        [Authorize(Roles = Roles.Administrador)]
        [HttpPost("registro-vendedor")]
        public async Task<IActionResult> RegistroVendedor(RegistrarVendedorDto dto)
        {
            var vendedor = _mapper.Map<Vendedor>(dto);
            var usuario = await _authService.RegistrarVendedor(dto.Username, dto.Email, dto.Password, vendedor);
            return Ok(new { usuario.Id, usuario.Username, usuario.Email, usuario.FechaRegistro, rol = usuario.Rol.ToString() });
        }

        [Authorize(Roles = Roles.AdminOVendedor)]
        [HttpPost("registro-cliente")]
        public async Task<IActionResult> RegistroCliente(RegistrarClienteDto dto)
        {
            var cliente = _mapper.Map<Cliente>(dto);
            var usuario = await _authService.RegistrarCliente(dto.Username, dto.Email, dto.Password, cliente);
            return Ok(new { usuario.Id, usuario.Username, usuario.Email, usuario.FechaRegistro, rol = usuario.Rol.ToString() });
        }
    }
}
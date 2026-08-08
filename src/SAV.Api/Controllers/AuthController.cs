using Microsoft.AspNetCore.Authorization;
using SAV.Api.Authorization;

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
            var respuesta = _mapper.Map<UsuarioRegistradoDto>(usuario);
            return CreatedAtAction(actionName: nameof(UsuarioController.ObtenerUsuarioPorId),controllerName: "Usuario",routeValues: new { id = usuario.Id },value: respuesta);
        }

        [Authorize(Roles = Roles.Administrador)]
        [HttpPost("registro-vendedor")]
        public async Task<IActionResult> RegistroVendedor(RegistrarVendedorDto dto)
        {
            var vendedor = _mapper.Map<Vendedor>(dto);
            var usuario = await _authService.RegistrarVendedor(dto.Username, dto.Email, dto.Password, vendedor);
            var respuesta = _mapper.Map<UsuarioRegistradoDto>(usuario);

            return CreatedAtAction(nameof(UsuarioController.ObtenerUsuarioPorId), "Usuario", new { id = usuario.Id }, respuesta);
        }

        [Authorize(Roles = Roles.AdminOVendedor)]
        [HttpPost("registro-cliente")]
        public async Task<IActionResult> RegistroCliente(RegistrarClienteDto dto)
        {
            var cliente = _mapper.Map<Cliente>(dto);
            var usuario = await _authService.RegistrarCliente(dto.Username, dto.Email, dto.Password, cliente);
            var respuesta = _mapper.Map<UsuarioRegistradoDto>(usuario);

            return CreatedAtAction(nameof(UsuarioController.ObtenerUsuarioPorId), "Usuario", new { id = usuario.Id }, respuesta);
        }
    }
}
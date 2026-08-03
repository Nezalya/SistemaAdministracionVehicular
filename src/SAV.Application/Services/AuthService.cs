using Microsoft.AspNetCore.Identity;

namespace SAV.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IVendedorRepository _vendedorRepository;
        private readonly IConcesionarioRepository _concesionarioRepository;
        private readonly ITokenService _tokenService;
        private readonly PasswordHasher<Usuario> _passwordHasher;   

        public AuthService(IUsuarioRepository usuarioRepository,
                           IClienteRepository clienteRepository,
                           IVendedorRepository vendedorRepository,
                           IConcesionarioRepository concesionarioRepository,
                           ITokenService tokenService,
                           PasswordHasher<Usuario> passwordHasher)
        {
            _usuarioRepository = usuarioRepository;
            _clienteRepository = clienteRepository;
            _vendedorRepository = vendedorRepository;
            _concesionarioRepository = concesionarioRepository;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;
        }

        public async Task<ResultadoAutenticacionDto> Login(string email, string password)
        {
            var usuario = await _usuarioRepository.ObtenerUsuarioPorEmail(email);
            if (usuario is null)
                throw new BusinessException("Correo o contraseña incorrectos.");

            if (!usuario.Activo)
                throw new BusinessException("El usuario está inactivo.");

            var verificacion = _passwordHasher.VerifyHashedPassword(usuario, usuario.PasswordHash, password);
            if (verificacion == PasswordVerificationResult.Failed)
                throw new BusinessException("Correo o contraseña incorrectos.");

            var token = _tokenService.GenerarToken(usuario);
            return new ResultadoAutenticacionDto
            {
                Token = token.Token,
                Usuario = usuario.Username,
                Rol = usuario.Rol.ToString(),
                Expira = token.Expira
            };
        }

        
        public async Task<Usuario> RegistrarAdministrador(string username, string email, string password)
        {
            var usuario = await CrearCuenta(username, email, password, RolUsuario.Administrador);
            await _usuarioRepository.Add(usuario);
            await _usuarioRepository.SaveChanges();
            return usuario;
        }

        public async Task<Usuario> RegistrarVendedor(string username, string email, string password, Vendedor vendedor)
        {
            
            var conMismoNif = await _vendedorRepository.ObtenerVendedorPorNif(vendedor.Nif);
            if (conMismoNif is not null)
                throw new BusinessException("Ya existe un vendedor con ese NIF.");

           
            var concesionario = (await _concesionarioRepository.GetAll()).FirstOrDefault();
            if (concesionario is null)
                throw new BusinessException("No hay un concesionario registrado en el sistema.");
            vendedor.ConcesionarioId = concesionario.Id;

            var usuario = await CrearCuenta(username, email, password, RolUsuario.Vendedor);
            usuario.Vendedor = vendedor;   

            await _usuarioRepository.Add(usuario);
            await _usuarioRepository.SaveChanges();
            return usuario;
        }

        public async Task<Usuario> RegistrarCliente(string username, string email, string password, Cliente cliente)
        {
            var conMismoNif = await _clienteRepository.ObtenerClientePorNif(cliente.Nif);
            if (conMismoNif is not null)
                throw new BusinessException("Ya existe un cliente con ese NIF.");

            cliente.FechaRegistro = DateTime.Now;

            var usuario = await CrearCuenta(username, email, password, RolUsuario.Cliente);
            usuario.Cliente = cliente;

            await _usuarioRepository.Add(usuario);
            await _usuarioRepository.SaveChanges();
            return usuario;
        }

        // Helper privado: email único + cuenta base (evita repetir en los 3 registros)
        private async Task<Usuario> CrearCuenta(string username, string email, string password, RolUsuario rol)
        {
            var existente = await _usuarioRepository.ObtenerUsuarioPorEmail(email);
            if (existente is not null)
                throw new BusinessException("El correo ya está registrado.");

            var usuario = new Usuario
            {
                Username = username,
                Email = email,
                Rol = rol,                         
                Activo = true,
                FechaRegistro = DateTime.Now       
            };
            usuario.PasswordHash = _passwordHasher.HashPassword(usuario, password);
            return usuario;
        }
    }
}
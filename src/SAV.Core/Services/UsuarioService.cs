using Microsoft.AspNetCore.Identity;
namespace SAV.Core.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly PasswordHasher<Usuario> _passwordHasher = new();

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEnumerable<Usuario>> ObtenerTodosLosUsuarios() 
        { 
            return await _usuarioRepository.GetAll();
        }

        public async Task<Usuario?> ObtenerUsuarioPorId(int id) 
        {
            return await _usuarioRepository.GetById(id);
        }

        public async Task<Usuario> CrearUsuario(Usuario usuario, string password)
        {
            usuario.PasswordHash = _passwordHasher.HashPassword(usuario, password);
            usuario.Activo = true;

            await _usuarioRepository.Add(usuario);
            await _usuarioRepository.SaveChanges();
            return usuario;
        }
    }
}
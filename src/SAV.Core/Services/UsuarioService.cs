using Microsoft.AspNetCore.Identity;
namespace SAV.Core.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

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

    }
}
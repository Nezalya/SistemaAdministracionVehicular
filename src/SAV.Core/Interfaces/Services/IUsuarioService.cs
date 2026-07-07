namespace SAV.Core.Interfaces.Services
{
    public interface IUsuarioService
    {
        Task<IEnumerable<Usuario>> ObtenerTodosLosUsuarios();
        Task<Usuario?> ObtenerUsuarioPorId(int id);
        Task<Usuario> CrearUsuario(Usuario usuario, string password);
    }
}
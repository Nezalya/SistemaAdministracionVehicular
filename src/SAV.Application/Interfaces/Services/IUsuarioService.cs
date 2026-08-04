namespace SAV.Application.Interfaces.Services
{
    public interface IUsuarioService
    {
        Task<IEnumerable<Usuario>> ObtenerTodosLosUsuarios();
        Task<Usuario?> ObtenerUsuarioPorId(int id);
    }
}
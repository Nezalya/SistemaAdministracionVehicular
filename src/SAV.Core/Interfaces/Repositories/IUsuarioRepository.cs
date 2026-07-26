namespace SAV.Core.Interfaces.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario?> ObtenerPorUsername(string username);
        Task<Usuario?> ObtenerUsuarioPorEmail(string email);
    }

}
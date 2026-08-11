namespace SAV.Application.Interfaces.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario?> ObtenerPorUsername(string username);
        Task<Usuario?> ObtenerUsuarioPorEmail(string email);
        Task<Usuario?> ObtenerPorVendedorId(int vendedorId);
    }

}
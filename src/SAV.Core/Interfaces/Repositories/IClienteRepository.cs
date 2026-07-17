namespace SAV.Core.Interfaces.Repositories
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<Cliente?> ObtenerClientePorNif(string nif);
    }
}
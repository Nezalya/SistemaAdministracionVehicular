namespace SAV.Application.Interfaces.Repositories
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<Cliente?> ObtenerClientePorNif(string nif);
        Task<ResumenComprasClienteDto?> ObtenerResumenCompras(int clienteId);
        Task<bool> TieneVentas(int clienteId);
    }
}

namespace SAV.Application.Interfaces.Repositories
{
    public interface IAutomovilRepository : IRepository<Automovil>
    {
        Task<IEnumerable<Automovil>> ObtenerAutomovilesDisponibles();
        Task<Automovil?> ObtenerAutomovil(int id);
        Task<Automovil?> ObtenerAutomovilPorBastidor(string numBastidor);
        Task<IEnumerable<Automovil>> ObtenerAutomovilesVendidos();
        Task<ResumenStockDto> ObtenerResumenStock();
        Task<PagedResult<Automovil>> BuscarAutomoviles(FiltroAutomovilDto filtro);
    }
}

global using SAV.Api.DTOs;
namespace SAV.Core.Interfaces.Repositories
{
    public interface IAutomovilRepository : IRepository<Automovil>
    {
        Task<IEnumerable<Automovil>> ObtenerAutomovilesDisponibles();
        Task<Automovil?> ObtenerAutomovil(int id);
        Task<IEnumerable<Automovil>> ObtenerTodosLosAutomoviles();
        Task<Automovil?> ObtenerAutomovilPorBastidor(string numBastidor);
        Task<IEnumerable<Automovil>> ObtenerAutomovilesVendidos();
        Task<ResumenStockDto> ObtenerResumenStock();
    }
}

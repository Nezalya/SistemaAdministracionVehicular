global using SAV.Api.DTOs;
using SAV.Core.Dtos;
namespace SAV.Core.Interfaces.Repositories
{
    public interface IAutomovilRepository : IRepository<Automovil>
    {
        Task<IEnumerable<Automovil>> ObtenerAutomovilesDisponibles();
        Task<Automovil?> ObtenerAutomovil(int id);
        Task<Automovil?> ObtenerAutomovilPorBastidor(string numBastidor);
        Task<IEnumerable<Automovil>> ObtenerAutomovilesVendidos();
        Task<ResumenStockDto> ObtenerResumenStock();
        Task<IEnumerable<Automovil>> BuscarAutomoviles(FiltroAutomovilDto filtro);
    }
}

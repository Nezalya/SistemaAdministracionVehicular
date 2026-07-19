using SAV.Core.Dtos;

namespace SAV.Core.Interfaces.Repositories
{
    public interface IVentaRepository : IRepository<Venta>
    {
        Task<Venta?> ObtenerVentaConDetalle(int id);
        Task<IEnumerable<Venta>> ObtenerVentasConDetalle();
    }
}
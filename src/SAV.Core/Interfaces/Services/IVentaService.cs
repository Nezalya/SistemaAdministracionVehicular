namespace SAV.Core.Interfaces.Services
{
    public interface IVentaService
    {
        Task<IEnumerable<Venta>> ObtenerTodasLasVentas();
        Task<Venta?> ObtenerVentaPorId(int id);
        Task<Venta> CrearVenta(Venta venta, List<int> caracteristicaIdsExtras);
    }
}
namespace SAV.Application.Interfaces.Services
{
    public interface IVentaService
    {
        Task<IEnumerable<Venta>> ObtenerTodasLasVentas();
        Task<PagedResult<Venta>> BuscarVentas(FiltroVentaDto filtro);
        Task<Venta?> ObtenerVentaPorId(int id);
        Task<Venta> CrearVenta(Venta venta, List<int> caracteristicaIdsExtras);
    }
}

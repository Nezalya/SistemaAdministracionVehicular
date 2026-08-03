namespace SAV.Application.Interfaces.Services
{
    public interface IVendedorService
    {
        Task<IEnumerable<Vendedor>> ObtenerTodosLosVendedores();
        Task<Vendedor?> ObtenerVendedorPorId(int id);
        Task<Vendedor> ActualizarVendedor(int id, Vendedor vendedor);
        Task EliminarVendedor(int id);
        Task<ResumenVentasVendedorDto> ObtenerResumenVentas(int vendedorId);
    }
}

using SAV.Core.Dtos;

namespace SAV.Core.Interfaces.Services
{
    public interface IVendedorService
    {
        Task<IEnumerable<Vendedor>> ObtenerTodosLosVendedores();
        Task<Vendedor?> ObtenerVendedorPorId(int id);
        Task<Vendedor> CrearVendedor(Vendedor vendedor);
        Task<Vendedor> ActualizarVendedor(int id, Vendedor vendedor);
        Task EliminarVendedor(int id);
        Task<ResumenVentasVendedorDto?> ObtenerResumenVentas(int vendedorId);
    }
}

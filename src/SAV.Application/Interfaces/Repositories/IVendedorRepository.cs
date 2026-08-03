namespace SAV.Application.Interfaces.Repositories
{
    public interface IVendedorRepository : IRepository<Vendedor>
    {
        Task<Vendedor?> ObtenerVendedorPorNif(string nif);
        Task<ResumenVentasVendedorDto?> ObtenerResumenVentas(int vendedorId);

        Task<bool> TieneVentas(int vendedorId);
    }
}
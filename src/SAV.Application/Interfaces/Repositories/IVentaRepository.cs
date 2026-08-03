namespace SAV.Application.Interfaces.Repositories
{
    public interface IVentaRepository : IRepository<Venta>
    {
        Task<Venta?> ObtenerVentaConDetalle(int id);
        Task<IEnumerable<Venta>> ObtenerVentasConDetalle();
    }
}
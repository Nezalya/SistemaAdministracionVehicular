namespace SAV.Core.Interfaces.Repositories
{
    public interface IVendedorRepository : IRepository<Vendedor>
    {
        Task<Vendedor?> ObtenerVendedorPorNif(string nif);
    }
}
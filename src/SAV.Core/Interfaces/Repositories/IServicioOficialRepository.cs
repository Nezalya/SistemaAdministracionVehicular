namespace SAV.Core.Interfaces.Repositories
{
    public interface IServicioOficialRepository : IRepository<ServicioOficial>
    {
        Task<ServicioOficial?> ObtenerServicioOficialPorNif(string nif);
    }
}
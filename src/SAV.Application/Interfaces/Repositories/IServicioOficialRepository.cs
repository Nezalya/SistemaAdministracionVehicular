namespace SAV.Application.Interfaces.Repositories
{
    public interface IServicioOficialRepository : IRepository<ServicioOficial>
    {
        Task<ServicioOficial?> ObtenerServicioOficialPorNif(string nif);
        Task<bool> TieneVentas(int servicioOficialId);

    }
}
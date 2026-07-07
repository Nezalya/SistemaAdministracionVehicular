namespace SAV.Core.Interfaces.Services
{
    public interface IServicioOficialService
    {
        Task<IEnumerable<ServicioOficial>> ObtenerTodosLosServiciosOficiales();
        Task<ServicioOficial?> ObtenerServicioOficialPorId(int id);
        Task<ServicioOficial> CrearServicioOficial(ServicioOficial servicioOficial);
        Task<ServicioOficial> ActualizarServicioOficial(int id, ServicioOficial servicioOficial);
        Task EliminarServicioOficial(int id);
    }
}
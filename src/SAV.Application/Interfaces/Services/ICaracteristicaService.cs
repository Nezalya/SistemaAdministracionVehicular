namespace SAV.Application.Interfaces.Services
{
    public interface ICaracteristicaService
    {
        Task<IEnumerable<Caracteristica>> ObtenerTodasLasCaracteristicas();
        Task<Caracteristica?> ObtenerCaracteristicaPorId(int id);
    }
}
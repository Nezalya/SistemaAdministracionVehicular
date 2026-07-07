namespace SAV.Core.Interfaces.Services
{
    public interface ICaracteristicaService
    {
        Task<IEnumerable<Caracteristica>> ObtenerTodasLasCaracteristicas();
        Task<Caracteristica?> ObtenerCaracteristicaPorId(int id);
    }
}
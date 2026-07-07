namespace SAV.Core.Interfaces.Services
{
    public interface IModeloService
    {
        Task<IEnumerable<Modelo>> ObtenerTodosLosModelos();
        Task<Modelo?> ObtenerModeloPorId(int id);
    }
}

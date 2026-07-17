namespace SAV.Core.Interfaces.Services
{
    public interface IModeloService
    {
        Task<IEnumerable<Modelo>> ObtenerTodosLosModelos();
        Task<Modelo?> ObtenerModeloPorId(int id);
        Task<IEnumerable<ModeloExtra>?> ObtenerExtrasDeModelo(int id);
        Task<IEnumerable<ModeloSerie>?> ObtenerSerieDeModelo(int id);        
        Task<Modelo?> ObtenerEquipamientoDeModelo(int id);
    }
}

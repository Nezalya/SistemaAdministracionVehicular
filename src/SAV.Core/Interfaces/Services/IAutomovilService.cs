namespace SAV.Core.Interfaces.Services
{
    public interface IAutomovilService
    {
        Task<IEnumerable<Automovil>> ObtenerTodosLosAutomoviles();
        Task<Automovil?> ObtenerAutomovilPorId(int id);
        Task<IEnumerable<Automovil>> ObtenerAutomovilesDisponibles();
        Task<Automovil> RegistrarAutomovil(Automovil automovil);
        Task<IEnumerable<Automovil>> ObtenerAutomovilesVendidos();   
        Task<Automovil> ActualizarAutomovil(int id, Automovil automovil);   
        Task EliminarAutomovil(int id);
    }
}
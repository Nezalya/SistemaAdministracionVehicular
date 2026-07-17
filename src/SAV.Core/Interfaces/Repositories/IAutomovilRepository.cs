namespace SAV.Core.Interfaces.Repositories
{
    public interface IAutomovilRepository : IRepository<Automovil>
    {
        Task<IEnumerable<Automovil>> ObtenerAutomovilesDisponibles();
        Task<Automovil?> ObtenerAutomovil(int id);
        Task<IEnumerable<Automovil>> ObternerTodosLosAutomoviles();
        Task<Automovil?> ObtenerAutomovilPorBastidor(string numBastidor);
        Task<IEnumerable<Automovil>> ObtenerAutomovilesVendidos();
    }
}

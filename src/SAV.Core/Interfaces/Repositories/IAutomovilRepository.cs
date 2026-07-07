namespace SAV.Core.Interfaces.Repositories
{
    public interface IAutomovilRepository : IRepository<Automovil>
    {
        Task<IEnumerable<Automovil>> GetDisponibles();
        Task<Automovil?> GetConModelo(int id);
    }
}

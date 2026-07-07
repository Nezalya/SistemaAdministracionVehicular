using SAV.Core.Interfaces.Repositories;

namespace SAV.Infrastructure.Persistence.Repositories
{
    public class AutomovilRepository : BaseRepository<Automovil>, IAutomovilRepository
    {
        public AutomovilRepository(SavDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Automovil>> GetDisponibles()
        {
            return await _entities
                .Where(a => a.Estado == EstadoAutomovil.Stock)
                .Include(a => a.Modelo)
                    .ThenInclude(m => m.Marca)
                .ToListAsync();
        }

        public async Task<Automovil?> GetConModelo(int id)
        {
            return await _entities
                .Include(a => a.Modelo)
                .ThenInclude(m => m.ExtrasDisponibles)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}

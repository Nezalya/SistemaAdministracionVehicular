

namespace SAV.Infrastructure.Persistence.Repositories
{
    public class VentaRepository:BaseRepository<Venta>, IVentaRepository
    {
        public VentaRepository(SavDbContext context) : base(context)
        {
        }

        public async Task<Venta?> ObtenerVentaConDetalle(int id)
        {
            return await _entities
                .Include(v => v.Extras)
                    .ThenInclude(e => e.Caracteristica)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<IEnumerable<Venta>> ObtenerVentasConDetalle()
        {
            return await _entities
                .Include(v => v.Extras)
                    .ThenInclude(e => e.Caracteristica)
                .ToListAsync();
        }
    }
}

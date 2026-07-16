using System;
using System.Collections.Generic;
using System.Text;

namespace SAV.Infrastructure.Persistence.Repositories
{
    public class ModeloRepository:BaseRepository<Modelo>,IModeloRepository
    {
        public ModeloRepository(SavDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Modelo>> ObtenerModelosConMarca()
        {
            return await _entities
                .Include(m => m.Marca)
                .ToListAsync();
        }

        public async Task<Modelo?> ObtenerModeloConMarca(int id)
        {
            return await _entities
                .Include(m => m.Marca)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Modelo?> ObtenerModeloConExtras(int id)
        {
            return await _entities
                .Include(m => m.ExtrasDisponibles)
                    .ThenInclude(me => me.Caracteristica)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}

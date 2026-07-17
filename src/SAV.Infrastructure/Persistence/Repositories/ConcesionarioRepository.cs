using System;
using System.Collections.Generic;
using System.Text;

namespace SAV.Infrastructure.Persistence.Repositories
{
    public class ConcesionarioRepository:BaseRepository<Concesionario>, IConcesionarioRepository
    {
        public ConcesionarioRepository(SavDbContext context) : base(context)
        {
        }

        public async Task<Concesionario?> ObtenerConcesionarioPorNif(string nif)
        {
            return await _entities.FirstOrDefaultAsync(c => c.Nif == nif);
        }
    }
}

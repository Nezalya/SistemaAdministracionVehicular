using System;
using System.Collections.Generic;
using System.Text;

namespace SAV.Infrastructure.Persistence.Repositories
{
    public class VendedorRepository:BaseRepository<Vendedor>, IVendedorRepository
    {
        public VendedorRepository(SavDbContext context) : base(context)
        {
        }

        public async Task<Vendedor?> ObtenerVendedorPorNif(string nif)
        {
            return await _entities.FirstOrDefaultAsync(v => v.Nif == nif);
        }
    }
}

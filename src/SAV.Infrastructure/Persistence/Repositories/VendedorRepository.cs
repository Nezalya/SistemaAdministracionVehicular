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
    }
}

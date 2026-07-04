using System;
using System.Collections.Generic;
using System.Text;

namespace SAV.Infrastructure.Persistence.Repositories
{
    public class ClienteRepository:BaseRepository<Cliente>,IClienteRepository
    {
        public ClienteRepository(SavDbContext context) : base(context)
        {
        }
    }
}

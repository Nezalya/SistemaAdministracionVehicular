using System;
using System.Collections.Generic;
using System.Text;

namespace SAV.Infrastructure.Persistence.Repositories
{
    public class PedidoFabricaRepository:BaseRepository<PedidoFabrica>,IPedidoFabricaRepository
    {
        public PedidoFabricaRepository(SavDbContext context) : base(context)
        {
        }
    }
}

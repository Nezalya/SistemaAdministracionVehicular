using System;
using System.Collections.Generic;
using System.Text;

namespace SAV.Infrastructure.Persistence.Repositories
{
    public class ServicioOficialRepository:BaseRepository<ServicioOficial>, IServicioOficialRepository
    {
        public ServicioOficialRepository(SavDbContext context) : base(context)
        {
        }
    }
}

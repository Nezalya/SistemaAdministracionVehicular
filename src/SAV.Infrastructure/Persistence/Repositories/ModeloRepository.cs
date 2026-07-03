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
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SAV.Infrastructure.Persistence.Repositories
{
    public class CaracteristicaRepository:BaseRepository<Caracteristica>,ICaracteristicaRepository
    {
        public CaracteristicaRepository(SavDbContext context) : base(context)
        {
        }
    }
}

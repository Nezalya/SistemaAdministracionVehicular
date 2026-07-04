using System;
using System.Collections.Generic;
using System.Text;

namespace SAV.Infrastructure.Persistence.Repositories
{
    public class UsuarioRepository:BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(SavDbContext context) : base(context)
        {
        }
    }
}

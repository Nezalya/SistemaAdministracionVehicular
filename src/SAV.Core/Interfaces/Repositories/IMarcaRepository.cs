using System;
using System.Collections.Generic;
using System.Text;

namespace SAV.Core.Interfaces.Repositories
{
    public interface IMarcaRepository: IRepository<Marca>
    {
        Task<Marca?> ObtenerMarcaPorNombre(string nombre);
    }
}

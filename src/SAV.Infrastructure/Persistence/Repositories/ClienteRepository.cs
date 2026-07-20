using SAV.Core.Dtos;
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
        public async Task<Cliente?> ObtenerClientePorNif(string nif)
        {
            return await _entities.FirstOrDefaultAsync(c => c.Nif == nif);
        }

        public async Task<ResumenComprasClienteDto?> ObtenerResumenCompras(int clienteId)
        {
            return await _context.Clientes
        .Where(c => c.Id == clienteId)
        .Select(c => new ResumenComprasClienteDto
        {
            ClienteId = c.Id,
            ClienteNombre = c.Nombre,
            TotalCompras = c.Ventas.Count(),
            MontoTotal = c.Ventas.Sum(v => (decimal?)v.PrecioCobrado) ?? 0
        })
        .FirstOrDefaultAsync();
        }

        public async Task<bool> TieneVentas(int clienteId)
        {
            return await _context.Ventas.AnyAsync(v => v.ClienteId == clienteId);
        }
    }
}

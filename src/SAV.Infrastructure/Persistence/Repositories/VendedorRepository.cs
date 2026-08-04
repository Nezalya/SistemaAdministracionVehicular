
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

        public async Task<ResumenVentasVendedorDto?> ObtenerResumenVentas(int vendedorId)
        {
            return await _context.Vendedores
        .Where(v => v.Id == vendedorId)
        .Select(v => new ResumenVentasVendedorDto
        {
            VendedorId = v.Id,
            VendedorNombre = v.Nombre,
            TotalVentas = v.Ventas.Count(),
            MontoTotal = v.Ventas.Sum(x => (decimal?)x.PrecioCobrado) ?? 0
        })
        .FirstOrDefaultAsync();
        }

        public async Task<bool> TieneVentas(int vendedorId)
        {
            return await _context.Ventas.AnyAsync(v => v.VendedorId == vendedorId);
        }
    }
}

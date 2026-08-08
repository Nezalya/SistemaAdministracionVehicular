namespace SAV.Infrastructure.Persistence.Repositories
{
    public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
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
                .AsNoTracking()
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

        public async Task<PagedResult<Cliente>> BuscarClientes(FiltroClienteDto filtro)
        {
            var query = _entities.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(filtro.Busqueda))
            {
                var termino = filtro.Busqueda.Trim();
                query = query.Where(c =>
                    c.Nombre.Contains(termino)
                    || c.Nif.Contains(termino)
                    || (c.Email != null && c.Email.Contains(termino))
                    || (c.Telefono != null && c.Telefono.Contains(termino)));
            }

            var total = await query.CountAsync();

            query = (filtro.SortBy?.ToLowerInvariant(), filtro.SortDescending) switch
            {
                ("nombre", false) => query.OrderBy(c => c.Nombre),
                ("nombre", true) => query.OrderByDescending(c => c.Nombre),
                ("nif", false) => query.OrderBy(c => c.Nif),
                ("nif", true) => query.OrderByDescending(c => c.Nif),
                ("fecharegistro", false) => query.OrderBy(c => c.FechaRegistro),
                ("fecharegistro", true) => query.OrderByDescending(c => c.FechaRegistro),
                _ => query.OrderBy(c => c.Nombre)
            };

            var items = await query
                .Skip((filtro.Page - 1) * filtro.PageSize)
                .Take(filtro.PageSize)
                .ToListAsync();

            return new PagedResult<Cliente>
            {
                Items = items,
                TotalCount = total,
                Page = filtro.Page,
                PageSize = filtro.PageSize
            };
        }
    }
}

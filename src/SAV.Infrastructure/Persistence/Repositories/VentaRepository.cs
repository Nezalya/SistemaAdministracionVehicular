namespace SAV.Infrastructure.Persistence.Repositories
{
    public class VentaRepository : BaseRepository<Venta>, IVentaRepository
    {
        public VentaRepository(SavDbContext context) : base(context)
        {
        }

        public async Task<Venta?> ObtenerVentaConDetalle(int id)
        {
            return await ConsultaConDetalle()
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<IEnumerable<Venta>> ObtenerVentasConDetalle()
        {
            return await ConsultaConDetalle().ToListAsync();
        }

        public async Task<PagedResult<Venta>> BuscarVentas(FiltroVentaDto filtro)
        {
            var query = ConsultaConDetalle();

            if (!string.IsNullOrWhiteSpace(filtro.Busqueda))
            {
                var termino = filtro.Busqueda.Trim();
                query = query.Where(v =>
                    (v.Matricula != null && v.Matricula.Contains(termino))
                    || v.Cliente.Nombre.Contains(termino)
                    || v.Automovil.NumBastidor.Contains(termino));
            }

            if (filtro.Desde.HasValue)
                query = query.Where(v => v.FechaVenta >= filtro.Desde.Value.Date);

            // Se incluye el día completo del extremo superior.
            if (filtro.Hasta.HasValue)
                query = query.Where(v => v.FechaVenta < filtro.Hasta.Value.Date.AddDays(1));

            if (filtro.FormaPago.HasValue)
                query = query.Where(v => v.FormaPago == filtro.FormaPago.Value);

            if (filtro.Origen.HasValue)
                query = query.Where(v => v.Origen == filtro.Origen.Value);

            if (filtro.VendedorId.HasValue)
                query = query.Where(v => v.VendedorId == filtro.VendedorId.Value);

            if (filtro.ClienteId.HasValue)
                query = query.Where(v => v.ClienteId == filtro.ClienteId.Value);

            var total = await query.CountAsync();

            query = (filtro.SortBy?.ToLowerInvariant(), filtro.SortDescending) switch
            {
                ("fecha", false) => query.OrderBy(v => v.FechaVenta),
                ("fecha", true) => query.OrderByDescending(v => v.FechaVenta),
                ("importe", false) => query.OrderBy(v => v.PrecioCobrado),
                ("importe", true) => query.OrderByDescending(v => v.PrecioCobrado),
                ("cliente", false) => query.OrderBy(v => v.Cliente.Nombre),
                ("cliente", true) => query.OrderByDescending(v => v.Cliente.Nombre),
                ("matricula", false) => query.OrderBy(v => v.Matricula),
                ("matricula", true) => query.OrderByDescending(v => v.Matricula),
                // Por defecto, las más recientes primero: es lo que se busca al abrir el listado.
                _ => query.OrderByDescending(v => v.FechaVenta).ThenByDescending(v => v.Id)
            };

            var items = await query
                .Skip((filtro.Page - 1) * filtro.PageSize)
                .Take(filtro.PageSize)
                .ToListAsync();

            return new PagedResult<Venta>
            {
                Items = items,
                TotalCount = total,
                Page = filtro.Page,
                PageSize = filtro.PageSize
            };
        }

        /// <summary>
        /// Carga la venta con todo lo necesario para describirla sin consultas adicionales.
        /// AsSplitQuery evita el producto cartesiano que provocarían las colecciones incluidas.
        /// </summary>
        private IQueryable<Venta> ConsultaConDetalle()
            => _entities
                .AsNoTracking()
                .Include(v => v.Cliente)
                .Include(v => v.Vendedor)
                .Include(v => v.ServicioOficial)
                .Include(v => v.Automovil)
                    .ThenInclude(a => a.Modelo)
                        .ThenInclude(m => m.Marca)
                .Include(v => v.Extras)
                    .ThenInclude(e => e.Caracteristica)
                .AsSplitQuery();
    }
}

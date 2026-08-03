using SAV.Api.DTOs;
using SAV.Core.Dtos;
using SAV.Core.Interfaces.Repositories;

namespace SAV.Infrastructure.Persistence.Repositories;

public class AutomovilRepository : BaseRepository<Automovil>, IAutomovilRepository
{
    public AutomovilRepository(SavDbContext context) : base(context)
    {
    }
    public async Task<IEnumerable<Automovil>> ObtenerAutomovilesDisponibles()
    {
        return await _entities
            .AsNoTracking()
            .Where(a => a.Estado == EstadoAutomovil.Stock)
            .Include(a => a.Modelo)
                .ThenInclude(m => m.Marca)
            .Include(a => a.ConcesionarioUbicacion)
            .Include(a => a.ServicioOficialUbicacion)
            .ToListAsync();
    }

    public async Task<Automovil?> ObtenerAutomovil(int id)
    {
        return await _entities
            .Include(a => a.Modelo)
                .ThenInclude(m => m.ExtrasDisponibles)
            .Include(a => a.Modelo)
                .ThenInclude(m => m.Marca)
            .Include(a => a.ConcesionarioUbicacion)
            .Include(a => a.ServicioOficialUbicacion)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Automovil?> ObtenerAutomovilPorBastidor(string numBastidor)
    {
        return await _entities.FirstOrDefaultAsync(a => a.NumBastidor == numBastidor);
    }

    public async Task<IEnumerable<Automovil>> ObtenerAutomovilesVendidos()
    {
        return await _entities
            .AsNoTracking()
            .Where(a => a.Estado == EstadoAutomovil.Vendido)
            .Include(a => a.Modelo)
                .ThenInclude(m => m.Marca)
            .Include(a => a.ConcesionarioUbicacion)
            .Include(a => a.ServicioOficialUbicacion)
            .ToListAsync();
    }

    public async Task<ResumenStockDto> ObtenerResumenStock()
    {
        
        var automovilesDisponibles = await _entities
            .Where(a => a.Estado == EstadoAutomovil.Stock)
            .Include(a => a.Modelo)
                .ThenInclude(m => m.Marca)
            .ToListAsync();

        
        var porMarca = automovilesDisponibles
            .GroupBy(a => a.Modelo.Marca.Nombre)
            .Select(gMarca => new StockPorMarcaDto
            {
                Marca = gMarca.Key,
                Cantidad = gMarca.Count(),
                Modelos = gMarca
                    .GroupBy(a => a.Modelo.Nombre)
                    .Select(gModelo => new StockPorModeloDto
                    {
                        Modelo = gModelo.Key,
                        Cantidad = gModelo.Count()
                    })
                    .ToList()
            })
            .ToList();

        return new ResumenStockDto
        {
            TotalDisponibles = automovilesDisponibles.Count,
            PorMarca = porMarca
        };
    }

    public async Task<IEnumerable<Automovil>> BuscarAutomoviles(FiltroAutomovilDto filtro)
    {
        var query = _entities
    .AsNoTracking()                              
    .Include(a => a.Modelo)
        .ThenInclude(m => m.Marca)
    .Include(a => a.ConcesionarioUbicacion)      
    .Include(a => a.ServicioOficialUbicacion)    
    .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filtro.Marca))
            query = query.Where(a => a.Modelo.Marca.Nombre == filtro.Marca);

        if (!string.IsNullOrWhiteSpace(filtro.Modelo))
            query = query.Where(a => a.Modelo.Nombre == filtro.Modelo);

        if (filtro.Estado.HasValue)
            query = query.Where(a => a.Estado == filtro.Estado.Value);

        if (!string.IsNullOrWhiteSpace(filtro.NumBastidor))
            query = query.Where(a => a.NumBastidor == filtro.NumBastidor);

        if (filtro.ServicioOficialUbicacionId.HasValue)
            query = query.Where(a => a.ServicioOficialUbicacionId == filtro.ServicioOficialUbicacionId.Value);

        if (filtro.SoloEnConcesionario == true)
            query = query.Where(a => a.ServicioOficialUbicacionId == null);

        return await query.ToListAsync();
    }
}




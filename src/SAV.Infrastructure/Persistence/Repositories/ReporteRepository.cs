
namespace SAV.Infrastructure.Persistence.Repositories
{
    public class ReporteRepository : IReporteRepository
    {
        private readonly SavDbContext _context;

        public ReporteRepository(SavDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardDto> ObtenerDashboard()
        {
            return new DashboardDto
            {
                Inventario = new InventarioResumenDto
                {
                    TotalAutos = await _context.Automoviles.CountAsync(),
                    Disponibles = await _context.Automoviles.CountAsync(a => a.Estado == EstadoAutomovil.Stock),
                    Vendidos = await _context.Automoviles.CountAsync(a => a.Estado == EstadoAutomovil.Vendido)
                },
                Ventas = new VentasResumenDto
                {
                    TotalVentas = await _context.Ventas.CountAsync(),
                    IngresosTotales = await _context.Ventas.SumAsync(v => (decimal?)v.PrecioCobrado) ?? 0
                },
                Entidades = new EntidadesResumenDto
                {
                    TotalClientes = await _context.Clientes.CountAsync(),
                    TotalVendedores = await _context.Vendedores.CountAsync(),
                    TotalServiciosOficiales = await _context.ServiciosOficiales.CountAsync()
                }
            };
        }
    }
}
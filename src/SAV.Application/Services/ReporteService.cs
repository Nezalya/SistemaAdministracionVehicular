namespace SAV.Application.Services
{
    public class ReporteService : IReporteService
    {
        private readonly IReporteRepository _reporteRepository;

        public ReporteService(IReporteRepository reporteRepository)
        {
            _reporteRepository = reporteRepository;
        }

        public async Task<DashboardDto> ObtenerDashboard()
        {
            return await _reporteRepository.ObtenerDashboard();
        }
    }
}
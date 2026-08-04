namespace SAV.Application.Interfaces.Services
{
    public interface IReporteService
    {
        Task<DashboardDto> ObtenerDashboard();
    }
}
namespace SAV.Application.Interfaces.Repositories
{
    public interface IReporteRepository
    {
        Task<DashboardDto> ObtenerDashboard();
    }
}

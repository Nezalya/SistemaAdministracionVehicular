using SAV.Core.Dtos;

namespace SAV.Core.Interfaces.Repositories
{
    public interface IReporteRepository
    {
        Task<DashboardDto> ObtenerDashboard();
    }
}

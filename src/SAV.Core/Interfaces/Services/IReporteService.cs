using SAV.Core.Dtos;

namespace SAV.Core.Interfaces.Services
{
    public interface IReporteService
    {
        Task<DashboardDto> ObtenerDashboard();
    }
}
using SAV.Contracts.Dtos;

namespace SAV.UI.Services.Api;

public interface IReportesApi
{
    Task<ResultadoApi<DashboardDto>> ObtenerDashboardAsync(CancellationToken ct = default);
}
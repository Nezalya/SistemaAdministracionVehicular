using SAV.Contracts.Dtos;
using SAV.UI.Services.Auth;

namespace SAV.UI.Services.Api;

public sealed class ReportesApi : ApiClienteBase, IReportesApi
{
    public ReportesApi(HttpClient http, ITokenAccessor tokenAccessor, ControlSesion sesion, ILogger<ReportesApi> logger)
        : base(http, tokenAccessor, sesion, logger) { }

    public Task<ResultadoApi<DashboardDto>> ObtenerDashboardAsync(CancellationToken ct = default)
        => GetAsync<DashboardDto>("api/reportes/dashboard", ct);
}
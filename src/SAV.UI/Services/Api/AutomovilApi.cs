using SAV.Contracts.Common;
using SAV.Contracts.Dtos;
using SAV.UI.Services.Auth;

namespace SAV.UI.Services.Api;

public interface IAutomovilApi
{
    Task<ResultadoApi<PagedResult<AutomovilDto>>> BuscarAsync(FiltroAutomovilDto filtro, CancellationToken ct = default);
    Task<ResultadoApi<IReadOnlyList<AutomovilDto>>> ObtenerDisponiblesAsync(CancellationToken ct = default);
    Task<ResultadoApi<IReadOnlyList<AutomovilDto>>> ObtenerVendidosAsync(CancellationToken ct = default);
    Task<ResultadoApi<AutomovilDto>> ObtenerPorIdAsync(int id, CancellationToken ct = default);
    Task<ResultadoApi<ResumenStockDto>> ObtenerResumenStockAsync(CancellationToken ct = default);
    Task<ResultadoApi<AutomovilDto>> RegistrarAsync(RegistrarAutomovilDto dto, CancellationToken ct = default);
    Task<ResultadoApi<AutomovilDto>> ActualizarAsync(int id, ActualizarAutomovilDto dto, CancellationToken ct = default);
    Task<ResultadoApi<object>> EliminarAsync(int id, CancellationToken ct = default);
    Task<ResultadoApi<GenerarAutomovilesLoteResultDto>> RegistrarLoteAsync(GenerarAutomovilesLoteDto dto, CancellationToken ct = default);
}

public sealed class AutomovilApi : ApiClienteBase, IAutomovilApi
{
    public AutomovilApi(HttpClient http, ITokenAccessor tokenAccessor, ControlSesion sesion, ILogger<AutomovilApi> logger)
        : base(http, tokenAccessor, sesion, logger) { }

    public Task<ResultadoApi<PagedResult<AutomovilDto>>> BuscarAsync(
        FiltroAutomovilDto filtro, CancellationToken ct = default)
        => GetAsync<PagedResult<AutomovilDto>>($"api/automovil{ConsultaApi.Construir(filtro)}", ct);

    public Task<ResultadoApi<IReadOnlyList<AutomovilDto>>> ObtenerDisponiblesAsync(CancellationToken ct = default)
        => GetAsync<IReadOnlyList<AutomovilDto>>("api/automovil/disponibles", ct);

    public Task<ResultadoApi<IReadOnlyList<AutomovilDto>>> ObtenerVendidosAsync(CancellationToken ct = default)
        => GetAsync<IReadOnlyList<AutomovilDto>>("api/automovil/vendidos", ct);

    public Task<ResultadoApi<AutomovilDto>> ObtenerPorIdAsync(int id, CancellationToken ct = default)
        => GetAsync<AutomovilDto>($"api/automovil/{id}", ct);

    public Task<ResultadoApi<ResumenStockDto>> ObtenerResumenStockAsync(CancellationToken ct = default)
        => GetAsync<ResumenStockDto>("api/automovil/resumen-stock", ct);

    public Task<ResultadoApi<AutomovilDto>> RegistrarAsync(RegistrarAutomovilDto dto, CancellationToken ct = default)
        => PostAsync<AutomovilDto>("api/automovil", dto, ct);

    public Task<ResultadoApi<AutomovilDto>> ActualizarAsync(int id, ActualizarAutomovilDto dto, CancellationToken ct = default)
        => PutAsync<AutomovilDto>($"api/automovil/{id}", dto, ct);

    public Task<ResultadoApi<object>> EliminarAsync(int id, CancellationToken ct = default)
        => DeleteAsync<object>($"api/automovil/{id}", ct);

    public Task<ResultadoApi<GenerarAutomovilesLoteResultDto>> RegistrarLoteAsync(
        GenerarAutomovilesLoteDto dto, CancellationToken ct = default)
        => PostAsync<GenerarAutomovilesLoteResultDto>("api/automovil/lote", dto, ct);

}

using System.Globalization;
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
    public AutomovilApi(HttpClient http, ITokenAccessor tokenAccessor, ILogger<AutomovilApi> logger)
        : base(http, tokenAccessor, logger) { }

    public Task<ResultadoApi<PagedResult<AutomovilDto>>> BuscarAsync(
        FiltroAutomovilDto filtro, CancellationToken ct = default)
        => GetAsync<PagedResult<AutomovilDto>>($"api/automovil{ConstruirConsulta(filtro)}", ct);

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

    /// <summary>
    /// El endpoint recibe el filtro por query string ([FromQuery] FiltroAutomovilDto).
    /// Solo se envían los valores presentes para no ensuciar la URL.
    /// </summary>
    private static string ConstruirConsulta(FiltroAutomovilDto filtro)
    {
        var partes = new List<string>
        {
            $"page={filtro.Page.ToString(CultureInfo.InvariantCulture)}",
            $"pageSize={filtro.PageSize.ToString(CultureInfo.InvariantCulture)}"
        };

        if (!string.IsNullOrWhiteSpace(filtro.SortBy))
        {
            partes.Add($"sortBy={Uri.EscapeDataString(filtro.SortBy)}");
            partes.Add($"sortDescending={(filtro.SortDescending ? "true" : "false")}");
        }

        if (!string.IsNullOrWhiteSpace(filtro.Marca))
            partes.Add($"marca={Uri.EscapeDataString(filtro.Marca)}");

        if (!string.IsNullOrWhiteSpace(filtro.Modelo))
            partes.Add($"modelo={Uri.EscapeDataString(filtro.Modelo)}");

        if (filtro.Estado.HasValue)
            partes.Add($"estado={filtro.Estado.Value}");

        if (!string.IsNullOrWhiteSpace(filtro.NumBastidor))
            partes.Add($"numBastidor={Uri.EscapeDataString(filtro.NumBastidor)}");

        if (filtro.ServicioOficialUbicacionId.HasValue)
            partes.Add($"servicioOficialUbicacionId={filtro.ServicioOficialUbicacionId.Value}");

        if (filtro.SoloEnConcesionario == true)
            partes.Add("soloEnConcesionario=true");

        return "?" + string.Join('&', partes);
    }
}

using SAV.Contracts.Dtos;
using SAV.UI.Services.Auth;

namespace SAV.UI.Services.Api;

// ---------------------------------------------------------------- Marcas

public interface IMarcaApi
{
    Task<ResultadoApi<IReadOnlyList<MarcaDto>>> ObtenerTodasAsync(CancellationToken ct = default);
    Task<ResultadoApi<MarcaDto>> ObtenerPorIdAsync(int id, CancellationToken ct = default);
}

public sealed class MarcaApi : ApiClienteBase, IMarcaApi
{
    public MarcaApi(HttpClient http, ITokenAccessor tokenAccessor, ControlSesion sesion, ILogger<MarcaApi> logger)
        : base(http, tokenAccessor, sesion, logger) { }

    public Task<ResultadoApi<IReadOnlyList<MarcaDto>>> ObtenerTodasAsync(CancellationToken ct = default)
        => GetAsync<IReadOnlyList<MarcaDto>>("api/marca", ct);

    public Task<ResultadoApi<MarcaDto>> ObtenerPorIdAsync(int id, CancellationToken ct = default)
        => GetAsync<MarcaDto>($"api/marca/{id}", ct);
}

// ---------------------------------------------------------------- Modelos

public interface IModeloApi
{
    Task<ResultadoApi<IReadOnlyList<ModeloDto>>> ObtenerTodosAsync(CancellationToken ct = default);
    Task<ResultadoApi<ModeloDto>> ObtenerPorIdAsync(int id, CancellationToken ct = default);
    Task<ResultadoApi<ConfiguracionModeloDto>> ObtenerEquipamientoAsync(int id, CancellationToken ct = default);
    Task<ResultadoApi<IReadOnlyList<ExtraDisponibleDto>>> ObtenerExtrasAsync(int id, CancellationToken ct = default);
    Task<ResultadoApi<IReadOnlyList<EquipamientoSerieDto>>> ObtenerSerieAsync(int id, CancellationToken ct = default);
}

public sealed class ModeloApi : ApiClienteBase, IModeloApi
{
    public ModeloApi(HttpClient http, ITokenAccessor tokenAccessor, ControlSesion sesion, ILogger<ModeloApi> logger)
        : base(http, tokenAccessor, sesion, logger) { }

    public Task<ResultadoApi<IReadOnlyList<ModeloDto>>> ObtenerTodosAsync(CancellationToken ct = default)
        => GetAsync<IReadOnlyList<ModeloDto>>("api/modelo", ct);

    public Task<ResultadoApi<ModeloDto>> ObtenerPorIdAsync(int id, CancellationToken ct = default)
        => GetAsync<ModeloDto>($"api/modelo/{id}", ct);

    public Task<ResultadoApi<ConfiguracionModeloDto>> ObtenerEquipamientoAsync(int id, CancellationToken ct = default)
        => GetAsync<ConfiguracionModeloDto>($"api/modelo/{id}/equipamiento", ct);

    public Task<ResultadoApi<IReadOnlyList<ExtraDisponibleDto>>> ObtenerExtrasAsync(int id, CancellationToken ct = default)
        => GetAsync<IReadOnlyList<ExtraDisponibleDto>>($"api/modelo/{id}/extras", ct);

    public Task<ResultadoApi<IReadOnlyList<EquipamientoSerieDto>>> ObtenerSerieAsync(int id, CancellationToken ct = default)
        => GetAsync<IReadOnlyList<EquipamientoSerieDto>>($"api/modelo/{id}/serie", ct);
}

// ---------------------------------------------------------------- Características

public interface ICaracteristicaApi
{
    Task<ResultadoApi<IReadOnlyList<CaracteristicaDto>>> ObtenerTodasAsync(CancellationToken ct = default);
    Task<ResultadoApi<CaracteristicaDto>> ObtenerPorIdAsync(int id, CancellationToken ct = default);
}

public sealed class CaracteristicaApi : ApiClienteBase, ICaracteristicaApi
{
    public CaracteristicaApi(HttpClient http, ITokenAccessor tokenAccessor, ControlSesion sesion, ILogger<CaracteristicaApi> logger)
        : base(http, tokenAccessor, sesion, logger) { }

    public Task<ResultadoApi<IReadOnlyList<CaracteristicaDto>>> ObtenerTodasAsync(CancellationToken ct = default)
        => GetAsync<IReadOnlyList<CaracteristicaDto>>("api/caracteristica", ct);

    public Task<ResultadoApi<CaracteristicaDto>> ObtenerPorIdAsync(int id, CancellationToken ct = default)
        => GetAsync<CaracteristicaDto>($"api/caracteristica/{id}", ct);
}

// ---------------------------------------------------------------- Fábrica

public interface IFabricaApi
{
    Task<ResultadoApi<ImportacionFabricaDto>> ImportarAsync(CancellationToken ct = default);
}

public sealed class FabricaApi : ApiClienteBase, IFabricaApi
{
    public FabricaApi(HttpClient http, ITokenAccessor tokenAccessor, ControlSesion sesion, ILogger<FabricaApi> logger)
        : base(http, tokenAccessor, sesion, logger) { }

    public Task<ResultadoApi<ImportacionFabricaDto>> ImportarAsync(CancellationToken ct = default)
        => PostAsync<ImportacionFabricaDto>("api/fabrica/importar", null, ct);
}

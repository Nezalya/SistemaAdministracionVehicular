using SAV.Contracts.Common;
using SAV.Contracts.Dtos;
using SAV.UI.Services.Auth;

namespace SAV.UI.Services.Api;

// ---------------------------------------------------------------- Clientes

public interface IClienteApi
{
    Task<ResultadoApi<PagedResult<ClienteDto>>> BuscarAsync(FiltroClienteDto filtro, CancellationToken ct = default);
    Task<ResultadoApi<ClienteDto>> ObtenerPorIdAsync(int id, CancellationToken ct = default);
    Task<ResultadoApi<ClienteDto>> CrearAsync(GuardarClienteDto dto, CancellationToken ct = default);
    Task<ResultadoApi<ClienteDto>> ActualizarAsync(int id, GuardarClienteDto dto, CancellationToken ct = default);
    Task<ResultadoApi<object>> EliminarAsync(int id, CancellationToken ct = default);
    Task<ResultadoApi<ResumenComprasClienteDto>> ObtenerResumenComprasAsync(int id, CancellationToken ct = default);
}

public sealed class ClienteApi : ApiClienteBase, IClienteApi
{
    public ClienteApi(HttpClient http, ITokenAccessor tokenAccessor, ControlSesion sesion, ILogger<ClienteApi> logger)
        : base(http, tokenAccessor, sesion, logger) { }

    public Task<ResultadoApi<PagedResult<ClienteDto>>> BuscarAsync(
        FiltroClienteDto filtro, CancellationToken ct = default)
        => GetAsync<PagedResult<ClienteDto>>($"api/cliente{ConsultaApi.Construir(filtro)}", ct);

    public Task<ResultadoApi<ClienteDto>> ObtenerPorIdAsync(int id, CancellationToken ct = default)
        => GetAsync<ClienteDto>($"api/cliente/{id}", ct);

    public Task<ResultadoApi<ClienteDto>> CrearAsync(GuardarClienteDto dto, CancellationToken ct = default)
        => PostAsync<ClienteDto>("api/cliente", dto, ct);

    public Task<ResultadoApi<ClienteDto>> ActualizarAsync(int id, GuardarClienteDto dto, CancellationToken ct = default)
        => PutAsync<ClienteDto>($"api/cliente/{id}", dto, ct);

    public Task<ResultadoApi<object>> EliminarAsync(int id, CancellationToken ct = default)
        => DeleteAsync<object>($"api/cliente/{id}", ct);

    public Task<ResultadoApi<ResumenComprasClienteDto>> ObtenerResumenComprasAsync(int id, CancellationToken ct = default)
        => GetAsync<ResumenComprasClienteDto>($"api/cliente/{id}/resumen-compras", ct);
}

// ---------------------------------------------------------------- Vendedores

public interface IVendedorApi
{
    Task<ResultadoApi<IReadOnlyList<VendedorDto>>> ObtenerTodosAsync(CancellationToken ct = default);
    Task<ResultadoApi<VendedorDto>> ObtenerPorIdAsync(int id, CancellationToken ct = default);
    Task<ResultadoApi<VendedorDto>> ActualizarAsync(int id, GuardarVendedorDto dto, CancellationToken ct = default);
    Task<ResultadoApi<object>> EliminarAsync(int id, CancellationToken ct = default);
    Task<ResultadoApi<ResumenVentasVendedorDto>> ObtenerResumenVentasAsync(int id, CancellationToken ct = default);
}

public sealed class VendedorApi : ApiClienteBase, IVendedorApi
{
    public VendedorApi(HttpClient http, ITokenAccessor tokenAccessor, ControlSesion sesion, ILogger<VendedorApi> logger)
        : base(http, tokenAccessor, sesion, logger) { }

    public Task<ResultadoApi<IReadOnlyList<VendedorDto>>> ObtenerTodosAsync(CancellationToken ct = default)
        => GetAsync<IReadOnlyList<VendedorDto>>("api/vendedor", ct);

    public Task<ResultadoApi<VendedorDto>> ObtenerPorIdAsync(int id, CancellationToken ct = default)
        => GetAsync<VendedorDto>($"api/vendedor/{id}", ct);

    public Task<ResultadoApi<VendedorDto>> ActualizarAsync(int id, GuardarVendedorDto dto, CancellationToken ct = default)
        => PutAsync<VendedorDto>($"api/vendedor/{id}", dto, ct);

    public Task<ResultadoApi<object>> EliminarAsync(int id, CancellationToken ct = default)
        => DeleteAsync<object>($"api/vendedor/{id}", ct);

    public Task<ResultadoApi<ResumenVentasVendedorDto>> ObtenerResumenVentasAsync(int id, CancellationToken ct = default)
        => GetAsync<ResumenVentasVendedorDto>($"api/vendedor/{id}/resumen-ventas", ct);
}

// ---------------------------------------------------------------- Servicios oficiales

public interface IServicioOficialApi
{
    Task<ResultadoApi<IReadOnlyList<ServicioOficialDto>>> ObtenerTodosAsync(CancellationToken ct = default);
    Task<ResultadoApi<ServicioOficialDto>> ObtenerPorIdAsync(int id, CancellationToken ct = default);
    Task<ResultadoApi<ServicioOficialDto>> CrearAsync(GuardarServicioOficialDto dto, CancellationToken ct = default);
    Task<ResultadoApi<ServicioOficialDto>> ActualizarAsync(int id, GuardarServicioOficialDto dto, CancellationToken ct = default);
    Task<ResultadoApi<object>> EliminarAsync(int id, CancellationToken ct = default);
}

public sealed class ServicioOficialApi : ApiClienteBase, IServicioOficialApi
{
    public ServicioOficialApi(HttpClient http, ITokenAccessor tokenAccessor, ControlSesion sesion, ILogger<ServicioOficialApi> logger)
        : base(http, tokenAccessor, sesion, logger) { }

    public Task<ResultadoApi<IReadOnlyList<ServicioOficialDto>>> ObtenerTodosAsync(CancellationToken ct = default)
        => GetAsync<IReadOnlyList<ServicioOficialDto>>("api/serviciosoficiales", ct);

    public Task<ResultadoApi<ServicioOficialDto>> ObtenerPorIdAsync(int id, CancellationToken ct = default)
        => GetAsync<ServicioOficialDto>($"api/serviciosoficiales/{id}", ct);

    public Task<ResultadoApi<ServicioOficialDto>> CrearAsync(GuardarServicioOficialDto dto, CancellationToken ct = default)
        => PostAsync<ServicioOficialDto>("api/serviciosoficiales", dto, ct);

    public Task<ResultadoApi<ServicioOficialDto>> ActualizarAsync(int id, GuardarServicioOficialDto dto, CancellationToken ct = default)
        => PutAsync<ServicioOficialDto>($"api/serviciosoficiales/{id}", dto, ct);

    public Task<ResultadoApi<object>> EliminarAsync(int id, CancellationToken ct = default)
        => DeleteAsync<object>($"api/serviciosoficiales/{id}", ct);
}

// ---------------------------------------------------------------- Concesionario

public interface IConcesionarioApi
{
    Task<ResultadoApi<IReadOnlyList<ConcesionarioDto>>> ObtenerTodosAsync(CancellationToken ct = default);
    Task<ResultadoApi<ConcesionarioDto>> ObtenerPorIdAsync(int id, CancellationToken ct = default);
    Task<ResultadoApi<ConcesionarioDto>> CrearAsync(GuardarConcesionarioDto dto, CancellationToken ct = default);
    Task<ResultadoApi<ConcesionarioDto>> ActualizarAsync(int id, GuardarConcesionarioDto dto, CancellationToken ct = default);
}

public sealed class ConcesionarioApi : ApiClienteBase, IConcesionarioApi
{
    public ConcesionarioApi(HttpClient http, ITokenAccessor tokenAccessor, ControlSesion sesion, ILogger<ConcesionarioApi> logger)
        : base(http, tokenAccessor, sesion, logger) { }

    public Task<ResultadoApi<IReadOnlyList<ConcesionarioDto>>> ObtenerTodosAsync(CancellationToken ct = default)
        => GetAsync<IReadOnlyList<ConcesionarioDto>>("api/concesionario", ct);

    public Task<ResultadoApi<ConcesionarioDto>> ObtenerPorIdAsync(int id, CancellationToken ct = default)
        => GetAsync<ConcesionarioDto>($"api/concesionario/{id}", ct);

    public Task<ResultadoApi<ConcesionarioDto>> CrearAsync(GuardarConcesionarioDto dto, CancellationToken ct = default)
        => PostAsync<ConcesionarioDto>("api/concesionario", dto, ct);

    public Task<ResultadoApi<ConcesionarioDto>> ActualizarAsync(int id, GuardarConcesionarioDto dto, CancellationToken ct = default)
        => PutAsync<ConcesionarioDto>($"api/concesionario/{id}", dto, ct);
}

// ---------------------------------------------------------------- Usuarios

public interface IUsuarioApi
{
    Task<ResultadoApi<IReadOnlyList<UsuarioDto>>> ObtenerTodosAsync(CancellationToken ct = default);
    Task<ResultadoApi<UsuarioDto>> ObtenerPorIdAsync(int id, CancellationToken ct = default);
}

public sealed class UsuarioApi : ApiClienteBase, IUsuarioApi
{
    public UsuarioApi(HttpClient http, ITokenAccessor tokenAccessor, ControlSesion sesion, ILogger<UsuarioApi> logger)
        : base(http, tokenAccessor, sesion, logger) { }

    public Task<ResultadoApi<IReadOnlyList<UsuarioDto>>> ObtenerTodosAsync(CancellationToken ct = default)
        => GetAsync<IReadOnlyList<UsuarioDto>>("api/usuario", ct);

    public Task<ResultadoApi<UsuarioDto>> ObtenerPorIdAsync(int id, CancellationToken ct = default)
        => GetAsync<UsuarioDto>($"api/usuario/{id}", ct);
}

// ---------------------------------------------------------------- Ventas

public interface IVentaApi
{
    Task<ResultadoApi<PagedResult<VentaDto>>> BuscarAsync(FiltroVentaDto filtro, CancellationToken ct = default);
    Task<ResultadoApi<VentaDto>> ObtenerPorIdAsync(int id, CancellationToken ct = default);
    Task<ResultadoApi<VentaDto>> CrearAsync(CrearVentaDto dto, CancellationToken ct = default);
}

public sealed class VentaApi : ApiClienteBase, IVentaApi
{
    public VentaApi(HttpClient http, ITokenAccessor tokenAccessor, ControlSesion sesion, ILogger<VentaApi> logger)
        : base(http, tokenAccessor, sesion, logger) { }

    public Task<ResultadoApi<PagedResult<VentaDto>>> BuscarAsync(
        FiltroVentaDto filtro, CancellationToken ct = default)
        => GetAsync<PagedResult<VentaDto>>($"api/ventas{ConsultaApi.Construir(filtro)}", ct);

    public Task<ResultadoApi<VentaDto>> ObtenerPorIdAsync(int id, CancellationToken ct = default)
        => GetAsync<VentaDto>($"api/ventas/{id}", ct);

    public Task<ResultadoApi<VentaDto>> CrearAsync(CrearVentaDto dto, CancellationToken ct = default)
        => PostAsync<VentaDto>("api/ventas", dto, ct);
}

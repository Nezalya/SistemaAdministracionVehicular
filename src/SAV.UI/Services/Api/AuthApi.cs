using SAV.Contracts.Dtos;
using SAV.UI.Services.Auth;

namespace SAV.UI.Services.Api;

public interface IAuthApi
{
    Task<ResultadoApi<ResultadoAutenticacionDto>> LoginAsync(LoginDto credenciales, CancellationToken ct = default);

    Task<ResultadoApi<UsuarioRegistradoDto>> RegistrarAdministradorAsync(RegistrarAdministradorDto dto, CancellationToken ct = default);
    Task<ResultadoApi<UsuarioRegistradoDto>> RegistrarVendedorAsync(RegistrarVendedorDto dto, CancellationToken ct = default);
    Task<ResultadoApi<UsuarioRegistradoDto>> RegistrarClienteAsync(RegistrarClienteDto dto, CancellationToken ct = default);
}

public sealed class AuthApi : ApiClienteBase, IAuthApi
{
    public AuthApi(HttpClient http, ITokenAccessor tokenAccessor, ControlSesion sesion, ILogger<AuthApi> logger)
        : base(http, tokenAccessor, sesion, logger) { }

    public Task<ResultadoApi<ResultadoAutenticacionDto>> LoginAsync(
        LoginDto credenciales, CancellationToken ct = default)
        => PostAsync<ResultadoAutenticacionDto>("api/auth/login", credenciales, ct);

    public Task<ResultadoApi<UsuarioRegistradoDto>> RegistrarAdministradorAsync(
        RegistrarAdministradorDto dto, CancellationToken ct = default)
        => PostAsync<UsuarioRegistradoDto>("api/auth/registro-administrador", dto, ct);

    public Task<ResultadoApi<UsuarioRegistradoDto>> RegistrarVendedorAsync(
        RegistrarVendedorDto dto, CancellationToken ct = default)
        => PostAsync<UsuarioRegistradoDto>("api/auth/registro-vendedor", dto, ct);

    public Task<ResultadoApi<UsuarioRegistradoDto>> RegistrarClienteAsync(
        RegistrarClienteDto dto, CancellationToken ct = default)
        => PostAsync<UsuarioRegistradoDto>("api/auth/registro-cliente", dto, ct);
}

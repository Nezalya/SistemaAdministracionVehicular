using SAV.Contracts.Dtos;
using SAV.UI.Services.Auth;

namespace SAV.UI.Services.Api;

public sealed class AuthApi : ApiClienteBase, IAuthApi
{
    public AuthApi(HttpClient http, ITokenAccessor tokenAccessor, ILogger<AuthApi> logger)
        : base(http, tokenAccessor, logger) { }

    public Task<ResultadoApi<ResultadoAutenticacionDto>> LoginAsync(
        LoginDto credenciales, CancellationToken ct = default)
        => PostAsync<ResultadoAutenticacionDto>("api/auth/login", credenciales, ct);
}
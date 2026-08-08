using SAV.Contracts.Dtos;

namespace SAV.UI.Services.Api;

public interface IAuthApi
{
    Task<ResultadoApi<ResultadoAutenticacionDto>> LoginAsync(
        LoginDto credenciales, CancellationToken ct = default);
}
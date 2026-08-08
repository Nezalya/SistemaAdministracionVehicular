using Microsoft.AspNetCore.Components.Authorization;

namespace SAV.UI.Services.Auth;

/// <summary>
/// Obtiene el JWT del usuario autenticado leyendo sus claims.
/// </summary>
/// <remarks>
/// Deliberadamente NO usamos un DelegatingHandler: IHttpClientFactory reutiliza la cadena
/// de handlers entre ámbitos (por defecto, dos minutos), así que un handler que capture
/// servicios con ámbito de circuito devolvería el token de otro usuario. Leerlo aquí, en
/// un servicio con ámbito de circuito, y adjuntarlo petición a petición es lo correcto.
/// </remarks>
public sealed class TokenAccessor : ITokenAccessor
{
    private readonly AuthenticationStateProvider _proveedorEstado;

    public TokenAccessor(AuthenticationStateProvider proveedorEstado)
        => _proveedorEstado = proveedorEstado;

    public async Task<string?> ObtenerTokenAsync()
    {
        var estado = await _proveedorEstado.GetAuthenticationStateAsync();
        return estado.User.FindFirst(ClaimsSav.Token)?.Value;
    }
}
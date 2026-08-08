namespace SAV.UI.Services.Auth;

/// <summary>
/// Obtiene el JWT del usuario autenticado para adjuntarlo a las llamadas a la API.
/// </summary>
public interface ITokenAccessor
{
    Task<string?> ObtenerTokenAsync();
}

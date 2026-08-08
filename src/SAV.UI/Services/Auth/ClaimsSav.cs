namespace SAV.UI.Services.Auth;

/// <summary>
/// Nombres de los claims propios que viajan dentro de la cookie de autenticación.
/// </summary>
public static class ClaimsSav
{
    /// <summary>El JWT emitido por la API. Nunca sale del servidor: viaja cifrado en la cookie.</summary>
    public const string Token = "sav:token";

    public const string VendedorId = "sav:vendedorId";
    public const string ClienteId = "sav:clienteId";
}
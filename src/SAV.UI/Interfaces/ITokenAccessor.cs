namespace SAV.UI.Services.Auth;

public interface ITokenAccessor
{
    Task<string?> ObtenerTokenAsync();
}
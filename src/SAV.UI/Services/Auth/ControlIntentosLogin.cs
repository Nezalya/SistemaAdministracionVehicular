using Microsoft.Extensions.Caching.Memory;

namespace SAV.UI.Services.Auth;

/// <summary>
/// Frena los ataques de fuerza bruta contando <b>fallos</b> de autenticación.
/// </summary>
/// <remarks>
/// Un limitador que cuente todas las peticiones al formulario castiga también a
/// quien acierta: en una oficina donde el personal comparte una IP pública, el
/// sexto empleado en entrar quedaría bloqueado. Aquí solo suman las credenciales
/// incorrectas, y el contador se borra en cuanto alguien acierta.
///
/// La clave combina IP y cuenta a propósito. Contar solo por cuenta permitiría a
/// un atacante dejar fuera a un usuario legítimo sin más que fallar contra su
/// correo; contar solo por IP arrastraría a toda la oficina.
/// </remarks>
public sealed class ControlIntentosLogin
{
    private const int FallosPermitidos = 5;
    private static readonly TimeSpan Bloqueo = TimeSpan.FromMinutes(5);

    private readonly IMemoryCache _cache;

    public ControlIntentosLogin(IMemoryCache cache) => _cache = cache;

    public bool EstaBloqueado(string? ip, string? correo)
        => _cache.TryGetValue(Clave(ip, correo), out int fallos) && fallos >= FallosPermitidos;

    public void RegistrarFallo(string? ip, string? correo)
    {
        var clave = Clave(ip, correo);
        var fallos = _cache.TryGetValue(clave, out int actuales) ? actuales + 1 : 1;

        // La ventana se renueva con cada fallo: insistir prolonga el bloqueo.
        _cache.Set(clave, fallos, Bloqueo);
    }

    /// <summary>Un acceso correcto limpia el historial de esa combinación.</summary>
    public void RegistrarExito(string? ip, string? correo) => _cache.Remove(Clave(ip, correo));

    public TimeSpan TiempoDeEspera => Bloqueo;

    private static string Clave(string? ip, string? correo)
        => $"login:{ip ?? "desconocida"}|{correo?.Trim().ToLowerInvariant() ?? string.Empty}";
}

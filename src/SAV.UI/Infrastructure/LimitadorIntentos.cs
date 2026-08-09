using System.Threading.RateLimiting;

namespace SAV.UI.Infrastructure;

/// <summary>
/// Freno grueso contra el envío masivo de formularios de acceso.
/// </summary>
/// <remarks>
/// Protege al servidor de una avalancha de peticiones desde un mismo origen, no
/// del descubrimiento de contraseñas: de eso se ocupa <see cref="Services.Auth.ControlIntentosLogin"/>,
/// que cuenta fallos por IP y cuenta.
///
/// El límite es holgado a propósito. Todo el personal de un concesionario suele
/// salir a Internet por la misma IP pública, así que un umbral estrecho dejaría
/// fuera a empleados legítimos que solo coincidieron en la hora de entrada.
/// </remarks>
public static class LimitadorIntentos
{
    public const string Politica = "login";

    private const int IntentosPermitidos = 30;
    private static readonly TimeSpan Ventana = TimeSpan.FromMinutes(1);

    public static IServiceCollection AddLimitadorDeIntentos(this IServiceCollection services)
        => services.AddRateLimiter(opciones =>
        {
            opciones.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            opciones.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(contexto =>
            {
                var esEnvioDeLogin =
                    HttpMethods.IsPost(contexto.Request.Method)
                    && contexto.Request.Path.StartsWithSegments("/login", StringComparison.OrdinalIgnoreCase);

                if (!esEnvioDeLogin)
                    return RateLimitPartition.GetNoLimiter("sin-limite");

                var origen = contexto.Connection.RemoteIpAddress?.ToString() ?? "desconocido";

                return RateLimitPartition.GetFixedWindowLimiter(
                    $"{Politica}:{origen}",
                    _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = IntentosPermitidos,
                        Window = Ventana,
                        QueueLimit = 0
                    });
            });
        });
}

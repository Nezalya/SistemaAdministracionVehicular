using System.Threading.RateLimiting;

namespace SAV.UI.Infrastructure;

/// <summary>
/// Limita los intentos de inicio de sesión por dirección IP.
/// </summary>
/// <remarks>
/// El formulario de login se envía a esta aplicación, que a su vez consulta a la
/// API. Sin este freno, un atacante puede probar contraseñas al ritmo que aguante
/// el servidor. El resto de peticiones no se limita: la aplicación es de uso
/// interno y estrangularlas solo estorbaría.
/// </remarks>
public static class LimitadorIntentos
{
    public const string Politica = "login";

    private const int IntentosPermitidos = 5;
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

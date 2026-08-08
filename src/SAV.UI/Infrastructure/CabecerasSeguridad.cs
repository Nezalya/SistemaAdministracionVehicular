namespace SAV.UI.Infrastructure;

/// <summary>
/// Cabeceras de seguridad para todas las respuestas.
/// </summary>
public static class CabecerasSeguridad
{
    /// <summary>
    /// Política de contenido. Blazor y Radzen necesitan estilos en línea, y el
    /// mapa de importaciones que emite Blazor es un script en línea, así que
    /// ambos requieren 'unsafe-inline'. El valor sigue aportando: acota el origen
    /// de scripts, imágenes y conexiones, impide incrustar la aplicación en otro
    /// sitio y bloquea plugins y cambios de &lt;base&gt;.
    /// </summary>
    private const string PoliticaContenido =
        "default-src 'self'; " +
        "script-src 'self' 'unsafe-inline'; " +
        "style-src 'self' 'unsafe-inline'; " +
        "img-src 'self' data:; " +
        "font-src 'self'; " +
        "connect-src 'self'; " +
        "frame-ancestors 'self'; " +
        "base-uri 'self'; " +
        "form-action 'self'; " +
        "object-src 'none'";

    public static IApplicationBuilder UseCabecerasSeguridad(this IApplicationBuilder app)
        => app.Use(async (contexto, siguiente) =>
        {
            // Se aplican justo antes de enviar la respuesta. Blazor añade su propia
            // cabecera de política de contenido durante el renderizado; hacerlo aquí
            // garantiza que quede una sola, la completa, en lugar de dos solapadas.
            contexto.Response.OnStarting(estado =>
            {
                var cabeceras = ((HttpContext)estado).Response.Headers;

                // Impide que el navegador reinterprete el tipo declarado del contenido.
                cabeceras["X-Content-Type-Options"] = "nosniff";

                // Evita filtrar la ruta completa (incluida ?returnUrl=) a terceros.
                cabeceras["Referrer-Policy"] = "strict-origin-when-cross-origin";

                // La aplicación no usa estas capacidades: se retiran explícitamente.
                cabeceras["Permissions-Policy"] = "camera=(), microphone=(), geolocation=(), payment=()";

                cabeceras["Content-Security-Policy"] = PoliticaContenido;

                return Task.CompletedTask;
            }, contexto);

            await siguiente();
        });
}

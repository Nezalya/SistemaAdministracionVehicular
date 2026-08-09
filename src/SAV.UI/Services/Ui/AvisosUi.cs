using Radzen;
using SAV.UI.Services.Api;

namespace SAV.UI.Services.Ui;

/// <summary>
/// Feedback al usuario en un único sitio: toda la aplicación notifica igual.
/// Envuelve el <see cref="NotificationService"/> de Radzen para no repetir
/// duraciones ni severidades por toda la UI.
/// </summary>
public sealed class AvisosUi
{
    private const int DuracionCorta = 3500;
    private const int DuracionLarga = 6000;

    private readonly NotificationService _notificaciones;

    public AvisosUi(NotificationService notificaciones) => _notificaciones = notificaciones;

    public void Exito(string mensaje, string? detalle = null) => Mostrar(
        NotificationSeverity.Success, mensaje, detalle, DuracionCorta);

    public void Info(string mensaje, string? detalle = null) => Mostrar(
        NotificationSeverity.Info, mensaje, detalle, DuracionCorta);

    public void Advertencia(string mensaje, string? detalle = null) => Mostrar(
        NotificationSeverity.Warning, mensaje, detalle, DuracionLarga);

    public void Error(string mensaje, string? detalle = null) => Mostrar(
        NotificationSeverity.Error, mensaje, detalle, DuracionLarga);

    /// <summary>
    /// Traduce un resultado fallido de la API a un aviso comprensible.
    /// Nunca expone detalles técnicos: el mensaje ya viene saneado del cliente.
    /// </summary>
    public void ErrorDeApi<T>(ResultadoApi<T> resultado, string accion)
    {
        var detalle = resultado.Mensaje;

        if (resultado.HayErroresDeValidacion)
            detalle = string.Join(" ", resultado.Errores!.SelectMany(e => e.Value).Distinct());

        Error($"No se pudo {accion}.", detalle);
    }

    private void Mostrar(NotificationSeverity severidad, string resumen, string? detalle, int duracion)
        => _notificaciones.Notify(new NotificationMessage
        {
            Severity = severidad,
            Summary = resumen,
            // Detail no admite nulos: se omite el segundo renglón si no hay detalle.
            Detail = detalle ?? string.Empty,
            Duration = duracion
        });
}

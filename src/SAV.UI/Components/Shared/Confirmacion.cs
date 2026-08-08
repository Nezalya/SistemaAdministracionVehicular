using Radzen;

namespace SAV.UI.Components.Shared;

/// <summary>
/// Confirmaciones consistentes para acciones destructivas.
/// Ninguna eliminación de la aplicación debe ocurrir sin pasar por aquí.
/// </summary>
public static class Confirmacion
{
    public static async Task<bool> EliminarAsync(
        DialogService dialogos, string queSeElimina, string? advertencia = null)
    {
        var mensaje = $"Vas a eliminar {queSeElimina}. Esta acción no se puede deshacer.";

        if (!string.IsNullOrWhiteSpace(advertencia))
            mensaje += $" {advertencia}";

        var respuesta = await dialogos.Confirm(
            mensaje,
            "¿Eliminar?",
            new ConfirmOptions
            {
                OkButtonText = "Eliminar",
                CancelButtonText = "Cancelar",
                Width = "440px"
            });

        return respuesta == true;
    }

    public static async Task<bool> PreguntarAsync(
        DialogService dialogos, string titulo, string mensaje,
        string textoConfirmar = "Continuar")
    {
        var respuesta = await dialogos.Confirm(
            mensaje, titulo,
            new ConfirmOptions
            {
                OkButtonText = textoConfirmar,
                CancelButtonText = "Cancelar",
                Width = "440px"
            });

        return respuesta == true;
    }
}

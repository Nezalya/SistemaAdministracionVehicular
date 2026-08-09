using Microsoft.AspNetCore.Components;

namespace SAV.UI.Services.Auth;

/// <summary>
/// Reacciona cuando la API rechaza al usuario por falta de autenticación.
/// </summary>
/// <remarks>
/// La cookie de la aplicación y el JWT caducan por caminos independientes. Si el
/// token muere antes que la cookie, el usuario seguiría viendo la aplicación como
/// si tuviera sesión mientras cada operación falla. Aquí se cierra ese hueco:
/// al primer 401 se lleva al usuario a cerrar sesión y volver a identificarse.
///
/// El servicio tiene ámbito de circuito y recuerda si ya redirigió, para que varias
/// llamadas simultáneas que fallen a la vez no provoquen navegaciones repetidas.
/// </remarks>
public sealed class ControlSesion
{
    private readonly NavigationManager _navegacion;
    private bool _yaRedirigido;

    public ControlSesion(NavigationManager navegacion) => _navegacion = navegacion;

    public void SesionCaducada()
    {
        if (_yaRedirigido) return;
        _yaRedirigido = true;

        var destino = Uri.EscapeDataString(new Uri(_navegacion.Uri).PathAndQuery);

        // forceLoad: la página de cierre se renderiza de forma estática porque
        // necesita HttpContext para eliminar la cookie.
        _navegacion.NavigateTo($"/sesion-expirada?returnUrl={destino}", forceLoad: true);
    }
}

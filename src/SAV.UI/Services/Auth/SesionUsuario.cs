using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace SAV.UI.Services.Auth;

/// <summary>
/// Lectura cómoda de la identidad del usuario del circuito actual.
/// Evita que cada pantalla repita el acceso a los claims.
/// </summary>
public sealed class SesionUsuario
{
    private readonly AuthenticationStateProvider _proveedorEstado;

    public SesionUsuario(AuthenticationStateProvider proveedorEstado)
        => _proveedorEstado = proveedorEstado;

    public async Task<ClaimsPrincipal> ObtenerUsuarioAsync()
        => (await _proveedorEstado.GetAuthenticationStateAsync()).User;

    public async Task<string?> ObtenerNombreAsync()
        => (await ObtenerUsuarioAsync()).Identity?.Name;

    public async Task<string?> ObtenerRolAsync()
        => (await ObtenerUsuarioAsync()).FindFirst(ClaimTypes.Role)?.Value;

    public async Task<bool> EsAdministradorAsync()
        => (await ObtenerUsuarioAsync()).IsInRole(Roles.Administrador);

    public async Task<bool> EsVendedorAsync()
        => (await ObtenerUsuarioAsync()).IsInRole(Roles.Vendedor);

    /// <summary>Id del vendedor asociado a la cuenta, si el usuario es vendedor.</summary>
    public async Task<int?> ObtenerVendedorIdAsync()
        => LeerEntero((await ObtenerUsuarioAsync()).FindFirst(ClaimsSav.VendedorId)?.Value);

    /// <summary>Id del cliente asociado a la cuenta, si el usuario es cliente.</summary>
    public async Task<int?> ObtenerClienteIdAsync()
        => LeerEntero((await ObtenerUsuarioAsync()).FindFirst(ClaimsSav.ClienteId)?.Value);

    private static int? LeerEntero(string? valor)
        => int.TryParse(valor, out var numero) ? numero : null;
}

/// <summary>Nombres de rol, alineados con los que emite la API en el JWT.</summary>
public static class Roles
{
    public const string Administrador = "Administrador";
    public const string Vendedor = "Vendedor";
    public const string Cliente = "Cliente";

    /// <summary>Para <c>AuthorizeView Roles="..."</c> y <c>[Authorize(Roles = ...)]</c>.</summary>
    public const string AdminOVendedor = "Administrador,Vendedor";
}

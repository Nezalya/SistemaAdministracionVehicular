using System.Globalization;
using SAV.Contracts.Common;
using SAV.Contracts.Dtos;

namespace SAV.UI.Services.Api;

/// <summary>
/// Construye la query string de los filtros paginados. Los endpoints los reciben
/// con [FromQuery], así que solo se envían los valores presentes para no
/// ensuciar la URL con parámetros vacíos.
/// </summary>
public static class ConsultaApi
{
    public static string Construir(FiltroAutomovilDto filtro)
    {
        var partes = Paginacion(filtro);

        Añadir(partes, "marca", filtro.Marca);
        Añadir(partes, "modelo", filtro.Modelo);
        Añadir(partes, "numBastidor", filtro.NumBastidor);

        if (filtro.Estado.HasValue) partes.Add($"estado={filtro.Estado.Value}");
        if (filtro.ServicioOficialUbicacionId.HasValue)
            partes.Add($"servicioOficialUbicacionId={filtro.ServicioOficialUbicacionId.Value}");
        if (filtro.SoloEnConcesionario == true) partes.Add("soloEnConcesionario=true");

        return Unir(partes);
    }

    public static string Construir(FiltroClienteDto filtro)
    {
        var partes = Paginacion(filtro);
        Añadir(partes, "busqueda", filtro.Busqueda);
        return Unir(partes);
    }

    public static string Construir(FiltroVentaDto filtro)
    {
        var partes = Paginacion(filtro);

        Añadir(partes, "busqueda", filtro.Busqueda);

        if (filtro.Desde.HasValue) partes.Add($"desde={Fecha(filtro.Desde.Value)}");
        if (filtro.Hasta.HasValue) partes.Add($"hasta={Fecha(filtro.Hasta.Value)}");
        if (filtro.FormaPago.HasValue) partes.Add($"formaPago={filtro.FormaPago.Value}");
        if (filtro.Origen.HasValue) partes.Add($"origen={filtro.Origen.Value}");
        if (filtro.VendedorId.HasValue) partes.Add($"vendedorId={filtro.VendedorId.Value}");
        if (filtro.ClienteId.HasValue) partes.Add($"clienteId={filtro.ClienteId.Value}");

        return Unir(partes);
    }

    private static List<string> Paginacion(PagedRequest peticion)
    {
        var partes = new List<string>
        {
            $"page={peticion.Page.ToString(CultureInfo.InvariantCulture)}",
            $"pageSize={peticion.PageSize.ToString(CultureInfo.InvariantCulture)}"
        };

        if (!string.IsNullOrWhiteSpace(peticion.SortBy))
        {
            partes.Add($"sortBy={Uri.EscapeDataString(peticion.SortBy)}");
            partes.Add($"sortDescending={(peticion.SortDescending ? "true" : "false")}");
        }

        return partes;
    }

    private static void Añadir(List<string> partes, string clave, string? valor)
    {
        if (!string.IsNullOrWhiteSpace(valor))
            partes.Add($"{clave}={Uri.EscapeDataString(valor.Trim())}");
    }

    private static string Fecha(DateTime valor)
        => Uri.EscapeDataString(valor.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));

    private static string Unir(List<string> partes) => "?" + string.Join('&', partes);
}

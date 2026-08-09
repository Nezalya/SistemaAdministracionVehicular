using System.Globalization;
using SAV.Core.Enums;

namespace SAV.UI.Services.Ui;

/// <summary>
/// Formato consistente en toda la aplicación. Un solo sitio donde cambiar
/// la moneda, el formato de fecha o el texto de los enums.
/// </summary>
public static class Formato
{
    /// <summary>Importe con separador de miles y dos decimales.</summary>
    public static string Moneda(decimal valor)
        => valor.ToString("N2", CultureInfo.CurrentCulture) + " $";

    public static string Moneda(decimal? valor)
        => valor.HasValue ? Moneda(valor.Value) : "—";

    public static string Numero(int valor)
        => valor.ToString("N0", CultureInfo.CurrentCulture);

    public static string Fecha(DateTime? valor)
        => valor?.ToString("dd/MM/yyyy", CultureInfo.CurrentCulture) ?? "—";

    public static string FechaHora(DateTime? valor)
        => valor?.ToString("dd/MM/yyyy HH:mm", CultureInfo.CurrentCulture) ?? "—";

    public static string Porcentaje(decimal? valor)
        => valor.HasValue ? valor.Value.ToString("0.##", CultureInfo.CurrentCulture) + " %" : "—";

    /// <summary>Devuelve el texto o un guion cuando no hay dato, para no dejar celdas vacías.</summary>
    public static string OGuion(string? texto)
        => string.IsNullOrWhiteSpace(texto) ? "—" : texto;

    public static string Texto(EstadoAutomovil estado) => estado switch
    {
        EstadoAutomovil.Stock => "En stock",
        EstadoAutomovil.Vendido => "Vendido",
        _ => estado.ToString()
    };

    public static string Texto(FormaPago formaPago) => formaPago switch
    {
        FormaPago.Contado => "Contado",
        FormaPago.Financiera => "Financiada",
        _ => formaPago.ToString()
    };

    public static string Texto(OrigenVenta origen) => origen switch
    {
        OrigenVenta.Stock => "Desde stock",
        OrigenVenta.EncargoFabrica => "Encargo a fábrica",
        _ => origen.ToString()
    };

    public static string Texto(TipoUbicacion tipo) => tipo switch
    {
        TipoUbicacion.Concesionario => "Concesionario",
        TipoUbicacion.ServicioOficial => "Servicio oficial",
        _ => tipo.ToString()
    };
}

namespace SAV.Contracts.Dtos;

/// <summary>
/// Criterios de búsqueda de ventas. La paginación y la ordenación
/// se resuelven en el servidor.
/// </summary>
public class FiltroVentaDto : PagedRequest
{
    /// <summary>Texto libre: busca en matrícula, cliente y bastidor.</summary>
    public string? Busqueda { get; set; }

    public DateTime? Desde { get; set; }
    public DateTime? Hasta { get; set; }

    public FormaPago? FormaPago { get; set; }
    public OrigenVenta? Origen { get; set; }

    public int? VendedorId { get; set; }
    public int? ClienteId { get; set; }
}

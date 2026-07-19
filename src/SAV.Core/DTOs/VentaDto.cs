namespace SAV.Api.DTOs;

public class VentaDto
{
    public int Id { get; set; }
    public int AutomovilId { get; set; }
    public int ClienteId { get; set; }
    public int? VendedorId { get; set; }
    public int? ServicioOficialId { get; set; }
    public decimal PrecioCobrado { get; set; }
    public FormaPago FormaPago { get; set; }
    public string? Financiera { get; set; }
    public DateTime FechaVenta { get; set; }
    public DateTime? FechaEntrega { get; set; }
    public string? Matricula { get; set; }
    public OrigenVenta Origen { get; set; }
    public decimal PrecioBase { get; set; }     
    public decimal TotalExtras { get; set; }    
    public decimal PrecioTotal { get; set; }    
    public List<VentaExtraDto> Extras { get; set; } = new();
}

public class VentaExtraDto
{
    public int CaracteristicaId { get; set; }
    public string? Nombre { get; set; }
    public decimal PrecioCobrado { get; set; }
}

public class CrearVentaDto
{
    public int AutomovilId { get; set; }
    public int ClienteId { get; set; }
    public int? VendedorId { get; set; }
    public int? ServicioOficialId { get; set; }
    public FormaPago FormaPago { get; set; }
    public string? Financiera { get; set; }
    public DateTime? FechaEntrega { get; set; }
    public string? Matricula { get; set; }
    public OrigenVenta Origen { get; set; }
    public List<int> CaracteristicaIdsExtras { get; set; } = new();
}
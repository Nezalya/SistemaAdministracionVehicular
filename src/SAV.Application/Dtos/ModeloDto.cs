namespace SAV.Application.DTOs;


public class ModeloDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public decimal PrecioBase { get; set; }
    public decimal? Descuento { get; set; }
    public string? PotenciaFiscal { get; set; }
    public string? Cilindrada { get; set; }
    public int MarcaId { get; set; }
    public string? MarcaNombre { get; set; }
}
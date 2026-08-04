namespace SAV.Application.DTOs;


public class ExtraDisponibleDto
{
    public int CaracteristicaId { get; set; }
    public string Nombre { get; set; } = null!;
    public decimal PrecioExtra { get; set; }
}
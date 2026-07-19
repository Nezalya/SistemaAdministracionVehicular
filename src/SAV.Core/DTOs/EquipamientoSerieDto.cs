namespace SAV.Api.DTOs
{
    public class EquipamientoSerieDto
    {
        public int CaracteristicaId { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Categoria { get; set; }
    }
}

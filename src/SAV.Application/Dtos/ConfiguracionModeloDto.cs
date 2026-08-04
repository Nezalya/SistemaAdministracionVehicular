namespace SAV.Application.DTOs;


    public class ConfiguracionModeloDto
    {
        public int ModeloId { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal PrecioBase { get; set; }
        public decimal? Descuento { get; set; }
        public List<EquipamientoSerieDto> EquipamientoDeSerie { get; set; } = new();
        public List<ExtraDisponibleDto> ExtrasDisponibles { get; set; } = new();
    }

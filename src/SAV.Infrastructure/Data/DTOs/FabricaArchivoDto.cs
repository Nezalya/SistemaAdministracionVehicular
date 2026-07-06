namespace SAV.Infrastructure.Data.Dtos
{
    public class FabricaArchivoDto
    {
        public FabricaInfoDto Fabrica { get; set; } = null!;
        public List<CaracteristicaDto> Caracteristicas { get; set; } = new();
        public List<MarcaDto> Marcas { get; set; } = new();
    }

    public class FabricaInfoDto
    {
        public string Nombre { get; set; } = null!;
        public string Version { get; set; } = null!;
        public string FechaActualizacion { get; set; } = null!;
    }

    public class CaracteristicaDto
    {
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public string? Categoria { get; set; }
    }

    public class MarcaDto
    {
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? PaisOrigen { get; set; }
        public List<ModeloDto> Modelos { get; set; } = new();
    }

    public class ModeloDto
    {
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public decimal PrecioBase { get; set; }
        public decimal? Descuento { get; set; }
        public string? PotenciaFiscal { get; set; }
        public string? Cilindrada { get; set; }
        public List<string> EquipamientoSerie { get; set; } = new();
        public List<ExtraDto> ExtrasDisponibles { get; set; } = new();
    }

    public class ExtraDto
    {
        public string CodigoCaracteristica { get; set; } = null!;
        public decimal PrecioExtra { get; set; }
    }
}
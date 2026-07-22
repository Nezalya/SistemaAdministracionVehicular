namespace SAV.Api.DTOs
{
    public class GenerarAutomovilesLoteDto
    {
        public int ModeloId { get; set; }
        public int CantidadPorColor { get; set; }
        public List<string> Colores { get; set; } = new();
        public int? AnoFabricacion { get; set; }
        public int? ConcesionarioUbicacionId { get; set; }
        public int? ServicioOficialUbicacionId { get; set; }
    }
}
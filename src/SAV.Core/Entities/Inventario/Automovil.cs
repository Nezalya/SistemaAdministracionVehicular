namespace SAV.Core.Entities.Inventario
{
    public class Automovil
    {
        public int Id { get; set; }
        public int ModeloId { get; set; }
        public string NumBastidor { get; set; } = null!;
        public string? Color { get; set; }
        public int? AnoFabricacion { get; set; }
        public EstadoAutomovil Estado { get; set; }

        public int ConcesionarioPropietarioId { get; set; }
        public int? ConcesionarioUbicacionId { get; set; }
        public int? ServicioOficialUbicacionId { get; set; }

        public Modelo Modelo { get; set; } = null!;
        public Concesionario ConcesionarioPropietario { get; set; } = null!;
        public Concesionario? ConcesionarioUbicacion { get; set; }
        public ServicioOficial? ServicioOficialUbicacion { get; set; }
        public Venta? Venta { get; set; }
    }
}

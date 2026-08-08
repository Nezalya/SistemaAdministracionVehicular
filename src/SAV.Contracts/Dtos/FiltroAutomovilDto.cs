namespace SAV.Contracts.Dtos;


    public class FiltroAutomovilDto : PagedRequest
{
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public EstadoAutomovil? Estado { get; set; }
        public string? NumBastidor { get; set; }
        public int? ServicioOficialUbicacionId { get; set; }   
        public bool? SoloEnConcesionario { get; set; }
    }

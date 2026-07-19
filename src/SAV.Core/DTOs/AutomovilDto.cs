namespace SAV.Api.DTOs;

public class AutomovilDto
{
    public int Id { get; set; }
    public int ModeloId { get; set; }
    public string? ModeloNombre { get; set; }
    public string NumBastidor { get; set; } = null!;
    public string? Color { get; set; }
    public int? AnoFabricacion { get; set; }
    public EstadoAutomovil Estado { get; set; }
    public int ConcesionarioPropietarioId { get; set; }
    public int? ConcesionarioUbicacionId { get; set; }      
    public int? ServicioOficialUbicacionId { get; set; }
}

public class RegistrarAutomovilDto
{
    public int ModeloId { get; set; }
    public string NumBastidor { get; set; } = null!;
    public string? Color { get; set; }
    public int? AnoFabricacion { get; set; }
    public int? ConcesionarioUbicacionId { get; set; }
    public int? ServicioOficialUbicacionId { get; set; }
}

public class ActualizarAutomovilDto
{
    public string? Color { get; set; }
    public int? AnoFabricacion { get; set; }
    public int? ConcesionarioUbicacionId { get; set; }
    public int? ServicioOficialUbicacionId { get; set; }
}
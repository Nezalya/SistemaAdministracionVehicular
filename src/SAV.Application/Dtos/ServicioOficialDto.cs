namespace SAV.Application.DTOs;


public class ServicioOficialDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string Nif { get; set; } = null!;
    public string? Domicilio { get; set; }
    public string? Telefono { get; set; }
    public int ConcesionarioId { get; set; }
}

public class GuardarServicioOficialDto
{
    public string Nombre { get; set; } = null!;
    public string Nif { get; set; } = null!;
    public string? Domicilio { get; set; }
    public string? Telefono { get; set; }
}
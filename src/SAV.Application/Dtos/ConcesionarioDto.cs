namespace SAV.Application.DTOs;

public class ConcesionarioDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string Nif { get; set; } = null!;
    public string? Domicilio { get; set; }
    public string? Telefono { get; set; }
    public string? Email { get; set; }
}

public class GuardarConcesionarioDto
{
    public string Nombre { get; set; } = null!;
    public string Nif { get; set; } = null!;
    public string? Domicilio { get; set; }
    public string? Telefono { get; set; }
    public string? Email { get; set; }
}
namespace SAV.Api.DTOs;

public class ClienteDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string Nif { get; set; } = null!;
    public string? Domicilio { get; set; }
    public string? Telefono { get; set; }
    public string? Email { get; set; }
    public DateTime FechaRegistro { get; set; }
}

public class GuardarClienteDto
{
    public string Nombre { get; set; } = null!;
    public string Nif { get; set; } = null!;
    public string? Domicilio { get; set; }
    public string? Telefono { get; set; }
    public string? Email { get; set; }
}
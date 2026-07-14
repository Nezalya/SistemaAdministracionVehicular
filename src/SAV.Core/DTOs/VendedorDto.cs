namespace SAV.Api.DTOs;

public class VendedorDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string Nif { get; set; } = null!;
    public string? Domicilio { get; set; }
    public string? Telefono { get; set; }
    public string? Email { get; set; }
    public int ConcesionarioId { get; set; }
}

public class GuardarVendedorDto
{
    public string Nombre { get; set; } = null!;
    public string Nif { get; set; } = null!;
    public string? Domicilio { get; set; }
    public string? Telefono { get; set; }
    public string? Email { get; set; }
    public int ConcesionarioId { get; set; }
}
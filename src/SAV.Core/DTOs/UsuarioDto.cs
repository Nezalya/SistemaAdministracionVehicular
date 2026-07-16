namespace SAV.Api.DTOs;

public class UsuarioDto
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public RolUsuario Rol { get; set; }
    public bool Activo { get; set; }
    public int? VendedorId { get; set; }
    public int? ClienteId { get; set; }
    public int? ConcesionarioId { get; set; }
}

public class CrearUsuarioDto
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public RolUsuario Rol { get; set; }
    public int? VendedorId { get; set; }
    public int? ClienteId { get; set; }
    public int? ConcesionarioId { get; set; }
}
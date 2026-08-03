namespace SAV.Application.DTOs;

public class UsuarioDto
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public RolUsuario Rol { get; set; }
    public bool Activo { get; set; }
    public int? VendedorId { get; set; }
    public int? ClienteId { get; set; }
    public int? ConcesionarioId { get; set; }
    public string Email { get; set; } = null!;
    public DateTime FechaRegistro { get; set; }
}

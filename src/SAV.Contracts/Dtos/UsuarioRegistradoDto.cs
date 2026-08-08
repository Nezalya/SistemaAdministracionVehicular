namespace SAV.Contracts.Dtos;

public class UsuarioRegistradoDto
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Rol { get; set; } = null!;
    public DateTime FechaRegistro { get; set; }
}

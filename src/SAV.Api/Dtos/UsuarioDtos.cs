namespace SAV.Api.Dtos
{
    public record UsuarioDto(int Id, string Username, int Rol, bool Activo);

    public record CreateUsuarioDto(string Username, string Password, int Rol, int? VendedorId = null, int? ConcesionarioId = null, int? ClienteId = null);
}

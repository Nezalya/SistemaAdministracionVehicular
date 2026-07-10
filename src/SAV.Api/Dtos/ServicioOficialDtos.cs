namespace SAV.Api.Dtos
{
    public record ServicioOficialDto(int Id, int ConcesionarioId, string Nombre, string Nif, string? Domicilio, string? Telefono);

    public record CreateServicioOficialDto(int ConcesionarioId, string Nombre, string Nif, string? Domicilio, string? Telefono);
}

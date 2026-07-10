namespace SAV.Api.Dtos
{
    public record MarcaDto(int Id, string Nombre);
    public record ModeloDto(int Id, string Nombre, int MarcaId);
}

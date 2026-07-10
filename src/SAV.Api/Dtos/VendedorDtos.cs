namespace SAV.Api.Dtos
{
    public record VendedorDto(int Id, string Nombre, string Telefono, string Email);

    public record CreateVendedorDto(string Nombre, string Telefono, string Email);
}

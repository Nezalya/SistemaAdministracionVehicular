using System;

namespace SAV.Api.Dtos
{
    public record ClienteDto(int Id, string Nombre, string Nif, string? Domicilio, string? Telefono, string? Email, DateTime FechaRegistro);

    public record CreateClienteDto(string Nombre, string Nif, string? Domicilio, string? Telefono, string? Email);
}

namespace SAV.Contracts.Dtos;

/// <summary>
/// Criterios de búsqueda de clientes. La paginación y la ordenación
/// se resuelven en el servidor, igual que en el inventario.
/// </summary>
public class FiltroClienteDto : PagedRequest
{
    /// <summary>Texto libre: busca en nombre, NIF, correo y teléfono.</summary>
    public string? Busqueda { get; set; }
}

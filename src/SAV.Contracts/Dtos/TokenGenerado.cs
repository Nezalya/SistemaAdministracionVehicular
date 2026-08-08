namespace SAV.Contracts.Dtos;

public class TokenGenerado
{
    public string Token { get; set; } = null!;
    public DateTime Expira { get; set; }
}

public class ResultadoAutenticacionDto
{
    public int UsuarioId { get; set; }
    public string Token { get; set; } = null!;
    public string Usuario { get; set; } = null!;
    public string Rol { get; set; } = null!;
    public DateTime Expira { get; set; }

    /// <summary>Identifica al vendedor cuando el usuario tiene rol Vendedor.</summary>
    public int? VendedorId { get; set; }

    /// <summary>Identifica al cliente cuando el usuario tiene rol Cliente.</summary>
    public int? ClienteId { get; set; }
}
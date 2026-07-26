namespace SAV.Core.Dtos
{
    public class TokenGenerado
    {
        public string Token { get; set; } = null!;
        public DateTime Expira { get; set; }
    }

    public class ResultadoAutenticacionDto
    {
        public string Token { get; set; } = null!;
        public string Usuario { get; set; } = null!;
        public string Rol { get; set; } = null!;
        public DateTime Expira { get; set; }
    }
}
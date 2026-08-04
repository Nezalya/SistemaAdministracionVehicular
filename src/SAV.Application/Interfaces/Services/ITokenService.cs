namespace SAV.Application.Interfaces.Services
{
    public interface ITokenService
    {
        TokenGenerado GenerarToken(Usuario usuario);
    }
}

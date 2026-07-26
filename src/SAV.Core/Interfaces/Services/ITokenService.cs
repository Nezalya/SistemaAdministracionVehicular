using SAV.Core.Dtos;

namespace SAV.Core.Interfaces.Services
{
    public interface ITokenService
    {
        TokenGenerado GenerarToken(Usuario usuario);
    }
}

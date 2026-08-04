
namespace SAV.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<ResultadoAutenticacionDto> Login(string email, string password);
        Task<Usuario> RegistrarAdministrador(string username, string email, string password);
        Task<Usuario> RegistrarVendedor(string username, string email, string password, Vendedor vendedor);
        Task<Usuario> RegistrarCliente(string username, string email, string password, Cliente cliente);
    }
}
namespace SAV.Application.Interfaces.Services
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> ObtenerTodosLosClientes();
        Task<PagedResult<Cliente>> BuscarClientes(FiltroClienteDto filtro);
        Task<Cliente?> ObtenerClientePorId(int id);
        Task<Cliente> CrearCliente(Cliente cliente);
        Task<Cliente> ActualizarCliente(int id, Cliente cliente);
        Task EliminarCliente(int id);
        Task<ResumenComprasClienteDto> ObtenerResumenCompras(int clienteId);
    }
}


namespace SAV.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<IEnumerable<Cliente>> ObtenerTodosLosClientes() 
        { 
            return await _clienteRepository.GetAll();
        }
        public async Task<PagedResult<Cliente>> BuscarClientes(FiltroClienteDto filtro)
        {
            return await _clienteRepository.BuscarClientes(filtro);
        }

        public async Task<Cliente?> ObtenerClientePorId(int id)
        {
            return await _clienteRepository.GetById(id);
        }

        public async Task<Cliente> CrearCliente(Cliente cliente)
        {
            var nifExistente = await _clienteRepository.ObtenerClientePorNif(cliente.Nif);
            if (nifExistente is not null)
                throw new BusinessException("Ya existe un cliente con ese NIF.");
            
            await _clienteRepository.Add(cliente);
            await _clienteRepository.SaveChanges();
            return cliente;
        }

        public async Task<Cliente> ActualizarCliente(int id, Cliente cliente)
        {
            var clienteExistente = await _clienteRepository.GetById(id);
            if (clienteExistente is null)
                throw new NotFoundException("El cliente no existe.");

            var conMismoNif = await _clienteRepository.ObtenerClientePorNif(cliente.Nif);
            if (conMismoNif is not null && conMismoNif.Id != id)
                throw new BusinessException("Ya existe otro cliente con ese NIF.");

            clienteExistente.Nombre = cliente.Nombre;
            clienteExistente.Nif = cliente.Nif;
            clienteExistente.Domicilio = cliente.Domicilio;
            clienteExistente.Telefono = cliente.Telefono;
            clienteExistente.Email = cliente.Email;

            await _clienteRepository.Update(clienteExistente);
            await _clienteRepository.SaveChanges();
            return clienteExistente;
        }

        public async Task EliminarCliente(int id)
        {
            var clienteExistente = await _clienteRepository.GetById(id);
            if (clienteExistente is null)
                throw new NotFoundException("El cliente no existe.");

            if (await _clienteRepository.TieneVentas(id))
                throw new BusinessException(
                    "No se puede eliminar el cliente porque tiene ventas registradas.");

            await _clienteRepository.Delete(id);
            await _clienteRepository.SaveChanges();
        }

        public async Task<ResumenComprasClienteDto> ObtenerResumenCompras(int clienteId)
        {
            var compraResumen = await _clienteRepository.ObtenerResumenCompras(clienteId);
           
            if (compraResumen is null)
                throw new NotFoundException("El cliente no existe.");
            
            if (compraResumen.TotalCompras == 0)
                throw new BusinessException("El cliente no tiene compras registradas.");
       
            return compraResumen;
        }
    }
}
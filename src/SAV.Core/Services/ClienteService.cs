namespace SAV.Core.Services
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
        public async Task<Cliente?> ObtenerClientePorId(int id) 
        {
            return await _clienteRepository.GetById(id);
        }

        public async Task<Cliente> CrearCliente(Cliente cliente)
        {
            var nifExistente = await _clienteRepository.ObtenerClientePorNif(cliente.Nif);
            if (nifExistente is not null)
                throw new InvalidOperationException("Ya existe un cliente con ese NIF.");
            
            await _clienteRepository.Add(cliente);
            await _clienteRepository.SaveChanges();
            return cliente;
        }

        public async Task<Cliente> ActualizarCliente(int id, Cliente cliente)
        {
            var ClienteExistente = await _clienteRepository.GetById(id);
            if (ClienteExistente is null)
                throw new InvalidOperationException("El cliente no existe.");

            var conMismoNif = await _clienteRepository.ObtenerClientePorNif(cliente.Nif);
            if (conMismoNif is not null && conMismoNif.Id != id)
                throw new InvalidOperationException("Ya existe otro cliente con ese NIF.");

            ClienteExistente.Nombre = cliente.Nombre;
            ClienteExistente.Nif = cliente.Nif;
            ClienteExistente.Domicilio = cliente.Domicilio;
            ClienteExistente.Telefono = cliente.Telefono;
            ClienteExistente.Email = cliente.Email;

            await _clienteRepository.Update(ClienteExistente);
            await _clienteRepository.SaveChanges();
            return ClienteExistente;
        }

        public async Task EliminarCliente(int id)
        {
            var ClienteExistente = await _clienteRepository.GetById(id);
            if (ClienteExistente is null)
                throw new InvalidOperationException("El cliente no existe.");

            await _clienteRepository.Delete(id);
            await _clienteRepository.SaveChanges();
        }
    }
}
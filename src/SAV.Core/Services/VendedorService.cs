namespace SAV.Core.Services
{
    public class VendedorService : IVendedorService
    {
        private readonly IVendedorRepository _vendedorRepository;
        public VendedorService(IVendedorRepository vendedorRepository)
        {
            _vendedorRepository = vendedorRepository;
        }
        public async Task<IEnumerable<Vendedor>> ObtenerTodosLosVendedores() 
        { 
            return await _vendedorRepository.GetAll();
        }
        public async Task<Vendedor?> ObtenerVendedorPorId(int id) 
        { 
            return await _vendedorRepository.GetById(id);
        }
        public async Task<Vendedor> CrearVendedor(Vendedor vendedor)
        {
            await _vendedorRepository.Add(vendedor);
            await _vendedorRepository.SaveChanges();
            return vendedor;
        }
        public async Task<Vendedor> ActualizarVendedor(int id, Vendedor vendedor)
        {
            var VendedorExistente = await _vendedorRepository.GetById(id);
            if (VendedorExistente is null)
                throw new InvalidOperationException("El vendedor no existe.");
            VendedorExistente.Nombre = vendedor.Nombre;
            VendedorExistente.Nif = vendedor.Nif;
            VendedorExistente.Domicilio = vendedor.Domicilio;
            VendedorExistente.Telefono = vendedor.Telefono;
            VendedorExistente.Email = vendedor.Email;
            VendedorExistente.ConcesionarioId = vendedor.ConcesionarioId;
            await _vendedorRepository.Update(VendedorExistente);
            await _vendedorRepository.SaveChanges();
            return VendedorExistente;
        }
        public async Task EliminarVendedor(int id)
        {
            var VendedorExistente = await _vendedorRepository.GetById(id);
            if (VendedorExistente is null)
                throw new InvalidOperationException("El vendedor no existe.");
            await _vendedorRepository.Delete(id);
            await _vendedorRepository.SaveChanges();
        }
    }
}

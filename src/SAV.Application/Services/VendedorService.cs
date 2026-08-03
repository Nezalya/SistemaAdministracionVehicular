namespace SAV.Application.Services
{
    public class VendedorService : IVendedorService
    {
        private readonly IVendedorRepository _vendedorRepository;
        private readonly IConcesionarioRepository _concesionarioRepository;
        private readonly IVentaRepository _ventaRepository;
        public VendedorService(IVendedorRepository vendedorRepository, IConcesionarioRepository concesionarioRepository)
        {
            _vendedorRepository = vendedorRepository;
            _concesionarioRepository = concesionarioRepository;
        }
        public async Task<IEnumerable<Vendedor>> ObtenerTodosLosVendedores()
        {
            return await _vendedorRepository.GetAll();
        }
        public async Task<Vendedor?> ObtenerVendedorPorId(int id)
        {
            return await _vendedorRepository.GetById(id);
        }

        public async Task<Vendedor> ActualizarVendedor(int id, Vendedor vendedor)
        {
            var vendedorExistente = await _vendedorRepository.GetById(id);
            if (vendedorExistente is null)
                throw new NotFoundException("El vendedor no existe.");

            var conMismoNif = await _vendedorRepository.ObtenerVendedorPorNif(vendedor.Nif);
            if (conMismoNif is not null && conMismoNif.Id != id)
                throw new BusinessException("Ya existe otro vendedor con ese NIF.");

            vendedorExistente.Nombre = vendedor.Nombre;
            vendedorExistente.Nif = vendedor.Nif;
            vendedorExistente.Domicilio = vendedor.Domicilio;
            vendedorExistente.Telefono = vendedor.Telefono;
            vendedorExistente.Email = vendedor.Email;

            await _vendedorRepository.Update(vendedorExistente);
            await _vendedorRepository.SaveChanges();
            return vendedorExistente;
        }
        public async Task EliminarVendedor(int id)
        {
            var vendedorExistente = await _vendedorRepository.GetById(id);
            if (vendedorExistente is null)
                throw new NotFoundException("El vendedor no existe.");

            if (await _vendedorRepository.TieneVentas(id))
                throw new BusinessException(
                    "No se puede eliminar el vendedor porque tiene ventas registradas.");

            await _vendedorRepository.Delete(id);
            await _vendedorRepository.SaveChanges();
        }

        public async Task<ResumenVentasVendedorDto> ObtenerResumenVentas(int vendedorId)
        {
            var resumen = await _vendedorRepository.ObtenerResumenVentas(vendedorId);
            if (resumen is null)
                throw new NotFoundException("El vendedor no existe.");
            return resumen;
        }
    }
}
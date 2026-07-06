namespace SAV.Core.Services
{
    public class MarcaService : IMarcaService
    {
        private readonly IMarcaRepository _marcaRepository;

        public MarcaService(IMarcaRepository marcaRepository)
        {
            _marcaRepository = marcaRepository;
        }

        public async Task<IEnumerable<Marca>> ObtenerTodasLasMarcas()
        {
            return await _marcaRepository.GetAll();
        }

        public async Task<Marca?> ObtenerMarcaPorId(int id)
        {
            return await _marcaRepository.GetById(id);
        }

        public async Task<Marca> CrearMarca(Marca marca)
        {
            var MarcaExistente = await _marcaRepository.GetByNombre(marca.Nombre);
            if (MarcaExistente is not null)
                throw new InvalidOperationException("Ya existe una marca con ese nombre.");

            await _marcaRepository.Add(marca);
            await _marcaRepository.SaveChanges();

            return marca;
        }

        public async Task<Marca> ActualizarMarca(int id, Marca marca)
        {
            var existente = await _marcaRepository.GetById(id);
            if (existente is null)
                throw new InvalidOperationException("La marca no existe.");

            // Regla: si cambió el nombre, que no choque con otra marca
            if (!existente.Nombre.Equals(marca.Nombre, StringComparison.OrdinalIgnoreCase))
            {
                var conMismoNombre = await _marcaRepository.GetByNombre(marca.Nombre);
                if (conMismoNombre is not null)
                    throw new InvalidOperationException("Ya existe otra marca con ese nombre.");
            }

            // Actualizamos los campos de la marca existente
            existente.Nombre = marca.Nombre;
            existente.PaisOrigen = marca.PaisOrigen;

            _marcaRepository.Update(existente);
            await _marcaRepository.SaveChanges();

            return existente;
        }

        public async Task EliminarMarca(int id)
        {
            // Regla: la marca debe existir
            var existente = await _marcaRepository.GetById(id);
            if (existente is null)
                throw new InvalidOperationException("La marca no existe.");

            await _marcaRepository.Delete(id);
            await _marcaRepository.SaveChanges();
        }
    }
}
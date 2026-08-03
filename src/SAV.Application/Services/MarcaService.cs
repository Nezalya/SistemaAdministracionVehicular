namespace SAV.Application.Services
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
    }
}
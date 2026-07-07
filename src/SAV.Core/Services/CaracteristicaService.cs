namespace SAV.Core.Services
{
    public class CaracteristicaService : ICaracteristicaService
    {
        private readonly ICaracteristicaRepository _caracteristicaRepository;
        public CaracteristicaService(ICaracteristicaRepository caracteristicaRepository)
        {
            _caracteristicaRepository = caracteristicaRepository;
        }
        public async Task<IEnumerable<Caracteristica>> ObtenerTodasLasCaracteristicas()
        {
            return await _caracteristicaRepository.GetAll();
        }
        
        public async Task<Caracteristica?> ObtenerCaracteristicaPorId(int id)
        {
            return await _caracteristicaRepository.GetById(id);
        }
    }
}
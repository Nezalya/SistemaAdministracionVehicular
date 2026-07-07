namespace SAV.Core.Services
{
    public class AutomovilService : IAutomovilService
    {
        private readonly IAutomovilRepository _automovilRepository;

        public AutomovilService(IAutomovilRepository automovilRepository)
        {
            _automovilRepository = automovilRepository;
        }

        public async Task<IEnumerable<Automovil>> ObtenerTodosLosAutomoviles() 
        { 
            return await _automovilRepository.GetAll();
        }

        public async Task<Automovil?> ObtenerAutomovilPorId(int id) 
        { 
            return await _automovilRepository.GetById(id);
        }

        public async Task<IEnumerable<Automovil>> ObtenerAutomovilesDisponibles() 
        { 
            return await _automovilRepository.GetDisponibles();
        }
        public async Task<Automovil> RegistrarAutomovil(Automovil automovil)//ojo con esta regla
        {
            automovil.Estado = EstadoAutomovil.Stock;

            await _automovilRepository.Add(automovil);
            await _automovilRepository.SaveChanges();
            return automovil;
        }
    }
}
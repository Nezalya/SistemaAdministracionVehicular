namespace SAV.Core.Services
{
    public class AutomovilService : IAutomovilService
    {
        private readonly IAutomovilRepository _automovilRepository;
        private readonly IConcesionarioRepository _concesionarioRepository;

        public AutomovilService(IAutomovilRepository automovilRepository, IConcesionarioRepository concesionarioRepository)
        {
            _automovilRepository = automovilRepository;
            _concesionarioRepository = concesionarioRepository;
        }

        public async Task<IEnumerable<Automovil>> ObtenerTodosLosAutomoviles() 
        { 
            return await _automovilRepository.ObternerTodosLosAutomoviles();
        }

        public async Task<Automovil?> ObtenerAutomovilPorId(int id) 
        { 
            return await _automovilRepository.ObtenerAutomovil(id);
        }

        public async Task<IEnumerable<Automovil>> ObtenerAutomovilesDisponibles() 
        { 
            return await _automovilRepository.ObtenerAutomovilesDisponibles();
        }
        public async Task<Automovil> RegistrarAutomovil(Automovil automovil)
        {

            var concesionarios = await _concesionarioRepository.GetAll();
            var concesionario = concesionarios.FirstOrDefault();
            if (concesionario is null)
                throw new InvalidOperationException("No hay un concesionario registrado en el sistema.");

            automovil.ConcesionarioPropietarioId = concesionario.Id;   
            automovil.Estado = EstadoAutomovil.Stock;

            await _automovilRepository.Add(automovil);
            await _automovilRepository.SaveChanges();
            return await _automovilRepository.ObtenerAutomovil(automovil.Id) ?? automovil;
        }
    }
}
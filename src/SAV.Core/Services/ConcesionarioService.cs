namespace SAV.Core.Services
{
    public class ConcesionarioService : IConcesionarioService
    {
        private readonly IConcesionarioRepository _concesionarioRepository;

        public ConcesionarioService(IConcesionarioRepository concesionarioRepository)
        {
            _concesionarioRepository = concesionarioRepository;
        }

        public async Task<IEnumerable<Concesionario>> ObtenerElConcesionario() {
            return await _concesionarioRepository.GetAll();
        }

        public async Task<Concesionario?> ObtenerConcesionarioPorId(int id)
        {
            return await _concesionarioRepository.GetById(id);
        }

        public async Task<Concesionario> CrearConcesionario(Concesionario concesionario)
        {
            // Regla de negocio: el sistema administra UN solo concesionario
            var existentes = await _concesionarioRepository.GetAll();
            if (existentes.Any())
                throw new InvalidOperationException("Ya existe un concesionario registrado. El sistema solo administra uno.");

            await _concesionarioRepository.Add(concesionario);
            await _concesionarioRepository.SaveChanges();
            return concesionario;
        }

        public async Task<Concesionario> ActualizarConcesionario(int id, Concesionario concesionario)
        {
            var existente = await _concesionarioRepository.GetById(id);
            if (existente is null)
                throw new InvalidOperationException("El concesionario no existe.");

            existente.Nombre = concesionario.Nombre;
            existente.Nif = concesionario.Nif;
            existente.Domicilio = concesionario.Domicilio;
            existente.Telefono = concesionario.Telefono;
            existente.Email = concesionario.Email;

            await _concesionarioRepository.Update(existente);
            await _concesionarioRepository.SaveChanges();
            return existente;
        }
    }
}
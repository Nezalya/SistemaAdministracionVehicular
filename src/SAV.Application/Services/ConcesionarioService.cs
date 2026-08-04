namespace SAV.Application.Services
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

            var nifExistente = await _concesionarioRepository.ObtenerConcesionarioPorNif(concesionario.Nif);
            if (nifExistente is not null)
                throw new BusinessException("Ya existe un concesionario con ese NIF.");

            // Regla de negocio: el sistema administra UN solo concesionario
            var concesionariosExistentes = await _concesionarioRepository.GetAll();
            if (concesionariosExistentes.Any())
                throw new BusinessException("Ya existe un concesionario registrado. El sistema solo administra uno.");

            await _concesionarioRepository.Add(concesionario);
            await _concesionarioRepository.SaveChanges();
            return concesionario;
        }

        public async Task<Concesionario> ActualizarConcesionario(int id, Concesionario concesionario)
        {
            var concesionarioExistente = await _concesionarioRepository.GetById(id);
            if (concesionarioExistente is null)
                throw new NotFoundException("El concesionario no existe.");

            var conMismoNif = await _concesionarioRepository.ObtenerConcesionarioPorNif(concesionario.Nif);
            if (conMismoNif is not null && conMismoNif.Id != id)
                throw new BusinessException("Ya existe otro concesionario con ese NIF.");


            concesionarioExistente.Nombre = concesionario.Nombre;
            concesionarioExistente.Nif = concesionario.Nif;
            concesionarioExistente.Domicilio = concesionario.Domicilio;
            concesionarioExistente.Telefono = concesionario.Telefono;
            concesionarioExistente.Email = concesionario.Email;

            await _concesionarioRepository.Update(concesionarioExistente);
            await _concesionarioRepository.SaveChanges();
            return concesionarioExistente;
        }
    }
}
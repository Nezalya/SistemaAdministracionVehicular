namespace SAV.Core.Services
{
    public class ServicioOficialService : IServicioOficialService
    {
        private readonly IServicioOficialRepository _servicioOficialRepository;
        private readonly IConcesionarioRepository _concesionarioRepository;

        public ServicioOficialService(IServicioOficialRepository servicioOficialRepository,
                                       IConcesionarioRepository concesionarioRepository)
        {
            _servicioOficialRepository = servicioOficialRepository;
            _concesionarioRepository = concesionarioRepository;
        }

        public async Task<IEnumerable<ServicioOficial>> ObtenerTodosLosServiciosOficiales()
        {
            return await _servicioOficialRepository.GetAll();
        }

        public async Task<ServicioOficial?> ObtenerServicioOficialPorId(int id)
        {
            return await _servicioOficialRepository.GetById(id);
        }

        public async Task<ServicioOficial> CrearServicioOficial(ServicioOficial servicioOficial)
        {

            var nifExistente = await _servicioOficialRepository.ObtenerServicioOficialPorNif(servicioOficial.Nif);
            if (nifExistente is not null)
                throw new InvalidOperationException("Ya existe un servicio oficial con ese NIF.");

            // Regla: el servicio oficial pertenece al único concesionario del sistema
            var concesionarios = await _concesionarioRepository.GetAll();
            var concesionario = concesionarios.FirstOrDefault();
            if (concesionario is null)
                throw new InvalidOperationException("No hay un concesionario registrado en el sistema.");

            servicioOficial.ConcesionarioId = concesionario.Id;

            await _servicioOficialRepository.Add(servicioOficial);
            await _servicioOficialRepository.SaveChanges();
            return servicioOficial;
        }

        public async Task<ServicioOficial> ActualizarServicioOficial(int id, ServicioOficial servicioOficial)
        {
            var SoExitente = await _servicioOficialRepository.GetById(id);
            if (SoExitente is null)
                throw new InvalidOperationException("El servicio oficial no existe.");

            var conMismoNif = await _servicioOficialRepository.ObtenerServicioOficialPorNif(servicioOficial.Nif);
            if (conMismoNif is not null && conMismoNif.Id != id)
                throw new InvalidOperationException("Ya existe otro servicio oficial con ese NIF.");

            SoExitente.Nombre = servicioOficial.Nombre;
            SoExitente.Nif = servicioOficial.Nif;
            SoExitente.Domicilio = servicioOficial.Domicilio;
            SoExitente.Telefono = servicioOficial.Telefono;


            await _servicioOficialRepository.Update(SoExitente);
            await _servicioOficialRepository.SaveChanges();
            return SoExitente;
        }

        public async Task EliminarServicioOficial(int id)
        {
            var SoExitente = await _servicioOficialRepository.GetById(id);
            if (SoExitente is null)
                throw new InvalidOperationException("El servicio oficial no existe.");

            await _servicioOficialRepository.Delete(id);
            await _servicioOficialRepository.SaveChanges();
        }
    }
}
namespace SAV.Core.Services
{
    public class ServicioOficialService : IServicioOficialService
    {
        private readonly IServicioOficialRepository _servicioOficialRepository;

        public ServicioOficialService(IServicioOficialRepository servicioOficialRepository)
        {
            _servicioOficialRepository = servicioOficialRepository;
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
            await _servicioOficialRepository.Add(servicioOficial);
            await _servicioOficialRepository.SaveChanges();
            return servicioOficial;
        }

        public async Task<ServicioOficial> ActualizarServicioOficial(int id, ServicioOficial servicioOficial)
        {
            var SoExitente = await _servicioOficialRepository.GetById(id);
            if (SoExitente is null)
                throw new InvalidOperationException("El servicio oficial no existe.");

            SoExitente.Nombre = servicioOficial.Nombre;
            SoExitente.Nif = servicioOficial.Nif;
            SoExitente.Domicilio = servicioOficial.Domicilio;
            SoExitente.Telefono = servicioOficial.Telefono;
            SoExitente.ConcesionarioId = servicioOficial.ConcesionarioId;

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
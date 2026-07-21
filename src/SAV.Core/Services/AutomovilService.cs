using SAV.Core.Dtos;

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

        public async Task<Automovil?> ObtenerAutomovilPorId(int id)
        {
            return await _automovilRepository.ObtenerAutomovil(id);
        }

        public async Task<IEnumerable<Automovil>> ObtenerAutomovilesDisponibles()
        {
            return await _automovilRepository.ObtenerAutomovilesDisponibles();
        }

        public async Task<IEnumerable<Automovil>> ObtenerAutomovilesVendidos()
        {
            return await _automovilRepository.ObtenerAutomovilesVendidos();
        }

        public async Task<Automovil> RegistrarAutomovil(Automovil automovil)
        {
            
            var existenteBastidor = await _automovilRepository.ObtenerAutomovilPorBastidor(automovil.NumBastidor);
            if (existenteBastidor is not null)
                throw new BusinessException("Ya existe un automóvil registrado con ese número de bastidor.");

            var concesionario = (await _concesionarioRepository.GetAll()).FirstOrDefault();
            if (concesionario is null)
                throw new BusinessException("No hay un concesionario registrado en el sistema.");

            automovil.ConcesionarioPropietarioId = concesionario.Id;

            ValidarUbicacion(automovil, concesionario.Id);

            automovil.Estado = EstadoAutomovil.Stock;

            await _automovilRepository.Add(automovil);
            await _automovilRepository.SaveChanges();

            return await _automovilRepository.ObtenerAutomovil(automovil.Id) ?? automovil;
        }

        public async Task<Automovil> ActualizarAutomovil(int id, Automovil automovil)
        {
            var existente = await _automovilRepository.ObtenerAutomovil(id);
            if (existente is null)
                throw new NotFoundException("El automóvil no existe.");

            
            existente.Color = automovil.Color;
            existente.AnoFabricacion = automovil.AnoFabricacion;

            existente.ConcesionarioUbicacionId = automovil.ConcesionarioUbicacionId;
            existente.ServicioOficialUbicacionId = automovil.ServicioOficialUbicacionId;

           
            var concesionario = (await _concesionarioRepository.GetAll()).FirstOrDefault();
            if (concesionario is null)
                throw new NotFoundException("No hay un concesionario registrado en el sistema.");
            ValidarUbicacion(existente, concesionario.Id);

            await _automovilRepository.Update(existente);
            await _automovilRepository.SaveChanges();
            return existente;
        }

        public async Task EliminarAutomovil(int id)
        {
            var automovilExistente = await _automovilRepository.ObtenerAutomovil(id);
            if (automovilExistente is null)
                throw new NotFoundException("El automóvil no existe.");

         
            if (automovilExistente.Estado == EstadoAutomovil.Vendido)
                throw new BusinessException("No se puede eliminar un automóvil que ya fue vendido.");

            await _automovilRepository.Delete(id);
            await _automovilRepository.SaveChanges();
        }

        //Metodo que funciona como un helper para validar la ubicación del automóvil, asegurando que no esté en dos lugares a la vez y asignando el concesionario si no tiene ubicación.
        private static void ValidarUbicacion(Automovil automovil, int concesionarioId)
        {
            bool enConcesionario = automovil.ConcesionarioUbicacionId.HasValue;
            bool enServicioOficial = automovil.ServicioOficialUbicacionId.HasValue;

            if (enConcesionario && enServicioOficial)
                throw new BusinessException(
                    "El automóvil no puede estar ubicado en dos lugares a la vez (concesionario y servicio oficial).");

            if (!enConcesionario && !enServicioOficial)
                automovil.ConcesionarioUbicacionId = concesionarioId;
        }

        public async Task<ResumenStockDto> ObtenerResumenStock()
        {
            return await _automovilRepository.ObtenerResumenStock();
        }

        public async Task<IEnumerable<Automovil>> BuscarAutomoviles(FiltroAutomovilDto filtro)
        {
            return await _automovilRepository.BuscarAutomoviles(filtro);
        }
    }
}
namespace SAV.Application.Services
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

            ValidarUbicacionHelper.ValidarUbicacion(automovil, concesionario.Id);

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
            ValidarUbicacionHelper.ValidarUbicacion(existente, concesionario.Id);

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

        public async Task<ResumenStockDto> ObtenerResumenStock()
        {
            return await _automovilRepository.ObtenerResumenStock();
        }

        public async Task<PagedResult<Automovil>> BuscarAutomoviles(FiltroAutomovilDto filtro)
        {
            return await _automovilRepository.BuscarAutomoviles(filtro);
        }

        public async Task<int> RegistrarLoteDeAutomoviles(int modeloId, int cantidadPorColor, List<string> colores,
    int? anoFabricacion, int? concesionarioUbicacionId, int? servicioOficialUbicacionId)
        {
            if (colores is null || colores.Count == 0)
                throw new BusinessException("Debe especificar al menos un color.");
            if (cantidadPorColor <= 0)
                throw new BusinessException("La cantidad por color debe ser mayor a cero.");

            var concesionario = (await _concesionarioRepository.GetAll()).FirstOrDefault();
            if (concesionario is null)
                throw new BusinessException("No hay un concesionario registrado en el sistema.");

            var nuevos = new List<Automovil>();

            foreach (var color in colores)
            {
                for (int i = 0; i < cantidadPorColor; i++)
                {
                    var auto = new Automovil
                    {
                        ModeloId = modeloId,
                        NumBastidor = GenerarBastidorHelper.GenerarBastidor(),                 
                        Color = color,
                        AnoFabricacion = anoFabricacion,
                        ConcesionarioPropietarioId = concesionario.Id,
                        ConcesionarioUbicacionId = concesionarioUbicacionId,
                        ServicioOficialUbicacionId = servicioOficialUbicacionId,
                        Estado = EstadoAutomovil.Stock
                    };

                    ValidarUbicacionHelper.ValidarUbicacion(auto, concesionario.Id);            
                    nuevos.Add(auto);
                }
            }

            foreach (var auto in nuevos)
                await _automovilRepository.Add(auto);

            await _automovilRepository.SaveChanges();                    
            return nuevos.Count;
        }

       
    }
}
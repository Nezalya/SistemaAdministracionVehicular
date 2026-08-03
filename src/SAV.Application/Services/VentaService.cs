namespace SAV.Application.Services
{
    public class VentaService : IVentaService
    {
        private readonly IVentaRepository _ventaRepository;
        private readonly IAutomovilRepository _automovilRepository;

        public VentaService(IVentaRepository ventaRepository, IAutomovilRepository automovilRepository)
        {
            _ventaRepository = ventaRepository;
            _automovilRepository = automovilRepository;
        }

        public async Task<IEnumerable<Venta>> ObtenerTodasLasVentas() 
        { 
            return await _ventaRepository.ObtenerVentasConDetalle();
        }

        public async Task<Venta?> ObtenerVentaPorId(int id) 
        { 
            return await _ventaRepository.ObtenerVentaConDetalle(id);
        }

        public async Task<Venta> CrearVenta(Venta venta, List<int> caracteristicaIdsExtras)
        {
            // Regla: exactamente uno de Vendedor o ServicioOficial (XOR)
            bool tieneVendedor = venta.VendedorId.HasValue;
            bool tieneServicioOficial = venta.ServicioOficialId.HasValue;

            if (tieneVendedor == tieneServicioOficial)
                throw new BusinessException(
                    "La venta debe tener exactamente un responsable: Un vendedor o un servicio oficial, no ambos ni ninguno.");

            if (venta.FechaEntrega is null)
                throw new BusinessException("La venta debe registrar una fecha de entrega.");

            if (string.IsNullOrWhiteSpace(venta.Matricula))
                throw new BusinessException("La venta debe registrar la matrícula del vehículo.");

            // Regla: el automóvil debe existir y estar en Stock
            var automovil = await _automovilRepository.ObtenerAutomovil(venta.AutomovilId);
            if (automovil is null)
                throw new NotFoundException("El automóvil no existe.");

            if (automovil.Estado != EstadoAutomovil.Stock)
                throw new BusinessException("El automóvil no está disponible para la venta.");

            // Regla: calcular el precio (base del modelo con descuento + extras), no confiar en el precio recibido
            decimal precioBase = automovil.Modelo.PrecioBase;
            if (automovil.Modelo.Descuento is > 0)
                precioBase -= precioBase * (automovil.Modelo.Descuento.Value / 100m);

            decimal totalExtras = 0;
            venta.Extras.Clear();

            foreach (var caracteristicaId in caracteristicaIdsExtras)
            {
                var extraDisponible = automovil.Modelo.ExtrasDisponibles
                    .FirstOrDefault(e => e.CaracteristicaId == caracteristicaId);

                if (extraDisponible is null)
                    throw new BusinessException($"El extra {caracteristicaId} no está disponible para este modelo.");

                venta.Extras.Add(new VentaExtra
                {
                    CaracteristicaId = caracteristicaId,
                    PrecioCobrado = extraDisponible.PrecioExtra
                });

                totalExtras += extraDisponible.PrecioExtra;
            }

            venta.PrecioCobrado = precioBase + totalExtras;
            venta.FechaVenta = DateTime.Now;

            // Marcar el auto como Vendido (misma transacción)
            automovil.Estado = EstadoAutomovil.Vendido;
            await _automovilRepository.Update(automovil);

            await _ventaRepository.Add(venta);

            // Un solo SaveChanges: la venta y el cambio de estado del auto se guardan juntos
            await _ventaRepository.SaveChanges();
            return await _ventaRepository.ObtenerVentaConDetalle(venta.Id) ?? venta;
        }
    }
}
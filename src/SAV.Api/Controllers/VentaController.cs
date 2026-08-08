// VentasController.cs
using Microsoft.AspNetCore.Authorization;
using SAV.Api.Authorization;

namespace SAV.Api.Controllers
{
    [Authorize(Roles = Roles.AdminOVendedor)]
    [ApiController]
    [Route("api/[controller]")]
    public class VentasController : ControllerBase
    {
        private readonly IVentaService _ventaService;
        private readonly IMapper _mapper;
        public VentasController(IVentaService service, IMapper mapper)
        { _ventaService = service; _mapper = mapper; }

        /// <summary>Listado paginado. Admite búsqueda, rango de fechas y filtros por forma de pago y origen.</summary>
        [HttpGet]
        public async Task<IActionResult> ObtenerVentas([FromQuery] FiltroVentaDto filtro)
        {
            var ventas = await _ventaService.BuscarVentas(filtro);
            return Ok(ventas.Map<Venta, VentaDto>(_mapper));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerVentaPorId(int id)
        {
            var venta = await _ventaService.ObtenerVentaPorId(id);
            if (venta is null) return NotFound();

            var ventaDto = _mapper.Map<VentaDto>(venta);
            return Ok(ventaDto);
        }

        [HttpPost]
        public async Task<IActionResult> CrearVenta(CrearVentaDto ventaDto)
        {
            var venta = _mapper.Map<Venta>(ventaDto);
            var ventaCreada = await _ventaService.CrearVenta(venta, ventaDto.CaracteristicaIdsExtras);
            return CreatedAtAction(nameof(ObtenerVentaPorId), new { id = ventaCreada.Id }, _mapper.Map<VentaDto>(ventaCreada));
        }
    }
}
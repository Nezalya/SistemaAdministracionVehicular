// VentasController.cs
namespace SAV.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentasController : ControllerBase
    {
        private readonly IVentaService _ventaService;
        private readonly IMapper _mapper;
        public VentasController(IVentaService service, IMapper mapper)
        { _ventaService = service; _mapper = mapper; }

        [HttpGet]
        public async Task<IActionResult> ObtenerVentas() 
        {
            var ventas = await _ventaService.ObtenerTodasLasVentas();
            var ventasDto = _mapper.Map<IEnumerable<VentaDto>>(ventas);
            return Ok(ventasDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerVentaPorId(int id)
        {
            var venta = await _ventaService.ObtenerVentaPorId(id);
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
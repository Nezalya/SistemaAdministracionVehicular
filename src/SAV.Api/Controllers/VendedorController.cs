namespace SAV.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendedorController : ControllerBase
    {
        private readonly IVendedorService _vendedorService;
        private readonly IMapper _mapper;
        public VendedorController(IVendedorService vendedorService, IMapper mapper)
        { 
            _vendedorService = vendedorService; 
            _mapper = mapper; 
        }


        [HttpGet]
        public async Task<IActionResult> ObtenerVendedores()
        {
            var vendedores = await _vendedorService.ObtenerTodosLosVendedores();
            var vendedoresDto = _mapper.Map<IEnumerable<VendedorDto>>(vendedores);
            return Ok(vendedoresDto);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerVendedorPorId(int id)
        {
            var vendedor = await _vendedorService.ObtenerVendedorPorId(id);
            var vendedorDto = _mapper.Map<VendedorDto>(vendedor);
            return Ok(vendedorDto);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarVendedor(int id, GuardarVendedorDto vendedorDto)
        {
            var vendedor = _mapper.Map<Vendedor>(vendedorDto);
            var vendedorActualizado = await _vendedorService.ActualizarVendedor(id, vendedor);
            return Ok(_mapper.Map<VendedorDto>(vendedorActualizado));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarVendedor(int id)
        {
            await _vendedorService.EliminarVendedor(id);
            return NoContent();
        }

        [HttpGet("{id}/resumen-ventas")]
        public async Task<IActionResult> ObtenerResumenVentas(int id)
        {
            var resumen = await _vendedorService.ObtenerResumenVentas(id);
            return Ok(resumen);
        }
    }
}
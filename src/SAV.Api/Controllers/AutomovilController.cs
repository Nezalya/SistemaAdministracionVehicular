namespace SAV.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutomovilController : ControllerBase
    {
        private readonly IAutomovilService _automovilService;
        private readonly IMapper _mapper;
        public AutomovilController(IAutomovilService automovilService, IMapper mapper)
        { 
            _automovilService = automovilService; 
            _mapper = mapper; }

        [HttpGet]
        public async Task<IActionResult> ObtenerAutomoviles()
        {
            var automoviles = await _automovilService.ObtenerTodosLosAutomoviles();
            var automovilesDto = _mapper.Map<IEnumerable<AutomovilDto>>(automoviles);
            return Ok(automovilesDto);
        }

        [HttpGet("disponibles")]
        public async Task<IActionResult> ObtenerAutomovilesDisponibles()
        {
            var automovilesDisponibles = await _automovilService.ObtenerAutomovilesDisponibles();
            var automovilesDisponiblesDto = _mapper.Map<IEnumerable<AutomovilDto>>(automovilesDisponibles);
            return Ok(automovilesDisponiblesDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerAutomovil(int id)
        {
            var automovil= await _automovilService.ObtenerAutomovilPorId(id);
            var automovilDto = _mapper.Map<AutomovilDto>(automovil);
            return Ok(automovilDto);

        }

        [HttpPost]
        public async Task<IActionResult> RegistrarAutomovil(RegistrarAutomovilDto automovilDto)
        {
            var automovil = _mapper.Map<Automovil>(automovilDto);
            var automovilCreado = await _automovilService.RegistrarAutomovil(automovil);
            return CreatedAtAction(nameof(ObtenerAutomovil), new { id = automovilCreado.Id }, _mapper.Map<AutomovilDto>(automovilCreado));
        }

        [HttpGet("vendidos")]
        public async Task<IActionResult> ObtenerAutomovilesVendidos()
        {
            var automovilesVendidos = await _automovilService.ObtenerAutomovilesVendidos();
            var automovilesVendidosDto = _mapper.Map<IEnumerable<AutomovilDto>>(automovilesVendidos);
            return Ok(automovilesVendidosDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarAutomovil(int id, ActualizarAutomovilDto dto)
        {
            var automovil = _mapper.Map<Automovil>(dto);
            var automovilActualizado = await _automovilService.ActualizarAutomovil(id, automovil);
            return Ok(_mapper.Map<AutomovilDto>(automovilActualizado));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarAutomovil(int id)
        {
            await _automovilService.EliminarAutomovil(id);
            return NoContent();
        }
    }
}
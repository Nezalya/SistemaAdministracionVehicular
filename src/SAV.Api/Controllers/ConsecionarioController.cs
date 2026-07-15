namespace SAV.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConcesionarioController : ControllerBase
    {
        private readonly IConcesionarioService _consesionarioService;
        private readonly IMapper _mapper;
        public ConcesionarioController(IConcesionarioService consesionarioService, IMapper mapper)
        { 
            _consesionarioService = consesionarioService; 
            _mapper = mapper; 
        }

        [HttpGet]
        public async Task<IActionResult> ObtenenerConsecionarios()
        {
            var concesionarios = await _consesionarioService.ObtenerElConcesionario();
            var concesionariosDto = _mapper.Map<IEnumerable<ConcesionarioDto>>(concesionarios);
            return Ok(concesionariosDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerConsesionario(int id)
        {
            var concesionario = await _consesionarioService.ObtenerConcesionarioPorId(id);
            if (concesionario is null) return NotFound();
            var concesionarioDto = _mapper.Map<ConcesionarioDto>(concesionario);
            return Ok(concesionarioDto);
        }

        [HttpPost]
        public async Task<IActionResult> CrearConcesionario(GuardarConcesionarioDto concesionarioDto)
        {
            var concesionario = _mapper.Map<Concesionario>(concesionarioDto);
            var concesionarioCreado = await _consesionarioService.CrearConcesionario(concesionario);
            return CreatedAtAction(nameof(ObtenerConsesionario), new { id = concesionarioCreado.Id }, _mapper.Map<ConcesionarioDto>(concesionarioCreado));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarConcesionario(int id, GuardarConcesionarioDto concesionarioDto)
        {
            var concesionario = _mapper.Map<Concesionario>(concesionarioDto);
            var concesionarioActualizado = await _consesionarioService.ActualizarConcesionario(id, concesionario);
            return Ok(_mapper.Map<ConcesionarioDto>(concesionarioActualizado));
        }
    }
}
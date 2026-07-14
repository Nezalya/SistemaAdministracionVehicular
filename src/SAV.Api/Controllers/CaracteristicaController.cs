namespace SAV.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CaracteristicaController : ControllerBase
    {
        private readonly ICaracteristicaService _caracteristicaService;
        private readonly IMapper _mapper;
        public CaracteristicaController(ICaracteristicaService caracteristicaService, IMapper mapper)
        { 
          _caracteristicaService = caracteristicaService; 
          _mapper = mapper; 
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerCaracteristicas()
        {
            var caracteristicas = await _caracteristicaService.ObtenerTodasLasCaracteristicas();
            var caracteristicasDto = _mapper.Map<IEnumerable<CaracteristicaDto>>(caracteristicas);
            return Ok(caracteristicasDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var caracteristica = await _caracteristicaService.ObtenerCaracteristicaPorId(id);
            var caracteristicaDto = _mapper.Map<CaracteristicaDto>(caracteristica);
            return Ok(caracteristicaDto);
        }
    }
}
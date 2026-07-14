namespace SAV.Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarcaController : ControllerBase
    {
        private readonly IMarcaService _marcaService;
        private readonly IMapper _mapper;
        public MarcaController(IMarcaService marcaService, IMapper mapper)
        { 
            _marcaService = marcaService;
            _mapper = mapper; 
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerMarcas()
        { 
            var marcas= await _marcaService.ObtenerTodasLasMarcas();
            var marcasDto = _mapper.Map<IEnumerable<MarcaDto>>(marcas);
            return Ok(marcasDto);
        }

            
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var marca = await _marcaService.ObtenerMarcaPorId(id);
            var marcaDto = _mapper.Map<MarcaDto>(marca);
            return Ok(marcaDto);
        }
    }
}
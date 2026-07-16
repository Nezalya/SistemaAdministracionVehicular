

namespace SAV.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModeloController : ControllerBase
    {
        private readonly IModeloService _modeloService;
        private readonly IMapper _mapper;
        public ModeloController(IModeloService modeloService, IMapper mapper)
        { 
          _modeloService = modeloService; 
          _mapper = mapper; 
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerModelos() { 
            var modelos = await _modeloService.ObtenerTodosLosModelos();
            var modelosDto = _mapper.Map<IEnumerable<ModeloDto>>(modelos);
            return Ok(modelosDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerModelosPorId(int id)
        {
            var modelo = await _modeloService.ObtenerModeloPorId(id);
            var modeloDto = _mapper.Map<ModeloDto>(modelo);
            return Ok(modeloDto);
        }
    }
}
using Microsoft.AspNetCore.Authorization;

namespace SAV.Api.Controllers
{
    [Authorize(Roles = Roles.AdminOVendedor)]
    [ApiController]
    [Route("api/[controller]")]
    public class AutomovilController : ControllerBase
    {
        private readonly IAutomovilService _automovilService;
        private readonly IMapper _mapper;
        public AutomovilController(IAutomovilService automovilService, IMapper mapper)
        { 
            _automovilService = automovilService; 
            _mapper = mapper; 
        }


        [HttpGet]
        public async Task<IActionResult> ObtenerAutomoviles([FromQuery] FiltroAutomovilDto filtro)
        {
            var automoviles = await _automovilService.BuscarAutomoviles(filtro);
            return Ok(automoviles.Map<Automovil, AutomovilDto>(_mapper));
        }

        [HttpGet("disponibles")]
        public async Task<IActionResult> ObtenerAutomovilesDisponibles()
        {
            var automovilesDisponibles = await _automovilService.ObtenerAutomovilesDisponibles();
            var automovilesDisponiblesDto = _mapper.Map<IEnumerable<AutomovilDto>>(automovilesDisponibles);
            return Ok(automovilesDisponiblesDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerAutomovilPorId(int id)
        {
            var automovil= await _automovilService.ObtenerAutomovilPorId(id);
            var automovilDto = _mapper.Map<AutomovilDto>(automovil);
            return Ok(automovilDto);

        }

        [Authorize(Roles = Roles.Administrador)]
        [HttpPost]
        public async Task<IActionResult> RegistrarAutomovil(RegistrarAutomovilDto automovilDto)
        {
            var automovil = _mapper.Map<Automovil>(automovilDto);
            var automovilCreado = await _automovilService.RegistrarAutomovil(automovil);
            return CreatedAtAction(nameof(ObtenerAutomovilPorId), new { id = automovilCreado.Id }, _mapper.Map<AutomovilDto>(automovilCreado));
        }

        [HttpGet("vendidos")]
        public async Task<IActionResult> ObtenerAutomovilesVendidos()
        {
            var automovilesVendidos = await _automovilService.ObtenerAutomovilesVendidos();
            var automovilesVendidosDto = _mapper.Map<IEnumerable<AutomovilDto>>(automovilesVendidos);
            return Ok(automovilesVendidosDto);
        }


        [Authorize(Roles = Roles.Administrador)]
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarAutomovil(int id, ActualizarAutomovilDto dto)
        {
            var automovil = _mapper.Map<Automovil>(dto);
            var automovilActualizado = await _automovilService.ActualizarAutomovil(id, automovil);
            return Ok(_mapper.Map<AutomovilDto>(automovilActualizado));
        }

        [Authorize(Roles = Roles.Administrador)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarAutomovil(int id)
        {
            await _automovilService.EliminarAutomovil(id);
            return NoContent();
        }

        [HttpGet("resumen-stock")]
        public async Task<IActionResult> ObtenerResumenStock()
        {
            var stockResumen = await _automovilService.ObtenerResumenStock();
            return Ok(stockResumen);
        }

        [Authorize(Roles = Roles.Administrador)]
        [HttpPost("lote")]
        public async Task<IActionResult> RegistrarLoteDeAutomoviles(GenerarAutomovilesLoteDto dto)
        {
            var total = await _automovilService.RegistrarLoteDeAutomoviles(
                dto.ModeloId, dto.CantidadPorColor, dto.Colores,
                dto.AnoFabricacion, dto.ConcesionarioUbicacionId, dto.ServicioOficialUbicacionId);

            return Ok(new GenerarAutomovilesLoteResultDto
            {
                Total = total,
                Mensaje = $"Se registraron {total} automóviles del modelo {dto.ModeloId}."
            });
        }

    }
}
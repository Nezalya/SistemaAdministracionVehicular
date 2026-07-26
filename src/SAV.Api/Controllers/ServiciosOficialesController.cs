using Microsoft.AspNetCore.Authorization;
using SAV.Api.Authorization;

namespace SAV.Api.Controllers
{

    [Authorize(Roles = Roles.Administrador)]
    [ApiController]
    [Route("api/[controller]")]
    public class ServiciosOficialesController : ControllerBase
    {
        private readonly IServicioOficialService _servicioOficialService;
        private readonly IMapper _mapper;
        public ServiciosOficialesController(IServicioOficialService servicioOficialService, IMapper mapper)
        { 
            _servicioOficialService = servicioOficialService; 
            _mapper = mapper; 
        }

        [Authorize(Roles = Roles.AdminOVendedor)]
        [HttpGet]
        public async Task<IActionResult> ObtenerServiciosOficiales()
        {
            var serviciosOficiales = await _servicioOficialService.ObtenerTodosLosServiciosOficiales();
            var serviciosOficialesDto = _mapper.Map<IEnumerable<ServicioOficialDto>>(serviciosOficiales);
            return Ok(serviciosOficialesDto);
        }

        [Authorize(Roles = Roles.AdminOVendedor)]
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerServicioOficialPorId(int id)
        {
            var servicioOficial = await _servicioOficialService.ObtenerServicioOficialPorId(id);
            var servicioOficialDto = _mapper.Map<ServicioOficialDto>(servicioOficial);
            return Ok(servicioOficialDto);
        }

        [HttpPost]
        public async Task<IActionResult> CrearServicioOficial(GuardarServicioOficialDto servicioOficialDto)
        {
            var servicioOficial = _mapper.Map<ServicioOficial>(servicioOficialDto);
            var servicioOficialCreado = await _servicioOficialService.CrearServicioOficial(servicioOficial);
            return CreatedAtAction(nameof(ObtenerServicioOficialPorId), new { id = servicioOficialCreado.Id }, _mapper.Map<ServicioOficialDto>(servicioOficialCreado));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarServicioOficial(int id, GuardarServicioOficialDto servicioOficialDto)
        {
            var servicioOficial = _mapper.Map<ServicioOficial>(servicioOficialDto);
            var servicioOficialActualizado = await _servicioOficialService.ActualizarServicioOficial(id, servicioOficial);
            return Ok(_mapper.Map<ServicioOficialDto>(servicioOficialActualizado));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarServicioOficial(int id)
        {
            await _servicioOficialService.EliminarServicioOficial(id);
            return NoContent();
        }
    }
}
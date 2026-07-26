using Microsoft.AspNetCore.Authorization;
using SAV.Api.Authorization;

namespace SAV.Api.Controllers
{
    [Authorize(Roles = Roles.Administrador)]
    [ApiController]
    [Route("api/[controller]")]
    public class ReportesController : ControllerBase
    {
        private readonly IReporteService _reporteService;

        public ReportesController(IReporteService reporteService)
        {
            _reporteService = reporteService;
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> ObtenerDashboard()
        {
            var dashboard = await _reporteService.ObtenerDashboard();
            return Ok(dashboard);
        }
    }
}
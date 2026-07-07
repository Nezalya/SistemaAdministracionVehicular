using Microsoft.AspNetCore.Mvc;
using SAV.Core.Interfaces.Services;

namespace SAV.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FabricaController : ControllerBase
    {
        private readonly IFabricaImportService _importService;

        public FabricaController(IFabricaImportService importService)
        {
            _importService = importService;
        }

        [HttpPost("importar")]
        public async Task<IActionResult> Importar()
        {
            var resultado = await _importService.Importar();
            return Ok(new { mensaje = resultado });
        }
    }
}
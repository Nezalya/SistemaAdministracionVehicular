using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace SAV.Api.Controllers
{
    [Authorize(Roles = Roles.Administrador)]
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
            return Ok(new ImportacionFabricaDto { Mensaje = resultado });
        }
    }
}
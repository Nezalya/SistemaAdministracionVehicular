using Microsoft.AspNetCore.Authorization;
using SAV.Api.Authorization;

namespace SAV.Api.Controllers
{
    [Authorize(Roles = Roles.AdminOVendedor)]
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;
        public ClienteController(IClienteService clienteService, IMapper mapper)
        {
            _clienteService = clienteService; 
            _mapper = mapper; 
        }

        /// <summary>Listado paginado. Admite búsqueda por nombre, NIF, correo o teléfono.</summary>
        [HttpGet]
        public async Task<IActionResult> ObtenerClientes([FromQuery] FiltroClienteDto filtro)
        {
            var clientes = await _clienteService.BuscarClientes(filtro);
            return Ok(clientes.Map<Cliente, ClienteDto>(_mapper));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerClientePorId(int id)
        {
            var cliente = await _clienteService.ObtenerClientePorId(id);
            if (cliente is null) return NotFound();

            var clienteDto = _mapper.Map<ClienteDto>(cliente);
            return Ok(clienteDto);
        }

        [HttpPost]
        public async Task<IActionResult> CrearCliente(GuardarClienteDto clienteDto)
        {
            var cliente = _mapper.Map<Cliente>(clienteDto);
            cliente.FechaRegistro = DateTime.Now;
            var clienteCreado = await _clienteService.CrearCliente(cliente);
            return CreatedAtAction(nameof(ObtenerClientePorId), new { id = clienteCreado.Id }, _mapper.Map<ClienteDto>(clienteCreado));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarCliente(int id, GuardarClienteDto clienteDto)
        {
            var cliente = _mapper.Map<Cliente>(clienteDto);
            var clienteActualizado = await _clienteService.ActualizarCliente(id, cliente);
            return Ok(_mapper.Map<ClienteDto>(clienteActualizado));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCliente(int id)
        {
            await _clienteService.EliminarCliente(id);
            return NoContent();
        }

        [HttpGet("{id}/resumen-compras")]
        public async Task<IActionResult> ObtenerResumenCompras(int id)
        {
            var resumen = await _clienteService.ObtenerResumenCompras(id);
            return Ok(resumen);
        }
    }
}
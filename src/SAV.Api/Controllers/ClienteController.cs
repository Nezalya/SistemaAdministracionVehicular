namespace SAV.Api.Controllers
{
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

        [HttpGet]
        public async Task<IActionResult> ObtenerClientes()
        {
            var clientes = await _clienteService.ObtenerTodosLosClientes();
            var clientesDto = _mapper.Map<IEnumerable<ClienteDto>>(clientes);
            return Ok(clientesDto);
        }
            

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerCliente(int id)
        {
            var cliente = await _clienteService.ObtenerClientePorId(id);
            var clienteDto = _mapper.Map<ClienteDto>(cliente);
            return Ok(clienteDto);
        }

        [HttpPost]
        public async Task<IActionResult> CrearCliente(GuardarClienteDto clienteDto)
        {
            var cliente = _mapper.Map<Cliente>(clienteDto);
            var fechaRegistro = cliente.FechaRegistro = DateTime.Now;
            var clienteCreado = await _clienteService.CrearCliente(cliente);
            return CreatedAtAction(nameof(ObtenerCliente), new { id = clienteCreado.Id }, _mapper.Map<ClienteDto>(clienteCreado));//revisar luego
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
    }
}
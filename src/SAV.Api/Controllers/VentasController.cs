using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using SAV.Api.Dtos;
using SAV.Core.Entities.Ventas;
using SAV.Core.Interfaces.Services;

namespace SAV.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentasController : ControllerBase
    {
        private readonly IVentaService _service;

        public VentasController(IVentaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _service.ObtenerTodasLasVentas();
            var dtos = items.Select(v => new VentaDto(v.Id, v.AutomovilId, v.ClienteId, v.VendedorId, v.PrecioCobrado, v.FormaPago, v.FechaVenta, v.Origen));
            return Ok(dtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.ObtenerVentaPorId(id);
            if (item is null) return NotFound();
            return Ok(new VentaDto(item.Id, item.AutomovilId, item.ClienteId, item.VendedorId, item.PrecioCobrado, item.FormaPago, item.FechaVenta, item.Origen));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVentaDto dto)
        {
            var entity = new Venta
            {
                AutomovilId = dto.AutomovilId,
                ClienteId = dto.ClienteId,
                VendedorId = dto.VendedorId,
                PrecioCobrado = dto.PrecioCobrado,
                FormaPago = dto.FormaPago,
                FechaVenta = dto.FechaVenta,
                Origen = dto.Origen
            };

            var creado = await _service.CrearVenta(entity, dto.CaracteristicaIdsExtras ?? new List<int>());
            return CreatedAtAction(nameof(GetById), new { id = creado.Id }, new VentaDto(creado.Id, creado.AutomovilId, creado.ClienteId, creado.VendedorId, creado.PrecioCobrado, creado.FormaPago, creado.FechaVenta, creado.Origen));
        }
    }
}

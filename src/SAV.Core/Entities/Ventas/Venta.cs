using SAV.Core.Entities.Inventario;
using SAV.Core.Entities.RedDeDistribuciónYPersonal;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAV.Core.Entities.Ventas
{
    public class Venta
    {
        public int Id { get; set; }
        public int AutomovilId { get; set; }
        public int ClienteId { get; set; }
        public int? VendedorId { get; set; }
        public int? ServicioOficialId { get; set; }
        public int? PedidoFabricaId { get; set; }
        public decimal PrecioCobrado { get; set; }
        public FormaPago FormaPago { get; set; }
        public string? Financiera { get; set; }
        public DateTime FechaVenta { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public string? Matricula { get; set; }
        public OrigenVenta Origen { get; set; }

        public Automovil Automovil { get; set; } = null!;
        public Cliente Cliente { get; set; } = null!;
        public Vendedor? Vendedor { get; set; }
        public ServicioOficial? ServicioOficial { get; set; }
        public PedidoFabrica? PedidoFabrica { get; set; }
        public ICollection<VentaExtra> Extras { get; set; } = new List<VentaExtra>();
    }
}

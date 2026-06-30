using SAV.Core.Entities.RedDeDistribuciónYPersonal;
using SAV.Core.Entities.Ventas;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAV.Core.Entities.PedidosAFabrica
{
    public class PedidoFabrica
    {
        public int Id { get; set; }
        public int ConcesionarioId { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime FechaEstimadaEntrega { get; set; }
        public EstadoPedido Estado { get; set; }
        public decimal TotalPedido { get; set; } //No en el modelo de dominio

        public Concesionario Concesionario { get; set; } = null!;
        public ICollection<ArticuloPedido> Articulos { get; set; } = new List<ArticuloPedido>();
        public ICollection<Venta> VentasOriginadas { get; set; } = new List<Venta>();
    }
}

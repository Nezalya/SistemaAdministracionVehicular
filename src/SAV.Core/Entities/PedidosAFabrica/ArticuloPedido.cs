using SAV.Core.Entities.Catalogo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAV.Core.Entities.PedidosAFabrica
{
    public class ArticuloPedido
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public TipoArticuloPedido TipoArticulo { get; set; }
        public int? ModeloId { get; set; }
        public int? CaracteristicaId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }

        public PedidoFabrica Pedido { get; set; } = null!;
        public Modelo? Modelo { get; set; }
        public Caracteristica? Caracteristica { get; set; }
    }
}

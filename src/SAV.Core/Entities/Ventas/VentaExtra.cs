using SAV.Core.Entities.Catalogo;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAV.Core.Entities.Ventas
{
    public class VentaExtra
    {
        public int VentaId { get; set; }
        public int CaracteristicaId { get; set; }
        public decimal PrecioCobrado { get; set; }

        public Venta Venta { get; set; } = null!;
        public Caracteristica Caracteristica { get; set; } = null!;
    }
}

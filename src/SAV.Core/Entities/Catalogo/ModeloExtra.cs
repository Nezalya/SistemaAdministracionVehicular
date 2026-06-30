using System;
using System.Collections.Generic;
using System.Text;

namespace SAV.Core.Entities.Catalogo
{
    public class ModeloExtra
    {
        public int ModeloId { get; set; }
        public int CaracteristicaId { get; set; }
        public decimal PrecioExtra { get; set; }

        public Modelo Modelo { get; set; } = null!;
        public Caracteristica Caracteristica { get; set; } = null!;
    }
}

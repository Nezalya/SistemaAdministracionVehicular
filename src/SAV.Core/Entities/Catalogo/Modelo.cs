using System;
using System.Collections.Generic;
using System.Text;

namespace SAV.Core.Entities.Catalogo
{
    public class Modelo
    {
        public int Id { get; set; }
        public int MarcaId { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal PrecioBase { get; set; }
        public decimal? Descuento { get; set; }
        public string? PotenciaFiscal { get; set; }
        public string? Cilindrada { get; set; }
        public string? Combustible { get; set; } //No en el modelo de dominio
        public string? Transmision { get; set; } //No en el modelo de dominio

        public Marca Marca { get; set; } = null!;
        public ICollection<ModeloSerie> EquipamientoDeSerie { get; set; } = new List<ModeloSerie>();
        public ICollection<ModeloExtra> ExtrasDisponibles { get; set; } = new List<ModeloExtra>();
        public ICollection<Automovil> Automoviles { get; set; } = new List<Automovil>();
        public ICollection<ArticuloPedido> ArticulosPedido { get; set; } = new List<ArticuloPedido>();
    }
}

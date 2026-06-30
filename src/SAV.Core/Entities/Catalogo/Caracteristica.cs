namespace SAV.Core.Entities.Catalogo
{
    public class Caracteristica
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public string? Categoria { get; set; }

        public ICollection<ModeloSerie> ModelosDeSerie { get; set; } = new List<ModeloSerie>();
        public ICollection<ModeloExtra> ModelosExtra { get; set; } = new List<ModeloExtra>();
        public ICollection<VentaExtra> VentasExtra { get; set; } = new List<VentaExtra>();
        public ICollection<ArticuloPedido> ArticulosPedido { get; set; } = new List<ArticuloPedido>();
    }
}

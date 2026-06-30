namespace SAV.Core.Entities.Catalogo
{
    public class ModeloSerie
    {
        public int ModeloId { get; set; }
        public int CaracteristicaId { get; set; }

        public Modelo Modelo { get; set; } = null!;
        public Caracteristica Caracteristica { get; set; } = null!;
    }
}

namespace SAV.Core.Entities.Catalogo
{
    public class Marca
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? PaisOrigen { get; set; }

        public ICollection<Modelo> Modelos { get; set; } = new List<Modelo>();
    }
}

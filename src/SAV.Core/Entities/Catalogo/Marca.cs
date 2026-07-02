namespace SAV.Core.Entities.Catalogo
{
    public class Marca
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;//Es null pero tendra un valor
        public string? PaisOrigen { get; set; }//Puede ser null

        public ICollection<Modelo> Modelos { get; set; } = new List<Modelo>();
    }
}

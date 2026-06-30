namespace SAV.Core.Entities.RedDeDistribuciónYPersonal
{
    public class Vendedor
    {
        public int Id { get; set; }
        public int ConcesionarioId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Nif { get; set; } = null!;
        public string? Domicilio { get; set; }
        public string? Telefono { get; set; } 
        public string? Email { get; set; } 
     
        public Concesionario Concesionario { get; set; } = null!;
        public ICollection<Venta> Ventas { get; set; } = new List<Venta>();
        public Usuario? Usuario { get; set; }
    }
}

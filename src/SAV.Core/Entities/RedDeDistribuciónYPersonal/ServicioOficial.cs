namespace SAV.Core.Entities.RedDeDistribuciónYPersonal
{
    public class ServicioOficial
    {
        public int Id { get; set; }
        public int ConcesionarioId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Nif { get; set; } = null!;
        public string? Domicilio { get; set; }
        public string? Telefono { get; set; } 

        public Concesionario Concesionario { get; set; } = null!;
        public ICollection<Automovil> AutomovilesUbicados { get; set; } = new List<Automovil>();
        public ICollection<Venta> Ventas { get; set; } = new List<Venta>();
    }
}

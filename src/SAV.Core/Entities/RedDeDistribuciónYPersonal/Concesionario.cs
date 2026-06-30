namespace SAV.Core.Entities.RedDeDistribuciónYPersonal
{
    public class Concesionario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Nif { get; set; } = null!;
        public string? Domicilio { get; set; }
        public string? Telefono { get; set; } //No en el modelo de dominio
        public string? Email { get; set; } //No en el modelo de dominio

        public ICollection<ServicioOficial> ServiciosOficiales { get; set; } = new List<ServicioOficial>();
        public ICollection<Vendedor> Vendedores { get; set; } = new List<Vendedor>();
        public ICollection<Automovil> AutomovilesPropios { get; set; } = new List<Automovil>();
        public ICollection<Automovil> AutomovilesUbicados { get; set; } = new List<Automovil>();
        public ICollection<PedidoFabrica> Pedidos { get; set; } = new List<PedidoFabrica>();
        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}

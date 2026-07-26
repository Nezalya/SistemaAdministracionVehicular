namespace SAV.Core.Entities.Seguridad
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!; 
        public RolUsuario Rol { get; set; }
        public int? VendedorId { get; set; }
        public int? ConcesionarioId { get; set; }
        public bool Activo { get; set; }
        public string Email { get; set; } = null!;
        public DateTime FechaRegistro { get; set; }

        public Vendedor? Vendedor { get; set; }
        public Concesionario? Concesionario { get; set; }
        public int? ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
    }
}

using SAV.Core.Entities.RedDeDistribuciónYPersonal;
using System;
using System.Collections.Generic;
using System.Text;

namespace SAV.Core.Entities.Seguridad
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!; //No en el modelo dominio
        public RolUsuario Rol { get; set; }
        public int? VendedorId { get; set; }
        public int? ConcesionarioId { get; set; }
        public bool Activo { get; set; } //No en el modelo dominio
        public DateTime? UltimoAcceso { get; set; } //No en el modelo dominio

        public Vendedor? Vendedor { get; set; }
        public Concesionario? Concesionario { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SAV.Core.Entities.RedDeDistribuciónYPersonal
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Nif { get; set; } = null!;
        public string? Domicilio { get; set; }
        public string? Telefono { get; set; } 
        public string? Email { get; set; } 
        public DateTime FechaRegistro { get; set; }
        public Usuario? Usuario { get; set; }

        public ICollection<Venta> Ventas { get; set; } = new List<Venta>();
    }
}

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
        public string? Telefono { get; set; } //No en el modelo de dominio
        public string? Email { get; set; } //No en el modelo de dominio
        public DateTime FechaRegistro { get; set; } //No en el modelo de dominio

        public ICollection<Venta> Ventas { get; set; } = new List<Venta>();
    }
}

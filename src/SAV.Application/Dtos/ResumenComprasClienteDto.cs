namespace SAV.Application.DTOs;


    public class ResumenComprasClienteDto
    {
        public int ClienteId { get; set; }
        public string ClienteNombre { get; set; } = null!;
        public int TotalCompras { get; set; }
        public decimal MontoTotal { get; set; }
    }

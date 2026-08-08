namespace SAV.Contracts.Dtos;


    public class ResumenVentasVendedorDto
    {
        public int VendedorId { get; set; }
        public string VendedorNombre { get; set; } = null!;
        public int TotalVentas { get; set; }
        public decimal MontoTotal { get; set; }
    }

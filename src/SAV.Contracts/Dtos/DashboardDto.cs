namespace SAV.Contracts.Dtos;


    public class DashboardDto
    {
        public InventarioResumenDto Inventario { get; set; } = new();
        public VentasResumenDto Ventas { get; set; } = new();
        public EntidadesResumenDto Entidades { get; set; } = new();
    }

    public class InventarioResumenDto
    {
        public int TotalAutos { get; set; }
        public int Disponibles { get; set; }
        public int Vendidos { get; set; }
    }

    public class VentasResumenDto
    {
        public int TotalVentas { get; set; }
        public decimal IngresosTotales { get; set; }
    }

    public class EntidadesResumenDto
    {
        public int TotalClientes { get; set; }
        public int TotalVendedores { get; set; }
        public int TotalServiciosOficiales { get; set; }
    }

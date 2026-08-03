namespace SAV.Application.DTOs;


public class ResumenStockDto
{
    public int TotalDisponibles { get; set; }
    public List<StockPorMarcaDto> PorMarca { get; set; } = new();
}

public class StockPorMarcaDto
{
    public string Marca { get; set; } = null!;
    public int Cantidad { get; set; }
    public List<StockPorModeloDto> Modelos { get; set; } = new();
}

public class StockPorModeloDto
{
    public string Modelo { get; set; } = null!;
    public int Cantidad { get; set; }
}
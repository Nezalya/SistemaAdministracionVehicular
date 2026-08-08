namespace SAV.Contracts.Common;

public class PagedResult<T>
{
    public IReadOnlyList<T> Items { get; init; } = [];
    public int TotalCount { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
}

public class PagedRequest
{
    /// <summary>Tope de seguridad: impide que un cliente pida páginas arbitrariamente grandes.</summary>
    public const int TamanoMaximoPagina = 100;
    public const int TamanoPaginaPorDefecto = 20;

    private readonly int _page = 1;
    private readonly int _pageSize = TamanoPaginaPorDefecto;

    public int Page
    {
        get => _page;
        init => _page = value < 1 ? 1 : value;
    }

    public int PageSize
    {
        get => _pageSize;
        init => _pageSize = value < 1
            ? TamanoPaginaPorDefecto
            : Math.Min(value, TamanoMaximoPagina);
    }

    public string? SortBy { get; init; }
    public bool SortDescending { get; init; }
}

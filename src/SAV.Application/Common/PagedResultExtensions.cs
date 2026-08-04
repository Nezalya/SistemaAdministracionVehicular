using AutoMapper;

namespace SAV.Application.Common;

public static class PagedResultExtensions
{
    public static PagedResult<TDestino> Map<TOrigen, TDestino>(
        this PagedResult<TOrigen> source,
        IMapper mapper)
    {
        return new PagedResult<TDestino>
        {
            Items = mapper.Map<List<TDestino>>(source.Items),
            TotalCount = source.TotalCount,
            Page = source.Page,
            PageSize = source.PageSize
        };
    }
}
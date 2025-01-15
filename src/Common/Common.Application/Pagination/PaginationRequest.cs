using Microsoft.AspNetCore.Mvc;

namespace Common.Application.Pagination;

public class PaginationRequest
{
    [FromQuery(Name = "pageNumber")]
    public int PageNumber { get; set; }

    [FromQuery(Name = "pageSize")]
    public int PageSize { get; set; }
    public int Skip => (PageNumber - 1) * PageSize;
    public int Take => PageSize;
}

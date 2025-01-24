using Microsoft.AspNetCore.Mvc;

namespace Common.Endpoints.Pagination;

public record PaginationRequest
{
    [FromQuery(Name = "pageNumber")]
    public int PageNumber { get; set; }

    [FromQuery(Name = "pageSize")]
    public int PageSize { get; set; }
}

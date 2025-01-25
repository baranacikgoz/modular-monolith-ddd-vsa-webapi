using Microsoft.AspNetCore.Mvc;

namespace Common.Endpoints.Pagination;

public record PaginationRequest
{
    [FromQuery]
    public int PageNumber { get; set; }

    [FromQuery]
    public int PageSize { get; set; }
}

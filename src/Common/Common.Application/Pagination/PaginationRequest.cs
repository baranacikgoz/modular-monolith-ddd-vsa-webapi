using Microsoft.AspNetCore.Mvc;

namespace Common.Application.Pagination;

public abstract record PaginationRequest
{
    [FromQuery] public required int PageNumber { get; init; }

    [FromQuery] public required int PageSize { get; init; }

    public int Skip => (PageNumber - 1) * PageSize;
    public int Take => PageSize;
}

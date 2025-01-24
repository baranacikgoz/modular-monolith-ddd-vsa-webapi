namespace Common.Application.Queries.Pagination;

public record PaginationQuery
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public int Skip => (PageNumber - 1) * PageSize;
    public int Take => PageSize;
}

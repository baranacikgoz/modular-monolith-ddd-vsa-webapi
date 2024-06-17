namespace Common.Application.Pagination;

public record PaginationRequest(int PageNumber, int PageSize)
{
    public int Skip => (PageNumber - 1) * PageSize;
}

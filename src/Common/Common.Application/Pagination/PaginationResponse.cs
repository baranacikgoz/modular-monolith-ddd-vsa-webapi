namespace Common.Application.Pagination;

public record PaginationResponse<T>(ICollection<T> Data, int TotalCount, int PageNumber, int PageSize)
{
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPrevious => PageNumber > 1;
    public bool HasNext => PageNumber < TotalPages;
    public int NextPageNumber => HasNext ? PageNumber + 1 : TotalPages;
    public int PreviousPageNumber => HasPrevious ? PageNumber - 1 : 1;
}

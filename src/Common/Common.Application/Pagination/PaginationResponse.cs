namespace Common.Application.Pagination;

/// <param name="TotalCount"><c>-1</c> when the caller asked for <c>IncludeTotal=false</c> (no count query ran).</param>
/// <param name="NextCursor">
///     Keyset cursor for the page after this one; null when this page was not full (no further rows) or the query gave
///     no tiebreaker (keyset pagination unavailable).
/// </param>
public record PaginationResponse<T>(ICollection<T> Data, int TotalCount, int PageNumber, int PageSize, string? NextCursor = null)
{
    public bool HasTotal => TotalCount >= 0;
    public int TotalPages => HasTotal ? (int)Math.Ceiling(TotalCount / (double)PageSize) : -1;
    public bool HasPrevious => PageNumber > 1;
    public bool HasNext => NextCursor is not null || (HasTotal && PageNumber < TotalPages);
    public int NextPageNumber => HasTotal && PageNumber < TotalPages ? PageNumber + 1 : Math.Max(PageNumber, TotalPages);
    public int PreviousPageNumber => HasPrevious ? PageNumber - 1 : 1;
}

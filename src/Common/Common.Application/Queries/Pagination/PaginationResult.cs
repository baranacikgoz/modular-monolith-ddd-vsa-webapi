using Common.Domain.ResultMonad;

namespace Common.Application.Queries.Pagination;

public record PaginationResult<T>(ICollection<T> Data, int TotalCount, int PageNumber, int PageSize) : IResult
{
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPrevious => PageNumber > 1;
    public bool HasNext => PageNumber < TotalPages;
    public int NextPageNumber => HasNext ? PageNumber + 1 : TotalPages;
    public int PreviousPageNumber => HasPrevious ? PageNumber - 1 : 1;

    // IResult interface is required for MediatR Validation Pipeline, do not remove.
    public Error? Error { get => default; set => throw new NotImplementedException(); }
}

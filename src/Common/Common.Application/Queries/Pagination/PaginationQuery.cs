using System.Linq.Expressions;
using Common.Domain.Entities;

namespace Common.Application.Queries.Pagination;

public record PaginationQuery<TEntity, TDto> where TEntity : IAuditableEntity
{
    public required int PageNumber { get; init; }
    public required int PageSize { get; init; }
    public int Skip => (PageNumber - 1) * PageSize;
    public int Take => PageSize;


    public required Expression<Func<TEntity, TDto>> Selector { get; init; }
    public Expression<Func<TEntity, bool>>? EnsureOwnership { get; init; }
    public required Func<TEntity, object>? OrderBy { get; init; }
    public required Func<TEntity, object>? OrderByDescending { get; init; }
    public Func<TEntity, object>? ThenBy { get; init; }
    public Func<TEntity, object>? ThenByDescending { get; init; }
}

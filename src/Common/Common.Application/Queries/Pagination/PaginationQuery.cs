using System.Linq.Expressions;
using Common.Domain.Entities;

namespace Common.Application.Queries.Pagination;

public record PaginationQuery<TEntity> where TEntity : IAuditableEntity
{
    public required int PageNumber { get; init; }
    public required int PageSize { get; init; }
    public int Skip => (PageNumber - 1) * PageSize;
    public int Take => PageSize;

    public Expression<Func<TEntity, bool>>? EnsureOwnership { get; init; }
    public required Expression<Func<TEntity, object>>? OrderBy { get; init; }
    public required Expression<Func<TEntity, object>>? OrderByDescending { get; init; }
    public Expression<Func<TEntity, object>>? ThenBy { get; init; }
    public Expression<Func<TEntity, object>>? ThenByDescending { get; init; }
}

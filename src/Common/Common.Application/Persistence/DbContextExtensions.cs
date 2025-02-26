

using Common.Application.Queries.Pagination;
using Common.Domain.Entities;
using Common.Domain.ResultMonad;
using Microsoft.EntityFrameworkCore;

namespace Common.Application.Persistence;

public static class DbContextExtensions
{
    public static IQueryable<TEntity> WhereIf<TEntity>(this IQueryable<TEntity> query, Expression<Func<TEntity, bool>> predicate, bool condition)
    {
        if (condition)
        {
            query = query.Where(predicate);
        }

        return query;
    }

    public static async Task<Result<TEntity>> SingleAsResultAsync<TEntity>(this IQueryable<TEntity> queryable, CancellationToken cancellationToken)
        => await Result<TEntity>.CreateAsync(
            taskToAwaitValue: () => queryable.SingleOrDefaultAsync(cancellationToken),
            errorIfValueNull: Error.NotFound(typeof(TEntity).Name));

    public static async Task<Result<TEntity>> FirstAsResultAsync<TEntity>(this IQueryable<TEntity> queryable, CancellationToken cancellationToken)
        => await Result<TEntity>.CreateAsync(
            taskToAwaitValue: () => queryable.FirstOrDefaultAsync(cancellationToken),
            errorIfValueNull: Error.NotFound(typeof(TEntity).Name));

    public static async Task<PaginationResult<TDto>> PaginateAsync<TEntity, TDto>(this IQueryable<TEntity> queryable, PaginationQuery<TEntity, TDto> paginationQuery, CancellationToken cancellationToken)
        where TEntity : IAuditableEntity
    {
        queryable = queryable
            .Skip(paginationQuery.Skip)
            .Take(paginationQuery.Take);

        IOrderedQueryable<TEntity> orderedQueryable;
        if (paginationQuery.OrderByDescending is not null)
        {
            orderedQueryable = queryable.OrderByDescending<TEntity, object>(paginationQuery.OrderByDescending);

            if (paginationQuery.ThenByDescending is not null)
            {
                orderedQueryable = orderedQueryable.ThenByDescending<TEntity, object>(paginationQuery.ThenByDescending);
                goto ExecuteQuery;
            }

            if (paginationQuery.ThenBy is not null)
            {
                orderedQueryable = orderedQueryable.ThenBy<TEntity, object>(paginationQuery.ThenBy);
                goto ExecuteQuery;
            }
        }

        if (paginationQuery.OrderBy is not null)
        {
            orderedQueryable = queryable.OrderBy(paginationQuery.OrderBy);

            if (paginationQuery.ThenByDescending is not null)
            {
                orderedQueryable = orderedQueryable.ThenByDescending<TEntity, object>(paginationQuery.ThenByDescending);
                goto ExecuteQuery;
            }

            if (paginationQuery.ThenBy is not null)
            {
                orderedQueryable = orderedQueryable.ThenBy<TEntity, object>(paginationQuery.ThenBy);
                goto ExecuteQuery;
            }
        }

    ExecuteQuery:

        var totalCount = orderedQueryable.CountAsync(cancellationToken);
        var data = await orderedQueryable.Select(paginationQuery.Selector).ToListAsync(cancellationToken);
    }
}

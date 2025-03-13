using System.Linq.Expressions;
using Common.Domain.Entities;
using Common.Domain.ResultMonad;
using Microsoft.EntityFrameworkCore;

namespace Common.Application.Queries.Pagination;

public static class DbContextExtensions
{
    public static async Task<PaginationResult<TDto>> PaginateAsync<TEntity, TDto>(
        this IQueryable<TEntity> queryable,
        PaginationQuery<TEntity, TDto> paginationQuery,
        CancellationToken cancellationToken)
        where TEntity : IAuditableEntity
    {
        if (paginationQuery.EnsureOwnership is not null)
        {
            queryable = queryable.Where(paginationQuery.EnsureOwnership);
        }

        // Apply ordering
        IOrderedQueryable<TEntity>? orderedQueryable = null;

        if (paginationQuery.OrderByDescending is not null)
        {
            orderedQueryable = queryable.OrderByDescending(paginationQuery.OrderByDescending);
        }
        else if (paginationQuery.OrderBy is not null)
        {
            orderedQueryable = queryable.OrderBy(paginationQuery.OrderBy);
        }

        if (orderedQueryable is not null)
        {
            if (paginationQuery.ThenByDescending is not null)
            {
                orderedQueryable = orderedQueryable.ThenByDescending(paginationQuery.ThenByDescending);
            }
            else if (paginationQuery.ThenBy is not null)
            {
                orderedQueryable = orderedQueryable.ThenBy(paginationQuery.ThenBy);
            }
        }
        else
        {
            orderedQueryable = queryable.OrderByDescending(e => e.CreatedOn); // Fallback to a default ordering
        }

        var totalCount = await queryable.CountAsync(cancellationToken);

        var data = await orderedQueryable
            .Skip(paginationQuery.Skip)
            .Take(paginationQuery.Take)
            .Select(paginationQuery.Selector)
            .ToListAsync(cancellationToken);

        return new PaginationResult<TDto>(data, totalCount, paginationQuery.PageNumber, data.Count);
    }
}

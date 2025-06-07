using System.Linq.Expressions;
using Common.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Common.Application.Pagination;

public static class DbContextExtensions
{
    public static async Task<PaginationResponse<TDto>> PaginateAsync<TEntity, TDto>(
        this IQueryable<TEntity> queryable,
        Expression<Func<TEntity, TDto>> selector,
        PaginationRequest request,
        Expression<Func<TEntity, object>>? orderBy = null,
        Expression<Func<TEntity, object>>? orderByDescending = null,
        Expression<Func<TEntity, object>>? thenBy = null,
        Expression<Func<TEntity, object>>? thenByDescending = null,
        CancellationToken cancellationToken = default)
        where TEntity : IAuditableEntity
    {
        // Apply ordering
        IOrderedQueryable<TEntity>? orderedQueryable = null;

        if (orderByDescending is not null)
        {
            orderedQueryable = queryable.OrderByDescending(orderByDescending);
        }
        else if (orderBy is not null)
        {
            orderedQueryable = queryable.OrderBy(orderBy);
        }

        if (orderedQueryable is not null)
        {
            if (thenByDescending is not null)
            {
                orderedQueryable = orderedQueryable.ThenByDescending(thenByDescending);
            }
            else if (thenBy is not null)
            {
                orderedQueryable = orderedQueryable.ThenBy(thenBy);
            }
        }
        else
        {
            orderedQueryable = queryable.OrderByDescending(e => e.CreatedOn); // Fallback to a default ordering
        }

        var totalCount = await queryable.CountAsync(cancellationToken);

        var data = await orderedQueryable
            .Skip(request.Skip)
            .Take(request.Take)
            .Select(selector)
            .ToListAsync(cancellationToken);

        return new PaginationResponse<TDto>(data, totalCount, request.PageNumber, data.Count);
    }
}

using System.Linq.Expressions;
using Common.Domain.ResultMonad;
using Microsoft.EntityFrameworkCore;

namespace Common.Application.Persistence;

public static class DbContextExtensions
{
    public static IQueryable<TEntity> WhereIf<TEntity>(this IQueryable<TEntity> query,
        Expression<Func<TEntity, bool>> predicate, bool condition)
    {
        if (condition)
        {
            query = query.Where(predicate);
        }

        return query;
    }

    public static async Task<Result<TEntity>> SingleAsResultAsync<TEntity>(this IQueryable<TEntity> queryable,
        CancellationToken cancellationToken)
    {
        return await Result<TEntity>.CreateAsync(
            () => queryable.SingleOrDefaultAsync(cancellationToken),
            Error.NotFound(typeof(TEntity).Name));
    }

    public static async Task<Result<TEntity>> FirstAsResultAsync<TEntity>(this IQueryable<TEntity> queryable,
        CancellationToken cancellationToken)
    {
        return await Result<TEntity>.CreateAsync(
            () => queryable.FirstOrDefaultAsync(cancellationToken),
            Error.NotFound(typeof(TEntity).Name));
    }

    public static async Task<Result<bool>> AnyAsResultAsync<TEntity>(this IQueryable<TEntity> queryable,
        CancellationToken cancellationToken)
    {
        return await Result<bool>.CreateAsync(
            () => queryable.AnyAsync(cancellationToken));
    }

    public static IQueryable<TEntity> TagWith<TEntity>(this IQueryable<TEntity> queryable, params object[] parameters)
    {
        return parameters.Length switch
        {
            1 => queryable.TagWith(tag: parameters[0].ToString() ?? string.Empty),
            2 => queryable.TagWith(tag: $"({parameters[0]})-({parameters[1]})"),
            3 => queryable.TagWith(tag: $"({parameters[0]})-({parameters[1]})-({parameters[2]})"),
            _ => queryable.TagWith(tag: parameters.Aggregate(
                string.Empty,
                (acc, next) => acc + $"({next})-",
                acc => acc[..^1]))
        };
    }
}

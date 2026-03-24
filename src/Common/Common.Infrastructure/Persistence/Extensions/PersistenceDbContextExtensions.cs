using System.Linq.Expressions;
using Common.Domain.ResultMonad;
using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure.Persistence.Extensions;

public static class PersistenceQueryableExtensions
{
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query,
        Expression<Func<T, bool>> predicate, bool condition)
    {
        if (condition)
        {
            query = query.Where(predicate);
        }

        return query;
    }

    public static async Task<Result<T>> SingleAsResultAsync<T>(
        this IQueryable<T> queryable,
        string resourceName,
        CancellationToken cancellationToken)
    {
        return await Result<T>.CreateAsync(
            () => queryable.SingleOrDefaultAsync(cancellationToken),
            Error.NotFound(resourceName));
    }

    public static async Task<Result<T>> FirstAsResultAsync<T>(
        this IQueryable<T> queryable,
        string resourceName,
        CancellationToken cancellationToken)
    {
        return await Result<T>.CreateAsync(
            () => queryable.FirstOrDefaultAsync(cancellationToken),
            Error.NotFound(resourceName));
    }

    public static async Task<Result<bool>> AnyAsResultAsync<T>(this IQueryable<T> queryable,
        CancellationToken cancellationToken)
    {
        return await Result<bool>.CreateAsync(() => queryable.AnyAsync(cancellationToken));
    }

    public static IQueryable<T> TagWith<T>(this IQueryable<T> queryable, params object[] parameters)
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

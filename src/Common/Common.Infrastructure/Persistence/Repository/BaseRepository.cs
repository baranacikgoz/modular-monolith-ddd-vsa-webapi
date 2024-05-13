using System.Linq.Expressions;
using System.Threading;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Common.Application.Auth;
using Common.Application.Persistence;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure.Persistence.Repository;

/// <summary>
/// Tried to implement the same as It is the same as: https://github.com/ardalis/Specification/blob/main/Specification.EntityFrameworkCore/src/Ardalis.Specification.EntityFrameworkCore/RepositoryBaseOfT.cs
/// Except for do not call SaveChanges here, and encourage to use IUnitOfWork
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="dbContext"></param>
/// <param name="currentUser"></param>
/// <param name="specificationEvaluator"></param>
public class BaseRepository<T>(
    DbContext dbContext,
    ICurrentUser currentUser
    ) : IRepository<T>
    where T : class
{
    private readonly SpecificationEvaluator _specificationEvaluator = SpecificationEvaluator.Default;
    public void Add(T entity) => dbContext.Set<T>().Add(entity);

    public void AddRange(IEnumerable<T> entities) => dbContext.Set<T>().AddRange(entities);

    public async Task<bool> AnyAsync(ISpecification<T> specification, CancellationToken cancellationToken)
        => await ApplySpecification(specification, true)
                .AnyAsync(cancellationToken)
                .ConfigureAwait(false);

    public IAsyncEnumerable<T> AsAsyncEnumerable(ISpecification<T> specification)
        => ApplySpecification(specification)
          .AsAsyncEnumerable();

    public Task<int> CountAsync(ISpecification<T> specification, CancellationToken cancellationToken)
        => ApplySpecification(specification, true)
          .CountAsync(cancellationToken);

    public void Delete(T entity) => dbContext.Set<T>().Remove(entity);

    public void DeleteRange(IEnumerable<T> entities) => dbContext.Set<T>().RemoveRange(entities);

    public async Task<Result> EnsureOwnedByCurrentUserAsync(Expression<Func<T, ApplicationUserId>> idSelector, CancellationToken cancellationToken)
    {
        var isOwnedBy = await IsOwnedByCurrentUserAync(idSelector, cancellationToken);
        return isOwnedBy ? Result.Success : Error.NotOwned(nameof(T), currentUser.Id);
    }

    public async Task<Result<T>> FirstOrDefaultAsResultAsync(ISpecification<T> specification, CancellationToken cancellationToken)
        => await Result<T>.CreateAsync(
            taskToAwaitValue: async () => await FirstOrDefaultAsync(specification, cancellationToken),
            errorIfValueNull: Error.NotFound(nameof(T)));

    public async Task<Result<TResult>> FirstOrDefaultAsResultAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken)
        => await Result<TResult>.CreateAsync(
            taskToAwaitValue: async () => await FirstOrDefaultAsync(specification, cancellationToken),
            errorIfValueNull: Error.NotFound(nameof(T)));

    public async Task<T?> FirstOrDefaultAsync(ISpecification<T> specification, CancellationToken cancellationToken)
        => await ApplySpecification(specification)
                .FirstOrDefaultAsync(cancellationToken);

    public async Task<TResult?> FirstOrDefaultAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken)
        => await ApplySpecification(specification)
                .FirstOrDefaultAsync(cancellationToken);

    public Task<bool> IsOwnedByCurrentUserAync(Expression<Func<T, ApplicationUserId>> idSelector, CancellationToken cancellationToken)
        => dbContext.Set<T>().AnyAsync(BuildIsOwnedByPredicate(idSelector, currentUser.Id), cancellationToken);

    public async Task<List<T>> ListAsync(ISpecification<T> specification, CancellationToken cancellationToken)
    {
        var queryResult = await ApplySpecification(specification).ToListAsync(cancellationToken);

        return specification.PostProcessingAction is null ? queryResult : specification.PostProcessingAction(queryResult).ToList();
    }

    public async Task<List<TResult>> ListAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken)
    {
        var queryResult = await ApplySpecification(specification).ToListAsync(cancellationToken);

        return specification.PostProcessingAction is null ? queryResult : specification.PostProcessingAction(queryResult).ToList();
    }

    public async Task<Result<T>> SingleOrDefaultAsResultAsync(ISingleResultSpecification<T> specification, CancellationToken cancellationToken)
        => await Result<T>.CreateAsync(
            taskToAwaitValue: async () => await SingleOrDefaultAsync(specification, cancellationToken),
            errorIfValueNull: Error.NotFound(nameof(T)));

    public async Task<Result<TResult>> SingleOrDefaultAsResultAsync<TResult>(ISingleResultSpecification<T, TResult> specification, CancellationToken cancellationToken)
        => await Result<TResult>.CreateAsync(
            taskToAwaitValue: async () => await SingleOrDefaultAsync(specification, cancellationToken),
            errorIfValueNull: Error.NotFound(nameof(T)));

    public async Task<T?> SingleOrDefaultAsync(ISingleResultSpecification<T> specification, CancellationToken cancellationToken)
        => await ApplySpecification(specification)
                .SingleOrDefaultAsync(cancellationToken);

    public async Task<TResult?> SingleOrDefaultAsync<TResult>(ISingleResultSpecification<T, TResult> specification, CancellationToken cancellationToken)
        => await ApplySpecification(specification)
                .SingleOrDefaultAsync(cancellationToken);

    /// <summary>
    /// It is the same as: https://github.com/ardalis/Specification/blob/main/Specification.EntityFrameworkCore/src/Ardalis.Specification.EntityFrameworkCore/RepositoryBaseOfT.cs
    /// </summary>
    /// <param name="specification"></param>
    /// <param name="evaluateCriteriaOnly"></param>
    /// <returns></returns>
    private IQueryable<T> ApplySpecification(ISpecification<T> specification, bool evaluateCriteriaOnly = false)
        => _specificationEvaluator.GetQuery(dbContext.Set<T>().AsQueryable(), specification, evaluateCriteriaOnly);

    /// <summary>
    /// It is the same as: https://github.com/ardalis/Specification/blob/main/Specification.EntityFrameworkCore/src/Ardalis.Specification.EntityFrameworkCore/RepositoryBaseOfT.cs
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="specification"></param>
    /// <returns></returns>
    private IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> specification)
        => _specificationEvaluator.GetQuery(dbContext.Set<T>().AsQueryable(), specification);

    private static Expression<Func<T, bool>> BuildIsOwnedByPredicate(
        Expression<Func<T, ApplicationUserId>> idSelector,
        ApplicationUserId id)
    {
        // Get the property expression and the constant value to compare against
        var parameterExp = idSelector.Parameters[0];
        var equalityExp = Expression.Equal(idSelector.Body, Expression.Constant(id, typeof(ApplicationUserId)));

        // Build and return the lambda expression
        return Expression.Lambda<Func<T, bool>>(equalityExp, parameterExp);
    }
}

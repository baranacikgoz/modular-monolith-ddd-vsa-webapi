using System.Linq.Expressions;
using Ardalis.Specification;
using Common.Application.DTOs;
using Common.Application.Pagination;
using Common.Domain.Entities;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;

namespace Common.Application.Persistence;
public interface IRepository<T>
    where T : class, IAuditableEntity
{
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    void Delete(T entity);
    void DeleteRange(IEnumerable<T> entities);
    Task<Result<T>> FirstOrDefaultAsResultAsync(ISpecification<T> specification, CancellationToken cancellationToken);
    Task<Result<TResult>> FirstOrDefaultAsResultAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken);
    Task<T?> FirstOrDefaultAsync(ISpecification<T> specification, CancellationToken cancellationToken);
    Task<Result<T>> SingleOrDefaultAsResultAsync(ISingleResultSpecification<T> specification, CancellationToken cancellationToken);
    Task<Result<TResult>> SingleOrDefaultAsResultAsync<TResult>(ISingleResultSpecification<T, TResult> specification, CancellationToken cancellationToken);
    Task<T?> SingleOrDefaultAsync(ISingleResultSpecification<T> specification, CancellationToken cancellationToken);
    Task<TResult?> SingleOrDefaultAsync<TResult>(ISingleResultSpecification<T, TResult> specification, CancellationToken cancellationToken);
    Task<List<T>> ListAsync(ISpecification<T> specification, CancellationToken cancellationToken);
    Task<List<TResult>> ListAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken);
    Task<PaginationResult<T>> PaginateAsync(PaginationSpec<T> paginationSpec, CancellationToken cancellationToken);
    Task<PaginationResult<TResult>> PaginateAsync<TResult>(PaginationSpec<T, TResult> paginationSpec, CancellationToken cancellationToken);
    Task<PaginationResult<EventDto>> GetEventHistoryAsync<TId>(TId id, PaginationRequest request, CancellationToken cancellationToken)
        where TId : IStronglyTypedId;
    Task<int> CountAsync(ISpecification<T> specification, CancellationToken cancellationToken);
    Task<Result<int>> CountAsyncAsResult(ISpecification<T> specification, CancellationToken cancellationToken);
    Task<bool> AnyAsync(ISpecification<T> specification, CancellationToken cancellationToken);
    Task<Result<bool>> AnyAsyncAsResult(ISpecification<T> specification, CancellationToken cancellationToken);
    IAsyncEnumerable<T> AsAsyncEnumerable(ISpecification<T> specification);
    Task<bool> IsOwnedByCurrentUserAync(Expression<Func<T, ApplicationUserId>> idSelector, CancellationToken cancellationToken);
    Task<Result> EnsureOwnedByCurrentUserAsync(Expression<Func<T, ApplicationUserId>> idSelector, CancellationToken cancellationToken);
}

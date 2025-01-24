using System.Linq.Expressions;
using System.Text.Json;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Common.Application.Auth;
using Common.Application.DTOs;
using Common.Application.Persistence;
using Common.Application.Queries.EventHistory;
using Common.Application.Queries.Pagination;
using Common.Domain.Aggregates;
using Common.Domain.Entities;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Common.Infrastructure.Persistence.Repository;

/// <summary>
/// Tried to implement the same as It is the same as: https://github.com/ardalis/Specification/blob/main/Specification.EntityFrameworkCore/src/Ardalis.Specification.EntityFrameworkCore/RepositoryBaseOfT.cs
/// Except for do not call SaveChanges here, and encourage to use IUnitOfWork
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="dbContext"></param>
/// <param name="currentUser"></param>
/// <param name="specificationEvaluator"></param>
public class BaseRepository<T>(DbContext dbContext) : IRepository<T>
    where T : class, IAuditableEntity
{
    private readonly SpecificationEvaluator _specificationEvaluator = SpecificationEvaluator.Default;
    public void Add(T entity) => dbContext.Set<T>().Add(entity);

    public void AddRange(IEnumerable<T> entities) => dbContext.Set<T>().AddRange(entities);

    public async Task<bool> AnyAsync(ISpecification<T> specification, CancellationToken cancellationToken)
        => await ApplySpecification(specification, true)
                .AnyAsync(cancellationToken);

    public async Task<Result<bool>> AnyAsyncAsResult(ISpecification<T> specification, CancellationToken cancellationToken)
        => await Result<bool>.CreateAsync(
            taskToAwaitValue: async () => await AnyAsync(specification, cancellationToken));

    public IAsyncEnumerable<T> AsAsyncEnumerable(ISpecification<T> specification)
        => ApplySpecification(specification)
          .AsAsyncEnumerable();

    public Task<int> CountAsync(ISpecification<T> specification, CancellationToken cancellationToken)
        => ApplySpecification(specification, true)
          .CountAsync(cancellationToken);

    public Task<Result<int>> CountAsyncAsResult(ISpecification<T> specification, CancellationToken cancellationToken)
        => Result<int>.CreateAsync(
            taskToAwaitValue: async () => await CountAsync(specification, cancellationToken));

    public void Delete(T entity) => dbContext.Set<T>().Remove(entity);

    public void DeleteRange(IEnumerable<T> entities) => dbContext.Set<T>().RemoveRange(entities);

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

    public async Task<PaginationResult<T>> PaginateAsync(PaginationSpec<T> paginationSpec, CancellationToken cancellationToken)
    {
        var queryResult = await ApplySpecification(paginationSpec).ToListAsync(cancellationToken);
        var totalCount = await CountAsync(paginationSpec, cancellationToken);

        return paginationSpec.PostProcessingAction is null
            ? new PaginationResult<T>(queryResult, totalCount, paginationSpec.PaginationQuery.PageNumber, paginationSpec.PaginationQuery.PageSize)
            : new PaginationResult<T>(paginationSpec.PostProcessingAction(queryResult).ToList(), totalCount, paginationSpec.PaginationQuery.PageNumber, paginationSpec.PaginationQuery.PageSize);
    }

    public async Task<PaginationResult<TResult>> PaginateAsync<TResult>(PaginationSpec<T, TResult> paginationSpec, CancellationToken cancellationToken)
    {
        var queryResult = await ApplySpecification(paginationSpec).ToListAsync(cancellationToken);
        var totalCount = await CountAsync(paginationSpec, cancellationToken);

        return paginationSpec.PostProcessingAction is null
            ? new PaginationResult<TResult>(queryResult, totalCount, paginationSpec.PaginationQuery.PageNumber, paginationSpec.PaginationQuery.PageSize)
            : new PaginationResult<TResult>(paginationSpec.PostProcessingAction(queryResult).ToList(), totalCount, paginationSpec.PaginationQuery.PageNumber, paginationSpec.PaginationQuery.PageSize);
    }

    public async Task<PaginationResult<EventDto>> GetEventHistoryAsync<TAggregate>(
    EventHistoryQuery<TAggregate> query,
    CancellationToken cancellationToken
        ) where TAggregate : class, IAggregateRoot
    {
        const string Query = @"
            SELECT
                ""Event"" AS ""Event"",
                ""CreatedBy"" AS ""CreatedBy"",
                COUNT(*) OVER() AS ""TotalCount""
            FROM
                ""Products"".""EventStoreEvents""
            WHERE
                ""AggregateId"" = @id AND ""AggregateType"" = @aggregateType
            ORDER BY 
                ""CreatedOn"" DESC
            OFFSET @Skip
            LIMIT @Take;
        ";

        var results = await dbContext
        .Database
        .SqlQueryRaw<PaginatedEventDto>(Query,
            new NpgsqlParameter("@id", query.AggregateId.Value),
            new NpgsqlParameter("@aggregateType", typeof(T).Name),
            new NpgsqlParameter("@Skip", query.Skip),
            new NpgsqlParameter("@Take", query.PageSize))
        .ToListAsync(cancellationToken);

        if (results.Count == 0)
        {
            return new PaginationResult<EventDto>([], 0, query.PageNumber, query.PageSize);
        }

        var totalCount = results[0].TotalCount;
        var eventDtos = results.Select(x => new EventDto(x.Event, x.CreatedBy)).ToList();

        return new PaginationResult<EventDto>(eventDtos, totalCount, query.PageNumber, query.PageSize);
    }

    private sealed record PaginatedEventDto(JsonElement Event, DefaultIdType CreatedBy, int TotalCount);

    public async Task<Result<T>> SingleOrDefaultAsResultAsync(ISingleResultSpecification<T> specification, CancellationToken cancellationToken)
        => await Result<T>.CreateAsync(
            taskToAwaitValue: async () => await SingleOrDefaultAsync(specification, cancellationToken),
            errorIfValueNull: Error.NotFound(typeof(T).Name));

    public async Task<Result<TResult>> SingleOrDefaultAsResultAsync<TResult>(ISingleResultSpecification<T, TResult> specification, CancellationToken cancellationToken)
        => await Result<TResult>.CreateAsync(
            taskToAwaitValue: async () => await SingleOrDefaultAsync(specification, cancellationToken),
            errorIfValueNull: Error.NotFound(typeof(T).Name));

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
}

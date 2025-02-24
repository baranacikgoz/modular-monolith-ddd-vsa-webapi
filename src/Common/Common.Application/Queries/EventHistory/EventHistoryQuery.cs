using Common.Application.CQS;
using Common.Application.DTOs;
using Common.Application.Persistence;
using Common.Application.Queries.Pagination;
using Common.Domain.Aggregates;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;

namespace Common.Application.Queries.EventHistory;

#pragma warning disable S2326 // Unused type parameters should be removed

public abstract record EventHistoryQuery<TAggregate> : PaginationQuery, IQuery<PaginationResult<EventDto>>
    where TAggregate : class, IAggregateRoot
{
    public required IStronglyTypedId AggregateId { get; init; }
}

public abstract class EventHistoryQueryHandler<TAggregate>(IRepository<TAggregate> repository) : IQueryHandler<EventHistoryQuery<TAggregate>, PaginationResult<EventDto>>
    where TAggregate : class, IAggregateRoot
{
    public async Task<Result<PaginationResult<EventDto>>> Handle(EventHistoryQuery<TAggregate> query, CancellationToken cancellationToken)
        => await repository.GetEventHistoryAsync(query, cancellationToken);
}

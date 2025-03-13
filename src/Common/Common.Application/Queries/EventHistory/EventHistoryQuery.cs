using Common.Application.CQS;
using Common.Application.DTOs;
using Common.Application.Queries.Pagination;
using Common.Domain.Aggregates;
using Common.Domain.StronglyTypedIds;

namespace Common.Application.Queries.EventHistory;

#pragma warning disable S2326 // Unused type parameters should be removed

public abstract record EventHistoryQuery<TAggregate> : PaginationQuery<TAggregate, EventDto>, IQuery<PaginationResult<EventDto>>
    where TAggregate : class, IAggregateRoot
{
    public required IStronglyTypedId AggregateId { get; init; }
}

#pragma warning restore #pragma warning disable S2326

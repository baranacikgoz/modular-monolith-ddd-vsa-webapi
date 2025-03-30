using Common.Application.CQS;
using Common.Application.DTOs;
using Common.Application.Queries.Pagination;
using Common.Domain.Aggregates;
using Common.Domain.StronglyTypedIds;

namespace Common.Application.Queries.EventHistory;

#pragma warning disable S2326 // Unused type parameters should be removed

public abstract record EventHistoryQuery<TAggregate> : IQuery<PaginationResult<EventDto>>
    where TAggregate : class, IAggregateRoot
{
    public required IStronglyTypedId AggregateId { get; init; }
    public required int PageNumber { get; init; }
    public required int PageSize { get; init; }
    public int Skip => (PageNumber - 1) * PageSize;
    public int Take => PageSize;

}

#pragma warning restore S2326 // Unused type parameters should be removed

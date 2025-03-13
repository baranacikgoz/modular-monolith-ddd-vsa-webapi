using Common.Application.CQS;
using Common.Application.DTOs;
using Common.Application.Persistence;
using Common.Application.Queries.Pagination;
using Common.Domain.Aggregates;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;

namespace Common.Application.Queries.EventHistory;

public abstract class EventHistoryQueryHandler<TAggregate>(BaseDbContext dbContext) : IQueryHandler<EventHistoryQuery<TAggregate>, PaginationResult<EventDto>>
    where TAggregate : class, IAggregateRoot
{
    public required IStronglyTypedId AggregateId { get; init; }

    public async Task<Result<PaginationResult<EventDto>>> Handle(EventHistoryQuery<TAggregate> request,
        CancellationToken cancellationToken)
        => await dbContext
            .GetEventHistoryAsync(request, cancellationToken);
}

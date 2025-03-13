using Common.Application.DTOs;
using Common.Application.Localization;
using Common.Application.Queries.Pagination;
using Common.Domain.Aggregates;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Common.Application.Queries.EventHistory;

#pragma warning restore S2326 // Unused type parameters should be removed

public class EventHistoryQueryValidator<T, TAggregate>
    : PaginationQueryValidator<EventHistoryQuery<TAggregate>, TAggregate, EventDto>
    where T : PaginationQuery<TAggregate, EventDto>
    where TAggregate : class, IAggregateRoot
{
    public EventHistoryQueryValidator(IStringLocalizer<ResxLocalizer> localizer)
        : base(localizer)
    {
        RuleFor(x => x.AggregateId)
            .NotEmpty()
                .WithMessage(localizer["EventHistoryRequest.Id.NotEmpty"]);
    }
}

#pragma warning restore S2326 // Unused type parameters should be removed

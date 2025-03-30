using Common.Application.DTOs;
using Common.Application.Localization;
using Common.Application.Queries.Pagination;
using Common.Application.Validation;
using Common.Domain.Aggregates;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Common.Application.Queries.EventHistory;

#pragma warning disable S2326 // Unused type parameters should be removed

public class EventHistoryQueryValidator<T, TAggregate> : CustomValidator<T>
    where T : EventHistoryQuery<TAggregate>
    where TAggregate : class, IAggregateRoot
{
    private const int PageNumberGreaterThanOrEqualTo = 1;
    private const int PageSizeInclusiveMin = 1;
    private const int PageSizeInclusiveMax = 1000;
    public EventHistoryQueryValidator(IStringLocalizer<ResxLocalizer> localizer)
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(PageNumberGreaterThanOrEqualTo)
                .WithMessage(localizer["EventHistoryRequest.PageNumber.GreaterThanOrEqualTo {0}", PageNumberGreaterThanOrEqualTo]);

        RuleFor(x => x.PageSize)
            .InclusiveBetween(PageSizeInclusiveMin, PageSizeInclusiveMax)
                .WithMessage(localizer["EventHistoryRequest.PageSize.InclusiveBetween {0} {1}", PageSizeInclusiveMin, PageSizeInclusiveMax]);

        RuleFor(x => x.AggregateId)
            .NotEmpty()
                .WithMessage(localizer["EventHistoryRequest.Id.NotEmpty"]);
    }
}

#pragma warning restore S2326 // Unused type parameters should be removed

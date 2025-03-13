using Common.Application.Localization;
using Common.Application.Queries.EventHistory;
using Microsoft.Extensions.Localization;
using Products.Domain.Stores;

namespace Products.Application.Stores.Features.History;

public sealed record GetStoreEventHistoryQuery : EventHistoryQuery<Store>;

public sealed class GetStoreEventHistoryQueryValidator(IStringLocalizer<ResxLocalizer> localizer)
    : EventHistoryQueryValidator<GetStoreEventHistoryQuery, Store>(localizer)
{
}

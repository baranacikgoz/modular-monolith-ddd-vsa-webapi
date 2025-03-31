using Common.Application.Localization;
using Common.Application.Queries.EventHistory;
using Microsoft.Extensions.Localization;
using Products.Application.Persistence;
using Products.Domain.Stores;

namespace Products.Application.Stores.Features.History;

public sealed record GetStoreEventHistoryQuery : EventHistoryQuery<Store, IProductsDbContext>;

public sealed class GetStoreEventHistoryQueryValidator(IStringLocalizer<ResxLocalizer> localizer)
    : EventHistoryQueryValidator<GetStoreEventHistoryQuery, Store, IProductsDbContext>(localizer)
{
}

using Common.Application.Localization;
using Common.Application.Queries.EventHistory;
using Microsoft.Extensions.Localization;
using Products.Domain.Products;

namespace Products.Application.Products.Features.History;

public sealed record GetProductEventHistoryQuery : EventHistoryQuery<Product>;

public sealed class GetProductEventHistoryQueryValidator(IStringLocalizer<ResxLocalizer> localizer) : EventHistoryQueryValidator<Product>(localizer)
{
}

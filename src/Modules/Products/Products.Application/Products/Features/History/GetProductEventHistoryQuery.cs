using Common.Application.Localization;
using Common.Application.Queries.EventHistory;
using Microsoft.Extensions.Localization;
using Products.Application.Persistence;
using Products.Domain.Products;

namespace Products.Application.Products.Features.History;

public sealed record GetProductEventHistoryQuery : EventHistoryQuery<Product, IProductsDbContext>;

public sealed class GetProductEventHistoryQueryValidator(IStringLocalizer<ResxLocalizer> localizer)
    : EventHistoryQueryValidator<GetProductEventHistoryQuery, Product, IProductsDbContext>(localizer)
{
}

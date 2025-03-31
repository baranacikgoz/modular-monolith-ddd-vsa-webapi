using Common.Application.Queries.EventHistory;
using Products.Application.Persistence;
using Products.Domain.Products;

namespace Products.Application.Products.Features.History;

public sealed class GetProductEventHistoryQueryHandler(IProductsDbContext dbContext) : EventHistoryQueryHandler<Product, IProductsDbContext>(dbContext)
{
}

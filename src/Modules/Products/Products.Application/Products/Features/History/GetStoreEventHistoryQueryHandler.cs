using Common.Application.Persistence;
using Common.Application.Queries.EventHistory;
using Products.Domain.Products;

namespace Products.Application.Products.Features.History;

public sealed class GetProductEventHistoryQueryHandler(IRepository<Product> repository) : EventHistoryQueryHandler<Product>(repository)
{
}

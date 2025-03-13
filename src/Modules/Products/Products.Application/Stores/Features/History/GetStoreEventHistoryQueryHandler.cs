using Common.Application.Persistence;
using Common.Application.Queries.EventHistory;
using Products.Application.Persistence;
using Products.Domain.Stores;

namespace Products.Application.Stores.Features.History;

public sealed class GetStoreEventHistoryQueryHandler(ProductsDbContext dbContext) : EventHistoryQueryHandler<Store>(dbContext)
{
}

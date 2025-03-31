using Common.Application.Queries.EventHistory;
using Products.Application.Persistence;
using Products.Domain.Stores;

namespace Products.Application.Stores.Features.History;

public sealed class GetStoreEventHistoryQueryHandler(IProductsDbContext dbContext) : EventHistoryQueryHandler<Store, IProductsDbContext>(dbContext)
{
}

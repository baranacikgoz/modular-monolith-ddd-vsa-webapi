using Common.Application.Persistence;
using Common.Application.Queries.EventHistory;
using Products.Domain.Stores;

namespace Products.Application.Stores.Features.History;

public sealed class GetStoreEventHistoryQueryHandler(IRepository<Store> repository) : EventHistoryQueryHandler<Store>(repository)
{
}

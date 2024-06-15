using Ardalis.Specification;
using Common.Domain.StronglyTypedIds;
using Inventory.Domain.Stores;

namespace Inventory.Application.Stores.Specs;
public class StoreByOwnerIdSpec : SingleResultSpecification<Store>
{
    public StoreByOwnerIdSpec(ApplicationUserId ownerId)
        => Query
            .Where(s => s.OwnerId == ownerId);
}

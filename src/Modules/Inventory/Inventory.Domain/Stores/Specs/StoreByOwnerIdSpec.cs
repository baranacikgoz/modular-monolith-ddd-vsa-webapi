using Ardalis.Specification;
using Common.Domain.StronglyTypedIds;

namespace Inventory.Domain.Stores.Specs;
public class StoreByOwnerIdSpec : SingleResultSpecification<Store>
{
    public StoreByOwnerIdSpec(ApplicationUserId ownerId)
        => Query
            .Where(s => s.OwnerId == ownerId);
}

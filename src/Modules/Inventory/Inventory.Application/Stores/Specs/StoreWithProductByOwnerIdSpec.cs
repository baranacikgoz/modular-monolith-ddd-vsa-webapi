using Ardalis.Specification;
using Common.Domain.StronglyTypedIds;
using Inventory.Domain.StoreProducts;
using Inventory.Domain.Stores;

namespace Inventory.Application.Stores.Specs;

public class StoreWithProductByOwnerIdSpec : SingleResultSpecification<Store>
{
    public StoreWithProductByOwnerIdSpec(ApplicationUserId ownerId, StoreProductId productId)
        => Query
            .Where(s => s.OwnerId == ownerId)
            .Include(s => s.Products
                            .Where(p => p.Id == productId));
}

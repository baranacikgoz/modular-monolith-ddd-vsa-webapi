using Ardalis.Specification;
using Common.Domain.StronglyTypedIds;
using Products.Domain.Stores;

namespace Products.Application.Stores.Specifications;

public sealed class OwnerIdByStoreIdSpec : SingleResultSpecification<Store, ApplicationUserId>
{
    public OwnerIdByStoreIdSpec(StoreId storeId)
        => Query
            .Select(x => x.OwnerId)
            .Where(x => x.Id == storeId);
}

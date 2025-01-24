using Ardalis.Specification;
using Common.Domain.StronglyTypedIds;
using Products.Domain.Stores;

namespace Products.Application.Stores.Specifications;

public sealed class StoreIdByOwnerIdSpec : SingleResultSpecification<Store, StoreId>
{
    public StoreIdByOwnerIdSpec(ApplicationUserId ownerId)
        => Query
            .Select(x => x.Id)
            .Where(x => x.OwnerId == ownerId);
}

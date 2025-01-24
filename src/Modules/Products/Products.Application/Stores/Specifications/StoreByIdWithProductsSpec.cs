using Ardalis.Specification;
using Products.Domain.Products;
using Products.Domain.Stores;

namespace Products.Application.Stores.Specifications;

public sealed class StoreByIdWithProductsSpec : SingleResultSpecification<Store>
{
    public StoreByIdWithProductsSpec(StoreId id, params ProductId[] productIds)
        => Query
            .Where(s => s.Id == id)
            .Include(s => s.Products.Where(p => productIds.Contains(p.Id)));
}

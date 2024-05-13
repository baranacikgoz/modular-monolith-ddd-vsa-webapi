using Ardalis.Specification;

namespace Inventory.Domain.StoreProducts;
public class StoreProductByIdSpec : SingleResultSpecification<StoreProduct>
{
    public StoreProductByIdSpec(StoreProductId id)
        => Query.Where(sp => sp.Id == id);
}

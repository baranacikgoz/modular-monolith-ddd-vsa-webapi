using Ardalis.Specification;

namespace Inventory.Domain.Products.Specs;
public class ProductByIdSpec : SingleResultSpecification<Product>
{
    public ProductByIdSpec(ProductId id)
        => Query.Where(p => p.Id == id);
}

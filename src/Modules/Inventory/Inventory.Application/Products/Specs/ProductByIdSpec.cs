using Ardalis.Specification;
using Inventory.Domain.Products;

namespace Inventory.Application.Products.Specs;
public class ProductByIdSpec : SingleResultSpecification<Product>
{
    public ProductByIdSpec(ProductId id)
        => Query.Where(p => p.Id == id);
}

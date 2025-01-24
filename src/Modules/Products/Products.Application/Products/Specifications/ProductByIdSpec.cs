using System.Linq.Expressions;
using Ardalis.Specification;
using Products.Domain.Products;

namespace Products.Application.Products.Specifications;

public sealed class ProductByIdSpec : SingleResultSpecification<Product>
{
    public ProductByIdSpec(ProductId id)
        => Query
            .Where(p => p.Id == id);
}

public sealed class ProductByIdSpec<TDto> : SingleResultSpecification<Product, TDto>
{
    public ProductByIdSpec(ProductId id, Expression<Func<Product, TDto>> selector)
        => Query
            .Select(selector)
            .Where(p => p.Id == id);
}

using System.Linq.Expressions;
using Ardalis.Specification;
using Products.Domain.ProductTemplates;

namespace Products.Application.ProductTemplates.Specifications;

public sealed class ProductTemplateByIdSpec : SingleResultSpecification<ProductTemplate>
{
    public ProductTemplateByIdSpec(ProductTemplateId id)
        => Query
            .Where(p => p.Id == id);
}

public sealed class ProductTemplateByIdSpec<TDto> : SingleResultSpecification<ProductTemplate, TDto>
{
    public ProductTemplateByIdSpec(ProductTemplateId id, Expression<Func<ProductTemplate, TDto>> selector)
        => Query
            .Select(selector)
            .Where(p => p.Id == id);
}

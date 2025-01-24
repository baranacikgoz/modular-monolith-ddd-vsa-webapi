using Ardalis.Specification;
using Products.Domain.ProductTemplates;

namespace Products.Application.ProductTemplates.Specifications;

public sealed class ActiveProductTemplateByIdSpec : SingleResultSpecification<ProductTemplate>
{
    public ActiveProductTemplateByIdSpec(ProductTemplateId id)
        => Query
            .Where(p => p.Id == id)
            .Where(p => p.IsActive);
}

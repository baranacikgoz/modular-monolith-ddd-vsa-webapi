using Ardalis.Specification;
using Common.Application.Queries.Pagination;
using Products.Application.ProductTemplates.DTOs;
using Products.Application.ProductTemplates.Features.Search;
using Products.Domain.ProductTemplates;

namespace Products.Application.ProductTemplates.Specifications;

public sealed class SearchProductTemplatesSpec : PaginationSpec<ProductTemplate, ProductTemplateDto>
{
    public SearchProductTemplatesSpec(SearchProductTemplatesQuery query)
        : base(query)
        => Query
            .Select(p => new ProductTemplateDto
            {
                Id = p.Id,
                Brand = p.Brand,
                Model = p.Model,
                Color = p.Color,
                CreatedBy = p.CreatedBy,
                CreatedOn = p.CreatedOn,
                LastModifiedBy = p.LastModifiedBy,
                LastModifiedOn = p.LastModifiedOn
            })
            .Search(s => s.Brand, $"%{query.Brand!}%", condition: query.Brand is not null)
            .Search(s => s.Model, $"%{query.Model!}%", condition: query.Model is not null)
            .Search(s => s.Color, $"%{query.Color!}%", condition: query.Color is not null);
}

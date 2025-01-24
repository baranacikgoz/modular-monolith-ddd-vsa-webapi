using Common.Application.Persistence;
using Common.Domain.ResultMonad;
using Products.Domain.ProductTemplates;
using Products.Application.ProductTemplates.DTOs;
using Products.Application.ProductTemplates.Specifications;
using Common.Application.Queries.Pagination;
using Common.Application.CQS;

namespace Products.Application.ProductTemplates.Features.Search;

public sealed class SearchProductTemplatesQueryHandler(IRepository<ProductTemplate> repository) : IQueryHandler<SearchProductTemplatesQuery, PaginationResult<ProductTemplateDto>>
{
    public async Task<Result<PaginationResult<ProductTemplateDto>>> Handle(SearchProductTemplatesQuery query, CancellationToken cancellationToken)
        => await repository.PaginateAsync(new SearchProductTemplatesSpec(query), cancellationToken);
}

using Common.Application.Persistence;
using Products.Domain.Products;
using Common.Domain.ResultMonad;
using Products.Application.Products.DTOs;
using Products.Application.Products.Specifications;
using Common.Application.Queries.Pagination;
using Common.Application.CQS;

namespace Products.Application.Products.Features.Search;

public sealed class SearchProductsQueryHandler(IRepository<Product> repository) : IQueryHandler<SearchProductsQuery, PaginationResult<ProductDto>>
{
    public async Task<Result<PaginationResult<ProductDto>>> Handle(SearchProductsQuery query, CancellationToken cancellationToken)
        => await repository.PaginateAsync(new SearchProductsSpec(query), cancellationToken);
}

using Common.Domain.ResultMonad;
using Common.Application.Queries.Pagination;
using Common.Application.CQS;
using Products.Application.Persistence;
using Microsoft.EntityFrameworkCore;
using Common.Application.Persistence;

namespace Products.Application.Products.Features.Search;

public sealed class SearchProductsQueryHandler<TDto>(ProductsDbContext dbContext) : IQueryHandler<SearchProductsQuery<TDto>, PaginationResult<TDto>>
{
    //public async Task<Result<PaginationResult<ProductDto>>> Handle(SearchProductsQuery query, CancellationToken cancellationToken)
    //    => await repository.PaginateAsync(new SearchProductsSpec(query), cancellationToken);

    public async Task<Result<PaginationResult<TDto>>> Handle(SearchProductsQuery<TDto> request, CancellationToken cancellationToken)
        => await dbContext
            .Products
            .AsNoTracking()
            .WhereIf(request.EnsureOwnership!, condition: request.EnsureOwnership is not null)
            .PaginateAsync<TDto>(request, cancellationToken);
}

using Common.Application.Persistence;
using Products.Domain.Stores;
using Common.Domain.ResultMonad;
using Products.Application.Stores.DTOs;
using Products.Application.Stores.Specifications;
using Common.Application.Queries.Pagination;
using Common.Application.CQS;
using Microsoft.EntityFrameworkCore;
using Products.Application.Persistence;

namespace Products.Application.Stores.Features.Search;

public sealed class SearchStoresQueryHandler(ProductsDbContext dbContext) : IQueryHandler<SearchStoresQuery, PaginationResult<StoreDto>>
{
    public async Task<Result<PaginationResult<StoreDto>>> Handle(SearchStoresQuery query, CancellationToken cancellationToken)
        // => await repository.PaginateAsync(new SearchStoresSpec(query), cancellationToken);
        => await dbContext
            .Stores
            .AsNoTracking()
            .TagWith(nameof(SearchStoresQuery))
            .
}

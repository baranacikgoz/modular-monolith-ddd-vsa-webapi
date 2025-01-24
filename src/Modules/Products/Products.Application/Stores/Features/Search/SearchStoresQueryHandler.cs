using Common.Application.Persistence;
using Products.Domain.Stores;
using Common.Domain.ResultMonad;
using Products.Application.Stores.DTOs;
using Products.Application.Stores.Specifications;
using Common.Application.Queries.Pagination;
using Common.Application.CQS;

namespace Products.Application.Stores.Features.Search;

public sealed class SearchStoresQueryHandler(IRepository<Store> repository) : IQueryHandler<SearchStoresQuery, PaginationResult<StoreDto>>
{
    public async Task<Result<PaginationResult<StoreDto>>> Handle(SearchStoresQuery query, CancellationToken cancellationToken)
        => await repository.PaginateAsync(new SearchStoresSpec(query), cancellationToken);
}

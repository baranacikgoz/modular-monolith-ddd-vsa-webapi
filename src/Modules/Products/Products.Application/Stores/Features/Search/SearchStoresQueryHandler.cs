using Common.Application.Persistence;
using Common.Domain.ResultMonad;
using Products.Application.Stores.DTOs;
using Common.Application.Queries.Pagination;
using Common.Application.CQS;
using Microsoft.EntityFrameworkCore;
using Products.Application.Persistence;

namespace Products.Application.Stores.Features.Search;

public sealed class SearchStoresQueryHandler(IProductsDbContext dbContext) : IQueryHandler<SearchStoresQuery, PaginationResult<StoreDto>>
{
    public async Task<Result<PaginationResult<StoreDto>>> Handle(SearchStoresQuery request, CancellationToken cancellationToken)
        => await dbContext
            .Stores
            .AsNoTracking()
            .TagWith(nameof(SearchStoresQuery))
            .WhereIf(request.EnsureOwnership!, condition: request.EnsureOwnership is not null)
            .WhereIf(s => EF.Functions.ILike(s.Name, $"%{request.Name}%"), condition: !string.IsNullOrWhiteSpace(request.Name))
            .WhereIf(s => EF.Functions.ILike(s.Description, $"%{request.Description}%"), condition: !string.IsNullOrWhiteSpace(request.Description))
            .WhereIf(s => EF.Functions.ILike(s.Address, $"%{request.Address}%"), condition: !string.IsNullOrWhiteSpace(request.Address))
            .PaginateAsync(
                paginationQuery: request,
                selector: s => new StoreDto
                {
                    Id = s.Id,
                    OwnerId = s.OwnerId,
                    Name = s.Name,
                    Description = s.Description,
                    Address = s.Address,
                    ProductCount = s.Products.Count,
                    CreatedBy = s.CreatedBy,
                    CreatedOn = s.CreatedOn,
                    LastModifiedBy = s.LastModifiedBy,
                    LastModifiedOn = s.LastModifiedOn
                },
                cancellationToken: cancellationToken);
}

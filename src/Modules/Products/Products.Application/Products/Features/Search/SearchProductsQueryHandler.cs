using Common.Domain.ResultMonad;
using Common.Application.Queries.Pagination;
using Common.Application.CQS;
using Products.Application.Persistence;
using Microsoft.EntityFrameworkCore;
using Common.Application.Persistence;
using Products.Application.Products.DTOs;

namespace Products.Application.Products.Features.Search;

public sealed class SearchProductsQueryHandler(IProductsDbContext dbContext) : IQueryHandler<SearchProductsQuery, PaginationResult<ProductDto>>
{
    public async Task<Result<PaginationResult<ProductDto>>> Handle(SearchProductsQuery request, CancellationToken cancellationToken)
        => await dbContext
            .Products
            .AsNoTracking()
            .TagWith(nameof(SearchProductsQuery))
            .WhereIf(request.EnsureOwnership!, condition: request.EnsureOwnership is not null)
            .WhereIf(p => p.StoreId == request.StoreId, condition: request.StoreId is not null)
            .WhereIf(p => EF.Functions.ILike(p.Name, $"%{request.Name}%"), condition: !string.IsNullOrWhiteSpace(request.Name))
            .WhereIf(p => EF.Functions.ILike(p.Description, $"%{request.Description}%"), condition: !string.IsNullOrWhiteSpace(request.Description))
            .WhereIf(p => p.Quantity >= request.MinQuantity!, condition: request.MinQuantity is not null)
            .WhereIf(p => p.Quantity <= request.MaxQuantity!, condition: request.MaxQuantity is not null)
            .WhereIf(p => p.Price >= request.MinPrice!, condition: request.MinPrice is not null)
            .WhereIf(p => p.Price <= request.MaxPrice!, condition: request.MaxPrice is not null)
            .PaginateAsync(
                paginationQuery: request,
                selector: p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Quantity = p.Quantity,
                    Price = p.Price,
                    CreatedBy = p.CreatedBy,
                    CreatedOn = p.CreatedOn,
                    LastModifiedBy = p.LastModifiedBy,
                    LastModifiedOn = p.LastModifiedOn
                },
                cancellationToken: cancellationToken);
}

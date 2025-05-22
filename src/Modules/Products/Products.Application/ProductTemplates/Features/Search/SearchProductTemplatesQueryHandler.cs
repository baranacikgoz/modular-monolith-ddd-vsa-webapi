using Common.Application.Persistence;
using Common.Domain.ResultMonad;
using Common.Application.CQS;
using Common.Application.Pagination;
using Products.Application.Persistence;
using Microsoft.EntityFrameworkCore;
using Products.Application.Products.Features.Search;
using Products.Application.ProductTemplates.DTOs;

namespace Products.Application.ProductTemplates.Features.Search;

public sealed class SearchProductTemplatesQueryHandler(IProductsDbContext dbContext) : IQueryHandler<SearchProductTemplatesRequest, PaginationResponse<ProductTemplateResponse>>
{
    public async Task<Result<PaginationResponse<ProductTemplateResponse>>> Handle(SearchProductTemplatesRequest request, CancellationToken cancellationToken)
        => await dbContext
            .ProductTemplates
            .AsNoTracking()
            .TagWith(nameof(SearchProductsRequest))
            .WhereIf(request.EnsureOwnership!, condition: request.EnsureOwnership is not null)
            .WhereIf(p => EF.Functions.ILike(p.Brand, $"%{request.Brand}%"), condition: !string.IsNullOrWhiteSpace(request.Brand))
            .WhereIf(p => EF.Functions.ILike(p.Model, $"%{request.Model}%"), condition: !string.IsNullOrWhiteSpace(request.Model))
            .WhereIf(p => EF.Functions.ILike(p.Color, $"%{request.Color}%"), condition: !string.IsNullOrWhiteSpace(request.Color))
            .PaginateAsync(
                request: request,
                selector: p => new ProductTemplateResponse
                {
                    Id = p.Id,
                    Brand = p.Brand,
                    Model = p.Model,
                    Color = p.Color,
                    CreatedBy = p.CreatedBy,
                    CreatedOn = p.CreatedOn,
                    LastModifiedBy = p.LastModifiedBy,
                    LastModifiedOn = p.LastModifiedOn,
                },
                cancellationToken: cancellationToken);

}

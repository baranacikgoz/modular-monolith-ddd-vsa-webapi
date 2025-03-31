using Common.Application.Persistence;
using Common.Domain.ResultMonad;
using Common.Application.Queries.Pagination;
using Common.Application.CQS;
using Products.Application.Persistence;
using Microsoft.EntityFrameworkCore;
using Products.Application.Products.Features.Search;
using Products.Application.ProductTemplates.DTOs;

namespace Products.Application.ProductTemplates.Features.Search;

public sealed class SearchProductTemplatesQueryHandler(IProductsDbContext dbContext) : IQueryHandler<SearchProductTemplatesQuery, PaginationResult<ProductTemplateDto>>
{
    public async Task<Result<PaginationResult<ProductTemplateDto>>> Handle(SearchProductTemplatesQuery request, CancellationToken cancellationToken)
        => await dbContext
            .ProductTemplates
            .AsNoTracking()
            .TagWith(nameof(SearchProductsQuery))
            .WhereIf(request.EnsureOwnership!, condition: request.EnsureOwnership is not null)
            .WhereIf(p => EF.Functions.ILike(p.Brand, $"%{request.Brand}%"), condition: !string.IsNullOrWhiteSpace(request.Brand))
            .WhereIf(p => EF.Functions.ILike(p.Model, $"%{request.Model}%"), condition: !string.IsNullOrWhiteSpace(request.Model))
            .WhereIf(p => EF.Functions.ILike(p.Color, $"%{request.Color}%"), condition: !string.IsNullOrWhiteSpace(request.Color))
            .PaginateAsync(
                paginationQuery: request,
                selector: p => new ProductTemplateDto
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

using Common.Domain.ResultMonad;
using Common.Application.Persistence;
using Common.Application.CQS;
using Microsoft.EntityFrameworkCore;
using Products.Application.Persistence;
using Products.Application.ProductTemplates.DTOs;

namespace Products.Application.ProductTemplates.Features.GetById;

public class GetProductTemplateByIdQueryHandler(ProductsDbContext dbContext) : IQueryHandler<GetProductTemplateByIdQuery, ProductTemplateDto>
{
    public async Task<Result<ProductTemplateDto>> Handle(GetProductTemplateByIdQuery request, CancellationToken cancellationToken)
        => await dbContext
            .ProductTemplates
            .AsNoTracking()
            .TagWith(nameof(GetProductTemplateByIdQuery), request.Id)
            .WhereIf(request.EnsureOwnership!, condition: request.EnsureOwnership is not null)
            .Select(pt => new ProductTemplateDto
            {
                Id = pt.Id,
                Brand = pt.Brand,
                Model = pt.Model,
                Color = pt.Color,
                CreatedBy = pt.CreatedBy,
                CreatedOn = pt.CreatedOn,
                LastModifiedBy = pt.LastModifiedBy,
                LastModifiedOn = pt.LastModifiedOn,
            })
            .SingleAsResultAsync(cancellationToken);
}

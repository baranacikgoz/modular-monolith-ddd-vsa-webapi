using Common.Application.CQS;
using Common.Application.Persistence;
using Common.Domain.ResultMonad;
using Microsoft.EntityFrameworkCore;
using Products.Application.Persistence;
using Products.Application.Products.DTOs;

namespace Products.Application.Products.Features.GetById;

public sealed class GetProductByIdQueryHandler(ProductsDbContext dbContext) : IQueryHandler<GetProductByIdQuery, ProductDto>
{
    public async Task<Result<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        => await dbContext
            .Products
            .AsNoTracking()
            .TagWith(nameof(GetProductByIdQuery), request.Id)
            .WhereIf(request.EnsureOwnership!, condition: request.EnsureOwnership is not null)
            .Select(p => new ProductDto
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
            })
            .SingleAsResultAsync(cancellationToken);
}

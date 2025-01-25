using Common.Application.CQS;
using Common.Application.Persistence;
using Common.Domain.ResultMonad;
using Products.Application.Products.DTOs;
using Products.Application.Products.Specifications;
using Products.Domain.Products;

namespace Products.Application.Products.Features.GetById;

public sealed class GetProductByIdQueryHandler(IRepository<Product> repository) : IQueryHandler<GetProductByIdQuery, ProductDto>
{
    public async Task<Result<ProductDto>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        => await repository
            .SingleOrDefaultAsResultAsync(new ProductByIdSpec<ProductDto>(query.Id, x => new ProductDto
            {
                Id = x.Id,
                StoreId = x.StoreId,
                Name = x.Name,
                Description = x.Description,
                Quantity = x.Quantity,
                Price = x.Price,
                CreatedBy = x.CreatedBy,
                CreatedOn = x.CreatedOn,
                LastModifiedBy = x.LastModifiedBy,
                LastModifiedOn = x.LastModifiedOn
            }), cancellationToken);
}

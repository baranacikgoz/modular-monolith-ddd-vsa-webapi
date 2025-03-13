using Common.Domain.ResultMonad;
using Common.Application.Persistence;
using Products.Domain.Stores;
using Products.Application.Stores.DTOs;
using Products.Application.Stores.Specifications;
using Common.Application.CQS;
using Microsoft.EntityFrameworkCore;
using Products.Application.Persistence;

namespace Products.Application.Stores.Features.GetById;

public sealed class GetStoreByIdQueryHandler(ProductsDbContext dbContext) : IQueryHandler<GetStoreByIdQuery, StoreDto>
{
    public async Task<Result<StoreDto>> Handle(GetStoreByIdQuery query, CancellationToken cancellationToken)
        => await dbContext
            .Stores
            .AsNoTracking()
            .TagWith(nameof(GetStoreByIdQuery), query.Id)
            .Where(s => s.Id == query.Id)
            .Select(s => new StoreDto
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
            })
            .SingleAsResultAsync(cancellationToken);

}

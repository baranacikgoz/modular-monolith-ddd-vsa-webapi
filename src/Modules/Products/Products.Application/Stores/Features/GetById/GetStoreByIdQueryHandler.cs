using Common.Domain.ResultMonad;
using Common.Application.Persistence;
using Products.Application.Stores.DTOs;
using Common.Application.CQS;
using Microsoft.EntityFrameworkCore;
using Products.Application.Persistence;
using Products.Application.Products.Features.GetById;

namespace Products.Application.Stores.Features.GetById;

public sealed class GetStoreByIdQueryHandler(ProductsDbContext dbContext) : IQueryHandler<GetStoreByIdQuery, StoreDto>
{
    public async Task<Result<StoreDto>> Handle(GetStoreByIdQuery request, CancellationToken cancellationToken)
        => await dbContext
            .Stores
            .AsNoTracking()
            .TagWith(nameof(GetStoreByIdQuery), request.Id)
            .WhereIf(request.EnsureOwnership!, condition: request.EnsureOwnership is not null)
            .Select(store => new StoreDto
            {
                Id = store.Id,
                OwnerId = store.OwnerId,
                Name = store.Name,
                Description = store.Description,
                Address = store.Address,
                ProductCount = store.Products.Count,
                CreatedBy = store.CreatedBy,
                CreatedOn = store.CreatedOn,
                LastModifiedBy = store.LastModifiedBy,
                LastModifiedOn = store.LastModifiedOn,
            })
            .SingleAsResultAsync(cancellationToken);
}

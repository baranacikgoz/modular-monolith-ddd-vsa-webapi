using Common.Domain.ResultMonad;
using Common.Application.Persistence;
using Products.Domain.Stores;
using Products.Application.Stores.DTOs;
using Products.Application.Stores.Specifications;
using Common.Application.CQS;

namespace Products.Application.Stores.Features.GetById;

public sealed class GetStoreByIdQueryHandler(IRepository<Store> repository) : IQueryHandler<GetStoreByIdQuery, StoreDto>
{
    public async Task<Result<StoreDto>> Handle(GetStoreByIdQuery query, CancellationToken cancellationToken)
        => await repository
            .SingleOrDefaultAsResultAsync(new StoreByIdSpec<StoreDto>(
                query.Id,
                x => new StoreDto
                {
                    Id = x.Id,
                    OwnerId = x.OwnerId,
                    Name = x.Name,
                    Description = x.Description,
                    Address = x.Address,
                    ProductCount = x.Products.Count,
                    CreatedBy = x.CreatedBy,
                    CreatedOn = x.CreatedOn,
                    LastModifiedBy = x.LastModifiedBy,
                    LastModifiedOn = x.LastModifiedOn
                }),
            cancellationToken);

}

using Common.Application.CQS;
using Common.Application.Persistence;
using Common.Domain.ResultMonad;
using Products.Application.Stores.Specifications;
using Products.Domain.Stores;

namespace Products.Application.Stores.Features.GetStoreIdByOwnerId;

public sealed class GetStoreIdByOwnerIdQueryHandler(IRepository<Store> repository) : IQueryHandler<GetStoreIdByOwnerIdQuery, StoreId>
{
    public async Task<Result<StoreId>> Handle(GetStoreIdByOwnerIdQuery query, CancellationToken cancellationToken)
        => await repository.SingleOrDefaultAsResultAsync(new StoreIdByOwnerIdSpec(query.OwnerId), cancellationToken);
}

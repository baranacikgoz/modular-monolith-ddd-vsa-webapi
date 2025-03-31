using Common.Application.CQS;
using Common.Application.Persistence;
using Common.Domain.ResultMonad;
using Microsoft.EntityFrameworkCore;
using Products.Application.Persistence;
using Products.Domain.Stores;

namespace Products.Application.Stores.Features.GetStoreIdByOwnerId;

public sealed class GetStoreIdByOwnerIdQueryHandler(IProductsDbContext dbContext) : IQueryHandler<GetStoreIdByOwnerIdQuery, StoreId>
{
    public async Task<Result<StoreId>> Handle(GetStoreIdByOwnerIdQuery query, CancellationToken cancellationToken)
        => await dbContext
            .Products
            .AsNoTracking()
            .TagWith(nameof(GetStoreIdByOwnerIdQuery), query.OwnerId)
            .Where(p => p.Store.OwnerId == query.OwnerId)
            .Select(p => p.StoreId)
            .FirstAsResultAsync(cancellationToken);
}

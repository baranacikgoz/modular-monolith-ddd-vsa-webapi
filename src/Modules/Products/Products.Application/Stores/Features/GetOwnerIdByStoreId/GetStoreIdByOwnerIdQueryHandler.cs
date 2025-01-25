using Common.Application.CQS;
using Common.Application.Persistence;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;
using Products.Application.Stores.Specifications;
using Products.Domain.Stores;

namespace Products.Application.Stores.Features.GetOwnerIdByStoreId;

public sealed class GetOwnerIdByStoreIdQueryHandler(IRepository<Store> repository) : IQueryHandler<GetOwnerIdByStoreIdQuery, ApplicationUserId>
{
    public async Task<Result<ApplicationUserId>> Handle(GetOwnerIdByStoreIdQuery query, CancellationToken cancellationToken)
        => await repository.SingleOrDefaultAsResultAsync(new OwnerIdByStoreIdSpec(query.StoreId), cancellationToken);
}

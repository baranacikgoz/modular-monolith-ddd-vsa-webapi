using Common.Domain.ResultMonad;
using Common.Application.Persistence;
using Products.Domain.Stores;
using Common.Application.CQS;
using Products.Application.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Products.Application.Stores.Features.Create;

public sealed class CreateStoreCommandHandler(IProductsDbContext dbContext) : ICommandHandler<CreateStoreCommand, StoreId>
{
    public async Task<Result<StoreId>> Handle(CreateStoreCommand command, CancellationToken cancellationToken)
        => await dbContext
            .Stores
            .AsNoTracking()
            .TagWith(nameof(CreateStoreCommand), "GetStoreByOwnerId", command.OwnerId)
            .Where(s => s.OwnerId == command.OwnerId)
            .AnyAsResultAsync(cancellationToken)
            .TapAsync(any => any ? Error.ViolatesUniqueConstraint(nameof(Store)) : Result.Success)
            .BindAsync(_ => Store.Create(command.OwnerId, command.Name, command.Description, command.Address))
            .TapAsync(store => dbContext.Stores.Add(store))
            .TapAsync(_ => dbContext.SaveChangesAsync(cancellationToken))
            .MapAsync(store => store.Id);
}

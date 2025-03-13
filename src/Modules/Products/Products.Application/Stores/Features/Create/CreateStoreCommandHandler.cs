using Common.Domain.ResultMonad;
using Microsoft.Extensions.DependencyInjection;
using Common.Application.Persistence;
using Products.Domain.Stores;
using Common.Application.CQS;
using Products.Application.Persistence;
using Products.Application.Stores.Specifications;

namespace Products.Application.Stores.Features.Create;

public sealed class CreateStoreCommandHandler(ProductsDbContext dbContext) : ICommandHandler<CreateStoreCommand, StoreId>
{
    public async Task<Result<StoreId>> Handle(CreateStoreCommand command, CancellationToken cancellationToken)
        => await dbContext
            .Stores
            .TagWith(nameof(CreateStoreCommand), "StoreByOwnerId", command.OwnerId)
            .Where(s => s.OwnerId == command.OwnerId)
            .AnyAsResultAsync(cancellationToken)
            .TapAsync(any => any ? Error.ViolatesUniqueConstraint(nameof(Store)) : Result.Success)
            .BindAsync(_ => Store.Create(command.OwnerId, command.Name, command.Description, command.Address))
            .TapAsync(store => dbContext.Stores.Add(store))
            .TapAsync(_ => dbContext.SaveChangesAsync(cancellationToken))
            .MapAsync(store => store.Id);
}

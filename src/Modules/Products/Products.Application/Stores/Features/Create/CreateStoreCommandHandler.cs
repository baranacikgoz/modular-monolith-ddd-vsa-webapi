using Common.Domain.ResultMonad;
using Microsoft.Extensions.DependencyInjection;
using Common.Application.Persistence;
using Products.Domain.Stores;
using Common.Application.CQS;
using Products.Application.Stores.Specifications;

namespace Products.Application.Stores.Features.Create;

public sealed class CreateStoreCommandHandler(
    IRepository<Store> repository,
    [FromKeyedServices(nameof(Products))] IUnitOfWork unitOfWork
    ) : ICommandHandler<CreateStoreCommand, StoreId>
{
    public async Task<Result<StoreId>> Handle(CreateStoreCommand command, CancellationToken cancellationToken)
        => await repository
            .AnyAsyncAsResult(new StoreIdByOwnerIdSpec(command.OwnerId), cancellationToken)
            .TapAsync(any => any ? Error.ViolatesUniqueConstraint(nameof(Store)) : Result.Success)
            .BindAsync(_ => Store.Create(command.OwnerId, command.Name, command.Description, command.Address))
            .TapAsync(store => repository.Add(store))
            .TapAsync(async _ => await unitOfWork.SaveChangesAsync(cancellationToken))
            .MapAsync(store => store.Id);
}

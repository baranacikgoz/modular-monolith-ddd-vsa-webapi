using Common.Domain.ResultMonad;
using Microsoft.Extensions.DependencyInjection;
using Common.Application.Persistence;
using Products.Domain.Stores;
using Common.Application.CQS;
using Products.Application.Stores.Specifications;

namespace Products.Application.Stores.Features.Update;

public sealed class UpdateStoreCommandHandler(
    IRepository<Store> repository,
    [FromKeyedServices(nameof(Products))] IUnitOfWork unitOfWork
    ) : ICommandHandler<UpdateStoreCommand>
{
    public async Task<Result> Handle(UpdateStoreCommand command, CancellationToken cancellationToken)
        => await repository
            .SingleOrDefaultAsResultAsync(new StoreByIdSpec(command.Id), cancellationToken)
            .TapAsync(store => store.Update(command.Name, command.Description, command.Address))
            .TapAsync(async _ => await unitOfWork.SaveChangesAsync(cancellationToken));
}

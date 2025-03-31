using Common.Domain.ResultMonad;
using Common.Application.CQS;
using Products.Application.Persistence;
using Common.Application.Persistence;

namespace Products.Application.Stores.Features.Update;

public sealed class UpdateStoreCommandHandler(IProductsDbContext dbContext) : ICommandHandler<UpdateStoreCommand>
{
    public async Task<Result> Handle(UpdateStoreCommand command, CancellationToken cancellationToken)
        => await dbContext
            .Stores
            .TagWith(nameof(UpdateStoreCommand), command.Id)
            .Where(s => s.Id == command.Id)
            .WhereIf(command.EnsureOwnership!, condition: command.EnsureOwnership is not null)
            .SingleAsResultAsync(cancellationToken)
            .TapAsync(store => store.Update(command.Name, command.Description, command.Address))
            .TapAsync(async _ => await dbContext.SaveChangesAsync(cancellationToken));
}

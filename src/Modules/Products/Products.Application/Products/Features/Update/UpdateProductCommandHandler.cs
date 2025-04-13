using Common.Domain.ResultMonad;
using Common.Application.Persistence;
using Common.Application.CQS;
using Products.Application.Persistence;

namespace Products.Application.Products.Features.Update;

public sealed class UpdateProductCommandHandler(IProductsDbContext dbContext) : ICommandHandler<UpdateProductCommand>
{
    public async Task<Result> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        => await dbContext
            .Products
            .TagWith(nameof(UpdateProductCommand), command.Id)
            .Where(p => p.Id == command.Id)
            .SingleAsResultAsync(cancellationToken)
            .TapAsync(product => product.Update(command.Name, command.Description, command.Quantity, command.Price))
            .TapAsync(async _ => await dbContext.SaveChangesAsync(cancellationToken));
}

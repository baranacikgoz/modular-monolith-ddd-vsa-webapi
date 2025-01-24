using Microsoft.Extensions.DependencyInjection;
using Common.Domain.ResultMonad;
using Common.Application.Persistence;
using Products.Domain.Products;
using Products.Application.Products.Specifications;
using Common.Application.CQS;

namespace Products.Application.Products.Features.Update;

public sealed class UpdateProductCommandHandler(
    IRepository<Product> repository,
    [FromKeyedServices(nameof(Products))] IUnitOfWork unitOfWork
    ) : ICommandHandler<UpdateProductCommand>
{
    public async Task<Result> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        => await repository
            .SingleOrDefaultAsResultAsync(new ProductByIdSpec(command.Id), cancellationToken)
            .TapAsync(product => product.Update(command.Name, command.Description, command.Quantity, command.Price))
            .TapAsync(async _ => await unitOfWork.SaveChangesAsync(cancellationToken));
}

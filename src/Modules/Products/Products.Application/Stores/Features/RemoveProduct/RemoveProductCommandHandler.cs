using Common.Application.CQS;
using Common.Application.Persistence;
using Common.Domain.ResultMonad;
using Microsoft.Extensions.DependencyInjection;
using Products.Application.Stores.Specifications;
using Products.Domain.Products;
using Products.Domain.Stores;

namespace Products.Application.Stores.Features.RemoveProduct;

public sealed class RemoveProductCommandHandler(
    IRepository<Store> storeRepository,
    IRepository<Product> productRepository,
    [FromKeyedServices(nameof(Products))] IUnitOfWork unitOfWork
    ) : ICommandHandler<RemoveProductCommand>
{
    public async Task<Result> Handle(RemoveProductCommand command, CancellationToken cancellationToken)
        => await storeRepository
            .SingleOrDefaultAsResultAsync(new StoreByIdWithProductsSpec(command.StoreId, command.ProductId), cancellationToken)
            .BindAsync(store =>
            {
                var product = store.Products.Single(x => x.Id == command.ProductId);

                return new { Store = store, Product = product };
            })
            .TapAsync(x => x.Store.RemoveProduct(x.Product))
            .TapAsync(x => productRepository.Delete(x.Product))
            .TapAsync(async _ => await unitOfWork.SaveChangesAsync(cancellationToken));
}

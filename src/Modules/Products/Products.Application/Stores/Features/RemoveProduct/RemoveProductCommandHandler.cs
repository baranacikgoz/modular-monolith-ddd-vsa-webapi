using Common.Application.CQS;
using Common.Application.Extensions;
using Common.Application.Persistence;
using Common.Domain.ResultMonad;
using Microsoft.EntityFrameworkCore;
using Products.Application.Persistence;

namespace Products.Application.Stores.Features.RemoveProduct;

public sealed class RemoveProductCommandHandler(ProductsDbContext dbContext) : ICommandHandler<RemoveProductCommand>
{
    public async Task<Result> Handle(RemoveProductCommand command, CancellationToken cancellationToken)
        => await dbContext
            .Stores
            .TagWith(nameof(RemoveProductCommand), "StoreById", command.StoreId)
            .Where(s => s.Id == command.StoreId)
            .Include(s => s.Products.Where(p => p.Id == command.ProductId))
            .SingleAsResultAsync(cancellationToken)
            .CombineAsync(store => store.Products.SingleAsResult(p => p.Id == command.ProductId))
            .TapAsync(tuple =>
            {
                var (store, product) = tuple;
                store.RemoveProduct(product);
            })
            .TapAsync(_ => dbContext.SaveChangesAsync(cancellationToken));
}

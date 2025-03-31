using Common.Application.CQS;
using Common.Application.Persistence;
using Common.Domain.ResultMonad;
using Products.Application.Persistence;
using Products.Domain.Products;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;

namespace Products.Application.Stores.Features.AddProduct;

public sealed class AddProductCommandHandler(IProductsDbContext dbContext) : ICommandHandler<AddProductCommand, ProductId>
{
    public async Task<Result<ProductId>> Handle(AddProductCommand command, CancellationToken cancellationToken)
        => await dbContext
            .Stores
            .TagWith(nameof(AddProductCommand), "StoreById", command.StoreId)
            .Where(s => s.Id == command.StoreId)
            .SingleAsResultAsync(cancellationToken)
            .CombineAsync(store => dbContext
                .ProductTemplates
                .TagWith(nameof(AddProductCommand), "ActiveProductTemplateById", command.ProductTemplateId)
                .Where(pt => pt.IsActive)
                .Where(pt => pt.Id == command.ProductTemplateId)
                .SingleAsResultAsync(cancellationToken))
            .CombineAsync<Store, ProductTemplate, Product>(tuple =>
            {
                var (store, productTemplate) = tuple;
                return Product.Create(store.Id, productTemplate.Id, command.Name, command.Description, command.Quantity,
                    command.Price);
            })
            .TapAsync(triple =>
            {
                var (store, _, product) = triple;
                store.AddProduct(product);
            })
            .TapAsync(_ => dbContext.SaveChangesAsync(cancellationToken))
            .MapAsync(triple =>
            {
                var (_, _, product) = triple;
                return product.Id;
            });
}

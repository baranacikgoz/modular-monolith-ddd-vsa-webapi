using Common.Application.CQS;
using Common.Application.Persistence;
using Common.Domain.ResultMonad;
using Microsoft.Extensions.DependencyInjection;
using Products.Application.ProductTemplates.Specifications;
using Products.Application.Stores.Specifications;
using Products.Domain.Products;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;

namespace Products.Application.Stores.Features.AddProduct;

public sealed class AddProductCommandHandler(
    IRepository<Store> storeRepository,
    IRepository<ProductTemplate> productTemplateRepository,
    [FromKeyedServices(nameof(Products))] IUnitOfWork unitOfWork
    ) : ICommandHandler<AddProductCommand, ProductId>
{
    public async Task<Result<ProductId>> Handle(AddProductCommand command, CancellationToken cancellationToken)
    {
        var storeResult = await storeRepository.SingleOrDefaultAsResultAsync(new StoreByIdSpec(command.StoreId), cancellationToken);
        if (storeResult.IsFailure)
        {
            return storeResult.Error!;
        }
        var store = storeResult.Value!;

        var activeProductTemplateResult = await productTemplateRepository.SingleOrDefaultAsResultAsync(new ActiveProductTemplateByIdSpec(command.ProductTemplateId), cancellationToken);
        if (activeProductTemplateResult.IsFailure)
        {
            return activeProductTemplateResult.Error!;
        }
        var productTemplate = activeProductTemplateResult.Value!;
        var product = Product.Create(store.Id, productTemplate.Id, command.Name, command.Description, command.Quantity, command.Price);
        store.AddProduct(product);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}

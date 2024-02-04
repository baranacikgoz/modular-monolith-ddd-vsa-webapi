namespace Sales.Persistence.Seeding;
using Common.InterModuleRequests.IdentityAndAuth;
using Microsoft.EntityFrameworkCore;
using Sales.Features.Products.Domain;
using Sales.Features.Stores.Domain;

internal sealed partial class Seeder
{
    private async Task SeedProductsAsync(CancellationToken cancellationToken)
    {
        var firstStoresIds = await dbContext
                                .Stores
                                .OrderBy(s => s.CreatedOn)
                                .Take(StoreCount)
                                .Select(s => s.Id)
                                .ToListAsync(cancellationToken);

        for (var i = 0; i < StoreCount; i++)
        {
            var productName = $"Product {i + 1}";
            await SeedProductsAsync(firstStoresIds[i], productName, cancellationToken);
        }

    }

    private async Task SeedProductsAsync(StoreId storeId, string productName, CancellationToken cancellationToken)
    {
        if (await dbContext.Products.AnyAsync(product => product.StoreId == storeId && product.Name == productName, cancellationToken))
        {
            return;
        }

        var product = Product.Create(
            storeId: storeId,
            name: productName,
            description: "Seeded by system."
        );

        await dbContext.Products.AddAsync(product, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}

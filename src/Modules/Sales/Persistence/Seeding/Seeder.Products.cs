namespace Sales.Persistence.Seeding;
using Common.InterModuleRequests.IdentityAndAuth;
using Microsoft.EntityFrameworkCore;
using Sales.Features.Products.Domain;
using Sales.Features.Stores.Domain;

internal sealed partial class Seeder
{
    public async Task SeedProductsAsync(CancellationToken cancellationToken)
    {
        var firstStoreId = await dbContext
                                .Stores
                                .Select(s => s.Id)
                                .FirstOrDefaultAsync(cancellationToken);

        var product = Product.Create(
            storeId: firstStoreId,
            name: "Product 1",
            description: "Seed by system.");

        await dbContext.Products.AddAsync(product, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}

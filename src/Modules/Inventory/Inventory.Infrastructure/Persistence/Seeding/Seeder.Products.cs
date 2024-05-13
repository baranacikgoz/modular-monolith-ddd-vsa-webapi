using Inventory.Domain.Products;
using Inventory.Domain.Stores;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Persistence.Seeding;
internal sealed partial class Seeder
{
    private async Task<List<ProductId>> SeedProductsAsync(CancellationToken cancellationToken)
    {
        var productIds = new List<ProductId>(ProductCount);

        for (var i = 0; i < ProductCount; i++)
        {
            var name = $"Seed Product {i + 1}";

            productIds.Add(await SeedProductsAsync(name, cancellationToken));
        }

        return productIds;
    }

    private async Task<ProductId> SeedProductsAsync(string name, CancellationToken cancellationToken)
    {
        if (await dbContext.Products.SingleOrDefaultAsync(product => product.Name == name, cancellationToken)
            is not Product product)
        {
            product = Product.Create(
                name: name,
                description: "Seeded by system.");

            await dbContext.Products.AddAsync(product, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        return product.Id;
    }
}

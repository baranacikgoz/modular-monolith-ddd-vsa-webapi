using Microsoft.EntityFrameworkCore;
using Products.Domain.Products;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;

namespace Products.Infrastructure.Persistence.Seeding;
internal sealed partial class Seeder
{
    private async Task SeedProductAsync(List<StoreId> storeIds, List<ProductTemplateId> productTemplateIds, int storeProductCountPerStore, CancellationToken cancellationToken)
    {
        var random = new Random(123);

        for (var i = 0; i < storeIds.Count; i++)
        {
            for (var j = 0; j < storeProductCountPerStore; j++)
            {
                var storeId = storeIds[i];
                var productTemplateId = productTemplateIds[i + j];
                var name = $"Product {j + 1}";

                if (await dbContext.Products.SingleOrDefaultAsync(sp => sp.StoreId == storeId && sp.ProductTemplateId == productTemplateId, cancellationToken)
                    is not Product storeProduct)
                {
#pragma warning disable CA5394
                    storeProduct = Product.Create(storeId, productTemplateId, name: name, description: "Seed", quantity: random.Next(1, 50), price: random.Next(500, 2000));
#pragma warning restore CA5394
                    dbContext.Products.Add(storeProduct);
                }
            }
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}

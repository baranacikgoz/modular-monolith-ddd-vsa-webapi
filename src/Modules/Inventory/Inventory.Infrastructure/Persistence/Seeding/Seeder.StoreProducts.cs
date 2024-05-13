using Inventory.Domain.Products;
using Inventory.Domain.StoreProducts;
using Inventory.Domain.Stores;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Persistence.Seeding;
internal sealed partial class Seeder
{
    private async Task SeedStoreProductAsync(List<StoreId> storeIds, List<ProductId> productIds, int storeProductCountPerStore, CancellationToken cancellationToken)
    {
        var random = new Random(123);

        for (var i = 0; i < storeIds.Count; i++)
        {
            for (var j = 0; j < storeProductCountPerStore; j++)
            {
                var storeId = storeIds[i];
                var productId = productIds[i + j];

                if (await dbContext.StoreProducts.SingleOrDefaultAsync(sp => sp.StoreId == storeId && sp.ProductId == productId, cancellationToken)
                    is not StoreProduct storeProduct)
                {
#pragma warning disable CA5394
                    storeProduct = StoreProduct.Create(storeId, productId, quantity: random.Next(1, 50), price: random.Next(500, 2000));
#pragma warning restore CA5394
                    dbContext.StoreProducts.Add(storeProduct);
                }
            }
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}

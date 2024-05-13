using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.IdentityAndAuth;
using Inventory.Domain.Products;
using Inventory.Domain.StoreProducts;
using Inventory.Domain.Stores;
using Inventory.Infrastructure.Persistence;

namespace Inventory.Persistence.Seeding;

internal sealed partial class Seeder(
    IInterModuleRequestClient<GetSeedUserIdsRequest, GetSeedUserIdsResponse> requestClient,
    InventoryDbContext dbContext
    )
{
    /// <summary>
    /// Keep <see cref="ProductCount"/> divisible by <see cref="StoreCount"/>
    /// </summary>
    private const int StoreCount = 2;
    private const int ProductCount = 4;
    public async Task SeedDbAsync(CancellationToken cancellationToken = default)
    {
        var storeIds = await SeedStoresAsync(cancellationToken);
        var productIds = await SeedProductsAsync(cancellationToken);

        var storeProductCountPerStore = ProductCount / StoreCount;

        await SeedStoreProductAsync(storeIds, productIds, storeProductCountPerStore, cancellationToken);
    }
}

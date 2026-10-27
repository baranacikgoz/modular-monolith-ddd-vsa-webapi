using Inventory.Application.Persistence;
using Inventory.Domain.StockLevels;
using Inventory.Domain.StockPushes;
using Inventory.Domain.WarehouseConnections;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Tests.Support;

/// <summary>A store with its stock levels and (unless asked otherwise) an active warehouse connection.</summary>
internal sealed record SeededStore(DefaultIdType StoreId, WarehouseConnection? Connection, IReadOnlyList<StockLevel> StockLevels);

internal static class StockPushTestData
{
    public static async Task<SeededStore> SeedStoreAsync(
        IServiceProvider services,
        int stockLevels = 1,
        int quantityOnHand = 10,
        bool withConnection = true,
        bool deactivated = false,
        WarehouseProvider provider = WarehouseProvider.JsonApi)
    {
        var storeId = DefaultIdType.CreateVersion7();
        var connection = withConnection
            ? (await WarehouseConnectionTestData.SeedAsync(services, storeId, provider, deactivated)).Connection
            : null;

        var seeded = new List<StockLevel>(stockLevels);
        for (var i = 0; i < stockLevels; i++)
        {
            var productId = await WarehouseConnectionTestData.SeedProductOfStoreAsync(services, storeId);
            seeded.Add(await StockLevelTestData.SeedStockLevelAsync(services, quantityOnHand, productId: productId));
        }

        return new SeededStore(storeId, connection, seeded);
    }

    /// <summary>Changes the quantity through the aggregate and saves, the way an endpoint or a consumer does.</summary>
    public static async Task ReceiveAsync(IServiceProvider services, StockLevelId id, int quantity)
    {
        await using var scope = services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        var stockLevel = await db.StockLevels.SingleAsync(s => s.Id == id);
        stockLevel.Receive(quantity, "Delivery");
        await db.SaveChangesAsync();
    }

    public static async Task<List<StockPushDirtyItem>> DirtyItemsAsync(IServiceProvider services)
    {
        await using var scope = services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        return await db.StockPushDirtyItems.AsNoTracking().OrderBy(d => d.MarkedOn).ToListAsync();
    }
}

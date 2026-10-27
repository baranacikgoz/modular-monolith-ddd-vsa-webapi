using System.Net.Http.Headers;
using Common.Infrastructure.Persistence.Outbox;
using Common.IntegrationEvents;
using Common.Tests;
using Inventory.Application.Persistence;
using Inventory.Domain.StockLevels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Tests.Support;

internal static class StockLevelTestData
{
    public static HttpClient AuthedClient(IntegrationTestFactory factory)
    {
        var client = factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("TestScheme");
        return client;
    }

    /// <summary>Saves a stock level through the aggregate, with an optional low stock threshold already in place.</summary>
    public static async Task<StockLevel> SeedStockLevelAsync(
        IServiceProvider services, int quantityOnHand = 10, int lowStockThreshold = 0, DefaultIdType? productId = null)
    {
        await using var scope = services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();

        var stockLevel = StockLevel.Create(productId ?? DefaultIdType.CreateVersion7(), quantityOnHand);
        stockLevel.ChangeLowStockThreshold(lowStockThreshold);
        db.StockLevels.Add(stockLevel);
        await db.SaveChangesAsync();

        return stockLevel;
    }

    public static async Task<StockLevel> ReloadStockLevelAsync(IServiceProvider services, StockLevelId id)
    {
        await using var scope = services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        return await db.StockLevels.AsNoTracking().SingleAsync(s => s.Id == id);
    }

    public static async Task<List<StockMovement>> LedgerOfAsync(IServiceProvider services, StockLevelId id)
    {
        await using var scope = services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        return await db.StockMovements.AsNoTracking()
            .Where(m => m.StockLevelId == id)
            .OrderBy(m => m.CreatedOn)
            .ToListAsync();
    }

    /// <summary>Unprocessed outbox rows carrying <typeparamref name="TEvent" />; the outbox processor is off in slice tests.</summary>
    public static async Task<List<TEvent>> QueuedEventsAsync<TEvent>(IServiceProvider services)
        where TEvent : IntegrationEvent
    {
        await using var scope = services.CreateAsyncScope();
        var outboxDb = scope.ServiceProvider.GetRequiredService<IOutboxDbContext>();
        var messages = await outboxDb.OutboxMessages.AsNoTracking().Where(m => !m.IsProcessed).ToListAsync();
        return messages.Select(m => m.Event).OfType<TEvent>().ToList();
    }
}

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

    /// <summary>
    ///     The ledger of one stock level in the order the rows were written. Rows of one save share a timestamp and their ids
    ///     (version 7, random inside a millisecond) do not say which came first, so inside a timestamp the rows are chained by
    ///     the running quantity: a row starts from the quantity the row before it ended on.
    /// </summary>
    public static async Task<List<StockMovement>> LedgerOfAsync(IServiceProvider services, StockLevelId id)
    {
        await using var scope = services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        var rows = await db.StockMovements.AsNoTracking()
            .Where(m => m.StockLevelId == id)
            .OrderBy(m => m.CreatedOn)
            .ThenBy(m => m.Id)
            .ToListAsync();

        return InWriteOrder(rows);
    }

    private static List<StockMovement> InWriteOrder(List<StockMovement> rows)
    {
        var ordered = new List<StockMovement>(rows.Count);

        foreach (var sameTimestamp in rows.GroupBy(m => m.CreatedOn))
        {
            var pending = sameTimestamp.ToList();
            var endedOn = ordered.Count > 0 ? ordered[^1].QuantityOnHandAfter : (int?)null;

            while (pending.Count > 0)
            {
                // The first row of the ledger has no row before it: it is the one no other pending row ends where it starts.
                var next = pending.FirstOrDefault(m => endedOn is { } ended
                                                    ? StartedFrom(m) == ended
                                                    : pending.All(other => other.QuantityOnHandAfter != StartedFrom(m)))
                           ?? pending[0];

                pending.Remove(next);
                ordered.Add(next);
                endedOn = next.QuantityOnHandAfter;
            }
        }

        return ordered;
    }

    private static int StartedFrom(StockMovement movement) => movement.QuantityOnHandAfter - movement.Quantity;

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

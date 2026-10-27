using Common.Application.Persistence.Inbox;
using Common.IntegrationEvents;
using Common.Tests;
using Inventory.Application.Persistence;
using Inventory.Application.ProductSummaries;
using Inventory.Domain.StockCounters;
using Inventory.Infrastructure.StockCounters;
using Inventory.Tests.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Inventory.Tests.StockCounters;

/// <summary>
///     Per-store daily totals kept from committed reservations: one atomic upsert per event inside the consumer's
///     transaction, so concurrent events never lose an increment and a redelivered event rolls its counter change back.
/// </summary>
[Collection("IntegrationTestCollection")]
public class StoreDailyStockTotalsTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private IServiceProvider Root => Factory.Services;

    private async Task<DefaultIdType> SeedProductOfAsync(DefaultIdType storeId)
    {
        var productId = DefaultIdType.CreateVersion7();
        await using var scope = Root.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        var summary = ProductSummary.Create(productId);
        summary.ApplyIfNewer(storeId, "Widget", 1);
        db.ProductSummaries.Add(summary);
        await db.SaveChangesAsync();
        return productId;
    }

    private static StockReservationCommittedIntegrationEvent Committed(
        DefaultIdType productId, int quantity, DateTimeOffset? createdOn = null)
    {
        var @event = new StockReservationCommittedIntegrationEvent(DefaultIdType.CreateVersion7(), productId, quantity);
        if (createdOn is { } on)
        {
            @event.CreatedOn = on;
        }

        return @event;
    }

    private Task CountAsync(StockReservationCommittedIntegrationEvent @event)
    {
        return ConsumerHarness.DeliverAsync<CountCommittedStockOnReservationCommittedHandler, StockReservationCommittedIntegrationEvent>(
            Root, @event);
    }

    private async Task<List<StoreDailyStockTotal>> TotalsAsync(DefaultIdType storeId)
    {
        await using var scope = Root.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        return await db.StoreDailyStockTotals.AsNoTracking().Where(t => t.StoreId == storeId).OrderBy(t => t.Day).ToListAsync();
    }

    [Fact]
    public async Task Count_TwoReservationsOfOneStore_TotalsGrowByBoth()
    {
        var storeId = DefaultIdType.CreateVersion7();
        var productId = await SeedProductOfAsync(storeId);

        await CountAsync(Committed(productId, 3));
        await CountAsync(Committed(productId, 4));

        var total = Assert.Single(await TotalsAsync(storeId));
        Assert.Equal(2, total.CommittedReservations);
        Assert.Equal(7, total.CommittedUnits);
    }

    [Fact]
    public async Task Count_ReservationsOfTwoProductsOfOneStore_ShareTheStoresRow()
    {
        var storeId = DefaultIdType.CreateVersion7();
        var first = await SeedProductOfAsync(storeId);
        var second = await SeedProductOfAsync(storeId);

        await CountAsync(Committed(first, 3));
        await CountAsync(Committed(second, 4));

        var total = Assert.Single(await TotalsAsync(storeId));
        Assert.Equal(7, total.CommittedUnits);
    }

    [Fact]
    public async Task Count_SameEventTwiceWithCacheEvicted_CountsOnceBecauseTheRedeliveryRollsBack()
    {
        var storeId = DefaultIdType.CreateVersion7();
        var productId = await SeedProductOfAsync(storeId);
        var @event = Committed(productId, 3);

        await CountAsync(@event);
        await ConsumerHarness.ForgetAsync<CountCommittedStockOnReservationCommittedHandler>(Root, @event);
        await CountAsync(@event);

        var total = Assert.Single(await TotalsAsync(storeId));
        Assert.Equal(1, total.CommittedReservations);
        Assert.Equal(3, total.CommittedUnits);
    }

    [Fact]
    public async Task Count_ManyEventsConcurrentlyOnOneStore_NoIncrementIsLost()
    {
        var storeId = DefaultIdType.CreateVersion7();
        var productId = await SeedProductOfAsync(storeId);

        await Task.WhenAll(Enumerable.Range(1, 10).Select(i => CountAsync(Committed(productId, i))));

        var total = Assert.Single(await TotalsAsync(storeId));
        Assert.Equal(10, total.CommittedReservations);
        Assert.Equal(55, total.CommittedUnits);
    }

    // The business time zone (inventory.json) is ahead of UTC: 23:30 UTC on 15 January is already 16 January there.
    [Fact]
    public async Task Count_EventsAroundMidnight_LandOnTheBusinessDayOfTheirTimestamp()
    {
        var storeId = DefaultIdType.CreateVersion7();
        var productId = await SeedProductOfAsync(storeId);

        await CountAsync(Committed(productId, 1, new DateTimeOffset(2026, 1, 15, 22, 30, 0, TimeSpan.Zero)));
        await CountAsync(Committed(productId, 2, new DateTimeOffset(2026, 1, 15, 23, 30, 0, TimeSpan.Zero)));

        var totals = await TotalsAsync(storeId);
        Assert.Equal([new DateOnly(2026, 1, 15), new DateOnly(2026, 1, 16)], totals.Select(t => t.Day).ToArray());
        Assert.Equal([1, 2], totals.Select(t => t.CommittedUnits).ToArray());
    }

    // The product's store is only known from the local copy; without it the message must fail so the bus retries once
    // the copy has caught up, and nothing is counted or marked processed meanwhile.
    [Fact]
    public async Task Count_ProductNotProjectedYet_ThrowsAndLeavesNoCounterAndNoInboxRow()
    {
        var @event = Committed(DefaultIdType.CreateVersion7(), 3);

        await Assert.ThrowsAsync<InvalidOperationException>(() => CountAsync(@event));

        await using var scope = Root.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        Assert.False(await db.Set<ProcessedMessage>().AsNoTracking().AnyAsync(m => m.MessageId == @event.Id));
    }
}

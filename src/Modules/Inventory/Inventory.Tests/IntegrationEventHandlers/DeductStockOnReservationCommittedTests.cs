using Common.Application.Persistence.Inbox;
using Common.IntegrationEvents;
using Common.Tests;
using Inventory.Application.Persistence;
using Inventory.Domain.StockLevels;
using Inventory.Infrastructure.IntegrationEventHandlers;
using Inventory.Tests.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Inventory.Tests.IntegrationEventHandlers;

/// <summary>
///     A committed reservation takes its quantity out of the on-hand stock by event: the consumer loads the stock
///     level with <c>SELECT ... FOR UPDATE</c> inside its own transaction, so concurrent movements on one stock level
///     queue on the row lock instead of one of them failing the Version check.
/// </summary>
[Collection("IntegrationTestCollection")]
public class DeductStockOnReservationCommittedTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private IServiceProvider Root => Factory.Services;

    private static StockReservationCommittedIntegrationEvent Committed(
        DefaultIdType productId, int quantity, DefaultIdType? reservationId = null)
    {
        return new StockReservationCommittedIntegrationEvent(reservationId ?? DefaultIdType.CreateVersion7(), productId, quantity);
    }

    private Task DeliverAsync(StockReservationCommittedIntegrationEvent @event)
    {
        return ConsumerHarness.DeliverAsync<DeductStockOnReservationCommittedHandler, StockReservationCommittedIntegrationEvent>(Root, @event);
    }

    private async Task<bool> InboxRowExistsAsync(StockReservationCommittedIntegrationEvent @event)
    {
        await using var scope = Root.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        return await db.Set<ProcessedMessage>().AsNoTracking()
            .AnyAsync(m => m.ConsumerName == nameof(DeductStockOnReservationCommittedHandler) && m.MessageId == @event.Id);
    }

    [Fact]
    public async Task Deduct_CommittedReservation_TakesTheQuantityOutAndAppendsAMovementWithoutAnActingUser()
    {
        var stockLevel = await StockLevelTestData.SeedStockLevelAsync(Root, quantityOnHand: 10);
        var @event = Committed(stockLevel.ProductId, 4);

        await DeliverAsync(@event);

        Assert.Equal(6, (await StockLevelTestData.ReloadStockLevelAsync(Root, stockLevel.Id)).QuantityOnHand);
        var movement = Assert.Single(await StockLevelTestData.LedgerOfAsync(Root, stockLevel.Id));
        Assert.Equal(StockMovementType.ReservationCommitted, movement.Type);
        Assert.Equal(-4, movement.Quantity);
        Assert.Equal(6, movement.QuantityOnHandAfter);
        Assert.Equal(@event.ReservationId, movement.ReservationId);
        Assert.Null(movement.CreatedBy);
    }

    [Fact]
    public async Task Deduct_SameEventTwiceWithCacheEvicted_DeductsOnce()
    {
        var stockLevel = await StockLevelTestData.SeedStockLevelAsync(Root, quantityOnHand: 10);
        var @event = Committed(stockLevel.ProductId, 4);

        await DeliverAsync(@event);
        await ConsumerHarness.ForgetAsync<DeductStockOnReservationCommittedHandler>(Root, @event);
        await DeliverAsync(@event);

        Assert.Equal(6, (await StockLevelTestData.ReloadStockLevelAsync(Root, stockLevel.Id)).QuantityOnHand);
        Assert.Single(await StockLevelTestData.LedgerOfAsync(Root, stockLevel.Id));
    }

    // The same reservation announced again under a new message id: no per-message inbox can tell, the ledger check under
    // the row lock does.
    [Fact]
    public async Task Deduct_SameReservationUnderTwoMessageIds_DeductsOnce()
    {
        var stockLevel = await StockLevelTestData.SeedStockLevelAsync(Root, quantityOnHand: 10);
        var reservationId = DefaultIdType.CreateVersion7();

        await DeliverAsync(Committed(stockLevel.ProductId, 4, reservationId));
        await DeliverAsync(Committed(stockLevel.ProductId, 4, reservationId));

        Assert.Equal(6, (await StockLevelTestData.ReloadStockLevelAsync(Root, stockLevel.Id)).QuantityOnHand);
        Assert.Single(await StockLevelTestData.LedgerOfAsync(Root, stockLevel.Id));
    }

    // Without the row lock the second writer loads the stock level before the first commits and fails the Version check.
    [Fact]
    public async Task Deduct_TwoReservationsConcurrentlyOnOneStockLevel_NeitherUpdateIsLost()
    {
        var stockLevel = await StockLevelTestData.SeedStockLevelAsync(Root, quantityOnHand: 100);

        await Task.WhenAll(
            DeliverAsync(Committed(stockLevel.ProductId, 30)),
            DeliverAsync(Committed(stockLevel.ProductId, 20)));

        Assert.Equal(50, (await StockLevelTestData.ReloadStockLevelAsync(Root, stockLevel.Id)).QuantityOnHand);
        var ledger = await StockLevelTestData.LedgerOfAsync(Root, stockLevel.Id);
        Assert.Equal(2, ledger.Count);
        // The second writer saw the first one's committed quantity, so one row carries the final 50.
        Assert.Contains(ledger, m => m.QuantityOnHandAfter == 50);
    }

    [Fact]
    public async Task Deduct_ManyReservationsConcurrentlyOnOneStockLevel_EveryOneIsApplied()
    {
        var stockLevel = await StockLevelTestData.SeedStockLevelAsync(Root, quantityOnHand: 100);

        await Task.WhenAll(Enumerable.Range(0, 6).Select(_ => DeliverAsync(Committed(stockLevel.ProductId, 5))));

        Assert.Equal(70, (await StockLevelTestData.ReloadStockLevelAsync(Root, stockLevel.Id)).QuantityOnHand);
        Assert.Equal(6, (await StockLevelTestData.LedgerOfAsync(Root, stockLevel.Id)).Count);
    }

    [Fact]
    public async Task Deduct_NoStockLevelForTheProduct_IsAcknowledgedWithoutEffect()
    {
        var @event = Committed(DefaultIdType.CreateVersion7(), 4);

        await DeliverAsync(@event);

        Assert.True(await InboxRowExistsAsync(@event));
    }

    [Fact]
    public async Task Deduct_MoreThanOnHand_IsAcknowledgedWithoutEffect()
    {
        var stockLevel = await StockLevelTestData.SeedStockLevelAsync(Root, quantityOnHand: 3);
        var @event = Committed(stockLevel.ProductId, 4);

        await DeliverAsync(@event);

        Assert.Equal(3, (await StockLevelTestData.ReloadStockLevelAsync(Root, stockLevel.Id)).QuantityOnHand);
        Assert.Empty(await StockLevelTestData.LedgerOfAsync(Root, stockLevel.Id));
        Assert.True(await InboxRowExistsAsync(@event));
    }

    [Fact]
    public async Task Deduct_CrossingTheThreshold_QueuesTheCrossedIntegrationEventInTheSameTransaction()
    {
        var stockLevel = await StockLevelTestData.SeedStockLevelAsync(Root, quantityOnHand: 10, lowStockThreshold: 5);

        await DeliverAsync(Committed(stockLevel.ProductId, 6));

        var queued = await StockLevelTestData.QueuedEventsAsync<StockLowThresholdCrossedIntegrationEvent>(Root);
        var crossed = Assert.Single(queued, e => e.ProductId == stockLevel.ProductId);
        Assert.Equal(4, crossed.QuantityOnHand);
    }
}

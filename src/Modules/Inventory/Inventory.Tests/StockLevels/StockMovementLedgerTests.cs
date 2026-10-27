using Common.Tests;
using Inventory.Application.Persistence;
using Inventory.Domain.StockLevels;
using Inventory.Tests.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Inventory.Tests.StockLevels;

/// <summary>The ledger rejects a tracked rewrite or delete of a persisted movement before anything reaches the database.</summary>
[Collection("IntegrationTestCollection")]
public class StockMovementLedgerTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private async Task<StockLevelId> SeedStockLevelWithOneMovementAsync()
    {
        var stockLevel = await StockLevelTestData.SeedStockLevelAsync(Factory.Services, quantityOnHand: 10);
        await using var scope = Factory.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        var tracked = await db.StockLevels.SingleAsync(s => s.Id == stockLevel.Id);
        tracked.Receive(5, "Supplier delivery");
        await db.SaveChangesAsync();
        return stockLevel.Id;
    }

    [Fact]
    public async Task SaveChanges_NewMovementAddedByTheAggregate_IsPersistedWithItsRunningQuantity()
    {
        var id = await SeedStockLevelWithOneMovementAsync();

        var movement = Assert.Single(await StockLevelTestData.LedgerOfAsync(Factory.Services, id));
        Assert.Equal(15, movement.QuantityOnHandAfter);
        Assert.NotEqual(default, movement.CreatedOn);
    }

    // Movement ids are version 7 and random inside a millisecond, so rows of one save can only be told apart by the
    // running quantity. Many stock levels make a wrong order show up on every run, not once in a while.
    [Fact]
    public async Task LedgerOf_RowsOfOneSave_ComeBackInTheOrderTheyWereWritten()
    {
        for (var attempt = 0; attempt < 25; attempt++)
        {
            var stockLevel = await StockLevelTestData.SeedStockLevelAsync(Factory.Services, quantityOnHand: 10);
            await using (var scope = Factory.Services.CreateAsyncScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
                var tracked = await db.StockLevels.SingleAsync(s => s.Id == stockLevel.Id);
                tracked.Receive(5, "Delivery");
                tracked.Adjust(-7, "Count");
                tracked.Receive(1, "Delivery");
                await db.SaveChangesAsync();
            }

            var ledger = await StockLevelTestData.LedgerOfAsync(Factory.Services, stockLevel.Id);

            Assert.Equal([15, 8, 9], ledger.Select(m => m.QuantityOnHandAfter).ToArray());
        }
    }

    [Fact]
    public async Task SaveChanges_RewritingAPersistedMovement_Throws()
    {
        var id = await SeedStockLevelWithOneMovementAsync();
        await using var scope = Factory.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        var movement = await db.StockMovements.SingleAsync(m => m.StockLevelId == id);

        db.Entry(movement).Property(m => m.Quantity).CurrentValue = 99;

        await Assert.ThrowsAsync<InvalidOperationException>(() => db.SaveChangesAsync());
        Assert.Equal(5, (await StockLevelTestData.LedgerOfAsync(Factory.Services, id)).Single().Quantity);
    }

    [Fact]
    public async Task SaveChanges_DeletingAPersistedMovement_Throws()
    {
        var id = await SeedStockLevelWithOneMovementAsync();
        await using var scope = Factory.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        var movement = await db.StockMovements.SingleAsync(m => m.StockLevelId == id);

        db.StockMovements.Remove(movement);

        await Assert.ThrowsAsync<InvalidOperationException>(() => db.SaveChangesAsync());
        Assert.Single(await StockLevelTestData.LedgerOfAsync(Factory.Services, id));
    }
}

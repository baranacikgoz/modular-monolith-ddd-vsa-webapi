using Common.Tests;
using Inventory.Application.Persistence;
using Inventory.Domain.StockLevels;
using Inventory.Infrastructure.Migrations;
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

    // Movement ids are version 7 and random inside a millisecond and the rows of one save share a timestamp, so only the
    // sequence tells their order. Many stock levels make a wrong order show up on every run, not once in a while.
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
            Assert.Equal([1L, 2L, 3L], ledger.Select(m => m.Sequence).ToArray());
        }
    }

    [Fact]
    public async Task SaveChanges_MovementsInSeparateSaves_ContinueTheSequenceFromThePersistedCounter()
    {
        var stockLevel = await StockLevelTestData.SeedStockLevelAsync(Factory.Services, quantityOnHand: 10);
        for (var save = 0; save < 3; save++)
        {
            await using var scope = Factory.Services.CreateAsyncScope();
            var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
            var tracked = await db.StockLevels.SingleAsync(s => s.Id == stockLevel.Id);
            tracked.Receive(1, "Delivery");
            await db.SaveChangesAsync();
        }

        var ledger = await StockLevelTestData.LedgerOfAsync(Factory.Services, stockLevel.Id);

        Assert.Equal([1L, 2L, 3L], ledger.Select(m => m.Sequence).ToArray());
        Assert.Equal(3L, (await StockLevelTestData.ReloadStockLevelAsync(Factory.Services, stockLevel.Id)).LastMovementSequence);
    }

    // Both writers read the counter at 0 and would hand out the sequence 1; the stock level's own concurrency check lets only
    // one of them save (the audit row of the second collides first, the sequence index is the last backstop), so the ledger
    // never holds the same place twice.
    [Fact]
    public async Task SaveChanges_TwoWritersOfOneStockLevel_TheSecondFailsInsteadOfRepeatingASequence()
    {
        var stockLevel = await StockLevelTestData.SeedStockLevelAsync(Factory.Services, quantityOnHand: 10);
        await using var firstScope = Factory.Services.CreateAsyncScope();
        await using var secondScope = Factory.Services.CreateAsyncScope();
        var firstDb = firstScope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        var secondDb = secondScope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        var first = await firstDb.StockLevels.SingleAsync(s => s.Id == stockLevel.Id);
        var second = await secondDb.StockLevels.SingleAsync(s => s.Id == stockLevel.Id);
        first.Receive(1, "First writer");
        second.Receive(2, "Second writer");

        await firstDb.SaveChangesAsync();

        await Assert.ThrowsAnyAsync<DbUpdateException>(() => secondDb.SaveChangesAsync());
        var ledger = await StockLevelTestData.LedgerOfAsync(Factory.Services, stockLevel.Id);
        Assert.Equal("First writer", Assert.Single(ledger).Reason);
    }

    // The migration numbers the rows that existed before the column did. The placeholders below are what the column holds
    // before the backfill runs (the unique index only exists afterwards, so here they are distinct negatives).
    [Fact]
    public async Task BackfillSql_ExistingRows_AreNumberedPerStockLevelByCreationTimeThenIdAndTheCounterFollows()
    {
        var first = await StockLevelTestData.SeedStockLevelAsync(Factory.Services, quantityOnHand: 10);
        var second = await StockLevelTestData.SeedStockLevelAsync(Factory.Services, quantityOnHand: 10);
        var empty = await StockLevelTestData.SeedStockLevelAsync(Factory.Services, quantityOnHand: 10);
        var t0 = DateTimeOffset.UtcNow.AddHours(-1);
        var olderOfFirst = await InsertLegacyRowAsync(first.Id, t0, placeholder: -1);
        var newerOfFirst = await InsertLegacyRowAsync(first.Id, t0.AddMinutes(5), placeholder: -2);
        var onlyOfSecond = await InsertLegacyRowAsync(second.Id, t0.AddMinutes(1), placeholder: -3);

        await using (var scope = Factory.Services.CreateAsyncScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
            await db.Database.ExecuteSqlRawAsync(AddStockMovementSequence.BackfillSql);
        }

        Assert.Equal(1L, await SequenceOfAsync(olderOfFirst));
        Assert.Equal(2L, await SequenceOfAsync(newerOfFirst));
        Assert.Equal(1L, await SequenceOfAsync(onlyOfSecond));
        Assert.Equal(2L, (await StockLevelTestData.ReloadStockLevelAsync(Factory.Services, first.Id)).LastMovementSequence);
        Assert.Equal(1L, (await StockLevelTestData.ReloadStockLevelAsync(Factory.Services, second.Id)).LastMovementSequence);
        Assert.Equal(0L, (await StockLevelTestData.ReloadStockLevelAsync(Factory.Services, empty.Id)).LastMovementSequence);
    }

    private async Task<StockMovementId> InsertLegacyRowAsync(StockLevelId stockLevelId, DateTimeOffset createdOn, long placeholder)
    {
        var id = StockMovementId.New();
        await using var scope = Factory.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        await db.Database.ExecuteSqlAsync($"""
            INSERT INTO "Inventory"."StockMovements"
                ("Id", "StockLevelId", "Sequence", "Type", "Quantity", "QuantityOnHandAfter", "CreatedOn")
            VALUES ({id.Value}, {stockLevelId.Value}, {placeholder}, 'Received', 1, 11, {createdOn})
            """);
        return id;
    }

    private async Task<long> SequenceOfAsync(StockMovementId id)
    {
        await using var scope = Factory.Services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        return await db.StockMovements.AsNoTracking().Where(m => m.Id == id).Select(m => m.Sequence).SingleAsync();
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

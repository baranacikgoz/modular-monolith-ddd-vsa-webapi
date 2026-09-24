using Common.Application.Persistence;
using Common.Application.Persistence.Inbox;
using Inventory.Application.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Outbox;
using Products.Application.Persistence;

namespace Host.Tests;

// Runs the real job against the all-modules host: every module DbContext contributes an IInboxCleanupTarget, so one
// run must prune the ProcessedMessages table of each module schema and keep the rows inside the retention window.
[Collection("Host")]
public class InboxCleanupJobTests(HostTestFactory factory)
{
    [Fact]
    public async Task ExecuteAsync_PrunesExpiredRowsInEveryModuleSchema_KeepsFreshOnes()
    {
        _ = factory.CreateClient();
        var now = DateTimeOffset.UtcNow;
        var consumer = $"JobProbe{Guid.NewGuid():N}";

        await using (var seed = factory.Services.CreateAsyncScope())
        {
            var products = (DbContext)seed.ServiceProvider.GetRequiredService<IProductsDbContext>();
            var inventory = (DbContext)seed.ServiceProvider.GetRequiredService<IInventoryDbContext>();

            // CachingOptions.IdempotencyKeyDuration is the retention (one day in caching.json).
            await InsertAsync(products, ProductsInsert, consumer, now.AddDays(-3));
            await InsertAsync(products, ProductsInsert, consumer, now);
            await InsertAsync(inventory, InventoryInsert, consumer, now.AddDays(-3));
            await InsertAsync(inventory, InventoryInsert, consumer, now);
        }

        await using (var run = factory.Services.CreateAsyncScope())
        {
            var job = ActivatorUtilities.CreateInstance<InboxCleanupJob>(run.ServiceProvider);
            await job.ExecuteAsync(CancellationToken.None);
        }

        await using var verify = factory.Services.CreateAsyncScope();
        Assert.Single(await ProcessedOnAsync(verify.ServiceProvider.GetRequiredService<IProductsDbContext>(), consumer));
        Assert.Single(await ProcessedOnAsync(verify.ServiceProvider.GetRequiredService<IInventoryDbContext>(), consumer));
    }

    private static Task<List<DateTimeOffset>> ProcessedOnAsync(IDbContext db, string consumer)
        => db.Set<ProcessedMessage>().AsNoTracking()
            .Where(m => m.ConsumerName == consumer)
            .Select(m => m.ProcessedOn)
            .ToListAsync();

    private const string ProductsInsert =
        "INSERT INTO \"Products\".\"ProcessedMessages\" (\"ConsumerName\", \"MessageId\", \"ProcessedOn\", \"CreatedOn\") VALUES ({0}, {1}, {2}, {2})";

    private const string InventoryInsert =
        "INSERT INTO \"Inventory\".\"ProcessedMessages\" (\"ConsumerName\", \"MessageId\", \"ProcessedOn\", \"CreatedOn\") VALUES ({0}, {1}, {2}, {2})";

    private static Task<int> InsertAsync(DbContext db, string sql, string consumer, DateTimeOffset processedOn)
        => db.Database.ExecuteSqlRawAsync(sql, consumer, DefaultIdType.CreateVersion7(), processedOn);
}

using System.Collections.Concurrent;
using System.Data.Common;
using Common.Application.Auth;
using Common.Application.EventBus;
using Common.Application.Storage;
using Common.Domain.StronglyTypedIds;
using Common.Infrastructure.EventBus;
using Common.Infrastructure.Persistence;
using Common.IntegrationEvents;
using Inventory.Application.BulkAdjustments;
using Inventory.Application.Persistence;
using Inventory.Domain.StockLevels.DomainEvents.v1;
using Inventory.Infrastructure.BulkAdjustments;
using Inventory.Infrastructure.Persistence;
using Inventory.Infrastructure.StockPushes;
using Inventory.Tests.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace Inventory.Tests.StockPushes;

/// <summary>
///     Every changed quantity of a save marks its stock level for the warehouse push. A chunk of a bulk adjustment changes
///     hundreds of quantities in one save, so what one mark costs in statements is what the chunk costs.
/// </summary>
public class StockPushMarkCostTests(IntegrationTestWebAppFactory factory) : BulkAdjustIntegrationTest(factory)
{
    private const int ChunkRows = 200;

    private sealed class CommandCounter : DbCommandInterceptor
    {
        private readonly ConcurrentBag<string> _commands = [];

        public IReadOnlyCollection<string> Commands => _commands;

        public override InterceptionResult<DbDataReader> ReaderExecuting(
            DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
        {
            _commands.Add(command.CommandText);
            return result;
        }

        public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(
            DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result,
            CancellationToken cancellationToken = default)
        {
            _commands.Add(command.CommandText);
            return ValueTask.FromResult(result);
        }

        public override InterceptionResult<int> NonQueryExecuting(
            DbCommand command, CommandEventData eventData, InterceptionResult<int> result)
        {
            _commands.Add(command.CommandText);
            return result;
        }

        public override ValueTask<InterceptionResult<int>> NonQueryExecutingAsync(
            DbCommand command, CommandEventData eventData, InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            _commands.Add(command.CommandText);
            return ValueTask.FromResult(result);
        }
    }

    [Fact]
    public async Task BulkAdjustChunk_OfTwoHundredRowsOfOneConnectedStore_IssuesNoMoreThanOneMarkStatementPerRow()
    {
        var store = await StockPushTestData.SeedStoreAsync(Factory.Services, stockLevels: ChunkRows);
        var job = await QueueJobAsync(Workbooks.Build([.. store.StockLevels.Select(s => (object[])[s.ProductId, 1])]));
        var counter = new CommandCounter();

        await RunWithCountedCommandsAsync(job.Id, counter);

        Assert.Equal(ChunkRows, (await StockPushTestData.DirtyItemsAsync(Factory.Services)).Count);
        var markStatements = counter.Commands
            .Count(text => text.Contains("StockPushDirtyItems", StringComparison.Ordinal)
                           || text.Contains("WarehouseConnections", StringComparison.Ordinal));
        Assert.True(
            markStatements <= ChunkRows,
            $"A chunk of {ChunkRows} rows issued {markStatements} statements for the push marks and their connection lookups.");
    }

    private async Task<StockBulkAdjustJob> QueueJobAsync(byte[] workbook)
    {
        var jobId = StockBulkAdjustJobId.New();
        var objectKey = $"inventory/bulk-adjustments/{jobId}.xlsx";
        using var content = new MemoryStream(workbook);
        await PrivateStore.UploadAsync(new UploadObjectRequest(objectKey, content, "application/octet-stream"), CancellationToken.None);

        var job = StockBulkAdjustJob.Queue(
            jobId, new ApplicationUserId(Guid.NewGuid()), DateTimeOffset.UtcNow, Guid.NewGuid().ToString("N"), "adjustments.xlsx",
            objectKey, "Cycle count");
        Db.StockBulkAdjustJobs.Add(job);
        await Db.SaveChangesAsync();
        Db.ChangeTracker.Clear();
        return job;
    }

    /// <summary>
    ///     One consumer message against a context of its own that reports every statement it executes. The mark handler is the
    ///     real one, resolved against that same context (the dispatcher gets a provider that holds just it), so its statements
    ///     are counted and run in the save's transaction exactly as in the host.
    /// </summary>
    private async Task RunWithCountedCommandsAsync(StockBulkAdjustJobId jobId, CommandCounter counter)
    {
        using var scope = Factory.Services.CreateScope();
        var services = scope.ServiceProvider;
        var options = new DbContextOptionsBuilder<InventoryDbContext>(services.GetRequiredService<DbContextOptions<InventoryDbContext>>())
            .AddInterceptors(counter)
            .Options;
        var outbox = new IntegrationEventOutbox();
        InventoryDbContext? dbContext = null;
        await using var handlers = new ServiceCollection()
            .AddSingleton<IInventoryDbContext>(_ => dbContext!)
            .AddSingleton<IIntegrationEventOutbox>(outbox)
            .AddSingleton(TimeProvider.System)
            .AddSingleton<IEventHandler<V1StockQuantityChangedDomainEvent>, MarkStockPushDirtyOnStockQuantityChangedHandler>()
            .BuildServiceProvider();
        dbContext = new InventoryDbContext(
            options, TimeProvider.System, services.GetRequiredService<ICurrentUser>(),
            services.GetRequiredService<ILogger<BaseDbContext>>(),
            new EventDispatcher(handlers, NullLogger<EventDispatcher>.Instance), outbox);
        await using var context = dbContext;

        var runner = new StockBulkAdjustJobRunner(
            context,
            PrivateStore,
            outbox,
            TestInventoryOptions.Wrapped(),
            TestInventoryOptions.BulkImport(),
            services.GetRequiredService<IBackgroundUserContext>(),
            TimeProvider.System,
            NullLogger<StockBulkAdjustJobRunner>.Instance);

        await runner.RunAsync(jobId, CancellationToken.None);
    }
}

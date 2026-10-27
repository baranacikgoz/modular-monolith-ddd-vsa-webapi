using System.Collections.Concurrent;
using System.Data.Common;
using Common.Application.Auth;
using Common.Infrastructure.EventBus;
using Common.Infrastructure.Persistence;
using Common.Tests;
using Inventory.Infrastructure.IntegrationEventHandlers;
using Inventory.Infrastructure.Persistence;
using Inventory.Infrastructure.StockPushes;
using Inventory.Tests.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Inventory.Tests.Persistence;

/// <summary>
///     A statement that reaches Postgres as raw SQL carries a tag like every LINQ query does, so a slow or blocked one in
///     <c>pg_stat_activity</c> or the slow query log names the code that issued it. Each statement is run against a context
///     that records the command text, and the data it returns shows the tag did not change what the statement does.
/// </summary>
[Collection("IntegrationTestCollection")]
public class RawSqlTagTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private sealed class CommandRecorder : DbCommandInterceptor
    {
        private readonly ConcurrentQueue<string> _commands = [];

        public IReadOnlyCollection<string> Commands => _commands;

        public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(
            DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result,
            CancellationToken cancellationToken = default)
        {
            _commands.Enqueue(command.CommandText);
            return ValueTask.FromResult(result);
        }
    }

    private InventoryDbContext NewContext(CommandRecorder recorder)
    {
        var services = Scope.ServiceProvider;
        var options = new DbContextOptionsBuilder<InventoryDbContext>(services.GetRequiredService<DbContextOptions<InventoryDbContext>>())
            .AddInterceptors(recorder)
            .Options;

        return new InventoryDbContext(
            options, services.GetRequiredService<TimeProvider>(), services.GetRequiredService<ICurrentUser>(),
            services.GetRequiredService<ILogger<BaseDbContext>>(), services.GetRequiredService<EventDispatcher>(),
            services.GetRequiredService<IntegrationEventOutbox>());
    }

    [Fact]
    public async Task LoadForUpdateAsync_RunsTaggedAndStillReturnsTheLockedStockLevel()
    {
        var store = await StockPushTestData.SeedStoreAsync(Factory.Services);
        var stockLevel = store.StockLevels.Single();
        var recorder = new CommandRecorder();
        await using var db = NewContext(recorder);
        await using var transaction = await db.Database.BeginTransactionAsync();

        var loaded = await StockLevelRowLock.LoadForUpdateAsync(db, stockLevel.ProductId, CancellationToken.None);

        Assert.False(loaded.IsFailure);
        Assert.Equal(stockLevel.Id, loaded.Value!.Id);
        var command = Assert.Single(recorder.Commands, c => c.Contains("FOR UPDATE", StringComparison.Ordinal));
        Assert.StartsWith($"-- ({nameof(StockLevelRowLock.LoadForUpdateAsync)})-({stockLevel.ProductId})", command, StringComparison.Ordinal);
    }

    [Fact]
    public async Task MarkForActiveConnectionAsync_RunsTaggedAndStillMarksTheStockLevel()
    {
        var store = await StockPushTestData.SeedStoreAsync(Factory.Services);
        var stockLevel = store.StockLevels.Single();
        var recorder = new CommandRecorder();
        await using var db = NewContext(recorder);

        var connections = await StockPushMarks.MarkForActiveConnectionAsync(
            db, stockLevel.Id, stockLevel.ProductId, DateTimeOffset.UtcNow, CancellationToken.None);

        Assert.Equal([store.Connection!.Id.Value], connections);
        Assert.Single(await StockPushTestData.DirtyItemsAsync(Factory.Services));
        var command = Assert.Single(recorder.Commands);
        Assert.StartsWith($"-- ({nameof(StockPushMarks.MarkForActiveConnectionAsync)})-({stockLevel.Id})", command, StringComparison.Ordinal);
    }

    [Fact]
    public async Task MarkStorePageAsync_RunsTaggedAndStillMarksThePage()
    {
        var store = await StockPushTestData.SeedStoreAsync(Factory.Services, stockLevels: 3);
        var recorder = new CommandRecorder();
        await using var db = NewContext(recorder);

        var page = await StockPushMarks.MarkStorePageAsync(
            db, store.StoreId, after: Guid.Empty, pageSize: 10, DateTimeOffset.UtcNow, CancellationToken.None);

        Assert.Equal(3, page.Count);
        Assert.Equal(3, (await StockPushTestData.DirtyItemsAsync(Factory.Services)).Count);
        var command = Assert.Single(recorder.Commands);
        Assert.StartsWith($"-- ({nameof(StockPushMarks.MarkStorePageAsync)})-({store.StoreId})", command, StringComparison.Ordinal);
    }
}

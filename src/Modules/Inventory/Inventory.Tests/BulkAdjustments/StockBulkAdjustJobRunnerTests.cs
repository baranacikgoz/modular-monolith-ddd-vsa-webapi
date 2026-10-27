using Common.Application.Auth;
using Common.Application.EventBus;
using Common.Application.Jobs;
using Common.Application.Options;
using Common.Application.Storage;
using Common.Domain.StronglyTypedIds;
using Common.IntegrationEvents;
using Inventory.Application.BulkAdjustments;
using Inventory.Application.Persistence;
using Inventory.Domain.StockLevels;
using Inventory.Domain.StockLevels.Errors;
using Inventory.Infrastructure.BulkAdjustments;
using Inventory.Tests.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;

namespace Inventory.Tests.BulkAdjustments;

/// <summary>Slice tests run with the outbox processor off, so these run the job the way the queue consumer would, in a DI
/// scope of their own, against a workbook held by the in-memory object store.</summary>
public class StockBulkAdjustJobRunnerTests(IntegrationTestWebAppFactory factory) : BulkAdjustIntegrationTest(factory)
{
    private const string Reason = "Cycle count";

    private static readonly ApplicationUserId Requester = new(Guid.NewGuid());

    private IServiceProvider Root => Factory.Services;

    private async Task<StockBulkAdjustJob> QueueJobAsync(byte[] workbook)
    {
        var jobId = StockBulkAdjustJobId.New();
        var objectKey = $"inventory/bulk-adjustments/{jobId}.xlsx";
        using var content = new MemoryStream(workbook);
        await PrivateStore.UploadAsync(new UploadObjectRequest(objectKey, content, "application/octet-stream"), CancellationToken.None);

        var job = StockBulkAdjustJob.Queue(
            jobId, Requester, DateTimeOffset.UtcNow, Guid.NewGuid().ToString("N"), "adjustments.xlsx", objectKey, Reason);
        Db.StockBulkAdjustJobs.Add(job);
        await Db.SaveChangesAsync();
        Db.ChangeTracker.Clear();
        return job;
    }

    /// <summary>One consumer message: a fresh DI scope, the way a queue delivery gets one. Options default to the host's own.</summary>
    private async Task RunMessageAsync(
        StockBulkAdjustJobId jobId, IOptions<InventoryOptions>? inventory = null, IOptions<BulkImportOptions>? bulkImport = null,
        IPrivateObjectStore? objectStore = null, Func<IInventoryDbContext, IInventoryDbContext>? wrapDbContext = null)
    {
        using var scope = Root.CreateScope();
        var services = scope.ServiceProvider;
        var dbContext = services.GetRequiredService<IInventoryDbContext>();
        var runner = new StockBulkAdjustJobRunner(
            wrapDbContext?.Invoke(dbContext) ?? dbContext,
            objectStore ?? PrivateStore,
            services.GetRequiredService<IIntegrationEventOutbox>(),
            inventory ?? TestInventoryOptions.Wrapped(),
            bulkImport ?? TestInventoryOptions.BulkImport(),
            services.GetRequiredService<IBackgroundUserContext>(),
            TimeProvider.System,
            NullLogger<StockBulkAdjustJobRunner>.Instance);

        await runner.RunAsync(jobId, CancellationToken.None);
    }

    private async Task<StockBulkAdjustJob> ReadJobAsync(StockBulkAdjustJobId jobId)
    {
        using var scope = Root.CreateScope();
        return await scope.ServiceProvider.GetRequiredService<IInventoryDbContext>().StockBulkAdjustJobs
            .AsNoTracking().SingleAsync(j => j.Id == jobId);
    }

    private async Task<int> QuantityOfAsync(StockLevel stockLevel)
    {
        return (await StockLevelTestData.ReloadStockLevelAsync(Root, stockLevel.Id)).QuantityOnHand;
    }

    [Fact]
    public async Task Run_RowsOfKnownProducts_AdjustsEachThroughTheAggregateAndAttributesTheLedgerToTheRequester()
    {
        var first = await StockLevelTestData.SeedStockLevelAsync(Root, quantityOnHand: 10);
        var second = await StockLevelTestData.SeedStockLevelAsync(Root, quantityOnHand: 10);
        var third = await StockLevelTestData.SeedStockLevelAsync(Root, quantityOnHand: 10);
        var job = await QueueJobAsync(Workbooks.Build([first.ProductId, 5], [second.ProductId, -3], [third.ProductId, 1]));

        await RunMessageAsync(job.Id);

        var finished = await ReadJobAsync(job.Id);
        Assert.Equal(JobStatus.Succeeded, finished.Status);
        Assert.Null(finished.FailureKey);
        Assert.Equal(3, finished.TotalRows);
        Assert.Equal(3, finished.Progress);
        Assert.Equal(3, finished.RowsAdjusted);
        Assert.Equal(0, finished.RowsRejected);
        Assert.NotNull(finished.FinishedOn);
        Assert.Equal(string.Empty, finished.ObjectKey);
        Assert.Empty(PrivateStore.Keys);

        int[] quantities = [await QuantityOfAsync(first), await QuantityOfAsync(second), await QuantityOfAsync(third)];
        Assert.Equal([15, 7, 11], quantities);
        var movement = Assert.Single(await StockLevelTestData.LedgerOfAsync(Root, second.Id));
        Assert.Equal(StockMovementType.Adjusted, movement.Type);
        Assert.Equal(-3, movement.Quantity);
        Assert.Equal(7, movement.QuantityOnHandAfter);
        Assert.Equal(Reason, movement.Reason);
        Assert.Equal(Requester, movement.CreatedBy);
    }

    [Fact]
    public async Task Run_MixedRows_ReportsTheRejectedRowsWithoutStoppingTheFile()
    {
        var stocked = await StockLevelTestData.SeedStockLevelAsync(Root, quantityOnHand: 10);
        var job = await QueueJobAsync(Workbooks.Build(
            [Guid.NewGuid(), 1],
            ["not-a-guid", 1],
            ["", 1],
            [stocked.ProductId, 0],
            [stocked.ProductId, -50],
            [stocked.ProductId, 4]));

        await RunMessageAsync(job.Id);

        var finished = await ReadJobAsync(job.Id);
        Assert.Equal(JobStatus.Succeeded, finished.Status);
        Assert.Equal(6, finished.Progress);
        Assert.Equal(1, finished.RowsAdjusted);
        Assert.Equal(5, finished.RowsRejected);
        Assert.Equal(
            [
                new StockBulkAdjustRowError(2, StockBulkAdjustRowErrors.UnknownProduct.Key),
                new StockBulkAdjustRowError(3, StockBulkAdjustRowErrors.InvalidProductId.Key),
                new StockBulkAdjustRowError(4, StockBulkAdjustRowErrors.MissingProductId.Key),
                new StockBulkAdjustRowError(5, StockBulkAdjustRowErrors.InvalidQuantity.Key),
                new StockBulkAdjustRowError(6, StockLevelErrors.InsufficientStock.Key)
            ],
            finished.RowErrors);
        Assert.Equal(14, await QuantityOfAsync(stocked));
        Assert.Single(await StockLevelTestData.LedgerOfAsync(Root, stocked.Id));
    }

    [Fact]
    public async Task Run_BatchSaved_ResetsTheCountOfRequeuesWithoutProgress()
    {
        var stocked = await StockLevelTestData.SeedStockLevelAsync(Root, quantityOnHand: 10);
        var job = await QueueJobAsync(Workbooks.Build([stocked.ProductId, 1]));
        await Db.StockBulkAdjustJobs.Where(j => j.Id == job.Id).ExecuteUpdateAsync(s => s.SetProperty(j => j.RequeueCount, 2));

        await RunMessageAsync(job.Id);

        Assert.Equal(0, (await ReadJobAsync(job.Id)).RequeueCount);
    }

    [Fact]
    public async Task Run_SameProductTwiceInOneFile_AppendsTwoLedgerRows()
    {
        var stocked = await StockLevelTestData.SeedStockLevelAsync(Root, quantityOnHand: 10);
        var job = await QueueJobAsync(Workbooks.Build([stocked.ProductId, 5], [stocked.ProductId, 5]));

        await RunMessageAsync(job.Id);

        Assert.Equal(20, await QuantityOfAsync(stocked));
        // Both rows of one save share a timestamp, so the running quantity is what tells them apart.
        Assert.Equal([15, 20], (await StockLevelTestData.LedgerOfAsync(Root, stocked.Id)).Select(m => m.QuantityOnHandAfter).Order());
    }

    [Fact]
    public async Task Run_RowsOfOneStoreWithAConnection_MarkDirtyAndQueueOnePushForTheConnection()
    {
        var store = await StockPushTestData.SeedStoreAsync(Root, stockLevels: 3);
        var job = await QueueJobAsync(Workbooks.Build(
            [store.StockLevels[0].ProductId, 1], [store.StockLevels[1].ProductId, 1], [store.StockLevels[2].ProductId, 1]));

        await RunMessageAsync(job.Id);

        Assert.Equal(3, (await StockPushTestData.DirtyItemsAsync(Root)).Count);
        var due = Assert.Single(await StockLevelTestData.QueuedEventsAsync<StockPushDueIntegrationEvent>(Root));
        Assert.Equal(store.Connection!.Id.Value, due.ConnectionId);
    }

    [Fact]
    public async Task Run_FileBiggerThanOneMessage_StoresItsCursorAndQueuesItselfUntilTheEnd()
    {
        var stockLevels = new List<StockLevel>();
        for (var i = 0; i < 5; i++)
        {
            stockLevels.Add(await StockLevelTestData.SeedStockLevelAsync(Root, quantityOnHand: 0));
        }

        var job = await QueueJobAsync(Workbooks.Build([.. stockLevels.Select(s => (object[])[s.ProductId, 1])]));
        var inventory = TestInventoryOptions.Wrapped(o => o.BulkAdjustBatchesPerMessage = 1);
        var bulkImport = TestInventoryOptions.BulkImport(o => o.SaveChunkSize = 2);

        await RunMessageAsync(job.Id, inventory, bulkImport);

        var afterFirst = await ReadJobAsync(job.Id);
        Assert.Equal(JobStatus.Queued, afterFirst.Status);
        Assert.Equal(3, afterFirst.LastProcessedRow);
        Assert.Equal(5, afterFirst.TotalRows);
        Assert.Equal(2, afterFirst.Progress);
        Assert.Equal(2, afterFirst.RowsAdjusted);
        var continuations = (await OutboxEventsAsync()).OfType<StockBulkAdjustRequestedIntegrationEvent>().Where(e => e.JobId == job.Id.Value);
        Assert.Single(continuations);

        await RunMessageAsync(job.Id, inventory, bulkImport);

        var afterSecond = await ReadJobAsync(job.Id);
        Assert.Equal(JobStatus.Queued, afterSecond.Status);
        Assert.Equal(5, afterSecond.LastProcessedRow);

        await RunMessageAsync(job.Id, inventory, bulkImport);

        var afterThird = await ReadJobAsync(job.Id);
        Assert.Equal(JobStatus.Succeeded, afterThird.Status);
        Assert.Equal(5, afterThird.RowsAdjusted);
        Assert.Equal(5, afterThird.Progress);
        foreach (var stockLevel in stockLevels)
        {
            Assert.Equal(1, await QuantityOfAsync(stockLevel));
        }

        Assert.Equal(2,(await OutboxEventsAsync()).OfType<StockBulkAdjustRequestedIntegrationEvent>().Count(e => e.JobId == job.Id.Value));
    }

    [Fact]
    public async Task Run_MoreRejectedRowsThanTheStoredCap_KeepsTheFirstOnesAndCountsAll()
    {
        var job = await QueueJobAsync(Workbooks.Build(["", 1], ["", 1], ["", 1], ["", 1]));

        await RunMessageAsync(job.Id, TestInventoryOptions.Wrapped(o => o.BulkAdjustMaxStoredRowErrors = 2));

        var finished = await ReadJobAsync(job.Id);
        Assert.Equal(4, finished.RowsRejected);
        Assert.Equal([2, 3], finished.RowErrors.Select(e => e.RowNumber));
    }

    [Fact]
    public async Task Run_FileOverTheRowCap_FailsWithTooManyRowsAndAdjustsNothing()
    {
        var stocked = await StockLevelTestData.SeedStockLevelAsync(Root, quantityOnHand: 10);
        var job = await QueueJobAsync(Workbooks.Build([stocked.ProductId, 1], [stocked.ProductId, 1], [stocked.ProductId, 1]));

        await RunMessageAsync(job.Id, bulkImport: TestInventoryOptions.BulkImport(o => o.MaxRows = 2));

        var finished = await ReadJobAsync(job.Id);
        Assert.Equal(JobStatus.Failed, finished.Status);
        Assert.Equal(StockBulkAdjustErrors.TooManyRows.Key, finished.FailureKey);
        Assert.Equal(10, await QuantityOfAsync(stocked));
        Assert.Empty(PrivateStore.Keys);
    }

    [Fact]
    public async Task Run_FileThatIsNotAWorkbook_FailsWithFileUnreadable()
    {
        var job = await QueueJobAsync("this is not a workbook"u8.ToArray());

        await RunMessageAsync(job.Id);

        var finished = await ReadJobAsync(job.Id);
        Assert.Equal(JobStatus.Failed, finished.Status);
        Assert.Equal(StockBulkAdjustErrors.FileUnreadable.Key, finished.FailureKey);
    }

    [Fact]
    public async Task Run_ConcurrentWriterOnAStockLevelDuringABatch_RejectsThatBatchsRowsAndGoesOnWithTheNext()
    {
        var stockLevels = new List<StockLevel>();
        for (var i = 0; i < 3; i++)
        {
            stockLevels.Add(await StockLevelTestData.SeedStockLevelAsync(Root, quantityOnHand: 10));
        }

        var job = await QueueJobAsync(Workbooks.Build([.. stockLevels.Select(s => (object[])[s.ProductId, 1])]));
        var touched = stockLevels[0].Id;

        // Another request edits a stock level of the first batch between the runner's read and its first save.
        async Task ConcurrentWriteAsync()
        {
            using var other = Root.CreateScope();
            await other.ServiceProvider.GetRequiredService<IInventoryDbContext>().StockLevels
                .Where(s => s.Id == touched)
                .ExecuteUpdateAsync(s => s.SetProperty(x => x.Version, x => x.Version + 1));
        }

        await RunMessageAsync(
            job.Id,
            bulkImport: TestInventoryOptions.BulkImport(o => o.SaveChunkSize = 2),
            wrapDbContext: inner => new ConflictingInventoryDbContext(inner, ConcurrentWriteAsync));

        var finished = await ReadJobAsync(job.Id);
        Assert.Equal(JobStatus.Succeeded, finished.Status);
        Assert.Equal(2, finished.RowsRejected);
        Assert.Equal(
            [
                new StockBulkAdjustRowError(2, StockBulkAdjustErrors.ConcurrentUpdate.Key),
                new StockBulkAdjustRowError(3, StockBulkAdjustErrors.ConcurrentUpdate.Key)
            ],
            finished.RowErrors);
        Assert.Equal(1, finished.RowsAdjusted);
        int[] quantities = [await QuantityOfAsync(stockLevels[0]), await QuantityOfAsync(stockLevels[1]), await QuantityOfAsync(stockLevels[2])];
        Assert.Equal([10, 10, 11], quantities);
    }

    [Fact]
    public async Task Run_JobThatIsNotQueued_DoesNothing()
    {
        var stocked = await StockLevelTestData.SeedStockLevelAsync(Root, quantityOnHand: 10);
        var job = await QueueJobAsync(Workbooks.Build([stocked.ProductId, 1]));
        await Db.StockBulkAdjustJobs.Where(j => j.Id == job.Id).ExecuteUpdateAsync(s => s.SetProperty(j => j.Status, JobStatus.Running));

        await RunMessageAsync(job.Id);

        Assert.Equal(JobStatus.Running, (await ReadJobAsync(job.Id)).Status);
        Assert.Equal(10, await QuantityOfAsync(stocked));
    }

    [Fact]
    public async Task Run_CancelledMidFile_HandsTheJobBackWithAMessageOfItsOwnAndRethrows()
    {
        var job = await QueueJobAsync(Workbooks.Build([Guid.NewGuid(), 1]));
        var cancelling = Substitute.For<IPrivateObjectStore>();
        cancelling.DownloadAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns<Task<Stream>>(_ => throw new OperationCanceledException());

        await Assert.ThrowsAsync<OperationCanceledException>(() => RunMessageAsync(job.Id, objectStore: cancelling));

        Assert.Equal(JobStatus.Queued, (await ReadJobAsync(job.Id)).Status);
        Assert.Single((await OutboxEventsAsync()).OfType<StockBulkAdjustRequestedIntegrationEvent>(), e => e.JobId == job.Id.Value);
    }
}

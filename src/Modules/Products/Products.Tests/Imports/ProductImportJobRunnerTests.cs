using Common.Application.Auth;
using Common.Application.EventBus;
using Common.Application.Jobs;
using Common.Application.Options;
using Common.Application.Storage;
using Common.Domain.StronglyTypedIds;
using Common.IntegrationEvents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Products.Application.Imports;
using Products.Application.Persistence;
using Products.Domain.Stores;
using Products.Infrastructure.Imports;
using Products.Tests.Support;
using Xunit;

namespace Products.Tests.Imports;

/// <summary>Slice tests run with the outbox processor off, so these run the job the way the queue consumer would, in a DI
/// scope of their own, against a workbook held by the in-memory object store.</summary>
public class ProductImportJobRunnerTests(IntegrationTestWebAppFactory factory) : ProductsIntegrationTest(factory)
{
    private static readonly ApplicationUserId Requester = new(Guid.NewGuid());

    private async Task<(ProductImportJob Job, SeededStore Seeded)> QueueJobAsync(
        byte[] workbook, params string[] existingProductNames)
    {
        var seeded = await ProductsSeed.StoreWithProductsAsync(Db, Requester, existingProductNames);
        var jobId = ProductImportJobId.New();
        var objectKey = $"products/imports/{jobId}.xlsx";
        using var content = new MemoryStream(workbook);
        await PrivateStore.UploadAsync(new UploadObjectRequest(objectKey, content, "application/octet-stream"), CancellationToken.None);

        var job = ProductImportJob.Queue(
            jobId, Requester, DateTimeOffset.UtcNow, seeded.Store.Id, seeded.Template.Id, Guid.NewGuid().ToString("N"), "products.xlsx", objectKey);
        Db.ImportJobs.Add(job);
        await Db.SaveChangesAsync();
        Db.ChangeTracker.Clear();
        return (job, seeded);
    }

    /// <summary>One consumer message: a fresh DI scope, the way a queue delivery gets one. Options default to the host's own.</summary>
    private async Task RunMessageAsync(
        ProductImportJobId jobId, IOptions<ProductsOptions>? products = null, IOptions<BulkImportOptions>? bulkImport = null,
        IPrivateObjectStore? objectStore = null, Func<IProductsDbContext, IProductsDbContext>? wrapDbContext = null)
    {
        using var scope = Factory.Services.CreateScope();
        var services = scope.ServiceProvider;
        var dbContext = services.GetRequiredService<IProductsDbContext>();
        var runner = new ProductImportJobRunner(
            wrapDbContext?.Invoke(dbContext) ?? dbContext,
            objectStore ?? PrivateStore,
            services.GetRequiredService<IIntegrationEventOutbox>(),
            products ?? TestOptions.Products(),
            bulkImport ?? TestOptions.BulkImport(),
            services.GetRequiredService<IBackgroundUserContext>(),
            TimeProvider.System,
            NullLogger<ProductImportJobRunner>.Instance);

        await runner.RunAsync(jobId, CancellationToken.None);
    }

    private async Task<ProductImportJob> ReadJobAsync(ProductImportJobId jobId)
    {
        using var scope = Factory.Services.CreateScope();
        return await scope.ServiceProvider.GetRequiredService<IProductsDbContext>().ImportJobs
            .AsNoTracking().SingleAsync(j => j.Id == jobId);
    }

    private async Task<List<Domain.Products.Product>> ProductsOfAsync(StoreId storeId)
    {
        using var scope = Factory.Services.CreateScope();
        return await scope.ServiceProvider.GetRequiredService<IProductsDbContext>().Products
            .AsNoTracking().Where(p => p.StoreId == storeId).OrderBy(p => p.Name).ToListAsync();
    }

    [Fact]
    public async Task Run_NewRows_CreatesTheProductsAttributedToTheRequesterAndDeletesTheUpload()
    {
        var (job, seeded) = await QueueJobAsync(Workbooks.Build(
            ["Desk lamp", "LED", 5, 49.9m],
            ["Office chair", "Mesh", 2, 120m],
            ["Bookshelf", "Oak", 1, 300m]));

        await RunMessageAsync(job.Id);

        var finished = await ReadJobAsync(job.Id);
        Assert.Equal(JobStatus.Succeeded, finished.Status);
        Assert.Null(finished.FailureKey);
        Assert.Equal(3, finished.TotalRows);
        Assert.Equal(3, finished.Progress);
        Assert.Equal(3, finished.ProductsCreated);
        Assert.Equal(0, finished.RowsRejected);
        Assert.NotNull(finished.FinishedOn);
        Assert.Equal(string.Empty, finished.ObjectKey);
        Assert.Empty(PrivateStore.Keys);

        var products = await ProductsOfAsync(seeded.Store.Id);
        Assert.Equal(["Bookshelf", "Desk lamp", "Office chair"], products.Select(p => p.Name));
        var lamp = products.Single(p => p.Name == "Desk lamp");
        Assert.Equal("LED", lamp.Description);
        Assert.Equal(5, lamp.Quantity);
        Assert.Equal(49.9m, lamp.Price);
        Assert.Equal(seeded.Template.Id, lamp.ProductTemplateId);
        Assert.All(products, p => Assert.Equal(Requester, p.CreatedBy));

        var changed = (await OutboxEventsAsync()).OfType<ProductChangedIntegrationEvent>().Where(e => e.StoreId == seeded.Store.Id.Value);
        Assert.Equal(3, changed.Count());
    }

    [Fact]
    public async Task Run_MixedRows_UpsertsByNameAndReportsTheRejectedRowsWithoutStoppingTheFile()
    {
        var (job, seeded) = await QueueJobAsync(
            Workbooks.Build(
                ["Desk lamp", "", 7, 10m],
                ["Sofa", "", 5, 10m],
                ["", "no name", 1, 1m],
                ["Chair", "", 0, 5m],
                ["Table", "", 3, "abc"],
                ["Table", "", 3, 30m],
                ["Table", "", 4, 30m]),
            "Desk lamp", "Sofa");

        await RunMessageAsync(job.Id);

        var finished = await ReadJobAsync(job.Id);
        Assert.Equal(JobStatus.Succeeded, finished.Status);
        Assert.Equal(7, finished.Progress);
        Assert.Equal(1, finished.ProductsCreated);
        // The lamp changed; the sofa did not; the second Table row updates the Table the first one created.
        Assert.Equal(2, finished.ProductsUpdated);
        Assert.Equal(3, finished.RowsRejected);
        Assert.Equal(
            [new ProductImportRowError(4, "MissingName"), new ProductImportRowError(5, "InvalidQuantity"), new ProductImportRowError(6, "InvalidPrice")],
            finished.RowErrors);

        var products = await ProductsOfAsync(seeded.Store.Id);
        Assert.Equal(["Desk lamp", "Sofa", "Table"], products.Select(p => p.Name));
        Assert.Equal(7, products.Single(p => p.Name == "Desk lamp").Quantity);
        Assert.Equal(4, products.Single(p => p.Name == "Table").Quantity);
    }

    [Fact]
    public async Task Run_StoreNotActive_RejectsEveryNewProductRowWithTheStoreStatusKey()
    {
        var (job, seeded) = await QueueJobAsync(Workbooks.Build(["Desk lamp", "LED", 5, 49.9m], ["Chair", "", 1, 5m]));
        var store = await Db.Stores.SingleAsync(s => s.Id == seeded.Store.Id);
        store.Suspend();
        await Db.SaveChangesAsync();
        Db.ChangeTracker.Clear();

        await RunMessageAsync(job.Id);

        var finished = await ReadJobAsync(job.Id);
        Assert.Equal(JobStatus.Succeeded, finished.Status);
        Assert.Equal(0, finished.ProductsCreated);
        Assert.Equal(2, finished.RowsRejected);
        Assert.All(finished.RowErrors, e => Assert.Equal("StoreIsSuspended", e.Reason));
        Assert.Empty(await ProductsOfAsync(seeded.Store.Id));
    }

    [Fact]
    public async Task Run_FileBiggerThanOneMessage_StoresItsCursorAndQueuesItselfUntilTheEnd()
    {
        var (job, seeded) = await QueueJobAsync(Workbooks.Build(
            ["P1", "", 1, 5m], ["P2", "", 1, 5m], ["P3", "", 1, 5m], ["P4", "", 1, 5m], ["P5", "", 1, 5m]));
        var products = TestOptions.Products(o => o.ImportBatchesPerMessage = 1);
        var bulkImport = TestOptions.BulkImport(o => o.SaveChunkSize = 2);

        await RunMessageAsync(job.Id, products, bulkImport);

        var afterFirst = await ReadJobAsync(job.Id);
        Assert.Equal(JobStatus.Queued, afterFirst.Status);
        Assert.Equal(3, afterFirst.LastProcessedRow);
        Assert.Equal(5, afterFirst.TotalRows);
        Assert.Equal(2, afterFirst.Progress);
        Assert.Equal(2, (await ProductsOfAsync(seeded.Store.Id)).Count);
        var continuations = (await OutboxEventsAsync()).OfType<ProductImportRequestedIntegrationEvent>().Where(e => e.JobId == job.Id.Value);
        Assert.Single(continuations);

        await RunMessageAsync(job.Id, products, bulkImport);

        var afterSecond = await ReadJobAsync(job.Id);
        Assert.Equal(JobStatus.Queued, afterSecond.Status);
        Assert.Equal(5, afterSecond.LastProcessedRow);
        Assert.Equal(4, (await ProductsOfAsync(seeded.Store.Id)).Count);

        await RunMessageAsync(job.Id, products, bulkImport);

        var afterThird = await ReadJobAsync(job.Id);
        Assert.Equal(JobStatus.Succeeded, afterThird.Status);
        Assert.Equal(5, afterThird.ProductsCreated);
        Assert.Equal(5, afterThird.Progress);
        Assert.Equal(["P1", "P2", "P3", "P4", "P5"], (await ProductsOfAsync(seeded.Store.Id)).Select(p => p.Name));
        Assert.Equal(2, (await OutboxEventsAsync()).OfType<ProductImportRequestedIntegrationEvent>().Count(e => e.JobId == job.Id.Value));
    }

    [Fact]
    public async Task Run_MoreRejectedRowsThanTheStoredCap_KeepsTheFirstOnesAndCountsAll()
    {
        var (job, _) = await QueueJobAsync(Workbooks.Build(["", "", 1, 5m], ["", "", 1, 5m], ["", "", 1, 5m], ["", "", 1, 5m]));

        await RunMessageAsync(job.Id, TestOptions.Products(o => o.ImportMaxStoredRowErrors = 2));

        var finished = await ReadJobAsync(job.Id);
        Assert.Equal(4, finished.RowsRejected);
        Assert.Equal([2, 3], finished.RowErrors.Select(e => e.RowNumber));
    }

    [Fact]
    public async Task Run_FileOverTheRowCap_FailsWithTooManyRowsAndImportsNothing()
    {
        var (job, seeded) = await QueueJobAsync(Workbooks.Build(["P1", "", 1, 5m], ["P2", "", 1, 5m], ["P3", "", 1, 5m]));

        await RunMessageAsync(job.Id, bulkImport: TestOptions.BulkImport(o => o.MaxRows = 2));

        var finished = await ReadJobAsync(job.Id);
        Assert.Equal(JobStatus.Failed, finished.Status);
        Assert.Equal(ProductImportErrors.TooManyRows.Key, finished.FailureKey);
        Assert.Empty(await ProductsOfAsync(seeded.Store.Id));
        Assert.Empty(PrivateStore.Keys);
    }

    [Fact]
    public async Task Run_FileThatIsNotAWorkbook_FailsWithFileUnreadable()
    {
        var (job, _) = await QueueJobAsync("this is not a workbook"u8.ToArray());

        await RunMessageAsync(job.Id);

        var finished = await ReadJobAsync(job.Id);
        Assert.Equal(JobStatus.Failed, finished.Status);
        Assert.Equal(ProductImportErrors.FileUnreadable.Key, finished.FailureKey);
    }

    [Fact]
    public async Task Run_StoreThatNoLongerExists_FailsWithNotFound()
    {
        var (job, seeded) = await QueueJobAsync(Workbooks.Build(["P1", "", 1, 5m]));
        await Db.Stores.Where(s => s.Id == seeded.Store.Id).ExecuteDeleteAsync();

        await RunMessageAsync(job.Id);

        var finished = await ReadJobAsync(job.Id);
        Assert.Equal(JobStatus.Failed, finished.Status);
        Assert.Equal("NotFound", finished.FailureKey);
    }

    [Fact]
    public async Task Run_ConcurrentWriterOnTheStoreDuringABatch_RejectsThatBatchsRowsAndGoesOnWithTheNext()
    {
        var (job, seeded) = await QueueJobAsync(Workbooks.Build(
            ["P1", "", 1, 5m], ["P2", "", 1, 5m], ["P3", "", 1, 5m]));
        var storeId = seeded.Store.Id;

        // Another request edits the store between the runner's read and its first save.
        async Task ConcurrentWriteAsync()
        {
            using var other = Factory.Services.CreateScope();
            await other.ServiceProvider.GetRequiredService<IProductsDbContext>().Stores
                .Where(s => s.Id == storeId)
                .ExecuteUpdateAsync(s => s.SetProperty(x => x.Version, x => x.Version + 1));
        }

        await RunMessageAsync(
            job.Id,
            bulkImport: TestOptions.BulkImport(o => o.SaveChunkSize = 2),
            wrapDbContext: inner => new ConflictingProductsDbContext(inner, ConcurrentWriteAsync));

        var finished = await ReadJobAsync(job.Id);
        Assert.Equal(JobStatus.Succeeded, finished.Status);
        Assert.Equal(2, finished.RowsRejected);
        Assert.Equal(
            [new ProductImportRowError(2, ProductImportErrors.ConcurrentUpdate.Key), new ProductImportRowError(3, ProductImportErrors.ConcurrentUpdate.Key)],
            finished.RowErrors);
        Assert.Equal(1, finished.ProductsCreated);
        Assert.Equal(["P3"], (await ProductsOfAsync(storeId)).Select(p => p.Name));
    }

    [Fact]
    public async Task Run_JobThatIsNotQueued_DoesNothing()
    {
        var (job, seeded) = await QueueJobAsync(Workbooks.Build(["P1", "", 1, 5m]));
        await Db.ImportJobs.Where(j => j.Id == job.Id).ExecuteUpdateAsync(s => s.SetProperty(j => j.Status, JobStatus.Running));

        await RunMessageAsync(job.Id);

        Assert.Equal(JobStatus.Running, (await ReadJobAsync(job.Id)).Status);
        Assert.Empty(await ProductsOfAsync(seeded.Store.Id));
    }

    [Fact]
    public async Task Run_CancelledMidFile_HandsTheJobBackWithAMessageOfItsOwnAndRethrows()
    {
        var (job, _) = await QueueJobAsync(Workbooks.Build(["P1", "", 1, 5m]));
        var cancelling = Substitute.For<IPrivateObjectStore>();
        cancelling.DownloadAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns<Task<Stream>>(_ => throw new OperationCanceledException());

        await Assert.ThrowsAsync<OperationCanceledException>(() => RunMessageAsync(job.Id, objectStore: cancelling));

        Assert.Equal(JobStatus.Queued, (await ReadJobAsync(job.Id)).Status);
        Assert.Single((await OutboxEventsAsync()).OfType<ProductImportRequestedIntegrationEvent>(), e => e.JobId == job.Id.Value);
    }

    [Fact]
    public async Task Run_SkuColumn_CreatesProductsThatCarryTheirSku()
    {
        var (job, seeded) = await QueueJobAsync(Workbooks.Build(
            ["Desk lamp", "", 5, 49.9m, "SKU-1"],
            ["Office chair", "", 2, 120m, ""]));

        await RunMessageAsync(job.Id);

        var products = await ProductsOfAsync(seeded.Store.Id);
        Assert.Equal("SKU-1", products.Single(p => p.Name == "Desk lamp").Sku);
        Assert.Null(products.Single(p => p.Name == "Office chair").Sku);
    }

    [Fact]
    public async Task Run_SkuAlreadyUsedInTheStoreOrEarlierInTheFile_RejectsOnlyThoseRows()
    {
        var (job, seeded) = await QueueJobAsync(
            Workbooks.Build(
                ["Table", "", 1, 5m, "SKU-1"],
                ["Shelf", "", 1, 5m, "SKU-2"],
                ["Stool", "", 1, 5m, "SKU-2"],
                ["Rug", "", 1, 5m, "SKU-3"]),
            "Existing");
        Db.Products.Add(Domain.Products.Product.Create(seeded.Store.Id, seeded.Template.Id, "Holder of SKU-1", "d", 1, 5m, "SKU-1"));
        await Db.SaveChangesAsync();
        Db.ChangeTracker.Clear();

        await RunMessageAsync(job.Id);

        var finished = await ReadJobAsync(job.Id);
        Assert.Equal(JobStatus.Succeeded, finished.Status);
        Assert.Equal(2, finished.ProductsCreated);
        Assert.Equal(
            [new ProductImportRowError(2, "SkuAlreadyExists"), new ProductImportRowError(4, "SkuAlreadyExists")],
            finished.RowErrors);
        var names = (await ProductsOfAsync(seeded.Store.Id)).Select(p => p.Name);
        Assert.Equal(["Existing", "Holder of SKU-1", "Rug", "Shelf"], names);
    }

    [Fact]
    public async Task Run_RowNamingADifferentSkuForAnExistingProduct_RejectsTheRow()
    {
        var (job, seeded) = await QueueJobAsync(Workbooks.Build(["Desk lamp", "", 9, 5m, "SKU-9"]));
        Db.Products.Add(Domain.Products.Product.Create(seeded.Store.Id, seeded.Template.Id, "Desk lamp", "d", 1, 5m, "SKU-1"));
        await Db.SaveChangesAsync();
        Db.ChangeTracker.Clear();

        await RunMessageAsync(job.Id);

        var finished = await ReadJobAsync(job.Id);
        Assert.Equal([new ProductImportRowError(2, "SkuDiffers")], finished.RowErrors);
        var lamp = Assert.Single(await ProductsOfAsync(seeded.Store.Id));
        Assert.Equal(1, lamp.Quantity);
    }

    [Fact]
    public async Task Run_ConcurrentWriterTakesASkuDuringABatch_RejectsThatBatchInsteadOfFailingTheJob()
    {
        var (job, seeded) = await QueueJobAsync(Workbooks.Build(["P1", "", 1, 5m, "SKU-1"], ["P2", "", 1, 5m, "SKU-2"]));
        var storeId = seeded.Store.Id;
        var templateId = seeded.Template.Id;

        // Another request adds a product with the same SKU between the runner's read and its save.
        async Task ConcurrentWriteAsync()
        {
            using var other = Factory.Services.CreateScope();
            var otherDb = other.ServiceProvider.GetRequiredService<IProductsDbContext>();
            otherDb.Products.Add(Domain.Products.Product.Create(storeId, templateId, "Concurrent", "d", 1, 5m, "SKU-1"));
            await otherDb.SaveChangesAsync();
        }

        await RunMessageAsync(job.Id, wrapDbContext: inner => new ConflictingProductsDbContext(inner, ConcurrentWriteAsync));

        var finished = await ReadJobAsync(job.Id);
        Assert.Equal(JobStatus.Succeeded, finished.Status);
        Assert.Equal(2, finished.RowsRejected);
        Assert.All(finished.RowErrors, e => Assert.Equal(ProductImportErrors.ConcurrentUpdate.Key, e.Reason));
        Assert.Equal(["Concurrent"], (await ProductsOfAsync(storeId)).Select(p => p.Name));
    }
}

using System.Text;
using Common.Application.Auth;
using Common.Application.Jobs;
using Common.Application.Options;
using Common.Application.Storage;
using Common.Domain.StronglyTypedIds;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Products.Application.Exports;
using Products.Application.Persistence;
using Products.Domain.Stores;
using Products.Infrastructure.Exports;
using Products.Tests.Support;
using Xunit;

namespace Products.Tests.Exports;

public class ProductExportJobRunnerTests(IntegrationTestWebAppFactory factory) : ProductsIntegrationTest(factory)
{
    private async Task<ProductExportJob> QueueJobAsync(StoreId? storeId = null, DateTimeOffset? queuedOn = null)
    {
        var job = ProductExportJob.Queue(DefaultUser, queuedOn ?? DateTimeOffset.UtcNow.AddMinutes(1), storeId);
        Db.ExportJobs.Add(job);
        await Db.SaveChangesAsync();
        Db.ChangeTracker.Clear();
        return job;
    }

    private async Task RunMessageAsync(ProductExportJobId jobId, IOptions<ProductsOptions>? options = null, IPrivateObjectStore? objectStore = null)
    {
        using var scope = Factory.Services.CreateScope();
        var runner = new ProductExportJobRunner(
            scope.ServiceProvider.GetRequiredService<IProductsDbContext>(),
            objectStore ?? PrivateStore,
            options ?? TestOptions.Products(),
            scope.ServiceProvider.GetRequiredService<IBackgroundUserContext>(),
            TimeProvider.System,
            NullLogger<ProductExportJobRunner>.Instance);

        await runner.RunAsync(jobId, CancellationToken.None);
    }

    private async Task<ProductExportJob> ReadJobAsync(ProductExportJobId jobId)
    {
        using var scope = Factory.Services.CreateScope();
        return await scope.ServiceProvider.GetRequiredService<IProductsDbContext>().ExportJobs.AsNoTracking().SingleAsync(j => j.Id == jobId);
    }

    private string ReadCsv(ProductExportJob job)
    {
        var bytes = PrivateStore.Read(job.ObjectKey!);
        Assert.Equal(Encoding.UTF8.GetPreamble(), bytes[..3]);
        return Encoding.UTF8.GetString(bytes, 3, bytes.Length - 3);
    }

    [Fact]
    public async Task Run_StoreFilter_WritesOnlyThatStoresProductsWithCorrectEscaping()
    {
        var wanted = await ProductsSeed.StoreWithProductsAsync(Db, DefaultUser, "Lamp, desk", "Say \"hi\"", "=SUM(A1)");
        await ProductsSeed.StoreWithProductsAsync(Db, new ApplicationUserId(Guid.NewGuid()), "Other store product");
        var queued = await QueueJobAsync(wanted.Store.Id);

        await RunMessageAsync(queued.Id);

        var job = await ReadJobAsync(queued.Id);
        Assert.Equal(JobStatus.Succeeded, job.Status);
        Assert.Equal(3, job.Progress);
        Assert.Equal($"products/exports/{job.Id}.csv", job.ObjectKey);
        Assert.StartsWith("products-", job.FileName, StringComparison.Ordinal);
        Assert.EndsWith(".csv", job.FileName, StringComparison.Ordinal);
        Assert.Equal("text/csv", PrivateStore.ContentTypeOf(job.ObjectKey!));

        var lines = ReadCsv(job).Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
        Assert.Equal("Id,StoreId,Name,Description,Quantity,Price,CreatedOn", lines[0]);
        Assert.Equal(4, lines.Length);
        Assert.Contains(lines, l => l.Contains("\"Lamp, desk\"", StringComparison.Ordinal));
        Assert.Contains(lines, l => l.Contains("\"Say \"\"hi\"\"\"", StringComparison.Ordinal));
        Assert.Contains(lines, l => l.Contains("'=SUM(A1)", StringComparison.Ordinal));
        Assert.DoesNotContain(lines, l => l.Contains("Other store product", StringComparison.Ordinal));
        Assert.All(lines.Skip(1), l => Assert.Contains(wanted.Store.Id.ToString(), l, StringComparison.Ordinal));
    }

    [Fact]
    public async Task Run_PagesThroughEveryProductExactlyOnce()
    {
        var seeded = await ProductsSeed.StoreWithProductsAsync(Db, DefaultUser, "P1", "P2", "P3", "P4", "P5");
        var queued = await QueueJobAsync();

        await RunMessageAsync(queued.Id, TestOptions.Products(o => o.ExportPageSize = 2));

        var job = await ReadJobAsync(queued.Id);
        var dataLines = ReadCsv(job).Split("\r\n", StringSplitOptions.RemoveEmptyEntries).Skip(1).ToList();
        Assert.Equal(5, dataLines.Count);
        var ids = dataLines.Select(l => l.Split(',')[0]).ToList();
        Assert.Equal(seeded.Products.Select(p => p.Id.ToString()).Order(), ids.Order());
    }

    [Fact]
    public async Task Run_ProductsAddedAfterTheJobWasQueued_AreNotInTheFile()
    {
        await ProductsSeed.StoreWithProductsAsync(Db, DefaultUser, "Added later");
        var queued = await QueueJobAsync(queuedOn: DateTimeOffset.UtcNow.AddHours(-1));

        await RunMessageAsync(queued.Id);

        var job = await ReadJobAsync(queued.Id);
        Assert.Equal(JobStatus.Succeeded, job.Status);
        Assert.Equal(0, job.Progress);
        Assert.Single(ReadCsv(job).Split("\r\n", StringSplitOptions.RemoveEmptyEntries));
    }

    [Fact]
    public async Task Run_MoreProductsThanTheRowLimit_FailsWithRowLimitExceededAndWritesNoFile()
    {
        await ProductsSeed.StoreWithProductsAsync(Db, DefaultUser, "P1", "P2");
        var queued = await QueueJobAsync();

        await RunMessageAsync(queued.Id, TestOptions.Products(o => o.ExportMaxRows = 1));

        var job = await ReadJobAsync(queued.Id);
        Assert.Equal(JobStatus.Failed, job.Status);
        Assert.Equal(ProductExportErrors.RowLimitExceeded.Key, job.FailureKey);
        Assert.Null(job.ObjectKey);
        Assert.Empty(PrivateStore.Keys);
    }

    [Fact]
    public async Task Run_StoreThatFailsToUpload_FailsTheJobInsteadOfThrowing()
    {
        await ProductsSeed.StoreWithProductsAsync(Db, DefaultUser, "P1");
        var queued = await QueueJobAsync();
        var broken = Substitute.For<IPrivateObjectStore>();
        broken.UploadAsync(Arg.Any<UploadObjectRequest>(), Arg.Any<CancellationToken>()).Returns<Task>(_ => throw new InvalidOperationException("storage down"));

        await RunMessageAsync(queued.Id, objectStore: broken);

        var job = await ReadJobAsync(queued.Id);
        Assert.Equal(JobStatus.Failed, job.Status);
        Assert.Equal(ProductExportErrors.UnexpectedError, job.FailureKey);
    }

    [Fact]
    public async Task Run_CancelledMidExport_HandsTheJobBackToTheQueueAndRethrows()
    {
        await ProductsSeed.StoreWithProductsAsync(Db, DefaultUser, "P1");
        var queued = await QueueJobAsync();
        var cancelling = Substitute.For<IPrivateObjectStore>();
        cancelling.UploadAsync(Arg.Any<UploadObjectRequest>(), Arg.Any<CancellationToken>()).Returns<Task>(_ => throw new OperationCanceledException());

        await Assert.ThrowsAsync<OperationCanceledException>(() => RunMessageAsync(queued.Id, objectStore: cancelling));

        Assert.Equal(JobStatus.Queued, (await ReadJobAsync(queued.Id)).Status);
    }

    [Fact]
    public async Task Run_JobThatIsNotQueued_DoesNothing()
    {
        var queued = await QueueJobAsync();
        await Db.ExportJobs.Where(j => j.Id == queued.Id).ExecuteUpdateAsync(s => s.SetProperty(j => j.Status, JobStatus.Running));

        await RunMessageAsync(queued.Id);

        Assert.Equal(JobStatus.Running, (await ReadJobAsync(queued.Id)).Status);
        Assert.Empty(PrivateStore.Keys);
    }
}

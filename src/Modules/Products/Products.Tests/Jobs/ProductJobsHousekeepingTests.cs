using Common.Application.Jobs;
using Common.Application.Storage;
using Common.Domain.StronglyTypedIds;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Products.Application.Exports;
using Products.Application.Imports;
using Products.Application.Persistence;
using Products.Domain.ProductTemplates;
using Products.Domain.Stores;
using Products.Infrastructure.Jobs;
using Products.Tests.Support;
using Xunit;

namespace Products.Tests.Jobs;

public class ProductJobsHousekeepingTests(IntegrationTestWebAppFactory factory) : ProductsIntegrationTest(factory)
{
    // The host's housekeeping options keep finished rows for 168 hours and fail Running rows after 60 minutes.
    private static readonly DateTimeOffset Expired = DateTimeOffset.UtcNow.AddDays(-30);

    private async Task PutAsync(string key)
    {
        using var content = new MemoryStream([1, 2, 3]);
        await PrivateStore.UploadAsync(new UploadObjectRequest(key, content, "application/octet-stream"), CancellationToken.None);
    }

    private static ProductImportJob NewImportJob(string objectKey)
    {
        return ProductImportJob.Queue(
            ProductImportJobId.New(), DefaultUser, Expired.AddMinutes(-5), StoreId.New(), ProductTemplateId.New(),
            Guid.NewGuid().ToString("N"), "products.xlsx", objectKey, string.Empty);
    }

    [Fact]
    public async Task RunAsync_ExpiredFinishedJobs_DeletesTheirFilesAndRowsAndKeepsRecentOnes()
    {
        var expiredImport = NewImportJob("products/imports/expired.xlsx");
        expiredImport.MarkFailed(Expired, "UnexpectedError");
        var recentImport = NewImportJob("products/imports/recent.xlsx");
        recentImport.MarkSucceeded(DateTimeOffset.UtcNow.AddHours(-1));
        var expiredExport = ProductExportJob.Queue(DefaultUser, Expired.AddMinutes(-5), null);
        expiredExport.MarkSucceeded(Expired);
        var recentExport = ProductExportJob.Queue(DefaultUser, DateTimeOffset.UtcNow.AddHours(-2), null);
        recentExport.MarkSucceeded(DateTimeOffset.UtcNow.AddHours(-1));
        Db.ImportJobs.AddRange(expiredImport, recentImport);
        Db.ExportJobs.AddRange(expiredExport, recentExport);
        await Db.SaveChangesAsync();
        await Db.ExportJobs.Where(j => j.Id == expiredExport.Id).ExecuteUpdateAsync(s => s.SetProperty(j => j.ObjectKey, "products/exports/expired.csv"));
        await Db.ExportJobs.Where(j => j.Id == recentExport.Id).ExecuteUpdateAsync(s => s.SetProperty(j => j.ObjectKey, "products/exports/recent.csv"));
        foreach (var key in new[] { "products/imports/expired.xlsx", "products/imports/recent.xlsx", "products/exports/expired.csv", "products/exports/recent.csv" })
        {
            await PutAsync(key);
        }

        using var scope = Factory.Services.CreateScope();
        await scope.ServiceProvider.GetRequiredService<ProductJobsHousekeeping>().RunAsync();

        Assert.Equal(["products/exports/recent.csv", "products/imports/recent.xlsx"], PrivateStore.Keys.Order());
        using var checkScope = Factory.Services.CreateScope();
        var db = checkScope.ServiceProvider.GetRequiredService<IProductsDbContext>();
        Assert.Equal([recentImport.Id], await db.ImportJobs.AsNoTracking().Select(j => j.Id).ToListAsync());
        Assert.Equal([recentExport.Id], await db.ExportJobs.AsNoTracking().Select(j => j.Id).ToListAsync());
    }

    [Fact]
    public async Task RunAsync_ImportWhoseWorkerDied_IsFailedAsStaleAndItsUploadIsKeptForTheRetentionWindow()
    {
        var stale = NewImportJob("products/imports/stale.xlsx");
        stale.MarkRunning(DateTimeOffset.UtcNow.AddHours(-3));
        Db.ImportJobs.Add(stale);
        await Db.SaveChangesAsync();
        await PutAsync("products/imports/stale.xlsx");

        using var scope = Factory.Services.CreateScope();
        await scope.ServiceProvider.GetRequiredService<ProductJobsHousekeeping>().RunAsync();

        using var checkScope = Factory.Services.CreateScope();
        var job = await checkScope.ServiceProvider.GetRequiredService<IProductsDbContext>().ImportJobs
            .AsNoTracking().SingleAsync(j => j.Id == stale.Id);
        Assert.Equal(JobStatus.Failed, job.Status);
        Assert.Equal(JobClaimExtensions.StaleFailureKey, job.FailureKey);
        Assert.Single(PrivateStore.Keys);
    }
}

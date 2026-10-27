using System.Linq.Expressions;
using Common.Application.BackgroundJobs;
using Common.Application.Jobs;
using Common.Application.Storage;
using Inventory.Application.BulkAdjustments;
using Inventory.Application.Persistence;
using Inventory.Infrastructure.BulkAdjustments;
using Inventory.Tests.Support;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;

namespace Inventory.Tests.BulkAdjustments;

public class StockJobsHousekeepingTests(IntegrationTestWebAppFactory factory) : BulkAdjustIntegrationTest(factory)
{
    // The host's housekeeping options keep finished rows for 168 hours and fail Running rows after 60 minutes.
    private static readonly DateTimeOffset Expired = DateTimeOffset.UtcNow.AddDays(-30);

    private async Task PutAsync(string key)
    {
        using var content = new MemoryStream([1, 2, 3]);
        await PrivateStore.UploadAsync(new UploadObjectRequest(key, content, "application/octet-stream"), CancellationToken.None);
    }

    private static StockBulkAdjustJob NewJob(string objectKey)
    {
        return StockBulkAdjustJob.Queue(
            StockBulkAdjustJobId.New(), DefaultUser, Expired.AddMinutes(-5), Guid.NewGuid().ToString("N"), "adjustments.xlsx", objectKey, "Cycle count");
    }

    [Fact]
    public async Task RunAsync_ExpiredFinishedJobs_DeletesTheirFilesAndRowsAndKeepsRecentOnes()
    {
        var expired = NewJob("inventory/bulk-adjustments/expired.xlsx");
        expired.MarkFailed(Expired, "UnexpectedError");
        var recent = NewJob("inventory/bulk-adjustments/recent.xlsx");
        recent.MarkSucceeded(DateTimeOffset.UtcNow.AddHours(-1));
        Db.StockBulkAdjustJobs.AddRange(expired, recent);
        await Db.SaveChangesAsync();
        await PutAsync("inventory/bulk-adjustments/expired.xlsx");
        await PutAsync("inventory/bulk-adjustments/recent.xlsx");

        using var scope = Factory.Services.CreateScope();
        await scope.ServiceProvider.GetRequiredService<StockJobsHousekeeping>().RunAsync();

        Assert.Equal(["inventory/bulk-adjustments/recent.xlsx"], PrivateStore.Keys);
        using var checkScope = Factory.Services.CreateScope();
        var db = checkScope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        Assert.Equal([recent.Id], await db.StockBulkAdjustJobs.AsNoTracking().Select(j => j.Id).ToListAsync());
    }

    [Fact]
    public async Task RunAsync_JobWhoseWorkerDied_IsFailedAsStaleAndItsUploadIsKeptForTheRetentionWindow()
    {
        var stale = NewJob("inventory/bulk-adjustments/stale.xlsx");
        stale.MarkRunning(DateTimeOffset.UtcNow.AddHours(-3));
        Db.StockBulkAdjustJobs.Add(stale);
        await Db.SaveChangesAsync();
        await PutAsync("inventory/bulk-adjustments/stale.xlsx");

        using var scope = Factory.Services.CreateScope();
        await scope.ServiceProvider.GetRequiredService<StockJobsHousekeeping>().RunAsync();

        using var checkScope = Factory.Services.CreateScope();
        var job = await checkScope.ServiceProvider.GetRequiredService<IInventoryDbContext>().StockBulkAdjustJobs
            .AsNoTracking().SingleAsync(j => j.Id == stale.Id);
        Assert.Equal(JobStatus.Failed, job.Status);
        Assert.Equal(JobClaimExtensions.StaleFailureKey, job.FailureKey);
        Assert.Single(PrivateStore.Keys);
    }

    [Fact]
    public async Task StartAsync_RegistersTheHousekeepingOnTheConfiguredCron()
    {
        var recurringJobs = Substitute.For<IRecurringBackgroundJobs>();
        var services = new ServiceCollection().AddSingleton(recurringJobs).BuildServiceProvider();
        var registrar = new StockJobsHousekeepingRegistrar(
            services,
            Options.Create(TestInventoryOptions.Create(configure: o => o.StockJobHousekeepingCron = "30 4 * * * *")),
            NullLogger<StockJobsHousekeepingRegistrar>.Instance);

        await registrar.StartAsync(CancellationToken.None);

        recurringJobs.Received(1).AddOrUpdate(
            "inventory-stock-job-housekeeping",
            Arg.Any<Expression<Func<StockJobsHousekeeping, Task>>>(),
            Arg.Is<Func<string>>(cron => cron() == "30 4 * * * *"));
    }

    [Fact]
    public async Task StartAsync_WithoutTheBackgroundJobsModule_RegistersNothingAndDoesNotThrow()
    {
        var registrar = new StockJobsHousekeepingRegistrar(
            new ServiceCollection().BuildServiceProvider(),
            Options.Create(TestInventoryOptions.Create()),
            NullLogger<StockJobsHousekeepingRegistrar>.Instance);

        await registrar.StartAsync(CancellationToken.None);
    }
}

public class StockBulkAdjustConsumerTests(IntegrationTestWebAppFactory factory) : BulkAdjustIntegrationTest(factory)
{
    /// <summary>A prefetched message waiting for a free slot sits unacked and can trip the broker's consumer timeout, so the
    /// consumer's prefetch count and concurrent message limit must be the same value, taken from its option.</summary>
    [Fact]
    public void Consumer_PrefetchCountAndConcurrentMessageLimitAreTheOptionsValue()
    {
        var definition = new RunStockBulkAdjustJobHandlerDefinition(
            TestInventoryOptions.Wrapped(o => o.BulkAdjustJobConcurrentMessageLimit = 3));
        var endpoint = Substitute.For<IReceiveEndpointConfigurator>();

        ((IConsumerDefinition<RunStockBulkAdjustJobHandler>)definition).Configure(
            endpoint, Substitute.For<IConsumerConfigurator<RunStockBulkAdjustJobHandler>>(), Substitute.For<IRegistrationContext>());

        Assert.Equal(3, endpoint.PrefetchCount);
        Assert.Equal(3, endpoint.ConcurrentMessageLimit);
    }

    /// <summary>The consumer is picked up by the Host's assembly scan; nothing registers it by hand, so this is the check that a
    /// rename or a moved namespace does not silently drop the job's queue.</summary>
    [Fact]
    public void Host_HasAReceiveEndpointForTheConsumer()
    {
        var health = Factory.Services.GetRequiredService<IBusControl>().CheckHealth();

        Assert.Contains(health.Endpoints.Keys, name => name.EndsWith($"/{nameof(RunStockBulkAdjustJobHandler)}", StringComparison.Ordinal));
    }
}

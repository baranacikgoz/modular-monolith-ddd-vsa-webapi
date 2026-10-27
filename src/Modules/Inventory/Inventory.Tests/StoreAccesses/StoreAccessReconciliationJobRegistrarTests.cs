using System.Linq.Expressions;
using Common.Application.BackgroundJobs;
using Common.Domain.StronglyTypedIds;
using Common.Tests;
using Inventory.Application.Persistence;
using Inventory.Application.StoreAccesses;
using Inventory.Infrastructure.StoreAccesses;
using Inventory.Tests.Support;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;

namespace Inventory.Tests.StoreAccesses;

[Collection("IntegrationTestCollection")]
public class StoreAccessReconciliationJobRegistrarTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private readonly IRecurringBackgroundJobs _recurringJobs = Substitute.For<IRecurringBackgroundJobs>();
    private readonly IBackgroundJobs _backgroundJobs = Substitute.For<IBackgroundJobs>();

    // The registrar only needs the BackgroundJobs services (absent from this module's test host, so substituted) and
    // the module's own DbContext, which comes from the real host.
    private StoreAccessReconciliationJobRegistrar CreateSut(bool withBackgroundJobsModule = true)
    {
        var services = new ServiceCollection();
        if (withBackgroundJobsModule)
        {
            services.AddSingleton(_recurringJobs);
            services.AddSingleton(_backgroundJobs);
        }

        services.AddScoped(_ => Scope.ServiceProvider.GetRequiredService<IInventoryDbContext>());

        return new StoreAccessReconciliationJobRegistrar(
            services.BuildServiceProvider(),
            Options.Create(TestInventoryOptions.Create(configure: o => o.StoreAccessReconciliationCron = "15 4 * * *")),
            NullLogger<StoreAccessReconciliationJobRegistrar>.Instance);
    }

    [Fact]
    public async Task StartAsync_RegistersTheReconciliationOnTheConfiguredCron()
    {
        await CreateSut().StartAsync(CancellationToken.None);

        _recurringJobs.Received(1).AddOrUpdate(
            "inventory-store-access-reconciliation",
            Arg.Any<Expression<Func<StoreAccessReconciliationJob, Task>>>(),
            Arg.Is<Func<string>>(cron => cron() == "15 4 * * *"));
    }

    [Fact]
    public async Task StartAsync_EmptyCopy_QueuesOneRunNow()
    {
        await CreateSut().StartAsync(CancellationToken.None);

        _backgroundJobs.Received(1).Enqueue(Arg.Any<Expression<Func<StoreAccessReconciliationJob, Task>>>());
    }

    [Fact]
    public async Task StartAsync_CopyAlreadyFilled_QueuesNothingNow()
    {
        var db = Scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        var row = StoreAccess.Create(DefaultIdType.CreateVersion7());
        row.ApplyIfNewer(new ApplicationUserId(DefaultIdType.CreateVersion7()), true, 1);
        db.StoreAccess.Add(row);
        await db.SaveChangesAsync();

        await CreateSut().StartAsync(CancellationToken.None);

        _backgroundJobs.DidNotReceive().Enqueue(Arg.Any<Expression<Func<StoreAccessReconciliationJob, Task>>>());
    }

    [Fact]
    public async Task StartAsync_WithoutTheBackgroundJobsModule_RegistersNothingAndDoesNotThrow()
    {
        await CreateSut(withBackgroundJobsModule: false).StartAsync(CancellationToken.None);

        _recurringJobs.DidNotReceiveWithAnyArgs().AddOrUpdate<StoreAccessReconciliationJob>(default!, default!, default!);
    }
}

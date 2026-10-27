using System.Linq.Expressions;
using Common.Application.BackgroundJobs;
using Common.Tests;
using Inventory.Application.Persistence;
using Inventory.Application.ProductSummaries;
using Inventory.Infrastructure.ProductSummaries;
using Inventory.Tests.Support;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;

namespace Inventory.Tests.ProductSummaries;

[Collection("IntegrationTestCollection")]
public class ProductSummariesReconciliationJobRegistrarTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private readonly IRecurringBackgroundJobs _recurringJobs = Substitute.For<IRecurringBackgroundJobs>();
    private readonly IBackgroundJobs _backgroundJobs = Substitute.For<IBackgroundJobs>();

    // The registrar only needs the BackgroundJobs services (absent from this module's test host, so substituted) and
    // the module's own DbContext, which comes from the real host.
    private ProductSummariesReconciliationJobRegistrar CreateSut(bool withBackgroundJobsModule = true)
    {
        var services = new ServiceCollection();
        if (withBackgroundJobsModule)
        {
            services.AddSingleton(_recurringJobs);
            services.AddSingleton(_backgroundJobs);
        }

        services.AddScoped(_ => Scope.ServiceProvider.GetRequiredService<IInventoryDbContext>());

        return new ProductSummariesReconciliationJobRegistrar(
            services.BuildServiceProvider(),
            Options.Create(TestInventoryOptions.Create(productSummaryReconciliationCron: "5 4 * * *")),
            NullLogger<ProductSummariesReconciliationJobRegistrar>.Instance);
    }

    [Fact]
    public async Task StartAsync_RegistersTheReconciliationOnTheConfiguredCron()
    {
        await CreateSut().StartAsync(CancellationToken.None);

        _recurringJobs.Received(1).AddOrUpdate(
            "inventory-product-summaries-reconciliation",
            Arg.Any<Expression<Func<ProductSummariesReconciliationJob, Task>>>(),
            Arg.Is<Func<string>>(cron => cron() == "5 4 * * *"));
    }

    [Fact]
    public async Task StartAsync_EmptyCopy_QueuesOneRunNow()
    {
        await CreateSut().StartAsync(CancellationToken.None);

        _backgroundJobs.Received(1).Enqueue(Arg.Any<Expression<Func<ProductSummariesReconciliationJob, Task>>>());
    }

    [Fact]
    public async Task StartAsync_CopyAlreadyFilled_QueuesNothingNow()
    {
        var db = Scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        var summary = ProductSummary.Create(DefaultIdType.CreateVersion7());
        summary.ApplyIfNewer(DefaultIdType.CreateVersion7(), "Widget", 1);
        db.ProductSummaries.Add(summary);
        await db.SaveChangesAsync();

        await CreateSut().StartAsync(CancellationToken.None);

        _backgroundJobs.DidNotReceive().Enqueue(Arg.Any<Expression<Func<ProductSummariesReconciliationJob, Task>>>());
    }

    [Fact]
    public async Task StartAsync_WithoutTheBackgroundJobsModule_RegistersNothingAndDoesNotThrow()
    {
        await CreateSut(withBackgroundJobsModule: false).StartAsync(CancellationToken.None);

        _recurringJobs.DidNotReceiveWithAnyArgs().AddOrUpdate<ProductSummariesReconciliationJob>(default!, default!, default!);
    }
}

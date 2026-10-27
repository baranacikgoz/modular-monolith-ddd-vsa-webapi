using System.Linq.Expressions;
using Common.Application.BackgroundJobs;
using Common.Application.Options;
using Common.Tests;
using Inventory.Application.Persistence;
using Inventory.Domain.StockReservations;
using Inventory.Domain.WarehouseConnections;
using Inventory.Domain.WebhookDeliveries;
using Inventory.Infrastructure.Webhooks;
using Inventory.Tests.Support;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;

namespace Inventory.Tests.Webhooks;

[Collection("IntegrationTestCollection")]
public class WebhookDeliveryRetentionTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private readonly IRecurringBackgroundJobs _recurringJobs = Substitute.For<IRecurringBackgroundJobs>();

    private WebhookDeliveryRetentionService CreateService(int retentionDays)
    {
        var options = Options.Create(TestInventoryOptions.Create(webhookDeliveryRetentionDays: retentionDays));
        return new WebhookDeliveryRetentionService(
            Scope.ServiceProvider.GetRequiredService<IInventoryDbContext>(), TimeProvider.System, options,
            NullLogger<WebhookDeliveryRetentionService>.Instance);
    }

    private async Task SeedDeliveryAsync(WarehouseConnectionId connectionId, string deliveryId, DateTimeOffset receivedOn)
    {
        var db = Scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        db.WebhookDeliveries.Add(WebhookDelivery.Receive(connectionId, deliveryId, StockReservationId.New(), receivedOn));
        await db.SaveChangesAsync();
    }

    [Fact]
    public async Task PurgeAsync_DeletesDeliveriesOlderThanTheRetentionAndKeepsTheRest()
    {
        var connectionId = WarehouseConnectionId.New();
        await SeedDeliveryAsync(connectionId, "old", DateTimeOffset.UtcNow.AddDays(-31));
        await SeedDeliveryAsync(connectionId, "recent", DateTimeOffset.UtcNow.AddDays(-29));

        await CreateService(retentionDays: 30).PurgeAsync();

        var db = Scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        var kept = await db.WebhookDeliveries.AsNoTracking().Where(d => d.ConnectionId == connectionId).Select(d => d.DeliveryId).ToListAsync();
        Assert.Equal(["recent"], kept);
    }

    [Fact]
    public async Task StartAsync_RegistersThePurgeOnTheConfiguredCron()
    {
        var services = new ServiceCollection().AddSingleton(_recurringJobs).BuildServiceProvider();
        var registrar = new WebhookDeliveryRetentionJobRegistrar(
            services, Options.Create(TestInventoryOptions.Create(webhookDeliveryRetentionCron: "15 5 * * *")),
            NullLogger<WebhookDeliveryRetentionJobRegistrar>.Instance);

        await registrar.StartAsync(CancellationToken.None);

        _recurringJobs.Received(1).AddOrUpdate(
            "inventory-webhook-delivery-retention",
            Arg.Any<Expression<Func<WebhookDeliveryRetentionService, Task>>>(),
            Arg.Is<Func<string>>(cron => cron() == "15 5 * * *"));
    }

    [Fact]
    public async Task StartAsync_WithoutTheBackgroundJobsModule_RegistersNothingAndDoesNotThrow()
    {
        var registrar = new WebhookDeliveryRetentionJobRegistrar(
            new ServiceCollection().BuildServiceProvider(), Options.Create(TestInventoryOptions.Create()),
            NullLogger<WebhookDeliveryRetentionJobRegistrar>.Instance);

        await registrar.StartAsync(CancellationToken.None);

        _recurringJobs.DidNotReceiveWithAnyArgs().AddOrUpdate<WebhookDeliveryRetentionService>(default!, default!, default!);
    }
}

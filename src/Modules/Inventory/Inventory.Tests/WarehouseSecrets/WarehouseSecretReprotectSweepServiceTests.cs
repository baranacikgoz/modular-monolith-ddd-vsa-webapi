using System.Linq.Expressions;
using Common.Application.BackgroundJobs;
using Common.Application.Options;
using Common.Infrastructure.Security;
using Common.IntegrationEvents;
using Common.Tests;
using Inventory.Application.Persistence;
using Inventory.Domain.WarehouseConnections;
using Inventory.Infrastructure.WarehouseSecrets;
using Inventory.Tests.Support;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;
using EventBus = Common.Application.EventBus;
using Keys = Inventory.Tests.Support.WarehouseSecretKeys;

namespace Inventory.Tests.WarehouseSecrets;

/// <summary>
///     The sweep is the finder under the rewrite: it reads only the key marker of each stored value and publishes, one message
///     per page that holds a value under another key. It never rewrites a value and never logs one.
/// </summary>
[Collection("IntegrationTestCollection")]
public class WarehouseSecretReprotectSweepServiceTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private const string ApiCredentialPlaintext = "api-credential-plaintext-77aa";
    private const string WebhookSecretPlaintext = "webhook-secret-plaintext-31cd";

    private readonly CapturingLogger<WarehouseSecretReprotectSweepService> _logger = new();

    private IServiceProvider Root => Factory.Services;

    private async Task SweepAsync(Action<InventoryOptions>? configure = null)
    {
        using var scope = Root.CreateScope();
        var services = scope.ServiceProvider;
        var sweep = new WarehouseSecretReprotectSweepService(
            services.GetRequiredService<IInventoryDbContext>(),
            services.GetRequiredService<EventBus.IIntegrationEventOutbox>(),
            Keys.Rotating,
            TestInventoryOptions.Wrapped(configure),
            _logger);

        await sweep.SweepAsync();
    }

    private Task<WarehouseConnection> SeedAsync(SecretProtector apiCredentialKey, SecretProtector webhookSecretKey)
    {
        return Keys.SeedConnectionAsync(
            Root, apiCredentialKey.Encrypt(ApiCredentialPlaintext), webhookSecretKey.Encrypt(WebhookSecretPlaintext));
    }

    private Task<List<WarehouseSecretsReprotectRequestedIntegrationEvent>> QueuedAsync()
    {
        return StockLevelTestData.QueuedEventsAsync<WarehouseSecretsReprotectRequestedIntegrationEvent>(Root);
    }

    [Fact]
    public async Task Sweep_ConnectionUnderAnOldKey_QueuesOneMessageNamingItAndRewritesNothing()
    {
        var old = await SeedAsync(Keys.Before, Keys.Before);
        await SeedAsync(Keys.Rotating, Keys.Rotating);
        var before = await WarehouseConnectionTestData.ReloadAsync(Root, old.Id);

        await SweepAsync();

        var message = Assert.Single(await QueuedAsync());
        Assert.Equal([old.Id.Value], message.ConnectionIds.ToArray());
        var after = await WarehouseConnectionTestData.ReloadAsync(Root, old.Id);
        Assert.Equal(before.EncryptedApiCredential, after.EncryptedApiCredential);
        Assert.Equal(before.EncryptedWebhookSecret, after.EncryptedWebhookSecret);
    }

    [Fact]
    public async Task Sweep_EveryConnectionUnderTheCurrentKey_QueuesNothing()
    {
        await SeedAsync(Keys.Rotating, Keys.Rotating);
        await SeedAsync(Keys.Rotating, Keys.Rotating);

        await SweepAsync();

        Assert.Empty(await QueuedAsync());
    }

    [Fact]
    public async Task Sweep_NoConnectionsAtAll_QueuesNothing()
    {
        await SweepAsync();

        Assert.Empty(await QueuedAsync());
    }

    [Theory]
    [InlineData(true, false)]
    [InlineData(false, true)]
    public async Task Sweep_OneOfTheTwoValuesUnderAnOldKey_QueuesTheConnection(bool apiCredentialOld, bool webhookSecretOld)
    {
        var connection = await SeedAsync(apiCredentialOld ? Keys.Before : Keys.Rotating, webhookSecretOld ? Keys.Before : Keys.Rotating);

        await SweepAsync();

        var message = Assert.Single(await QueuedAsync());
        Assert.Equal([connection.Id.Value], message.ConnectionIds.ToArray());
    }

    [Fact]
    public async Task Sweep_ManyOldConnections_QueuesPagesNoLargerThanTheSweepPageSize()
    {
        var all = new List<DefaultIdType>();
        for (var i = 0; i < 5; i++)
        {
            all.Add((await SeedAsync(Keys.Before, Keys.Before)).Id.Value);
        }

        await SweepAsync(o => o.SweepPageSize = 2);

        var messages = await QueuedAsync();
        Assert.Equal([1, 2, 2], messages.Select(m => m.ConnectionIds.Count).Order().ToArray());
        Assert.Equal(all.Order().ToArray(), messages.SelectMany(m => m.ConnectionIds).Order().ToArray());
    }

    // The value is not the sweep's to open: a damaged marker is logged and left out, the other connections still go.
    [Fact]
    public async Task Sweep_ValueWithoutAReadableMarker_IsLoggedAndTheOthersAreStillQueued()
    {
        var damaged = await Keys.SeedConnectionAsync(Root, [1, 2, 3], Keys.Rotating.Encrypt(WebhookSecretPlaintext));
        var old = await SeedAsync(Keys.Before, Keys.Before);

        await SweepAsync();

        var message = Assert.Single(await QueuedAsync());
        Assert.Equal([old.Id.Value], message.ConnectionIds.ToArray());
        var warning = Assert.Single(_logger.Entries, e => e.Level == LogLevel.Warning);
        Assert.Contains(damaged.Id.Value.ToString(), warning.Message, StringComparison.Ordinal);
        Assert.Contains("ApiCredential", warning.Message, StringComparison.Ordinal);
    }

    [Fact]
    public async Task Sweep_AnyRun_NeverLogsAPlaintext()
    {
        await SeedAsync(Keys.Before, Keys.Before);
        await Keys.SeedConnectionAsync(Root, [1, 2, 3], [4, 5, 6]);

        await SweepAsync();

        Assert.NotEmpty(_logger.Entries);
        var everything = string.Join('\n', _logger.Entries.Select(e => $"{e.Message}\n{e.Exception}"));
        Assert.DoesNotContain(ApiCredentialPlaintext, everything, StringComparison.Ordinal);
        Assert.DoesNotContain(WebhookSecretPlaintext, everything, StringComparison.Ordinal);
    }

    [Fact]
    public async Task StartAsync_RegistersTheSweepOnTheConfiguredCron()
    {
        var recurringJobs = Substitute.For<IRecurringBackgroundJobs>();
        var services = new ServiceCollection().AddSingleton(recurringJobs).BuildServiceProvider();
        var registrar = new WarehouseSecretReprotectSweepJobRegistrar(
            services,
            Options.Create(TestInventoryOptions.Create(configure: o => o.WarehouseSecretReprotectCron = "15 40 4 * * *")),
            NullLogger<WarehouseSecretReprotectSweepJobRegistrar>.Instance);

        await registrar.StartAsync(CancellationToken.None);

        recurringJobs.Received(1).AddOrUpdate(
            "inventory-warehouse-secret-reprotect-sweep",
            Arg.Any<Expression<Func<WarehouseSecretReprotectSweepService, Task>>>(),
            Arg.Is<Func<string>>(cron => cron() == "15 40 4 * * *"));
    }

    [Fact]
    public async Task StartAsync_WithoutTheBackgroundJobsModule_RegistersNothingAndDoesNotThrow()
    {
        var registrar = new WarehouseSecretReprotectSweepJobRegistrar(
            new ServiceCollection().BuildServiceProvider(),
            Options.Create(TestInventoryOptions.Create()),
            NullLogger<WarehouseSecretReprotectSweepJobRegistrar>.Instance);

        await registrar.StartAsync(CancellationToken.None);
    }
}

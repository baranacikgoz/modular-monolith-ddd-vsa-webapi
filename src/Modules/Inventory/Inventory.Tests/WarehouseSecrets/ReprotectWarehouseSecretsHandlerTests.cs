using Common.Application.Options;
using Common.Application.Persistence.Inbox;
using Common.Infrastructure.Security;
using Common.IntegrationEvents;
using Common.Tests;
using Inventory.Application.Persistence;
using Inventory.Domain.WarehouseConnections;
using Inventory.Domain.WarehouseConnections.DomainEvents.v1;
using Inventory.Infrastructure.WarehouseSecrets;
using Inventory.Tests.Support;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Xunit;
using ZiggyCreatures.Caching.Fusion;
using Keys = Inventory.Tests.Support.WarehouseSecretKeys;

namespace Inventory.Tests.WarehouseSecrets;

/// <summary>
///     After an encryption key rotation the consumer rewrites the stored secrets of a page of connections under the current
///     key. The protector under test is the system after the rotation: it seals under the current key and still opens the old
///     key's values. A value under the current key is not touched, a value nobody can open is logged and skipped, and nothing
///     decrypted ever reaches a log.
/// </summary>
[Collection("IntegrationTestCollection")]
public class ReprotectWarehouseSecretsHandlerTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private const string ApiCredentialPlaintext = "api-credential-plaintext-9c1f";
    private const string WebhookSecretPlaintext = "webhook-secret-plaintext-4be2";

    private readonly CapturingLogger<ReprotectWarehouseSecretsHandler> _logger = new();

    private IServiceProvider Root => Factory.Services;

    private async Task DeliverAsync(Action<InventoryOptions>? configure, params WarehouseConnection[] connections)
    {
        await DeliverAsync(configure, [.. connections.Select(c => c.Id.Value)]);
    }

    private async Task DeliverAsync(Action<InventoryOptions>? configure, DefaultIdType[] connectionIds)
    {
        await using var scope = Root.CreateAsyncScope();
        var services = scope.ServiceProvider;
        var handler = new ReprotectWarehouseSecretsHandler(
            services.GetRequiredService<IInventoryDbContext>(),
            Keys.Rotating,
            TestInventoryOptions.Wrapped(configure),
            services.GetRequiredService<IFusionCache>(),
            services.GetRequiredService<IOptions<CachingOptions>>(),
            services.GetRequiredService<IInboxStore<IInventoryDbContext>>(),
            _logger);

        await ConsumerHarness.DeliverAsync(handler, new WarehouseSecretsReprotectRequestedIntegrationEvent(connectionIds));
    }

    private Task<WarehouseConnection> SeedAsync(SecretProtector apiCredentialKey, SecretProtector webhookSecretKey, bool deactivated = false)
    {
        return Keys.SeedConnectionAsync(
            Root, apiCredentialKey.Encrypt(ApiCredentialPlaintext), webhookSecretKey.Encrypt(WebhookSecretPlaintext), deactivated);
    }

    private Task<WarehouseConnection> ReloadAsync(WarehouseConnection connection)
    {
        return WarehouseConnectionTestData.ReloadAsync(Root, connection.Id);
    }

    private async Task<List<V1WarehouseConnectionSecretsReprotectedDomainEvent>> ReprotectEventsAsync(WarehouseConnection connection)
    {
        await using var scope = Root.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();
        var entries = await db.AuditLog.AsNoTracking().Where(a => a.AggregateId == connection.Id.Value).ToListAsync();
        return entries.Select(a => a.Event).OfType<V1WarehouseConnectionSecretsReprotectedDomainEvent>().ToList();
    }

    [Fact]
    public async Task Consume_ValuesUnderTheOldKey_AreRewrittenUnderTheCurrentKeyAndStayTheSameSecrets()
    {
        var connection = await SeedAsync(Keys.Before, Keys.Before);

        await DeliverAsync(null, connection);

        var stored = await ReloadAsync(connection);
        Assert.False(Keys.Rotating.NeedsReprotect(stored.EncryptedApiCredential));
        Assert.False(Keys.Rotating.NeedsReprotect(stored.EncryptedWebhookSecret));
        Assert.Equal(ApiCredentialPlaintext, Keys.Rotating.Decrypt(stored.EncryptedApiCredential));
        Assert.Equal(WebhookSecretPlaintext, Keys.Rotating.Decrypt(stored.EncryptedWebhookSecret));
        var @event = Assert.Single(await ReprotectEventsAsync(connection));
        Assert.True(@event.ApiCredential);
        Assert.True(@event.WebhookSecret);
    }

    [Fact]
    public async Task Consume_ValuesUnderTheCurrentKey_AreLeftUntouchedAndNothingIsRaised()
    {
        var connection = await SeedAsync(Keys.Rotating, Keys.Rotating);
        var before = await ReloadAsync(connection);

        await DeliverAsync(null, connection);

        var after = await ReloadAsync(connection);
        Assert.Equal(before.EncryptedApiCredential, after.EncryptedApiCredential);
        Assert.Equal(before.EncryptedWebhookSecret, after.EncryptedWebhookSecret);
        Assert.Null(after.LastModifiedOn);
        Assert.Empty(await ReprotectEventsAsync(connection));
    }

    [Fact]
    public async Task Consume_OnlyOneValueUnderTheOldKey_RewritesJustThatOne()
    {
        var connection = await SeedAsync(Keys.Rotating, Keys.Before);
        var before = await ReloadAsync(connection);

        await DeliverAsync(null, connection);

        var after = await ReloadAsync(connection);
        Assert.Equal(before.EncryptedApiCredential, after.EncryptedApiCredential);
        Assert.NotEqual(before.EncryptedWebhookSecret, after.EncryptedWebhookSecret);
        Assert.Equal(WebhookSecretPlaintext, Keys.Rotating.Decrypt(after.EncryptedWebhookSecret));
        var @event = Assert.Single(await ReprotectEventsAsync(connection));
        Assert.False(@event.ApiCredential);
        Assert.True(@event.WebhookSecret);
    }

    [Fact]
    public async Task Consume_DeactivatedConnection_IsRewrittenToo()
    {
        var connection = await SeedAsync(Keys.Before, Keys.Before, deactivated: true);

        await DeliverAsync(null, connection);

        var stored = await ReloadAsync(connection);
        Assert.Equal(WarehouseConnectionStatus.Deactivated, stored.Status);
        Assert.False(Keys.Rotating.NeedsReprotect(stored.EncryptedApiCredential));
        Assert.False(Keys.Rotating.NeedsReprotect(stored.EncryptedWebhookSecret));
    }

    // A value sealed under a key that is not configured cannot be opened, and a retry would find the same: it is logged and
    // skipped, the message is acknowledged, and the rows next to it are still rewritten.
    [Fact]
    public async Task Consume_UndecryptableValue_IsLoggedAndSkippedWithoutFailingTheMessageOrTheOtherRows()
    {
        var unopenable = await SeedAsync(Keys.Stranger, Keys.Before);
        var healthy = await SeedAsync(Keys.Before, Keys.Before);
        var untouchedApiCredential = (await ReloadAsync(unopenable)).EncryptedApiCredential;

        await DeliverAsync(null, unopenable, healthy);

        var afterUnopenable = await ReloadAsync(unopenable);
        Assert.Equal(untouchedApiCredential, afterUnopenable.EncryptedApiCredential);
        Assert.Equal(WebhookSecretPlaintext, Keys.Rotating.Decrypt(afterUnopenable.EncryptedWebhookSecret));
        Assert.False(Keys.Rotating.NeedsReprotect((await ReloadAsync(healthy)).EncryptedApiCredential));
        var warning = Assert.Single(_logger.Entries, e => e.Level == LogLevel.Warning);
        Assert.Contains(unopenable.Id.Value.ToString(), warning.Message, StringComparison.Ordinal);
        Assert.Contains("ApiCredential", warning.Message, StringComparison.Ordinal);
    }

    [Fact]
    public async Task Consume_AnyRun_NeverLogsAPlaintextOrAStoredValue()
    {
        var unopenable = await SeedAsync(Keys.Stranger, Keys.Stranger);
        var healthy = await SeedAsync(Keys.Before, Keys.Before);
        var storedValues = new[] { unopenable, healthy }
            .SelectMany(c => new[] { c.EncryptedApiCredential, c.EncryptedWebhookSecret })
            .Select(Convert.ToBase64String)
            .ToList();

        await DeliverAsync(null, unopenable, healthy);

        Assert.NotEmpty(_logger.Entries);
        var everything = string.Join('\n', _logger.Entries.Select(e => $"{e.Message}\n{e.Exception}"));
        Assert.DoesNotContain(ApiCredentialPlaintext, everything, StringComparison.Ordinal);
        Assert.DoesNotContain(WebhookSecretPlaintext, everything, StringComparison.Ordinal);
        Assert.All(storedValues, value => Assert.DoesNotContain(value, everything, StringComparison.Ordinal));
    }

    [Fact]
    public async Task Consume_TheSamePageAgain_ChangesNothingTheSecondTime()
    {
        var connection = await SeedAsync(Keys.Before, Keys.Before);
        await DeliverAsync(null, connection);
        var afterFirst = await ReloadAsync(connection);

        await DeliverAsync(null, connection);

        var afterSecond = await ReloadAsync(connection);
        Assert.Equal(afterFirst.EncryptedApiCredential, afterSecond.EncryptedApiCredential);
        Assert.Equal(afterFirst.EncryptedWebhookSecret, afterSecond.EncryptedWebhookSecret);
        Assert.Single(await ReprotectEventsAsync(connection));
    }

    [Fact]
    public async Task Consume_MoreIdsThanAPage_RewritesOnlyThePageSizeAndLeavesTheRestForTheNextSweep()
    {
        var first = await SeedAsync(Keys.Before, Keys.Before);
        var second = await SeedAsync(Keys.Before, Keys.Before);
        var third = await SeedAsync(Keys.Before, Keys.Before);

        await DeliverAsync(o => o.SweepPageSize = 2, first, second, third);

        var stillOld = 0;
        foreach (var connection in new[] { first, second, third })
        {
            stillOld += Keys.Rotating.NeedsReprotect((await ReloadAsync(connection)).EncryptedApiCredential) ? 1 : 0;
        }

        Assert.Equal(1, stillOld);
    }

    /// <summary>The consumer is picked up by the Host's assembly scan; this is the check that a rename or a moved namespace does not silently drop its queue.</summary>
    [Fact]
    public void Host_HasAReceiveEndpointForTheConsumer()
    {
        var health = Root.GetRequiredService<IBusControl>().CheckHealth();

        Assert.Contains(health.Endpoints.Keys, name => name.EndsWith($"/{nameof(ReprotectWarehouseSecretsHandler)}", StringComparison.Ordinal));
    }

    [Fact]
    public async Task Consume_UnknownConnection_IsIgnored()
    {
        await DeliverAsync(null, [DefaultIdType.CreateVersion7()]);

        Assert.DoesNotContain(_logger.Entries, e => e.Level >= LogLevel.Warning);
    }
}

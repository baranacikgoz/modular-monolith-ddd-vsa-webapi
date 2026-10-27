using System.Security.Cryptography;
using Common.Application.EventBus;
using Common.Application.Options;
using Common.Application.Persistence.Inbox;
using Common.Infrastructure.Persistence.Extensions;
using Common.Infrastructure.Security;
using Common.IntegrationEvents;
using Inventory.Application.Persistence;
using Inventory.Domain.WarehouseConnections;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ZiggyCreatures.Caching.Fusion;

namespace Inventory.Infrastructure.WarehouseSecrets;

/// <summary>
///     The queue consumer behind <see cref="WarehouseSecretsReprotectRequestedIntegrationEvent" />: for each connection of the
///     page it asks <see cref="SecretProtector.NeedsReprotect" /> about the stored API credential and the stored webhook secret
///     and rewrites the ones under another key with <see cref="SecretProtector.Reprotect" />, all in one save, so a page is
///     one transaction (the inbox row of the message commits with it). A value already under the current key is not touched, and
///     a connection with nothing to rewrite raises nothing, so a repeated message is a no-op. A value this system cannot open
///     (a key that is no longer configured, a damaged value) is logged by connection id and secret name and left as it is: the
///     message is acknowledged rather than failed, because retrying cannot change the answer, and the sweep reports the value
///     again on its next run. Nothing decrypted is ever logged or returned. Only a fault of this system, the database, is an
///     exception.
/// </summary>
public sealed partial class ReprotectWarehouseSecretsHandler(
    IInventoryDbContext dbContext,
    SecretProtector protector,
    IOptions<InventoryOptions> options,
    IFusionCache cache,
    IOptions<CachingOptions> cachingOptions,
    IInboxStore<IInventoryDbContext> inbox,
    ILogger<ReprotectWarehouseSecretsHandler> logger
) : IntegrationEventHandlerBase<WarehouseSecretsReprotectRequestedIntegrationEvent>(cache, cachingOptions, inbox, logger)
{
    protected override async Task ProcessAsync(WarehouseSecretsReprotectRequestedIntegrationEvent @event, CancellationToken cancellationToken)
    {
        // A page is at most SweepPageSize connections; a longer list is cut, and the sweep names the rest again on its next run.
        var ids = @event.ConnectionIds
            .Distinct()
            .Take(options.Value.SweepPageSize)
            .Select(id => new WarehouseConnectionId(id))
            .ToList();

        var connections = await dbContext.WarehouseConnections
            .TagWith(nameof(ProcessAsync), ids.Count)
            .Where(c => ids.Contains(c.Id))
            .ToListAsync(cancellationToken);

        var rewritten = 0;
        foreach (var connection in connections)
        {
            var apiCredential = ReprotectedOrNull(connection.Id, "ApiCredential", connection.EncryptedApiCredential);
            var webhookSecret = ReprotectedOrNull(connection.Id, "WebhookSecret", connection.EncryptedWebhookSecret);

            connection.ReprotectSecrets(apiCredential, webhookSecret);
            if (apiCredential is not null || webhookSecret is not null)
            {
                rewritten++;
            }
        }

        await dbContext.SaveChangesAsync(cancellationToken);
        LogPageDone(logger, connections.Count, rewritten);
    }

    /// <summary>The value under the current key, or null when it already is, or cannot be opened (logged).</summary>
    private byte[]? ReprotectedOrNull(WarehouseConnectionId connectionId, string secret, byte[] storedValue)
    {
        try
        {
            return protector.NeedsReprotect(storedValue) ? protector.Reprotect(storedValue) : null;
        }
        catch (CryptographicException ex)
        {
            LogUnopenable(logger, ex, connectionId.Value, secret);
            return null;
        }
    }

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Warehouse secret reprotect page: {ConnectionCount} connections read, {RewrittenCount} rewritten under the current key.")]
    private static partial void LogPageDone(ILogger logger, int connectionCount, int rewrittenCount);

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "The stored {Secret} of warehouse connection {ConnectionId} cannot be opened with the configured keys; it is left as it is.")]
    private static partial void LogUnopenable(ILogger logger, Exception exception, DefaultIdType connectionId, string secret);
}

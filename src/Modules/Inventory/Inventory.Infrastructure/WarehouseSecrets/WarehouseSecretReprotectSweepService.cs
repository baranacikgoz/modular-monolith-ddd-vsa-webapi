using System.Security.Cryptography;
using Common.Application.EventBus;
using Common.Application.Options;
using Common.Infrastructure.Persistence.Extensions;
using Common.Infrastructure.Security;
using Common.IntegrationEvents;
using Inventory.Application.Persistence;
using Inventory.Domain.WarehouseConnections;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Inventory.Infrastructure.WarehouseSecrets;

/// <summary>
///     The recurring job body behind an encryption key rotation: it walks the warehouse connections in id order,
///     <see cref="InventoryOptions.SweepPageSize" /> at a time, reads only the key marker of each stored API credential and
///     webhook secret (<see cref="SecretProtector.NeedsReprotect" />, nothing is decrypted), and publishes one
///     <see cref="WarehouseSecretsReprotectRequestedIntegrationEvent" /> for each page that holds a value under a key other than
///     the current one. A page with nothing to rewrite publishes nothing, so outside a rotation the sweep is silent. It never
///     rewrites a value and never logs one: the queue consumer (<see cref="ReprotectWarehouseSecretsHandler" />) rewrites. A value
///     whose marker cannot even be read is logged and left out, so one damaged row cannot hold the sweep up.
/// </summary>
public sealed partial class WarehouseSecretReprotectSweepService(
    IInventoryDbContext dbContext,
    IIntegrationEventOutbox outbox,
    SecretProtector protector,
    IOptions<InventoryOptions> options,
    ILogger<WarehouseSecretReprotectSweepService> logger)
{
    public async Task SweepAsync(CancellationToken cancellationToken = default)
    {
        var pageSize = options.Value.SweepPageSize;
        var visited = 0;
        var queued = 0;

        // Publishing changes no connection, so the set is stable while it is paged through.
        while (true)
        {
            var page = await dbContext.WarehouseConnections
                .AsNoTracking()
                .TagWith(nameof(SweepAsync), visited)
                .OrderBy(c => c.Id)
                .Skip(visited)
                .Take(pageSize)
                .Select(c => new { c.Id, c.EncryptedApiCredential, c.EncryptedWebhookSecret })
                .ToListAsync(cancellationToken);

            var due = page
                .Where(c => AnyUnderAnotherKey(c.Id, c.EncryptedApiCredential, c.EncryptedWebhookSecret))
                .Select(c => c.Id.Value)
                .ToList();

            if (due.Count > 0)
            {
                outbox.Collect(new WarehouseSecretsReprotectRequestedIntegrationEvent(due));
                await dbContext.SaveChangesAsync(cancellationToken);
                queued += due.Count;
            }

            visited += page.Count;
            if (page.Count < pageSize)
            {
                break;
            }
        }

        LogSweepComplete(logger, visited, queued);
    }

    /// <summary>Both values of a connection are always asked, so a damaged one is logged whether or not the other one is due.</summary>
    private bool AnyUnderAnotherKey(WarehouseConnectionId connectionId, byte[] apiCredential, byte[] webhookSecret)
    {
        var apiCredentialDue = IsUnderAnotherKey(connectionId, "ApiCredential", apiCredential);
        var webhookSecretDue = IsUnderAnotherKey(connectionId, "WebhookSecret", webhookSecret);
        return apiCredentialDue || webhookSecretDue;
    }

    private bool IsUnderAnotherKey(WarehouseConnectionId connectionId, string secret, byte[] storedValue)
    {
        try
        {
            return protector.NeedsReprotect(storedValue);
        }
        catch (CryptographicException ex)
        {
            LogUnreadableMarker(logger, ex, connectionId.Value, secret);
            return false;
        }
    }

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Warehouse secret reprotect sweep looked at {ConnectionCount} connections and queued {DueCount} for a rewrite.")]
    private static partial void LogSweepComplete(ILogger logger, int connectionCount, int dueCount);

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "The stored {Secret} of warehouse connection {ConnectionId} has no readable key marker; it is left out of the rewrite.")]
    private static partial void LogUnreadableMarker(ILogger logger, Exception exception, DefaultIdType connectionId, string secret);
}

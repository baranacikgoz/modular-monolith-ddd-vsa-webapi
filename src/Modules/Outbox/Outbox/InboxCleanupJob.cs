using Common.Application.Options;
using Common.Application.Persistence.Inbox;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Outbox.Telemetry;

namespace Outbox;

/// <summary>
///     Prunes every module's consumer inbox (<see cref="ProcessedMessage"/> rows) the way
///     <see cref="OutboxCleanupJob"/> prunes published outbox rows. Retention is
///     <see cref="CachingOptions.IdempotencyKeyDuration"/>: the inbox row and the cache key guard the same
///     window, so a message older than that is treated as new by both layers.
/// </summary>
public sealed partial class InboxCleanupJob(
    IServiceScopeFactory scopeFactory,
    IOptions<OutboxOptions> outboxOptions,
    IOptions<CachingOptions> cachingOptions,
    TimeProvider timeProvider,
    ILogger<InboxCleanupJob> logger)
{
    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var activity = OutboxTelemetry.ActivitySource.StartActivity(nameof(ExecuteAsync));

        var cleanupOptions = outboxOptions.Value.Cleanup;
        if (!cleanupOptions.Enabled)
        {
            return;
        }

        var retention = cachingOptions.Value.IdempotencyKeyDuration;
        var cutoff = timeProvider.GetUtcNow() - retention;
        var batchSize = cleanupOptions.BatchSize;

        using var scope = scopeFactory.CreateScope();
        var targets = scope.ServiceProvider.GetRequiredService<IEnumerable<IInboxCleanupTarget>>();

        foreach (var target in targets)
        {
            var totalDeleted = 0;

            while (!cancellationToken.IsCancellationRequested)
            {
                var deleted = await target.CleanupAsync(cutoff, batchSize, cancellationToken);
                if (deleted == 0)
                {
                    break;
                }

                totalDeleted += deleted;
                LogDeletedBatch(logger, target.ModuleName, deleted, totalDeleted);
            }

            if (totalDeleted > 0)
            {
                LogCleanupComplete(logger, target.ModuleName, totalDeleted, retention);
            }
        }
    }

    [LoggerMessage(Level = LogLevel.Debug,
        Message = "Deleted {Deleted} processed inbox messages from {Module} (total: {TotalDeleted}).")]
    private static partial void LogDeletedBatch(ILogger logger, string module, int deleted, int totalDeleted);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Inbox cleanup complete for {Module}. Deleted {TotalDeleted} processed messages older than {Retention}.")]
    private static partial void LogCleanupComplete(ILogger logger, string module, int totalDeleted, TimeSpan retention);
}

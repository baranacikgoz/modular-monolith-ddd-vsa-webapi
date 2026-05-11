using Common.Application.Options;
using Common.Infrastructure.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Outbox.Telemetry;

namespace Outbox;

public sealed partial class OutboxCleanupJob(
    IServiceScopeFactory scopeFactory,
    IOptions<OutboxOptions> outboxOptions,
    ILogger<OutboxCleanupJob> logger)
{
    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var activity = OutboxTelemetry.ActivitySource.StartActivity(nameof(ExecuteAsync));

        var cleanupOptions = outboxOptions.Value.Cleanup;

        if (!cleanupOptions.Enabled)
        {
            return;
        }

        var cutoff = DateTimeOffset.UtcNow.AddDays(-cleanupOptions.RetentionDays);
        var batchSize = cleanupOptions.BatchSize;
        var totalDeleted = 0;

        while (!cancellationToken.IsCancellationRequested)
        {
            using var scope = scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<IOutboxDbContext>();

            var deleted = await db.OutboxMessages
                .Where(m => m.IsProcessed && m.ProcessedOn < cutoff)
                .Take(batchSize)
                .ExecuteDeleteAsync(cancellationToken);

            if (deleted == 0)
            {
                break;
            }

            totalDeleted += deleted;
            LogDeletedBatch(logger, deleted, totalDeleted, cleanupOptions.RetentionDays);
        }

        if (totalDeleted > 0)
        {
            LogCleanupComplete(logger, totalDeleted, cleanupOptions.RetentionDays);
        }
    }

    [LoggerMessage(Level = LogLevel.Debug,
        Message = "Deleted {Deleted} processed outbox messages (total: {TotalDeleted}, retention: {RetentionDays} days).")]
    private static partial void LogDeletedBatch(ILogger logger, int deleted, int totalDeleted, int retentionDays);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Outbox cleanup complete. Deleted {TotalDeleted} processed messages older than {RetentionDays} days.")]
    private static partial void LogCleanupComplete(ILogger logger, int totalDeleted, int retentionDays);
}

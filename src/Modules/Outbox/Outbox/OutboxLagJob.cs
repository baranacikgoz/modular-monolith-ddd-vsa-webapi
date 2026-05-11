using Common.Application.Options;
using Common.Infrastructure.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Outbox.Telemetry;

namespace Outbox;

public sealed partial class OutboxLagJob(
    IServiceScopeFactory scopeFactory,
    IOptions<OutboxOptions> outboxOptions,
    ILogger<OutboxLagJob> logger)
{
    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var lagThreshold = TimeSpan.FromMinutes(outboxOptions.Value.LagThresholdMinutes);
        var cutoff = DateTimeOffset.UtcNow.Add(-lagThreshold);

        using var scope = scopeFactory.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IOutboxDbContext>();

        var count = await db.OutboxMessages
            .CountAsync(m => !m.IsProcessed && m.CreatedOn < cutoff, cancellationToken);

        OutboxTelemetry.SetLagCount(count);
        LogLagCount(logger, count, outboxOptions.Value.LagThresholdMinutes);
    }

    [LoggerMessage(Level = LogLevel.Debug,
        Message = "Outbox lag count: {Count} messages older than {LagThresholdMinutes} minutes.")]
    private static partial void LogLagCount(ILogger logger, long count, int lagThresholdMinutes);
}

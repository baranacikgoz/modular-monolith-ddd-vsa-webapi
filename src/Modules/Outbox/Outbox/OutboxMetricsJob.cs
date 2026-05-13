using Common.Application.Options;
using Common.Infrastructure.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Outbox.Telemetry;

namespace Outbox;

public sealed partial class OutboxMetricsJob(
    IServiceScopeFactory scopeFactory,
    IOptions<OutboxOptions> outboxOptions,
    ILogger<OutboxMetricsJob> logger)
{
    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var activity = OutboxTelemetry.ActivitySource.StartActivity(nameof(ExecuteAsync));

        var lagThreshold = TimeSpan.FromMinutes(outboxOptions.Value.LagThresholdMinutes);
        var cutoff = DateTimeOffset.UtcNow.Add(-lagThreshold);

        using var scope = scopeFactory.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<IOutboxDbContext>();

        var count = await db.OutboxMessages
            .CountAsync(m => !m.IsProcessed && m.FailedOn == null && m.CreatedOn < cutoff, cancellationToken);

        OutboxTelemetry.SetLagCount(count);

        var stuckCount = await db.OutboxMessages
            .CountAsync(m => !m.IsProcessed && m.FailedOn != null, cancellationToken);

        OutboxTelemetry.SetStuckCount(stuckCount);

        LogMetrics(logger, count, outboxOptions.Value.LagThresholdMinutes, stuckCount);
    }

    [LoggerMessage(Level = LogLevel.Debug,
        Message = "Outbox metrics: lag={LagCount} (>{LagThresholdMinutes}min), stuck={StuckCount}.")]
    private static partial void LogMetrics(ILogger logger, long lagCount, int lagThresholdMinutes, long stuckCount);
}

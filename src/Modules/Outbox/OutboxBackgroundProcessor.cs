using Common.Application.EventBus;
using Common.Domain.Events;
using Common.Infrastructure.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Outbox.Persistence;

namespace Common.Infrastructure.Persistence.Outbox;
internal sealed partial class OutboxBackgroundProcessor(
    IServiceScopeFactory scopeFactory,
    IOptions<OutboxOptions> outboxOptionsProvider,
    ILogger<OutboxBackgroundProcessor> logger
    ) : IHostedService
{
    private readonly int _backgroundJobPeriodInMilliSeconds = outboxOptionsProvider.Value.BackgroundJobPeriodInMilliseconds;
    private readonly int _maxBackoffInMilliSeconds = outboxOptionsProvider.Value.MaxBackoffDelayInMilliseconds;
    private readonly int _batchSizePerExecution = outboxOptionsProvider.Value.BatchSizePerExecution;
    private readonly int _maxFailCountBeforeSentToDeadLetter = outboxOptionsProvider.Value.MaxFailCountBeforeSentToDeadLetter;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var backoffDelay = _backgroundJobPeriodInMilliSeconds;

        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(backoffDelay, cancellationToken);

                LogBeginProcessingOutboxMessages(logger);

                using var scope = scopeFactory.CreateScope();

                var utcNow = scope.ServiceProvider.GetRequiredService<TimeProvider>().GetUtcNow();
                var outboxDbContext = scope.ServiceProvider.GetRequiredService<OutboxDbContext>();

                await using var transaction = await outboxDbContext.Database.BeginTransactionAsync(cancellationToken);

                var outboxMessages = await outboxDbContext
                                            .OutboxMessages
                                            .FromSqlRaw(
                                                """
                                                SELECT o."Id", o."CreatedBy", o."CreatedOn", o."Event", o."FailedCount", o."IsProcessed", o."LastFailedOn",                          o."LastModifiedBy",o."LastModifiedIp", o."LastModifiedOn", o."ProcessedOn", o.xmin

                                                FROM "Outbox"."OutboxMessages" AS o
                                                WHERE NOT (o."IsProcessed") ORDER BY o."CreatedOn"
                                                LIMIT {0}
                                                FOR UPDATE SKIP LOCKED
                                                """, _batchSizePerExecution)
                                            .ToListAsync(cancellationToken);

                if (outboxMessages.Count == 0)
                {
                    LogZeroOutboxMessages(logger);

                    await transaction.RollbackAsync(cancellationToken);

                    backoffDelay = Math.Min(backoffDelay * 2, _maxBackoffInMilliSeconds);
                    continue;
                }

                // Reset backoff when there are messages found to process
                backoffDelay = _backgroundJobPeriodInMilliSeconds;

                LogFoundOutboxMessages(logger, outboxMessages.Count);
                var eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();

                foreach (var outboxMessage in outboxMessages)
                {
                    try
                    {
                        var @event = outboxMessage.Event;
                        await eventBus.PublishAsync(@event, cancellationToken);
                        outboxMessage.MarkAsProcessed(utcNow);
                    }
#pragma warning disable CA1031
                    catch (Exception ex)
#pragma warning restore CA1031
                    {
                        LogExceptionOccuredDuringEventPublishing(
                            logger,
                            ex,
                            outboxMessage.Id,
                            outboxMessage.Event.GetType().Name,
                            outboxMessage.Event);

                        outboxMessage.MarkAsFailed(utcNow);

                        if (outboxMessage.FailedCount >= _maxFailCountBeforeSentToDeadLetter)
                        {
                            outboxDbContext.OutboxMessages.Remove(outboxMessage);
                            outboxDbContext.DeadLetterMessages.Add(DeadLetterMessage.CreateFrom(outboxMessage));
                        }
                    }
                }

                try
                {
                    await outboxDbContext.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                    LogProcessedOutboxMessages(logger, outboxMessages.Count);
                }
#pragma warning disable CA1031
                catch (Exception ex)
#pragma warning restore CA1031
                {
                    await transaction.RollbackAsync(cancellationToken);
                    LogExceptionOccuredDuringSavingChanges(logger, ex);
                }
            }
        }
#pragma warning disable CA1031
        catch (Exception ex)
#pragma warning restore CA1031
        {
            // FATAL
            LogUncaughtFatalExceptionOccuredWhileProcessingOutboxMessages(logger, ex);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;

    [LoggerMessage(
        Level = LogLevel.Trace,
        Message = "Begin processing outbox messages...")]
    private static partial void LogBeginProcessingOutboxMessages(ILogger logger);

    [LoggerMessage(
        Level = LogLevel.Trace,
        Message = "Zero outbox messages found, continuing...")]
    private static partial void LogZeroOutboxMessages(ILogger logger);

    [LoggerMessage(
        Level = LogLevel.Debug,
        Message = "Found ({OutboxMessagesCount}) outbox messages, processing...")]
    private static partial void LogFoundOutboxMessages(ILogger logger, int OutboxMessagesCount);

    [LoggerMessage(
        Level = LogLevel.Debug,
        Message = "Processed ({OutboxMessagesCount}) outbox messages.")]
    private static partial void LogProcessedOutboxMessages(ILogger logger, int OutboxMessagesCount);

    [LoggerMessage(
        Level = LogLevel.Critical,
        Message = "An exception occured during event publishing for outbox message with Id: {OutboxMessageId}. Event type: {EventType}. Payload: {@Payload}")]
    private static partial void LogExceptionOccuredDuringEventPublishing(ILogger logger, Exception ex, int OutboxMessageId, string EventType, IEvent Payload);

    [LoggerMessage(
        Level = LogLevel.Critical,
        Message = "An exception occured during saving changes in outbox database.")]
    private static partial void LogExceptionOccuredDuringSavingChanges(ILogger logger, Exception ex);

    [LoggerMessage(
        Level = LogLevel.Critical,
        Message = "FATAL: Uncaught exception occured while processing outbox messages")]
    private static partial void LogUncaughtFatalExceptionOccuredWhileProcessingOutboxMessages(ILogger logger, Exception ex);
}

using System.Threading;
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
    ) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var outboxOptions = outboxOptionsProvider.Value;
        var backgroundJobPeriodInMilliSeconds = outboxOptions.BackgroundJobPeriodInMilliseconds;
        var backoffDelay = outboxOptions.BackgroundJobPeriodInMilliseconds;
        var maxBackoffDelay = outboxOptions.MaxBackoffDelayInMilliseconds;
        var batchSizePerExecution = outboxOptions.BatchSizePerExecution;
        var maxFailCountBeforeSentToDeadLetter = outboxOptions.MaxFailCountBeforeSentToDeadLetter;

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(backoffDelay, stoppingToken);

                LogBeginProcessingOutboxMessages(logger);

                using var scope = scopeFactory.CreateScope();

                var utcNow = scope.ServiceProvider.GetRequiredService<TimeProvider>().GetUtcNow();
                var outboxDbContext = scope.ServiceProvider.GetRequiredService<OutboxDbContext>();

                await using var transaction = await outboxDbContext.Database.BeginTransactionAsync(stoppingToken);

                var outboxMessages = await outboxDbContext
                                            .OutboxMessages
                                            .FromSqlRaw(
                                                """
                                                SELECT o."Id", o."CreatedBy", o."CreatedOn", o."Event", o."FailedCount", o."IsProcessed", o."LastFailedOn", o."LastModifiedBy",o."LastModifiedIp", o."LastModifiedOn", o."ProcessedOn", o.xmin
                                                FROM "Outbox"."OutboxMessages" AS o
                                                WHERE NOT (o."IsProcessed") ORDER BY o."CreatedOn"
                                                LIMIT {0}
                                                FOR UPDATE SKIP LOCKED
                                                """, batchSizePerExecution)
                                            .ToListAsync(stoppingToken);

                if (outboxMessages.Count == 0)
                {
                    LogZeroOutboxMessages(logger);

                    await transaction.RollbackAsync(stoppingToken);

                    backoffDelay = Math.Min(backoffDelay * 2, maxBackoffDelay);
                    continue;
                }

                // Reset backoff when there are messages found to process
                backoffDelay = backgroundJobPeriodInMilliSeconds;

                LogFoundOutboxMessages(logger, outboxMessages.Count);
                var eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();

                foreach (var outboxMessage in outboxMessages)
                {
                    try
                    {
                        var @event = outboxMessage.Event;
                        await eventBus.PublishAsync(@event, stoppingToken);
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

                        if (outboxMessage.FailedCount >= maxFailCountBeforeSentToDeadLetter)
                        {
                            outboxDbContext.OutboxMessages.Remove(outboxMessage);
                            outboxDbContext.DeadLetterMessages.Add(DeadLetterMessage.CreateFrom(outboxMessage));
                        }
                    }
                }

                try
                {
                    await outboxDbContext.SaveChangesAsync(stoppingToken);
                    await transaction.CommitAsync(stoppingToken);
                    LogProcessedOutboxMessages(logger, outboxMessages.Count);
                }
#pragma warning disable CA1031
                catch (Exception ex)
#pragma warning restore CA1031
                {
                    await transaction.RollbackAsync(stoppingToken);
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

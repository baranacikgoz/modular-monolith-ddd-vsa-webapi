using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Common.Core.Interfaces;
using Common.EventBus.Contracts;
using Common.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Common.Persistence.Outbox;
internal partial class OutboxBackgroundService(
    IServiceScopeFactory scopeFactory,
    IOptions<OutboxOptions> outboxOptionsProvider,
    ILogger<OutboxBackgroundService> logger
    ) : IHostedService
{
    private readonly int _backgroundJobPeriodInMilliSeconds = outboxOptionsProvider.Value.BackgroundJobPeriodInSeconds * 1000;
    private readonly int _batchSizePerExecution = outboxOptionsProvider.Value.BatchSizePerExecution;
    private readonly int _maxFailCountBeforeSentToDeadLetter = outboxOptionsProvider.Value.MaxFailCountBeforeSentToDeadLetter;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            LogBeginProcessingOutboxMessages(logger);
            using var scope = scopeFactory.CreateScope();
            var outboxDbContext = scope.ServiceProvider.GetRequiredService<OutboxDbContext>();

            var outboxMessages = await outboxDbContext
                                       .OutboxMessages
                                       .Where(x => !x.IsProcessed)
                                       .OrderBy(x => x.CreatedOn)
                                       .Take(_batchSizePerExecution)
                                       .ToListAsync(cancellationToken);

            if (outboxMessages.Count == 0)
            {
                LogZeroOutboxMessages(logger);

                await Task.Delay(_backgroundJobPeriodInMilliSeconds, cancellationToken);
                continue;
            }

            LogFoundOutboxMessages(logger, outboxMessages.Count);
            var eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();

            foreach (var outboxMessage in outboxMessages)
            {
                try
                {
                    var type = Type.GetType(outboxMessage.Type);
                    if (type is null)
                    {
                        LogEventTypeIsNull(logger, outboxMessage.Id);
                        continue;
                    }

                    string payload = outboxMessage.Payload;
                    var @event = JsonSerializer.Deserialize(payload, type);
                    if (@event is null)
                    {
                        LogEventIsNull(logger, outboxMessage.Id);
                        continue;
                    }
                    await eventBus.PublishAsync((IEvent)@event, cancellationToken);
                    outboxMessage.MarkAsProcessed();
                }
#pragma warning disable CA1031
                catch (Exception ex)
#pragma warning restore CA1031
                {
                    LogExceptionOccuredDuringEventPublishing(logger, ex, outboxMessage.Id, outboxMessage.Type, outboxMessage.Payload);

                    outboxMessage.MarkAsFailed();

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
            }
#pragma warning disable CA1031
            catch (Exception ex)
#pragma warning restore CA1031
            {
                LogExceptionOccuredDuringSavingChanges(logger, ex);

                // Wait for a while before retrying
                await Task.Delay(_backgroundJobPeriodInMilliSeconds * 3, cancellationToken);
                continue;
            }

            LogProcessedOutboxMessages(logger, outboxMessages.Count);

            await Task.Delay(_backgroundJobPeriodInMilliSeconds, cancellationToken);
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
        Message = "Event type is null for outbox message with Id: {OutboxMessageId}")]
    private static partial void LogEventTypeIsNull(ILogger logger, int OutboxMessageId);

    [LoggerMessage(
        Level = LogLevel.Critical,
        Message = "Event is null for outbox message with Id: {OutboxMessageId}")]
    private static partial void LogEventIsNull(ILogger logger, int OutboxMessageId);

    [LoggerMessage(
        Level = LogLevel.Critical,
        Message = "An exception occured during event publishing for outbox message with Id: {OutboxMessageId}. Event type: {EventType}. Payload: {Payload}")]
    private static partial void LogExceptionOccuredDuringEventPublishing(ILogger logger, Exception ex, int OutboxMessageId, string EventType, string Payload);

    [LoggerMessage(
        Level = LogLevel.Critical,
        Message = "An exception occured during saving changes in outbox database.")]
    private static partial void LogExceptionOccuredDuringSavingChanges(ILogger logger, Exception ex);
}

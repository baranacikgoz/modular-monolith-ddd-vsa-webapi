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
    private readonly OutboxOptions _outboxOptions = outboxOptionsProvider.Value;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            using var scope = scopeFactory.CreateScope();
            var outboxDbContext = scope.ServiceProvider.GetRequiredService<OutboxDbContext>();

            var batchSize = _outboxOptions.BatchSizePerExecution;

            var outboxMessages = await outboxDbContext
                                       .OutboxMessages
                                       .Where(x => !x.IsProcessed)
                                       .OrderBy(x => x.CreatedOn)
                                       .Take(batchSize)
                                       .ToListAsync(cancellationToken);

            if (outboxMessages.Count == 0)
            {
                continue;
            }

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

                    var maxFailCount = _outboxOptions.MaxFailCountBeforeSentToDeadLetter;

                    if (outboxMessage.FailedCount >= maxFailCount)
                    {
                        outboxDbContext.OutboxMessages.Remove(outboxMessage);
                        outboxDbContext.DeadLetterMessages.Add(DeadLetterMessage.CreateFrom(outboxMessage));
                    }
                }
            }

            await outboxDbContext.SaveChangesAsync(cancellationToken);

            await Task.Delay(_outboxOptions.BackgroundJobPeriodInMilliSeconds, cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;

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
}

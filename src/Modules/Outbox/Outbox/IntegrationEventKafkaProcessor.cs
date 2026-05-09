using System.Text.Json;
using Common.Application.EventBus;
using Common.Application.Options;
using Common.Infrastructure.Persistence.Extensions;
using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Outbox.Persistence;

namespace Outbox;

public partial class IntegrationEventKafkaProcessor(
    IOptions<OutboxOptions> outboxOptionsProvider,
    IServiceScopeFactory serviceScopeFactory,
    TimeProvider timeProvider,
    ILogger<IntegrationEventKafkaProcessor> logger
) : BackgroundService
{
    private readonly JsonSerializerOptions _dlqSerializerOptions = new() { WriteIndented = false };

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        LogStarting(logger, outboxOptionsProvider.Value.IntegrationEventKafkaConsumer.TopicName);
        LogDlqConfigured(logger, outboxOptionsProvider.Value.IntegrationEventKafkaDlqProducer.TopicName);

        var setupRetryDelay = TimeSpan.FromSeconds(outboxOptionsProvider.Value.SetupRetryDelaySeconds);
        while (!stoppingToken.IsCancellationRequested)
        {
            IConsumer<Ignore, IntegrationEventOutboxMessageDto>? consumer = null;
            IProducer<Null, string>? dlqProducer = null;
            try
            {
                using (consumer = BuildConsumer())
                using (dlqProducer = BuildDlqProducer())
                {
                    consumer.Subscribe(outboxOptionsProvider.Value.IntegrationEventKafkaConsumer.TopicName);
                    LogConsumerSubscribed(logger);

                    await ConsumeLoop(consumer, dlqProducer, stoppingToken);

                    LogConsumeLoopFinished(logger);
                }
            }
            catch (OperationCanceledException)
            {
                LogStopping(logger);
                break;
            }
            catch (Exception ex)
            {
                LogUnhandledException(logger, ex, setupRetryDelay.TotalSeconds);

                consumer?.Dispose();
                dlqProducer?.Dispose();

                try
                {
                    await Task.Delay(setupRetryDelay, stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    LogRetryDelayCancelled(logger);
                    break;
                }
            }
        }

        LogStopped(logger);
    }

    private async Task ConsumeLoop(
        IConsumer<Ignore, IntegrationEventOutboxMessageDto> consumer,
        IProducer<Null, string> dlqProducer,
        CancellationToken stoppingToken)
    {
        var dlqTopicName = outboxOptionsProvider.Value.IntegrationEventKafkaDlqProducer.TopicName;
        var consumeErrorDelay = TimeSpan.FromSeconds(outboxOptionsProvider.Value.ConsumeErrorDelaySeconds);
        var processingErrorDelay = TimeSpan.FromSeconds(outboxOptionsProvider.Value.ProcessingErrorDelaySeconds);

        var firstConsume = true;

        while (!stoppingToken.IsCancellationRequested)
        {
            ConsumeResult<Ignore, IntegrationEventOutboxMessageDto>? consumeResult = null;
            try
            {
                if (firstConsume)
                {
                    consumeResult = await Task.Run(() => consumer.Consume(0), stoppingToken);
                    firstConsume = false;
                    if (consumeResult is null)
                    {
                        continue;
                    }
                }
                else
                {
                    consumeResult = await Task.Run(() => consumer.Consume(stoppingToken), stoppingToken);
                }

                var currentTopic = consumeResult.Topic;
                var currentPartition = consumeResult.Partition.Value;
                var currentOffset = consumeResult.Offset.Value;

                if (outboxOptionsProvider.Value.IntegrationEventKafkaConsumer.EnablePartitionEof && consumeResult.IsPartitionEOF)
                {
                    LogPartitionEof(logger, currentPartition, currentOffset, currentTopic);
                    continue;
                }

                LogMessageConsumed(logger, currentTopic, currentPartition, currentOffset);

                using (var scope = serviceScopeFactory.CreateScope())
                {
                    await ProcessMessageAsync(scope.ServiceProvider, consumeResult, stoppingToken);
                }

                try
                {
                    consumer.Commit(consumeResult);
                    LogOffsetCommitted(logger, currentOffset, currentTopic, currentPartition);
                }
                catch (KafkaException kex) when (kex.Error.IsFatal)
                {
                    LogFatalCommitError(logger, kex, currentOffset, currentTopic, currentPartition);
                    throw;
                }
                catch (KafkaException ex)
                {
                    LogNonFatalCommitError(logger, ex, currentOffset, currentTopic, currentPartition);
                }
            }
            catch (ConsumeException ex)
            {
                LogConsumeError(logger, ex, ex.Error.Reason, ex.Error.Code, ex.Error.IsFatal,
                    ex.Error.IsLocalError, ex.Error.IsBrokerError);

                if (ex.Error.IsFatal)
                {
                    LogFatalConsumeError(logger);
                    throw;
                }

                LogNonFatalConsumeError(logger, consumeErrorDelay.TotalSeconds);
                await Task.Delay(consumeErrorDelay, stoppingToken);
            }
            catch (OperationCanceledException)
            {
                LogConsumeLoopCancelled(logger);
                throw;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (consumeResult != null)
                {
                    LogProcessConcurrencyError(logger, ex, ex.GetType().Name, consumeResult.Offset.Value,
                        consumeResult.Topic, consumeResult.Partition.Value);
                }
                else
                {
                    LogProcessErrorNullResult(logger, ex, ex.GetType().Name);
                }

                await Task.Delay(processingErrorDelay, stoppingToken);
            }
            catch (Exception ex)
            {
                if (consumeResult != null)
                {
                    LogProcessingError(logger, ex, consumeResult.Offset.Value, consumeResult.Topic,
                        consumeResult.Partition.Value, dlqTopicName);

                    var dlqSuccess = await SendToDlqAsync(dlqProducer, dlqTopicName, consumeResult, ex, stoppingToken);

                    if (dlqSuccess)
                    {
                        LogDlqSuccess(logger, consumeResult.Offset.Value, consumeResult.Topic,
                            consumeResult.Partition.Value, dlqTopicName);
                        try
                        {
                            consumer.Commit(consumeResult);
                        }
                        catch (KafkaException kex) when (kex.Error.IsFatal)
                        {
                            LogFatalDlqCommitError(logger, kex, consumeResult.Offset.Value,
                                consumeResult.Topic, consumeResult.Partition.Value);
                            throw;
                        }
                        catch (KafkaException kex)
                        {
                            LogNonFatalDlqCommitError(logger, kex, consumeResult.Offset.Value,
                                consumeResult.Topic, consumeResult.Partition.Value);
                        }
                    }
                    else
                    {
                        LogDlqFailure(logger, consumeResult.Offset.Value, consumeResult.Topic,
                            consumeResult.Partition.Value);
                    }
                }
                else
                {
                    LogProcessingErrorNullConsumeResult(logger, ex);
                }

                await Task.Delay(processingErrorDelay, stoppingToken);
            }
        }
    }

    private async Task ProcessMessageAsync(
        IServiceProvider serviceProvider,
        ConsumeResult<Ignore, IntegrationEventOutboxMessageDto> consumeResult,
        CancellationToken cancellationToken)
    {
        var dto = consumeResult.Message.Value;

        var topic = consumeResult.Topic;
        var partition = consumeResult.Partition.Value;
        var offset = consumeResult.Offset.Value;

        if (dto == null)
        {
            LogNullMessage(logger, offset, topic, partition);
            return;
        }

        if (dto.IsProcessed)
        {
            LogAlreadyProcessedPayload(logger, dto.Id, topic, partition, offset);
            return;
        }

        LogStartProcessing(logger, dto.Id, topic, partition, offset);

        var dbContext = serviceProvider.GetRequiredService<OutboxDbContext>();

        var outboxMessage = await dbContext
            .IntegrationEventOutboxMessages
            .TagWith(nameof(IntegrationEventKafkaProcessor), dto.Id)
            .SingleOrDefaultAsync(x => x.Id == dto.Id, cancellationToken);

        if (outboxMessage == null)
        {
            LogMessageNotFound(logger, dto.Id, offset, topic, partition);
            return;
        }

        if (outboxMessage.IsProcessed)
        {
            LogAlreadyProcessedDb(logger, outboxMessage.Id, topic, partition, offset);
            return;
        }

        var @event = outboxMessage.Event;
        var dispatcher = serviceProvider.GetRequiredService<IIntegrationEventDispatcher>();
        await dispatcher.DispatchAsync(@event, cancellationToken);
        outboxMessage.MarkAsProcessed(timeProvider.GetUtcNow());

        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException dbConcurrencyEx)
        {
            LogConcurrencyConflict(logger, dbConcurrencyEx, dto.Id, topic, partition, offset);
            throw;
        }

        LogProcessSuccess(logger, dto.Id, topic, partition, offset);
    }

    private async Task<bool> SendToDlqAsync(IProducer<Null, string> dlqProducer, string dlqTopic,
        ConsumeResult<Ignore, IntegrationEventOutboxMessageDto> failedResult,
        Exception exception, CancellationToken cancellationToken)
    {
        var originalTopic = failedResult.Topic;
        var originalPartition = failedResult.Partition.Value;
        var originalOffset = failedResult.Offset.Value;

        try
        {
            var dlqMessage = new DlqMessage<IntegrationEventOutboxMessageDto>
            {
                FailedTimestampUtc = timeProvider.GetUtcNow(),
                OriginalTopic = originalTopic,
                OriginalPartition = originalPartition,
                OriginalOffset = originalOffset,
                ConsumerGroupId = outboxOptionsProvider.Value.IntegrationEventKafkaConsumer.GroupId,
                ExceptionType = exception.GetType().FullName,
                ExceptionMessage = exception.Message,
                ExceptionStackTrace = exception.StackTrace,
                OriginalMessage = failedResult.Message.Value
            };

            var dlqPayload = JsonSerializer.Serialize(dlqMessage, _dlqSerializerOptions);

            LogSendingToDlq(logger, originalOffset, originalTopic, originalPartition, dlqTopic, exception.Message);

            var kafkaMessage = new Message<Null, string> { Value = dlqPayload };
            var deliveryResult = await dlqProducer.ProduceAsync(dlqTopic, kafkaMessage, cancellationToken);

            LogProducedToDlq(logger, deliveryResult.Topic, deliveryResult.Partition.Value, deliveryResult.Offset.Value);

            return true;
        }
        catch (ProduceException<Null, string> pex)
        {
            LogFatalDlqProduceError(logger, pex, dlqTopic, originalOffset, originalTopic, originalPartition, pex.Error.Reason);
            return false;
        }
        catch (OperationCanceledException)
        {
            LogDlqCancelled(logger, originalOffset, originalTopic, originalPartition);
            return false;
        }
        catch (Exception ex)
        {
            LogDlqUnexpectedError(logger, ex, dlqTopic, originalOffset, originalTopic, originalPartition);
            return false;
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        LogStopAsyncCalled(logger);
        await base.StopAsync(cancellationToken);
        LogStopAsyncFinished(logger);
    }

    private IConsumer<Ignore, IntegrationEventOutboxMessageDto> BuildConsumer()
    {
        var kafkaConsumerOptions = outboxOptionsProvider.Value.IntegrationEventKafkaConsumer;
        if (!Enum.TryParse(kafkaConsumerOptions.AutoOffsetReset, true, out AutoOffsetReset autoOffsetReset))
        {
            throw new ArgumentException(
                $"Auto offset reset parameter {kafkaConsumerOptions.AutoOffsetReset} is invalid.");
        }

        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = kafkaConsumerOptions.BootstrapServers,
            GroupId = kafkaConsumerOptions.GroupId,
            AutoOffsetReset = autoOffsetReset,
            EnableAutoCommit = false,
            EnablePartitionEof = kafkaConsumerOptions.EnablePartitionEof,
            SessionTimeoutMs = kafkaConsumerOptions.SessionTimeoutMs,
            HeartbeatIntervalMs = kafkaConsumerOptions.HeartbeatIntervalMs,
            MaxPollIntervalMs = kafkaConsumerOptions.MaxPollIntervalMs
        };

        return new ConsumerBuilder<Ignore, IntegrationEventOutboxMessageDto>(consumerConfig)
            .SetValueDeserializer(new IntegrationEventOutboxMessageDtoDeserializer())
            .SetErrorHandler((_, e) => LogKafkaConsumerError(logger, e.Reason, e.Code, e.IsFatal))
            .SetLogHandler((_, log) =>
            {
                var logLevel = ParseLogLevel(log.Level);
                if (logLevel >= LogLevel.Information)
                {
                    LogKafkaConsumerLog(logger, log.Facility, log.Message);
                }
            })
            .Build();
    }

    private IProducer<Null, string> BuildDlqProducer()
    {
        var kafkaProducerOptions = outboxOptionsProvider.Value.IntegrationEventKafkaDlqProducer;
        var producerConfig = new ProducerConfig
        {
            BootstrapServers = kafkaProducerOptions.BootstrapServers,
            Acks = Acks.All
        };
        return new ProducerBuilder<Null, string>(producerConfig).Build();
    }

    private static LogLevel ParseLogLevel(SyslogLevel logLevel) => logLevel switch
    {
        SyslogLevel.Debug => LogLevel.Debug,
        SyslogLevel.Info => LogLevel.Information,
        SyslogLevel.Notice => LogLevel.Information,
        SyslogLevel.Warning => LogLevel.Warning,
        SyslogLevel.Error => LogLevel.Error,
        SyslogLevel.Critical => LogLevel.Critical,
        SyslogLevel.Alert => LogLevel.Critical,
        SyslogLevel.Emergency => LogLevel.Critical,
        _ => LogLevel.Critical
    };

    [LoggerMessage(Level = LogLevel.Information,
        Message = "IntegrationEventKafkaProcessor starting. Subscribing to topic: {TopicName}")]
    private static partial void LogStarting(ILogger logger, string topicName);

    [LoggerMessage(Level = LogLevel.Information, Message = "Integration event DLQ configured for topic: {DlqTopicName}")]
    private static partial void LogDlqConfigured(ILogger logger, string dlqTopicName);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Consumer subscribed successfully. Starting consume loop.")]
    private static partial void LogConsumerSubscribed(ILogger logger);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Consume loop finished gracefully (likely cancellation).")]
    private static partial void LogConsumeLoopFinished(ILogger logger);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "IntegrationEventKafkaProcessor stopping due to cancellation request.")]
    private static partial void LogStopping(ILogger logger);

    [LoggerMessage(Level = LogLevel.Critical,
        Message = "Unhandled exception during client setup or ConsumeLoop execution. Retrying in {DelaySeconds} seconds...")]
    private static partial void LogUnhandledException(ILogger logger, Exception ex, double delaySeconds);

    [LoggerMessage(Level = LogLevel.Information, Message = "Retry delay cancelled. IntegrationEventKafkaProcessor stopping.")]
    private static partial void LogRetryDelayCancelled(ILogger logger);

    [LoggerMessage(Level = LogLevel.Information, Message = "IntegrationEventKafkaProcessor stopped.")]
    private static partial void LogStopped(ILogger logger);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Reached end of Partition {Partition} at Offset {Offset} (Topic: {Topic})")]
    private static partial void LogPartitionEof(ILogger logger, int partition, long offset, string topic);

    [LoggerMessage(Level = LogLevel.Debug,
        Message = "Consumed message from Topic {Topic}, Partition {Partition}, Offset {Offset}")]
    private static partial void LogMessageConsumed(ILogger logger, string topic, int partition, long offset);

    [LoggerMessage(Level = LogLevel.Debug,
        Message = "Committed Offset {Offset} for Topic {Topic}, Partition {Partition}")]
    private static partial void LogOffsetCommitted(ILogger logger, long offset, string topic, int partition);

    [LoggerMessage(Level = LogLevel.Critical,
        Message = "FATAL Kafka error during commit for Offset {Offset} (Topic: {Topic}, Partition: {Partition}). Stopping consumer.")]
    private static partial void LogFatalCommitError(ILogger logger, Exception ex, long offset, string topic, int partition);

    [LoggerMessage(Level = LogLevel.Error,
        Message = "Non-fatal Kafka error during commit for Offset {Offset} (Topic: {Topic}, Partition: {Partition}).")]
    private static partial void LogNonFatalCommitError(ILogger logger, Exception ex, long offset, string topic, int partition);

    [LoggerMessage(Level = LogLevel.Error,
        Message = "Consume error: {Reason}. Code: {Code}, IsFatal: {IsFatal}, IsLocal: {IsLocal}, IsBroker: {IsBroker}")]
    private static partial void LogConsumeError(ILogger logger, Exception ex, string reason, ErrorCode code,
        bool isFatal, bool isLocal, bool isBroker);

    [LoggerMessage(Level = LogLevel.Critical, Message = "Fatal Kafka consume error encountered. Stopping processor.")]
    private static partial void LogFatalConsumeError(ILogger logger);

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "Non-fatal consume error. Delaying {DelaySeconds} seconds before next attempt...")]
    private static partial void LogNonFatalConsumeError(ILogger logger, double delaySeconds);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Consume loop cancellation requested during Consume operation.")]
    private static partial void LogConsumeLoopCancelled(ILogger logger);

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "{ExceptionType} occurred within ProcessMessageAsync for Offset {Offset} (Topic: {Topic}, Partition: {Partition}). Offset not committed.")]
    private static partial void LogProcessConcurrencyError(ILogger logger, Exception ex, string exceptionType,
        long offset, string topic, int partition);

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "{ExceptionType} occurred within ProcessMessageAsync but ConsumeResult is null.")]
    private static partial void LogProcessErrorNullResult(ILogger logger, Exception ex, string exceptionType);

    [LoggerMessage(Level = LogLevel.Error,
        Message = "Error processing message at Offset {Offset} (Topic: {Topic}, Partition: {Partition}). Sending to DLQ topic {DlqTopicName}...")]
    private static partial void LogProcessingError(ILogger logger, Exception ex, long offset, string topic,
        int partition, string dlqTopicName);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Successfully sent message from Offset {Offset} (Topic: {Topic}, Partition: {Partition}) to DLQ topic {DlqTopicName}.")]
    private static partial void LogDlqSuccess(ILogger logger, long offset, string topic, int partition, string dlqTopicName);

    [LoggerMessage(Level = LogLevel.Critical,
        Message = "FATAL Kafka error during commit for DLQ'd Offset {Offset} (Topic: {Topic}, Partition: {Partition}).")]
    private static partial void LogFatalDlqCommitError(ILogger logger, Exception ex, long offset, string topic, int partition);

    [LoggerMessage(Level = LogLevel.Error,
        Message = "Non-fatal Kafka error committing DLQ'd Offset {Offset} (Topic: {Topic}, Partition: {Partition}).")]
    private static partial void LogNonFatalDlqCommitError(ILogger logger, Exception ex, long offset, string topic, int partition);

    [LoggerMessage(Level = LogLevel.Error,
        Message = "Failed to send message from Offset {Offset} (Topic: {Topic}, Partition: {Partition}) to DLQ.")]
    private static partial void LogDlqFailure(ILogger logger, long offset, string topic, int partition);

    [LoggerMessage(Level = LogLevel.Error, Message = "Processing error occurred but ConsumeResult was null.")]
    private static partial void LogProcessingErrorNullConsumeResult(ILogger logger, Exception ex);

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "Received null message value at Offset {Offset} (Topic: {Topic}, Partition: {Partition}). Skipping.")]
    private static partial void LogNullMessage(ILogger logger, long offset, string topic, int partition);

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "IntegrationEventOutboxMessage Id {MessageId} (Topic: {Topic}, Partition: {Partition}, Offset: {Offset}) already marked IsProcessed in payload. Skipping.")]
    private static partial void LogAlreadyProcessedPayload(ILogger logger, int messageId, string topic, int partition, long offset);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Starting processing for integration event message Id {MessageId} (Topic: {Topic}, Partition: {Partition}, Offset: {Offset})...")]
    private static partial void LogStartProcessing(ILogger logger, int messageId, string topic, int partition, long offset);

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "IntegrationEventOutboxMessage Id {MessageId} not found in DB for Offset {Offset} (Topic: {Topic}, Partition: {Partition}). Skipping.")]
    private static partial void LogMessageNotFound(ILogger logger, int messageId, long offset, string topic, int partition);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "IntegrationEventOutboxMessage Id {MessageId} (Topic: {Topic}, Partition: {Partition}, Offset: {Offset}) already IsProcessed in DB. Committing offset.")]
    private static partial void LogAlreadyProcessedDb(ILogger logger, int messageId, string topic, int partition, long offset);

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "Concurrency conflict saving IntegrationEventOutboxMessage ID {MessageId} (Topic: {Topic}, Partition: {Partition}, Offset: {Offset}). Rethrowing to allow retry.")]
    private static partial void LogConcurrencyConflict(ILogger logger, Exception ex, int messageId, string topic, int partition, long offset);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Successfully processed integration event message Id {MessageId} (Topic: {Topic}, Partition: {Partition}, Offset: {Offset}).")]
    private static partial void LogProcessSuccess(ILogger logger, int messageId, string topic, int partition, long offset);

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "Sending failed message from Offset {OriginalOffset} (Topic: {OriginalTopic}, Partition: {OriginalPartition}) to DLQ {DlqTopic}: {ErrorMessage}")]
    private static partial void LogSendingToDlq(ILogger logger, long originalOffset, string originalTopic,
        int originalPartition, string dlqTopic, string errorMessage);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Message produced to DLQ topic {DlqTopic}, Partition {Partition}, Offset {Offset}")]
    private static partial void LogProducedToDlq(ILogger logger, string dlqTopic, int partition, long offset);

    [LoggerMessage(Level = LogLevel.Critical,
        Message = "FATAL error producing to DLQ {DlqTopic} for Offset {OriginalOffset} (Topic: {OriginalTopic}, Partition: {OriginalPartition}). Error: {Reason}")]
    private static partial void LogFatalDlqProduceError(ILogger logger, Exception ex, string dlqTopic,
        long originalOffset, string originalTopic, int originalPartition, string reason);

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "DLQ production cancelled for Offset {OriginalOffset} (Topic: {OriginalTopic}, Partition: {OriginalPartition}).")]
    private static partial void LogDlqCancelled(ILogger logger, long originalOffset, string originalTopic, int originalPartition);

    [LoggerMessage(Level = LogLevel.Error,
        Message = "Unexpected error sending to DLQ {DlqTopic} for Offset {OriginalOffset} (Topic: {OriginalTopic}, Partition: {OriginalPartition}).")]
    private static partial void LogDlqUnexpectedError(ILogger logger, Exception ex, string dlqTopic,
        long originalOffset, string originalTopic, int originalPartition);

    [LoggerMessage(Level = LogLevel.Error,
        Message = "Kafka Consumer Error: {Reason} (Code: {Code}, IsFatal: {IsFatal})")]
    private static partial void LogKafkaConsumerError(ILogger logger, string reason, ErrorCode code, bool isFatal);

    [LoggerMessage(Level = LogLevel.Information, Message = "Kafka Consumer Log: [{Facility}] {Message}")]
    private static partial void LogKafkaConsumerLog(ILogger logger, string facility, string message);

    [LoggerMessage(Level = LogLevel.Information, Message = "IntegrationEventKafkaProcessor StopAsync called.")]
    private static partial void LogStopAsyncCalled(ILogger logger);

    [LoggerMessage(Level = LogLevel.Information, Message = "IntegrationEventKafkaProcessor has finished stopping.")]
    private static partial void LogStopAsyncFinished(ILogger logger);
}

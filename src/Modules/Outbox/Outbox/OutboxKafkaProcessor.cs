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

public partial class OutboxKafkaProcessor(
    IOptions<OutboxOptions> outboxOptionsProvider,
    IServiceScopeFactory serviceScopeFactory,
    TimeProvider timeProvider,
    IEventBus eventBus,
    ILogger<OutboxKafkaProcessor> logger
) : BackgroundService
{
    private readonly JsonSerializerOptions _dlqSerializerOptions = new() { WriteIndented = false };
    private readonly OutboxOptions _outboxOptions = outboxOptionsProvider.Value;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        LoggerMessages.LogStarting(logger, _outboxOptions.KafkaConsumer.TopicName);
        LoggerMessages.LogDlqConfigured(logger, _outboxOptions.KafkaDlqProducer.TopicName);

        var setupRetryDelay = TimeSpan.FromSeconds(_outboxOptions.SetupRetryDelaySeconds);
        while (!stoppingToken.IsCancellationRequested)
        {
            IConsumer<Ignore, OutboxMessageDto>? consumer = null;
            IProducer<Null, string>? dlqProducer = null;
            try
            {
                using (consumer = BuildConsumer())
                using (dlqProducer = BuildDlqProducer())
                {
                    consumer.Subscribe(_outboxOptions.KafkaConsumer.TopicName);
                    LoggerMessages.LogConsumerSubscribed(logger);

                    await ConsumeLoop(consumer, dlqProducer, stoppingToken);

                    LoggerMessages.LogConsumeLoopFinished(logger);
                }
            }
            catch (OperationCanceledException)
            {
                LoggerMessages.LogStopping(logger);
                break;
            }
            catch (Exception ex) // Catch build errors or unhandled ConsumeLoop errors
            {
                LoggerMessages.LogUnhandledException(logger, ex, setupRetryDelay.TotalSeconds);

                consumer?.Dispose();
                dlqProducer?.Dispose();

                try
                {
                    await Task.Delay(setupRetryDelay, stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    LoggerMessages.LogRetryDelayCancelled(logger);
                    break;
                }
            }
        }

        LoggerMessages.LogStopped(logger);
    }

    private async Task ConsumeLoop(
        IConsumer<Ignore, OutboxMessageDto> consumer,
        IProducer<Null, string> dlqProducer,
        CancellationToken stoppingToken)
    {
        var dlqTopicName = _outboxOptions.KafkaDlqProducer.TopicName;
        var consumeErrorDelay = TimeSpan.FromSeconds(_outboxOptions.ConsumeErrorDelaySeconds);
        var processingErrorDelay = TimeSpan.FromSeconds(_outboxOptions.ProcessingErrorDelaySeconds);

        // CONSIDER: Ensure the MaxPollIntervalMs (Kafka consumer config, default 5 min)
        // is longer than the maximum expected time for ProcessMessageAsync to complete,
        // otherwise the consumer might be kicked out of the group.

        var firstConsume = true;

        while (!stoppingToken.IsCancellationRequested)
        {
            ConsumeResult<Ignore, OutboxMessageDto>? consumeResult = null;
            try
            {
                if (firstConsume)
                {
                    // The first consume attempt must be wrapped in a Task with immediate consume (0 millisecond timeout) due
                    // if the first consume call is 'consumer.Consume(stoppingToken);' and  if there is no message to fetch at first, it blocks the application startup.
                    consumeResult = await Task.Run(() => consumer.Consume(0), stoppingToken);
                    firstConsume = false;
                    if (consumeResult is null)
                    {
                        continue;
                    }
                }
                else
                {
                    consumeResult = consumer.Consume(stoppingToken);
                }

                var currentTopic = consumeResult.Topic;
                var currentPartition = consumeResult.Partition.Value;
                var currentOffset = consumeResult.Offset.Value;

                if (_outboxOptions.KafkaConsumer.EnablePartitionEof && consumeResult.IsPartitionEOF)
                {
                    LoggerMessages.LogPartitionEof(logger, currentPartition, currentOffset, currentTopic);
                    continue;
                }

                LoggerMessages.LogMessageConsumed(logger, currentTopic, currentPartition, currentOffset);

                using (var scope = serviceScopeFactory.CreateScope())
                {
                    // ProcessMessageAsync MUST throw an exception for processing failures
                    // that should trigger DLQ (e.g., validation, business rule, dependency failure).
                    await ProcessMessageAsync(scope.ServiceProvider, consumeResult, stoppingToken);
                }

                // Commit only after SUCCESSFUL processing (no exception thrown)
                try
                {
                    consumer.Commit(consumeResult);
                    LoggerMessages.LogOffsetCommitted(logger, currentOffset, currentTopic, currentPartition);
                }
                catch (KafkaException kex) when (kex.Error.IsFatal)
                {
                    LoggerMessages.LogFatalCommitError(logger, kex, currentOffset, currentTopic, currentPartition);
                    throw; // Rethrow fatal commit errors to stop the processor
                }
                catch (KafkaException ex)
                {
                    LoggerMessages.LogNonFatalCommitError(logger, ex, currentOffset, currentTopic, currentPartition);
                    // IMPORTANT: Ensure ProcessMessageAsync logic is idempotent, as this message
                    // might be redelivered and reprocessed if the commit failure persists.
                }
            }
            catch (ConsumeException ex)
            {
                // Handle errors during the Consume call itself
                LoggerMessages.LogConsumeError(logger, ex, ex.Error.Reason, ex.Error.Code, ex.Error.IsFatal,
                    ex.Error.IsLocalError, ex.Error.IsBrokerError);

                if (ex.Error.IsFatal)
                {
                    LoggerMessages.LogFatalConsumeError(logger);
                    throw; // Rethrow to exit the loop and potentially the service
                }

                // For non-fatal errors (like temporary disconnects), log and the loop will retry Consume.
                LoggerMessages.LogNonFatalConsumeError(logger, consumeErrorDelay.TotalSeconds);
                await Task.Delay(consumeErrorDelay, stoppingToken);
            }
            catch (OperationCanceledException)
            {
                LoggerMessages.LogConsumeLoopCancelled(logger);
                throw;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // The offset will NOT be committed, allowing Kafka to redeliver.
                if (consumeResult != null)
                {
                    LoggerMessages.LogProcessConcurrencyError(logger, ex, ex.GetType().Name, consumeResult.Offset.Value,
                        consumeResult.Topic, consumeResult.Partition.Value);
                }
                else
                {
                    LoggerMessages.LogProcessErrorNullResult(logger, ex, ex.GetType().Name);
                }

                await Task.Delay(processingErrorDelay, stoppingToken);
            }
            catch (Exception ex) // Catch other processing exceptions
            {
                if (consumeResult != null)
                {
                    LoggerMessages.LogProcessingError(logger, ex, consumeResult.Offset.Value, consumeResult.Topic,
                        consumeResult.Partition.Value, dlqTopicName);

                    var dlqSuccess = await SendToDlqAsync(dlqProducer, dlqTopicName, consumeResult, ex, stoppingToken);

                    if (dlqSuccess)
                    {
                        LoggerMessages.LogDlqSuccess(logger, consumeResult.Offset.Value, consumeResult.Topic,
                            consumeResult.Partition.Value, dlqTopicName);
                        try
                        {
                            consumer.Commit(consumeResult); // Remove poison pill
                        }
                        catch (KafkaException kex) when (kex.Error.IsFatal)
                        {
                            LoggerMessages.LogFatalDlqCommitError(logger, kex, consumeResult.Offset.Value,
                                consumeResult.Topic, consumeResult.Partition.Value);
                            throw; // Stop if we can't commit past a DLQ'd message
                        }
                        catch (KafkaException kex)
                        {
                            LoggerMessages.LogNonFatalDlqCommitError(logger, kex, consumeResult.Offset.Value,
                                consumeResult.Topic, consumeResult.Partition.Value);
                        }
                    }
                    else
                    {
                        LoggerMessages.LogDlqFailure(logger, consumeResult.Offset.Value, consumeResult.Topic,
                            consumeResult.Partition.Value);
                        // Consider adding retry logic within SendToDlqAsync for transient errors
                        // or implementing a circuit breaker if DLQ production fails persistently.
                    }
                }
                else
                {
                    LoggerMessages.LogProcessingErrorNullConsumeResult(logger, ex);
                }

                // Delay slightly after any processing error before next consume attempt
                await Task.Delay(processingErrorDelay, stoppingToken);
            }
        }
    }

    private async Task ProcessMessageAsync(
        IServiceProvider serviceProvider,
        ConsumeResult<Ignore, OutboxMessageDto> consumeResult,
        CancellationToken cancellationToken)
    {
        var outboxMessageDto = consumeResult.Message.Value;

        var topic = consumeResult.Topic;
        var partition = consumeResult.Partition.Value;
        var offset = consumeResult.Offset.Value;

        if (outboxMessageDto == null)
        {
            LoggerMessages.LogNullMessage(logger, offset, topic, partition);
            return;
        }

        if (outboxMessageDto.IsProcessed)
        {
            // This should not happen! Usually indicates an issue upstream or unexpected DB update captured by Debezium
            LoggerMessages.LogAlreadyProcessedPayload(logger, outboxMessageDto.Id, topic, partition, offset);
            return;
        }

        LoggerMessages.LogStartProcessing(logger, outboxMessageDto.Id, topic, partition, offset);

        var dbContext = serviceProvider.GetRequiredService<OutboxDbContext>();

        var outboxMessage = await dbContext
            .OutboxMessages
            .TagWith(nameof(OutboxKafkaProcessor), outboxMessageDto.Id)
            .SingleOrDefaultAsync(x => x.Id == outboxMessageDto.Id, cancellationToken);

        if (outboxMessage == null)
        {
            LoggerMessages.LogMessageNotFound(logger, outboxMessageDto.Id, offset, topic, partition);
            return;
        }

        if (outboxMessage.IsProcessed)
        {
            LoggerMessages.LogAlreadyProcessedDb(logger, outboxMessage.Id, topic, partition, offset);
            return;
        }

        // --- Proceed with processing ---
        var @event = outboxMessage.Event;
        await eventBus.PublishAsync(@event, cancellationToken); // Pass injected eventBus
        outboxMessage.MarkAsProcessed(timeProvider.GetUtcNow()); // Pass injected timeProvider

        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException dbConcurrencyEx)
        {
            LoggerMessages.LogConcurrencyConflict(logger, dbConcurrencyEx, outboxMessageDto.Id, topic, partition,
                offset);
            throw;
        }

        LoggerMessages.LogProcessSuccess(logger, outboxMessageDto.Id, topic, partition, offset);
    }

    private async Task<bool> SendToDlqAsync(IProducer<Null, string> dlqProducer, string dlqTopic,
        ConsumeResult<Ignore, OutboxMessageDto> failedResult,
        Exception exception, CancellationToken cancellationToken)
    {
        var originalTopic = failedResult.Topic;
        var originalPartition = failedResult.Partition.Value;
        var originalOffset = failedResult.Offset.Value;

        try
        {
            var dlqMessage = new DlqMessage<OutboxMessageDto>
            {
                FailedTimestampUtc = DateTimeOffset.UtcNow,
                OriginalTopic = originalTopic,
                OriginalPartition = originalPartition,
                OriginalOffset = originalOffset,
                ConsumerGroupId = _outboxOptions.KafkaConsumer.GroupId, // Use stored options
                ExceptionType = exception.GetType().FullName,
                ExceptionMessage = exception.Message,
                OriginalMessage = failedResult.Message.Value
            };

            var dlqPayload = JsonSerializer.Serialize(dlqMessage, _dlqSerializerOptions);

            LoggerMessages.LogSendingToDlq(logger, originalOffset, originalTopic, originalPartition, dlqTopic,
                exception.Message);

            var kafkaMessage = new Message<Null, string> { Value = dlqPayload };
            var deliveryResult = await dlqProducer.ProduceAsync(dlqTopic, kafkaMessage, cancellationToken);

            LoggerMessages.LogProducedToDlq(logger, deliveryResult.Topic, deliveryResult.Partition.Value,
                deliveryResult.Offset.Value);

            return true;
        }
        catch (ProduceException<Null, string> pex)
        {
            LoggerMessages.LogFatalDlqProduceError(logger, pex, dlqTopic, originalOffset, originalTopic,
                originalPartition, pex.Error.Reason);
            return false;
        }
        catch (OperationCanceledException)
        {
            LoggerMessages.LogDlqCancelled(logger, originalOffset, originalTopic, originalPartition);
            return false;
        }
        catch (Exception ex)
        {
            LoggerMessages.LogDlqUnexpectedError(logger, ex, dlqTopic, originalOffset, originalTopic,
                originalPartition);
            return false;
        }
    }

    // Override StopAsync for explicit cleanup logging if needed,
    // though the using statement + CancellationToken handle the core logic.
    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        LoggerMessages.LogStopAsyncCalled(logger);
        // Allow the base class cancellation mechanism to work
        await base.StopAsync(cancellationToken);
        LoggerMessages.LogStopAsyncFinished(logger);
    }

    private IConsumer<Ignore, OutboxMessageDto> BuildConsumer()
    {
        var kafkaConsumerOptions = outboxOptionsProvider.Value.KafkaConsumer;
        if (!Enum.TryParse(kafkaConsumerOptions.AutoOffsetReset, true,
                out AutoOffsetReset autoOffsetReset)) // Added ignoreCase=true
        {
            throw new ArgumentException(
                $"Auto offset reset parameter {kafkaConsumerOptions.AutoOffsetReset} is invalid, could not be parsed into a valid AutoOffsetReset.");
        }

        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = kafkaConsumerOptions.BootstrapServers,
            GroupId = kafkaConsumerOptions.GroupId,
            AutoOffsetReset = autoOffsetReset,
            EnableAutoCommit = false, // Explicitly disable auto-commit for manual control
            EnablePartitionEof = kafkaConsumerOptions.EnablePartitionEof,
            SessionTimeoutMs = kafkaConsumerOptions.SessionTimeoutMs,
            HeartbeatIntervalMs = kafkaConsumerOptions.HeartbeatIntervalMs
        };

        // Build the consumer with handlers
        return new ConsumerBuilder<Ignore, OutboxMessageDto>(consumerConfig)
            .SetValueDeserializer(new OutboxMessageDtoDeserializer())
            .SetErrorHandler((_, e) =>
                LoggerMessages.LogKafkaConsumerError(logger, e.Reason, e.Code, e.IsFatal))
            .SetLogHandler((_, log) =>
            {
                var logLevel = ParseLogLevel(log.Level);
                // Filter noisy logs if needed
                if (logLevel >= LogLevel.Information)
                {
                    LoggerMessages.LogKafkaConsumerLog(logger, log.Facility, log.Message);
                }
            })
            .Build();
    }

    private IProducer<Null, string> BuildDlqProducer()
    {
        var kafkaProducerOptions = outboxOptionsProvider.Value.KafkaDlqProducer;

        var producerConfig = new ProducerConfig
        {
            BootstrapServers = kafkaProducerOptions.BootstrapServers, Acks = Acks.All
            // Add other relevant producer settings from options if needed
        };

        return new ProducerBuilder<Null, string>(producerConfig).Build();
    }

    private static LogLevel ParseLogLevel(SyslogLevel logLevel)
    {
        return logLevel switch
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
    }

    private static partial class LoggerMessages
    {
        [LoggerMessage(Level = LogLevel.Information,
            Message = "OutboxKafkaProcessor starting. Subscribing to topic: {TopicName}")]
        public static partial void LogStarting(ILogger logger, string topicName);

        [LoggerMessage(Level = LogLevel.Information, Message = "DLQ configured for topic: {DlqTopicName}")]
        public static partial void LogDlqConfigured(ILogger logger, string dlqTopicName);

        [LoggerMessage(Level = LogLevel.Information,
            Message = "Consumer subscribed successfully. Starting consume loop.")]
        public static partial void LogConsumerSubscribed(ILogger logger);

        [LoggerMessage(Level = LogLevel.Information,
            Message = "Consume loop finished gracefully (likely cancellation).")]
        public static partial void LogConsumeLoopFinished(ILogger logger);

        [LoggerMessage(Level = LogLevel.Information,
            Message = "OutboxKafkaProcessor stopping due to cancellation request.")]
        public static partial void LogStopping(ILogger logger);

        [LoggerMessage(Level = LogLevel.Critical,
            Message =
                "Unhandled exception during client setup or ConsumeLoop execution. Retrying connection/setup in {DelaySeconds} seconds...")]
        public static partial void LogUnhandledException(ILogger logger, Exception ex, double delaySeconds);

        [LoggerMessage(Level = LogLevel.Information, Message = "Retry delay cancelled. OutboxKafkaProcessor stopping.")]
        public static partial void LogRetryDelayCancelled(ILogger logger);

        [LoggerMessage(Level = LogLevel.Information, Message = "OutboxKafkaProcessor stopped.")]
        public static partial void LogStopped(ILogger logger);

        [LoggerMessage(Level = LogLevel.Information,
            Message = "Reached end of Partition {Partition} at Offset {Offset} (Topic: {Topic})")]
        public static partial void LogPartitionEof(ILogger logger, int partition, long offset, string topic);

        [LoggerMessage(Level = LogLevel.Debug,
            Message = "Consumed message from Topic {Topic}, Partition {Partition}, Offset {Offset}")]
        public static partial void LogMessageConsumed(ILogger logger, string topic, int partition, long offset);

        [LoggerMessage(Level = LogLevel.Debug,
            Message = "Committed Offset {Offset} for Topic {Topic}, Partition {Partition}")]
        public static partial void LogOffsetCommitted(ILogger logger, long offset, string topic, int partition);

        [LoggerMessage(Level = LogLevel.Critical,
            Message =
                "FATAL Kafka error during commit for Offset {Offset} (Topic: {Topic}, Partition: {Partition}). Stopping consumer.")]
        public static partial void LogFatalCommitError(ILogger logger, Exception ex, long offset, string topic,
            int partition);

        [LoggerMessage(Level = LogLevel.Error,
            Message =
                "Non-fatal Kafka error during commit for Offset {Offset} (Topic: {Topic}, Partition: {Partition}). Processing succeeded but commit failed.")]
        public static partial void LogNonFatalCommitError(ILogger logger, Exception ex, long offset, string topic,
            int partition);

        [LoggerMessage(Level = LogLevel.Error,
            Message =
                "Consume error: {Reason}. Code: {Code}, IsFatal: {IsFatal}, IsLocal: {IsLocal}, IsBroker: {IsBroker}")]
        public static partial void LogConsumeError(ILogger logger, Exception ex, string reason, ErrorCode code,
            bool isFatal, bool isLocal, bool isBroker);

        [LoggerMessage(Level = LogLevel.Critical,
            Message = "Fatal Kafka consume error encountered. Stopping processor.")]
        public static partial void LogFatalConsumeError(ILogger logger);

        [LoggerMessage(Level = LogLevel.Warning,
            Message = "Non-fatal consume error. Delaying {DelaySeconds} seconds before next attempt...")]
        public static partial void LogNonFatalConsumeError(ILogger logger, double delaySeconds);

        [LoggerMessage(Level = LogLevel.Information,
            Message = "Consume loop cancellation requested during Consume operation.")]
        public static partial void LogConsumeLoopCancelled(ILogger logger);

        [LoggerMessage(Level = LogLevel.Warning,
            Message =
                "{ExceptionType} occured within ProcessMessageAsync for Offset {Offset} (Topic: {Topic}, Partition: {Partition}). Offset not committed.")]
        public static partial void LogProcessConcurrencyError(ILogger logger, Exception ex, string exceptionType,
            long offset, string topic, int partition);

        [LoggerMessage(Level = LogLevel.Warning,
            Message =
                "{ExceptionType} occured within ProcessMessageAsync but ConsumeResult is null. Offset not committed.")]
        public static partial void LogProcessErrorNullResult(ILogger logger, Exception ex, string exceptionType);

        [LoggerMessage(Level = LogLevel.Error,
            Message =
                "Error processing message at Offset {Offset} (Topic: {Topic}, Partition: {Partition}). Sending to DLQ topic {DlqTopicName}...")]
        public static partial void LogProcessingError(ILogger logger, Exception ex, long offset, string topic,
            int partition, string dlqTopicName);

        [LoggerMessage(Level = LogLevel.Information,
            Message =
                "Successfully sent message from Offset {Offset} (Topic: {Topic}, Partition: {Partition}) to DLQ topic {DlqTopicName}. Committing original offset.")]
        public static partial void LogDlqSuccess(ILogger logger, long offset, string topic, int partition,
            string dlqTopicName);

        [LoggerMessage(Level = LogLevel.Critical,
            Message =
                "FATAL Kafka error during commit for DLQ'd Offset {Offset} (Topic: {Topic}, Partition: {Partition}).")]
        public static partial void LogFatalDlqCommitError(ILogger logger, Exception ex, long offset, string topic,
            int partition);

        [LoggerMessage(Level = LogLevel.Error,
            Message =
                "Non-fatal Kafka error committing DLQ'd Offset {Offset} (Topic: {Topic}, Partition: {Partition}). Consumer may reprocess.")]
        public static partial void LogNonFatalDlqCommitError(ILogger logger, Exception ex, long offset, string topic,
            int partition);

        [LoggerMessage(Level = LogLevel.Error,
            Message =
                "Failed to send message from Offset {Offset} (Topic: {Topic}, Partition: {Partition}) to DLQ. **Offset will not be committed.**")]
        public static partial void LogDlqFailure(ILogger logger, long offset, string topic, int partition);

        [LoggerMessage(Level = LogLevel.Error,
            Message = "Processing error occurred but ConsumeResult was null. Cannot DLQ or commit.")]
        public static partial void LogProcessingErrorNullConsumeResult(ILogger logger, Exception ex);

        [LoggerMessage(Level = LogLevel.Warning,
            Message =
                "Received null message value at Offset {Offset} (Topic: {Topic}, Partition: {Partition}). Committing offset and skipping processing.")]
        public static partial void LogNullMessage(ILogger logger, long offset, string topic, int partition);

        [LoggerMessage(Level = LogLevel.Warning,
            Message =
                "Outbox message Id {MessageId} (Topic: {Topic}, Partition: {Partition}, Offset: {Offset}) was already marked 'IsProcessed=true' in the Kafka message payload. Skipping processing and committing offset.")]
        public static partial void LogAlreadyProcessedPayload(ILogger logger, int messageId, string topic,
            int partition, long offset);

        [LoggerMessage(Level = LogLevel.Information,
            Message =
                "Starting processing for message Id {MessageId} (Topic: {Topic}, Partition: {Partition}, Offset: {Offset})...")]
        public static partial void LogStartProcessing(ILogger logger, int messageId, string topic, int partition,
            long offset);

        [LoggerMessage(Level = LogLevel.Warning,
            Message =
                "OutboxMessage Id {MessageId} not found in database for Offset {Offset} (Topic: {Topic}, Partition: {Partition}). Committing offset and skipping.")]
        public static partial void LogMessageNotFound(ILogger logger, int messageId, long offset, string topic,
            int partition);

        [LoggerMessage(Level = LogLevel.Information,
            Message =
                "Outbox message Id {MessageId} (Topic: {Topic}, Partition: {Partition}, Offset: {Offset}) was already marked 'IsProcessed=true' in the Database. Committing offset.")]
        public static partial void LogAlreadyProcessedDb(ILogger logger, int messageId, string topic, int partition,
            long offset);

        [LoggerMessage(Level = LogLevel.Warning,
            Message =
                "Concurrency conflict saving OutboxMessage ID {MessageId} (Topic: {Topic}, Partition: {Partition}, Offset: {Offset}). Rethrowing to allow retry.")]
        public static partial void LogConcurrencyConflict(ILogger logger, Exception ex, int messageId, string topic,
            int partition, long offset);

        [LoggerMessage(Level = LogLevel.Information,
            Message =
                "Successfully processed message Id {MessageId} (Topic: {Topic}, Partition: {Partition}, Offset: {Offset}).")]
        public static partial void LogProcessSuccess(ILogger logger, int messageId, string topic, int partition,
            long offset);

        [LoggerMessage(Level = LogLevel.Warning,
            Message =
                "Sending failed message from Offset {OriginalOffset} (Topic: {OriginalTopic}, Partition: {OriginalPartition}) to DLQ topic {DlqTopic} due to error: {ErrorMessage}")]
        public static partial void LogSendingToDlq(ILogger logger, long originalOffset, string originalTopic,
            int originalPartition, string dlqTopic, string errorMessage);

        [LoggerMessage(Level = LogLevel.Information,
            Message = "Message successfully produced to DLQ topic {DlqTopic}, Partition {Partition}, Offset {Offset}")]
        public static partial void LogProducedToDlq(ILogger logger, string dlqTopic, int partition, long offset);

        [LoggerMessage(Level = LogLevel.Critical,
            Message =
                "FATAL error producing message to DLQ topic {DlqTopic} for original Offset {OriginalOffset} (Topic: {OriginalTopic}, Partition: {OriginalPartition}). Error: {Reason}")]
        public static partial void LogFatalDlqProduceError(ILogger logger, Exception ex, string dlqTopic,
            long originalOffset, string originalTopic, int originalPartition, string reason);

        [LoggerMessage(Level = LogLevel.Warning,
            Message =
                "DLQ production cancelled for original Offset {OriginalOffset} (Topic: {OriginalTopic}, Partition: {OriginalPartition}).")]
        public static partial void LogDlqCancelled(ILogger logger, long originalOffset, string originalTopic,
            int originalPartition);

        [LoggerMessage(Level = LogLevel.Error,
            Message =
                "Unexpected error sending message to DLQ topic {DlqTopic} for original Offset {OriginalOffset} (Topic: {OriginalTopic}, Partition: {OriginalPartition}).")]
        public static partial void LogDlqUnexpectedError(ILogger logger, Exception ex, string dlqTopic,
            long originalOffset, string originalTopic, int originalPartition);

        [LoggerMessage(Level = LogLevel.Error,
            Message = "Kafka Consumer Error: {Reason} (Code: {Code}, IsFatal: {IsFatal})")]
        public static partial void LogKafkaConsumerError(ILogger logger, string reason, ErrorCode code, bool isFatal);

        [LoggerMessage(Level = LogLevel.Information, Message = "Kafka Consumer Log: [{Facility}] {Message}")]
        public static partial void LogKafkaConsumerLog(ILogger logger, string facility, string message);

        [LoggerMessage(Level = LogLevel.Information, Message = "OutboxKafkaProcessor StopAsync called.")]
        public static partial void LogStopAsyncCalled(ILogger logger);

        [LoggerMessage(Level = LogLevel.Information, Message = "OutboxKafkaProcessor has finished stopping.")]
        public static partial void LogStopAsyncFinished(ILogger logger);
    }
}

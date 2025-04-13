using System.Text.Json;
using Common.Application.EventBus;
using Common.Application.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Outbox.Persistence;
using Common.Application.Persistence;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Outbox;

public class OutboxKafkaProcessor(
    IOptions<OutboxOptions> outboxOptionsProvider,
    IServiceScopeFactory serviceScopeFactory,
    TimeProvider timeProvider,
    IEventBus eventBus,
    ILogger<OutboxKafkaProcessor> logger
) : BackgroundService
{
    private readonly OutboxOptions _outboxOptions = outboxOptionsProvider.Value;
    private readonly JsonSerializerOptions _dlqSerializerOptions = new() { WriteIndented = false };

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("OutboxKafkaProcessor starting. Subscribing to topic: {TopicName}",
            _outboxOptions.KafkaConsumer.TopicName);
        logger.LogInformation("DLQ configured for topic: {DlqTopicName}", _outboxOptions.KafkaDlqProducer.TopicName);

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
                    logger.LogInformation("Consumer subscribed successfully. Starting consume loop.");

                    await ConsumeLoop(consumer, dlqProducer, stoppingToken);

                    logger.LogInformation("Consume loop finished gracefully (likely cancellation).");
                }
            }
            catch (OperationCanceledException)
            {
                logger.LogInformation("OutboxKafkaProcessor stopping due to cancellation request.");
                break;
            }
            catch (Exception ex) // Catch build errors or unhandled ConsumeLoop errors
            {
                logger.LogCritical(ex,
                    "Unhandled exception during client setup or ConsumeLoop execution. Retrying connection/setup in {DelaySeconds} seconds...",
                    setupRetryDelay.TotalSeconds);

                consumer?.Dispose();
                dlqProducer?.Dispose();

                try
                {
                    await Task.Delay(setupRetryDelay, stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    logger.LogInformation("Retry delay cancelled. OutboxKafkaProcessor stopping.");
                    break;
                }
            }
        }

        logger.LogInformation("OutboxKafkaProcessor stopped.");
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
                    logger.LogInformation("Reached end of Partition {Partition} at Offset {Offset} (Topic: {Topic})",
                        currentPartition, currentOffset, currentTopic);
                    continue;
                }

                logger.LogDebug("Consumed message from Topic {Topic}, Partition {Partition}, Offset {Offset}",
                    currentTopic, currentPartition, currentOffset);

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
                    logger.LogDebug("Committed Offset {Offset} for Topic {Topic}, Partition {Partition}",
                        currentOffset, currentTopic, currentPartition);
                }
                catch (KafkaException kex) when (kex.Error.IsFatal)
                {
                    logger.LogCritical(kex,
                        "FATAL Kafka error during commit for Offset {Offset} (Topic: {Topic}, Partition: {Partition}). Stopping consumer.",
                        currentOffset, currentTopic, currentPartition);
                    throw; // Rethrow fatal commit errors to stop the processor
                }
                catch (KafkaException ex)
                {
                    logger.LogError(ex,
                        "Non-fatal Kafka error during commit for Offset {Offset} (Topic: {Topic}, Partition: {Partition}). Processing succeeded but commit failed.",
                        currentOffset, currentTopic, currentPartition);
                    // IMPORTANT: Ensure ProcessMessageAsync logic is idempotent, as this message
                    // might be redelivered and reprocessed if the commit failure persists.
                }
            }
            catch (ConsumeException ex)
            {
                // Handle errors during the Consume call itself
                logger.LogError(ex,
                    "Consume error: {Reason}. Code: {Code}, IsFatal: {IsFatal}, IsLocal: {IsLocal}, IsBroker: {IsBroker}",
                    ex.Error.Reason, ex.Error.Code, ex.Error.IsFatal, ex.Error.IsLocalError, ex.Error.IsBrokerError);

                if (ex.Error.IsFatal)
                {
                    logger.LogCritical("Fatal Kafka consume error encountered. Stopping processor.");
                    throw; // Rethrow to exit the loop and potentially the service
                }

                // For non-fatal errors (like temporary disconnects), log and the loop will retry Consume.
                logger.LogWarning("Non-fatal consume error. Delaying {DelaySeconds} seconds before next attempt...",
                    consumeErrorDelay.TotalSeconds); // Use variable
                await Task.Delay(consumeErrorDelay, stoppingToken);
            }
            catch (OperationCanceledException)
            {
                logger.LogInformation("Consume loop cancellation requested during Consume operation.");
                throw;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // The offset will NOT be committed, allowing Kafka to redeliver.
                if (consumeResult != null)
                {
                    logger.LogWarning(ex,
                        "{ExceptionType} occured within ProcessMessageAsync for Offset {Offset} (Topic: {Topic}, Partition: {Partition}). Offset not committed.",
                        ex.GetType().Name,
                        consumeResult.Offset.Value,
                        consumeResult.Topic,
                        consumeResult.Partition.Value);
                }
                else
                {
                    logger.LogWarning(ex,
                        "{ExceptionType} occured within ProcessMessageAsync but ConsumeResult is null. Offset not committed.",
                        ex.GetType().Name);
                }

                await Task.Delay(processingErrorDelay, stoppingToken);
            }
            catch (Exception ex) // Catch other processing exceptions
            {
                if (consumeResult != null)
                {
                    logger.LogError(ex,
                        "Error processing message at Offset {Offset} (Topic: {Topic}, Partition: {Partition}). Sending to DLQ topic {DlqTopicName}...",
                        consumeResult.Offset.Value,
                        consumeResult.Topic,
                        consumeResult.Partition.Value,
                        dlqTopicName);

                    var dlqSuccess = await SendToDlqAsync(dlqProducer, dlqTopicName, consumeResult, ex, stoppingToken);

                    if (dlqSuccess)
                    {
                        logger.LogInformation(
                            "Successfully sent message from Offset {Offset} (Topic: {Topic}, Partition: {Partition}) to DLQ topic {DlqTopicName}. Committing original offset.",
                            consumeResult.Offset.Value,
                            consumeResult.Topic,
                            consumeResult.Partition.Value,
                            dlqTopicName);
                        try
                        {
                            consumer.Commit(consumeResult); // Remove poison pill
                        }
                        catch (KafkaException kex) when (kex.Error.IsFatal)
                        {
                            logger.LogCritical(kex,
                                "FATAL Kafka error during commit for DLQ'd Offset {Offset} (Topic: {Topic}, Partition: {Partition}).",
                                consumeResult.Offset.Value, consumeResult.Topic, consumeResult.Partition.Value);
                            throw; // Stop if we can't commit past a DLQ'd message
                        }
                        catch (KafkaException kex)
                        {
                            logger.LogError(kex,
                                "Non-fatal Kafka error committing DLQ'd Offset {Offset} (Topic: {Topic}, Partition: {Partition}). Consumer may reprocess.",
                                consumeResult.Offset.Value, consumeResult.Topic, consumeResult.Partition.Value);
                        }
                    }
                    else
                    {
                        logger.LogError(
                            "Failed to send message from Offset {Offset} (Topic: {Topic}, Partition: {Partition}) to DLQ. **Offset will not be committed.**",
                            consumeResult.Offset.Value,
                            consumeResult.Topic,
                            consumeResult.Partition.Value);
                        // Consider adding retry logic within SendToDlqAsync for transient errors
                        // or implementing a circuit breaker if DLQ production fails persistently.
                    }
                }
                else
                {
                    logger.LogError(ex, "Processing error occurred but ConsumeResult was null. Cannot DLQ or commit.");
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
            logger.LogWarning(
                "Received null message value at Offset {Offset} (Topic: {Topic}, Partition: {Partition}). Committing offset and skipping processing.",
                offset, topic, partition);
            return;
        }

        if (outboxMessageDto.IsProcessed)
        {
            // This should not happen! Usually indicates an issue upstream or unexpected DB update captured by Debezium
            logger.LogWarning(
                "Outbox message Id {MessageId} (Topic: {Topic}, Partition: {Partition}, Offset: {Offset}) was already marked 'IsProcessed=true' in the Kafka message payload. Skipping processing and committing offset.",
                outboxMessageDto.Id, topic, partition, offset);
            return;
        }

        logger.LogInformation(
            "Starting processing for message Id {MessageId} (Topic: {Topic}, Partition: {Partition}, Offset: {Offset})...",
            outboxMessageDto.Id, topic, partition, offset);

        var dbContext = serviceProvider.GetRequiredService<OutboxDbContext>();

        var outboxMessage = await dbContext
            .OutboxMessages
            .TagWith(nameof(OutboxKafkaProcessor), outboxMessageDto.Id)
            .SingleOrDefaultAsync(x => x.Id == outboxMessageDto.Id, cancellationToken);

        if (outboxMessage == null)
        {
            logger.LogWarning(
                "OutboxMessage Id {MessageId} not found in database for Offset {Offset} (Topic: {Topic}, Partition: {Partition}). Committing offset and skipping.",
                outboxMessageDto.Id, offset, topic, partition);
            return;
        }

        if (outboxMessage.IsProcessed)
        {
            logger.LogInformation(
                "Outbox message Id {MessageId} (Topic: {Topic}, Partition: {Partition}, Offset: {Offset}) was already marked 'IsProcessed=true' in the Database. Committing offset.",
                outboxMessage.Id, topic, partition, offset);
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
            logger.LogWarning(dbConcurrencyEx,
                "Concurrency conflict saving OutboxMessage ID {MessageId} (Topic: {Topic}, Partition: {Partition}, Offset: {Offset}). Rethrowing to allow retry.",
                outboxMessageDto.Id, topic, partition, offset);
            throw;
        }

        logger.LogInformation(
            "Successfully processed message Id {MessageId} (Topic: {Topic}, Partition: {Partition}, Offset: {Offset}).",
            outboxMessageDto.Id, topic, partition, offset);
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

            logger.LogWarning(
                "Sending failed message from Offset {OriginalOffset} (Topic: {OriginalTopic}, Partition: {OriginalPartition}) to DLQ topic {DlqTopic} due to error: {ErrorMessage}",
                originalOffset, originalTopic, originalPartition, dlqTopic, exception.Message);

            var kafkaMessage = new Message<Null, string> { Value = dlqPayload };
            var deliveryResult = await dlqProducer.ProduceAsync(dlqTopic, kafkaMessage, cancellationToken);

            logger.LogInformation(
                "Message successfully produced to DLQ topic {DlqTopic}, Partition {Partition}, Offset {Offset}",
                deliveryResult.Topic,
                deliveryResult.Partition.Value,
                deliveryResult.Offset.Value);

            return true;
        }
        catch (ProduceException<Null, string> pex)
        {
            logger.LogCritical(pex,
                "FATAL error producing message to DLQ topic {DlqTopic} for original Offset {OriginalOffset} (Topic: {OriginalTopic}, Partition: {OriginalPartition}). Error: {Reason}",
                dlqTopic, originalOffset, originalTopic, originalPartition, pex.Error.Reason);
            return false;
        }
        catch (OperationCanceledException)
        {
            logger.LogWarning(
                "DLQ production cancelled for original Offset {OriginalOffset} (Topic: {OriginalTopic}, Partition: {OriginalPartition}).",
                originalOffset, originalTopic, originalPartition);
            return false;
        }
        catch (Exception ex)
        {
            logger.LogError(ex,
                "Unexpected error sending message to DLQ topic {DlqTopic} for original Offset {OriginalOffset} (Topic: {OriginalTopic}, Partition: {OriginalPartition}).",
                dlqTopic, originalOffset, originalTopic, originalPartition);
            return false;
        }
    }

    // Override StopAsync for explicit cleanup logging if needed,
    // though the using statement + CancellationToken handle the core logic.
    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("OutboxKafkaProcessor StopAsync called.");
        // Allow the base class cancellation mechanism to work
        await base.StopAsync(cancellationToken);
        logger.LogInformation("OutboxKafkaProcessor has finished stopping.");
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
            HeartbeatIntervalMs = kafkaConsumerOptions.HeartbeatIntervalMs,
        };

        // Build the consumer with handlers
        return new ConsumerBuilder<Ignore, OutboxMessageDto>(consumerConfig)
            .SetValueDeserializer(new OutboxMessageDtoDeserializer())
            .SetErrorHandler((_, e) =>
                logger.LogError("Kafka Consumer Error: {Reason} (Code: {Code}, IsFatal: {IsFatal})", e.Reason, e.Code,
                    e.IsFatal))
            .SetLogHandler((_, log) =>
            {
                var logLevel = ParseLogLevel(log.Level);
                // Filter noisy logs if needed
                if (logLevel >= LogLevel.Information)
                {
                    logger.Log(logLevel, "Kafka Consumer Log: [{Facility}] {Message}", log.Facility, log.Message);
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
}

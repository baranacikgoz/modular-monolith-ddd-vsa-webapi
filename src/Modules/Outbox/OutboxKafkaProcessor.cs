using System.Text.Json;
using Common.Application.EventBus;
using Common.Application.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Outbox.Persistence;
using Common.Application.Persistence;

namespace Outbox;

using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options; // Required for IOptions
using System;
using System.Threading;
using System.Threading.Tasks;

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

                // --- Ensure disposal if partially created before error ---
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

        // CONSIdER: Ensure the MaxPollIntervalMs (Kafka consumer config, default 5 min)
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

                if (_outboxOptions.KafkaConsumer.EnablePartitionEof && consumeResult.IsPartitionEOF)
                {
                    logger.LogInformation("Reached end of partition {Partition}, offset {Offset}.",
                        consumeResult.Partition, consumeResult.Offset);
                    continue;
                }

                logger.LogDebug("Consumed message from {TopicPartitionOffset}",
                    consumeResult.TopicPartitionOffset);

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
                    logger.LogDebug("Committed offset {Offset}", consumeResult.TopicPartitionOffset);
                }
                catch (KafkaException kex) when (kex.Error.IsFatal)
                {
                    logger.LogCritical(kex, "FATAL Kafka error during commit for offset {Offset}. Stopping consumer.",
                        consumeResult.TopicPartitionOffset);
                    throw; // Rethrow fatal commit errors to stop the processor
                }
                catch (KafkaException ex)
                {
                    logger.LogError(ex,
                        "Non-fatal Kafka error during commit for offset {Offset}. Processing succeeded but commit failed.",
                        consumeResult.TopicPartitionOffset);
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
                var failedOffset = consumeResult?.TopicPartitionOffset.ToString() ?? "N/A";
                // The offset will NOT be committed, allowing Kafka to redeliver.
                logger.LogWarning(ex,
                    "{ExcetionType} occured within ProcessMessageAsync for offset {Offset}. Offset not committed.", ex.GetType(), failedOffset);
            }
            catch (Exception ex) // Catch exceptions from ProcessMessageAsync or other logic
            {
                var failedOffset = consumeResult?.TopicPartitionOffset.ToString() ?? "N/A";
                logger.LogError(ex,
                    "Error processing message at offset {Offset}. Sending to DLQ topic {DlqTopicName}...",
                    failedOffset, dlqTopicName);

                if (consumeResult != null) // Ensure we have a message to DLQ
                {
                    var dlqSuccess = await SendToDlqAsync(dlqProducer, dlqTopicName, consumeResult, ex, stoppingToken);

                    if (dlqSuccess)
                    {
                        logger.LogInformation(
                            "Successfully sent message from offset {Offset} to DLQ topic {DlqTopicName}. Committing original offset.", failedOffset, dlqTopicName);
                        // Commit original offset ONLY AFTER successful DLQ
                        try
                        {
                            consumer.Commit(consumeResult); // Remove poison pill
                        }
                        catch (KafkaException kex) when (kex.Error.IsFatal)
                        {
                            logger.LogCritical(kex, "FATAL Kafka error during commit for DLQ'd offset {Offset}.",
                                failedOffset);
                            throw; // Stop if we can't commit past a DLQ'd message
                        }
                        catch (KafkaException kex)
                        {
                            logger.LogError(kex,
                                "Non-fatal Kafka error committing DLQ'd offset {Offset}. Consumer may reprocess.", failedOffset);
                        }
                    }
                    else
                    {
                        logger.LogError(
                            "Failed to send message from offset {Offset} to DLQ. **Offset will not be committed.** Consumer will retry processing and DLQ.", failedOffset);
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

    /// <summary>
    /// Processes the consumed message. Includes fetching from DB, business logic,
    /// and updating the OutboxMessage status.
    /// MUST throw exceptions for processing errors that require DLQ intervention.
    /// SHOULD handle DbUpdateConcurrencyException by logging and re-throwing to prevent commit without DLQ.
    /// </summary>
    private async Task ProcessMessageAsync(
        IServiceProvider serviceProvider,
        ConsumeResult<Ignore, OutboxMessageDto> consumeResult,
        CancellationToken cancellationToken)
    {
        var outboxMessageDto = consumeResult.Message.Value;
        var offset = consumeResult.TopicPartitionOffset;

        // Handle potential null message value (e.g., Kafka tombstone if config changes)
        if (outboxMessageDto == null)
        {
            logger.LogCritical(
                "Received null message value at offset {Offset}. Committing offset and skipping processing.", offset);
            // Treat as processed/ignorable - return normally so offset gets committed.
            return;
        }

        if (outboxMessageDto.IsProcessed)
        {
            // This should not happen! Usually indicates an issue upstream or unexpected DB update captured by Debezium
            logger.LogWarning(
                "Outbox message Id {MessageId} (Offset: {Offset}) was already marked 'IsProcessed=true' in the Kafka message payload. This might indicate an upstream issue. Skipping processing and committing offset.",
                outboxMessageDto.Id, offset);

            return;
        }

        logger.LogInformation("Starting processing for message Id {MessageId} at {Offset}...", outboxMessageDto.Id, offset);

        var dbContext = serviceProvider.GetRequiredService<OutboxDbContext>();

        var outboxMessage = await dbContext
            .OutboxMessages
            .TagWith(nameof(OutboxKafkaProcessor), outboxMessageDto.Id)
            .SingleOrDefaultAsync(x => x.Id == outboxMessageDto.Id, cancellationToken);

        if (outboxMessage is null)
        {
            // Throw exception and trigger DLQ
            throw new InvalidOperationException($"Could not find outbox message with Id {outboxMessageDto.Id}.");
        }

        if (outboxMessage.IsProcessed)
        {
            // By low chance, but probably processed by another instance. Not an issue.
            logger.LogInformation("Outbox message Id {MessageId} (Offset: {Offset}) was already marked 'IsProcessed=true' in the Database. Likely processed by another instance. Committing offset.",
                outboxMessage.Id, offset);
            return;
        }

        var @event = outboxMessage.Event;
        await eventBus.PublishAsync(@event, cancellationToken);
        outboxMessage.MarkAsProcessed(timeProvider.GetUtcNow());
        await dbContext.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Successfully processed message Id {MessageId} at {Offset}.", outboxMessageDto.Id, offset);
    }

    private async Task<bool> SendToDlqAsync(IProducer<Null, string> dlqProducer, string dlqTopic,
        ConsumeResult<Ignore, OutboxMessageDto> failedResult,
        Exception exception, CancellationToken cancellationToken)
    {
        try
        {
            var dlqMessage = new DlqMessage<OutboxMessageDto> // Define this helper class
            {
                FailedTimestampUtc = DateTimeOffset.UtcNow,
                OriginalTopic = failedResult.Topic,
                OriginalPartition = failedResult.Partition.Value,
                OriginalOffset = failedResult.Offset.Value,
                ConsumerGroupId = outboxOptionsProvider.Value.KafkaConsumer.GroupId,
                ExceptionType = exception.GetType().FullName,
                ExceptionMessage = exception.Message,
                ExceptionStackTrace = exception.StackTrace, // Be cautious with stack trace size/sensitivity
                OriginalMessage = failedResult.Message.Value // Include the original message payload
            };

            // Serialize the DLQ message info to JSON
            var dlqPayload =
                JsonSerializer.Serialize(dlqMessage, _dlqSerializerOptions); // Use options consistent with needs

            logger.LogWarning(
                "Sending failed message from {OriginalTopicPartitionOffset} to DLQ topic {DlqTopic} due to error: {ErrorMessage}",
                failedResult.TopicPartitionOffset, dlqTopic, exception.Message);

            // Produce the message to the DLQ topic. Use the original message's timestamp if desired.
            // Using Message<Null, string> for the DLQ message itself.
            var kafkaMessage = new Message<Null, string> { Value = dlqPayload };
            var deliveryResult = await dlqProducer.ProduceAsync(dlqTopic, kafkaMessage, cancellationToken);

            logger.LogInformation("Message successfully produced to DLQ topic {DlqTopicPartitionOffset}",
                deliveryResult.TopicPartitionOffset);
            return true; // Indicate success
        }
        catch (ProduceException<Null, string> pex)
        {
            logger.LogCritical(pex,
                "FATAL error producing message to DLQ topic {DlqTopic} for original offset {OriginalOffset}. Error: {Reason}",
                dlqTopic, failedResult.TopicPartitionOffset, pex.Error.Reason);
            return false; // Indicate failure
        }
        catch (Exception ex)
        {
            logger.LogError(ex,
                "Unexpected error sending message to DLQ topic {DlqTopic} for original offset {OriginalOffset}.",
                dlqTopic, failedResult.TopicPartitionOffset);
            return false; // Indicate failure
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
        if (!Enum.TryParse(kafkaConsumerOptions.AutoOffsetReset, out AutoOffsetReset autoOffsetReset))
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

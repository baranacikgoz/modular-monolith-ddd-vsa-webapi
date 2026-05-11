using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text.Json;
using Common.Application.Options;
using Common.Application.Persistence.Outbox;
using Common.Domain.Events;
using Common.Infrastructure.Extensions;
using Common.Infrastructure.Persistence.Outbox;
using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Outbox.Telemetry;

namespace Outbox;

public abstract partial class KafkaOutboxProcessorBase<TDto>(
    IOptions<OutboxOptions> outboxOptionsProvider,
    IServiceScopeFactory serviceScopeFactory,
    TimeProvider timeProvider,
    ILogger logger
) : BackgroundService
    where TDto : class, IOutboxMessageDto
{
    private readonly JsonSerializerOptions _dlqSerializerOptions = new() { WriteIndented = false };
    private readonly ConcurrentDictionary<int, int> _retryCount = new();
    private int _consecutiveDlqFailures;

    protected abstract IDeserializer<TDto> CreateDeserializer();
    protected abstract KafkaConsumer GetConsumerConfig(OutboxOptions options);
    protected abstract KafkaProducer GetDlqConfig(OutboxOptions options);
    protected abstract Task<IOutboxMessage?> LoadEntityAsync(IOutboxDbContext db, int id, CancellationToken ct);
    protected abstract Task DispatchEventAsync(IEvent @event, IServiceProvider sp, CancellationToken ct);

    private void VerifyDlqTopicExists()
    {
        var dlqConfig = GetDlqConfig(outboxOptionsProvider.Value);
        try
        {
            using var adminClient = new AdminClientBuilder(
                    new AdminClientConfig { BootstrapServers = dlqConfig.BootstrapServers })
                .Build();

            var metadata = adminClient.GetMetadata(dlqConfig.TopicName, TimeSpan.FromSeconds(5));
            var topic = metadata.Topics.FirstOrDefault(t => t.Topic == dlqConfig.TopicName);

            if (topic == null || topic.Error.Code != ErrorCode.NoError)
            {
                var errCode = topic?.Error.Code.ToString() ?? "UNKNOWN";
                LogDlqTopicNotFound(logger, dlqConfig.TopicName, errCode);
                return;
            }

            LogDlqTopicVerified(logger, dlqConfig.TopicName);
        }
        catch (Exception ex)
        {
            // Kafka broker unreachable or topic not found — log warning and continue.
            // DLQ production will fail with a clear error at runtime if topic missing.
            LogDlqTopicVerificationSkipped(logger, dlqConfig.TopicName, ex.Message);
        }
    }

    protected sealed override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        #pragma warning disable CA1873 // Startup path — called once per processor lifetime
            LogStarting(logger, GetConsumerConfig(outboxOptionsProvider.Value).TopicName);
            LogDlqConfigured(logger, GetDlqConfig(outboxOptionsProvider.Value).TopicName);
#pragma warning restore CA1873

        VerifyDlqTopicExists();

        var setupRetryDelay = TimeSpan.FromSeconds(outboxOptionsProvider.Value.SetupRetryDelaySeconds);
        while (!stoppingToken.IsCancellationRequested)
        {
            _consecutiveDlqFailures = 0;
            IConsumer<Ignore, TDto>? consumer = null;
            IProducer<Null, string>? dlqProducer = null;
            try
            {
                consumer = BuildConsumer();
                dlqProducer = BuildDlqProducer();
                consumer.Subscribe(GetConsumerConfig(outboxOptionsProvider.Value).TopicName);
                LogConsumerSubscribed(logger);

                await ConsumeLoop(consumer, dlqProducer, stoppingToken);

                LogConsumeLoopFinished(logger);
            }
            catch (OperationCanceledException)
            {
                LogStopping(logger);
                break;
            }
            catch (Exception ex)
            {
                LogUnhandledException(logger, ex, setupRetryDelay.TotalSeconds);

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
            finally
            {
                if (consumer is not null)
                {
                    try
                    {
                        consumer.Close();
                    }
                    catch (Exception closeEx)
                    {
                        LogConsumerCloseError(logger, closeEx, closeEx.Message);
                    }

                    consumer.Dispose();
                }

                dlqProducer?.Dispose();
            }
        }

        LogStopped(logger);
    }

    private async Task ConsumeLoop(
        IConsumer<Ignore, TDto> consumer,
        IProducer<Null, string> dlqProducer,
        CancellationToken stoppingToken)
    {
        var dlqTopicName = GetDlqConfig(outboxOptionsProvider.Value).TopicName;
        var consumeErrorDelay = TimeSpan.FromSeconds(outboxOptionsProvider.Value.ConsumeErrorDelaySeconds);
        var processingErrorDelay = TimeSpan.FromSeconds(outboxOptionsProvider.Value.ProcessingErrorDelaySeconds);

        while (!stoppingToken.IsCancellationRequested)
        {
            ConsumeResult<Ignore, TDto>? consumeResult = null;
            try
            {
                consumeResult = await Task.Run(() => consumer.Consume(stoppingToken), stoppingToken);

                var currentTopic = consumeResult.Topic;
                var currentPartition = consumeResult.Partition.Value;
                var currentOffset = consumeResult.Offset.Value;

                if (GetConsumerConfig(outboxOptionsProvider.Value).EnablePartitionEof && consumeResult.IsPartitionEOF)
                {
                    LogPartitionEof(logger, currentPartition, currentOffset, currentTopic);
                    continue;
                }

                LogMessageConsumed(logger, currentTopic, currentPartition, currentOffset);

                using (var scope = serviceScopeFactory.CreateScope())
                {
                    var processTimeout = outboxOptionsProvider.Value.ProcessTimeoutSeconds;
                    using var timeoutCts = new CancellationTokenSource(
                        TimeSpan.FromSeconds(processTimeout));
                    using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(
                        stoppingToken, timeoutCts.Token);
                    try
                    {
                        await ProcessMessageAsync(
                            scope.ServiceProvider, consumeResult, linkedCts.Token);
                    }
                    catch (OperationCanceledException) when (!stoppingToken.IsCancellationRequested)
                    {
                        throw new TimeoutException(
                            $"ProcessMessageAsync timed out after {processTimeout} seconds.");
                    }
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

                _consecutiveDlqFailures = 0;
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
                    OutboxTelemetry.MessagesFailed.Add(1);
                    LogProcessingError(logger, ex, consumeResult.Offset.Value, consumeResult.Topic,
                        consumeResult.Partition.Value, dlqTopicName);

                    var maxRetries = outboxOptionsProvider.Value.ProcessingErrorMaxRetryCount;
                    var messageId = consumeResult.Message?.Value?.Id;
                    if (maxRetries.HasValue && messageId.HasValue)
                    {
                        var retries = _retryCount.AddOrUpdate(messageId.Value, 1, (_, c) => c + 1);
                        if (retries > maxRetries.Value)
                        {
                            LogMaxRetriesExceeded(logger, messageId.Value, consumeResult.Offset.Value,
                                consumeResult.Topic, consumeResult.Partition.Value, maxRetries.Value);
                            try
                            {
                                consumer.Commit(consumeResult);
                            }
                            catch (KafkaException kex)
                            {
                                LogNonFatalDlqCommitError(logger, kex, consumeResult.Offset.Value,
                                    consumeResult.Topic, consumeResult.Partition.Value);
                            }

                            _retryCount.TryRemove(messageId.Value, out _);
                            continue;
                        }
                    }

                    var dlqSuccess = await SendToDlqAsync(dlqProducer, dlqTopicName, consumeResult, ex, stoppingToken);

                    if (dlqSuccess)
                    {
                        _consecutiveDlqFailures = 0;
                        OutboxTelemetry.MessagesDlqProduced.Add(1);
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
                        OutboxTelemetry.MessagesDlqFailed.Add(1);
                        LogDlqFailure(logger, consumeResult.Offset.Value, consumeResult.Topic,
                            consumeResult.Partition.Value);

                        _consecutiveDlqFailures++;

                        var maxDlqFailures = outboxOptionsProvider.Value.MaxConsecutiveDlqFailures;
                        if (_consecutiveDlqFailures >= maxDlqFailures)
                        {
                            LogMaxConsecutiveDlqFailures(logger, _consecutiveDlqFailures,
                                consumeResult.Offset.Value, consumeResult.Topic,
                                consumeResult.Partition.Value);
                            throw new InvalidOperationException(
                                $"Max consecutive DLQ failures ({maxDlqFailures}) exceeded.");
                        }
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
        ConsumeResult<Ignore, TDto> consumeResult,
        CancellationToken cancellationToken)
    {
        var dto = consumeResult.Message.Value;

        if (dto == null)
        {
            LogNullMessage(logger, consumeResult.Offset.Value, consumeResult.Topic, consumeResult.Partition.Value);
            return;
        }

        var parentActivityContext = dto is { TraceId: not null, ParentSpanId: not null } &&
            ActivityContext.TryParse(
                $"00-{dto.TraceId}-{dto.ParentSpanId}-01",
                "w3c",
                out var parsed)
            ? parsed
            : default;

        using var activity = parentActivityContext != default
            ? OutboxTelemetry.ActivitySource.StartActivity(
                "KafkaOutboxProcessor.ProcessMessage",
                ActivityKind.Consumer,
                parentActivityContext)
            : OutboxTelemetry.ActivitySource.StartActivityForCaller();

        var sw = Stopwatch.StartNew();

        var topic = consumeResult.Topic;
        var partition = consumeResult.Partition.Value;
        var offset = consumeResult.Offset.Value;

        if (dto.IsProcessed)
        {
            LogAlreadyProcessedPayload(logger, dto.Id, topic, partition, offset);
            return;
        }

        LogStartProcessing(logger, dto.Id, topic, partition, offset);

        var dbContext = serviceProvider.GetRequiredService<IOutboxDbContext>();

        var outboxMessage = await LoadEntityAsync(dbContext, dto.Id, cancellationToken);

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
        await DispatchEventAsync(@event!, serviceProvider, cancellationToken);
        outboxMessage.MarkAsProcessed(timeProvider.GetUtcNow());

        try
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException dbConcurrencyEx)
        {
            LogConcurrencyConflict(logger, dbConcurrencyEx, dto.Id, topic, partition, offset);
            OutboxTelemetry.MessagesFailed.Add(1);
            throw;
        }

        activity?.SetStatus(ActivityStatusCode.Ok);
        sw.Stop();
        OutboxTelemetry.ProcessingDuration.Record(sw.ElapsedMilliseconds);
        OutboxTelemetry.MessagesProcessed.Add(1);
        LogProcessSuccess(logger, dto.Id, topic, partition, offset);
    }

    private async Task<bool> SendToDlqAsync(IProducer<Null, string> dlqProducer, string dlqTopic,
        ConsumeResult<Ignore, TDto> failedResult,
        Exception exception, CancellationToken cancellationToken)
    {
        var originalTopic = failedResult.Topic;
        var originalPartition = failedResult.Partition.Value;
        var originalOffset = failedResult.Offset.Value;

        try
        {
            var dlqMessage = new DlqMessage<TDto>
            {
                FailedTimestampUtc = timeProvider.GetUtcNow(),
                OriginalTopic = originalTopic,
                OriginalPartition = originalPartition,
                OriginalOffset = originalOffset,
                ConsumerGroupId = GetConsumerConfig(outboxOptionsProvider.Value).GroupId,
                ExceptionType = exception.GetType().FullName,
                ExceptionMessage = exception.Message,
                ExceptionStackTrace = exception.StackTrace,
                OriginalMessage = failedResult.Message.Value
            };

            var dlqPayload = JsonSerializer.Serialize(dlqMessage, _dlqSerializerOptions);

            LogSendingToDlq(logger, originalOffset, originalTopic, originalPartition, dlqTopic,
                exception.Message);

            var kafkaMessage = new Message<Null, string> { Value = dlqPayload };

            // Retry DLQ production up to 2 times for transient broker failures
            const int maxDlqRetries = 2;
            DeliveryResult<Null, string>? deliveryResult = null;
            for (var attempt = 0; attempt <= maxDlqRetries; attempt++)
            {
                try
                {
                    deliveryResult = await dlqProducer.ProduceAsync(
                        dlqTopic, kafkaMessage, cancellationToken);
                    break;
                }
                catch (ProduceException<Null, string>) when (attempt < maxDlqRetries)
                {
                    LogDlqProduceRetry(logger, attempt + 1, maxDlqRetries, dlqTopic);
                    // Use a short fixed delay instead of indefinite block
                    await Task.Delay(100 * (attempt + 1), cancellationToken);
                }
            }

            if (deliveryResult != null)
            {
                LogProducedToDlq(logger, deliveryResult.Topic, deliveryResult.Partition.Value,
                    deliveryResult.Offset.Value);
                return true;
            }

            LogFatalDlqProduceError(logger, null!, dlqTopic, originalOffset, originalTopic,
                originalPartition, "Max DLQ retries exceeded");
            return false;
        }
        catch (OperationCanceledException)
        {
            LogDlqCancelled(logger, originalOffset, originalTopic, originalPartition);
            return false;
        }
        catch (Exception ex)
        {
            LogDlqUnexpectedError(logger, ex, dlqTopic, originalOffset, originalTopic,
                originalPartition);
            return false;
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        LogStopAsyncCalled(logger);
        await base.StopAsync(cancellationToken);
        LogStopAsyncFinished(logger);
    }

    private IConsumer<Ignore, TDto> BuildConsumer()
    {
        var kafkaConsumerOptions = GetConsumerConfig(outboxOptionsProvider.Value);
        if (!Enum.TryParse(kafkaConsumerOptions.AutoOffsetReset, true,
                out AutoOffsetReset autoOffsetReset))
        {
            throw new ArgumentException(
                $"Auto offset reset parameter {kafkaConsumerOptions.AutoOffsetReset} is invalid, could not be parsed into a valid AutoOffsetReset.");
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

        return new ConsumerBuilder<Ignore, TDto>(consumerConfig)
            .SetValueDeserializer(CreateDeserializer())
            .SetErrorHandler((_, e) =>
                LogKafkaConsumerError(logger, e.Reason, e.Code, e.IsFatal))
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
        var kafkaProducerOptions = GetDlqConfig(outboxOptionsProvider.Value);

        var producerConfig = new ProducerConfig
        {
            BootstrapServers = kafkaProducerOptions.BootstrapServers, Acks = Acks.All
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

    [LoggerMessage(Level = LogLevel.Information,
        Message = "KafkaOutboxProcessor starting. Subscribing to topic: {TopicName}")]
    private static partial void LogStarting(ILogger logger, string topicName);

    [LoggerMessage(Level = LogLevel.Information, Message = "DLQ configured for topic: {DlqTopicName}")]
    private static partial void LogDlqConfigured(ILogger logger, string dlqTopicName);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Consumer subscribed successfully. Starting consume loop.")]
    private static partial void LogConsumerSubscribed(ILogger logger);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Consume loop finished gracefully (likely cancellation).")]
    private static partial void LogConsumeLoopFinished(ILogger logger);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "KafkaOutboxProcessor stopping due to cancellation request.")]
    private static partial void LogStopping(ILogger logger);

    [LoggerMessage(Level = LogLevel.Critical,
        Message =
            "Unhandled exception during client setup or ConsumeLoop execution. Retrying connection/setup in {DelaySeconds} seconds...")]
    private static partial void LogUnhandledException(ILogger logger, Exception ex, double delaySeconds);

    [LoggerMessage(Level = LogLevel.Information, Message = "Retry delay cancelled. KafkaOutboxProcessor stopping.")]
    private static partial void LogRetryDelayCancelled(ILogger logger);

    [LoggerMessage(Level = LogLevel.Information, Message = "KafkaOutboxProcessor stopped.")]
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
        Message =
            "FATAL Kafka error during commit for Offset {Offset} (Topic: {Topic}, Partition: {Partition}). Stopping consumer.")]
    private static partial void LogFatalCommitError(ILogger logger, Exception ex, long offset, string topic,
        int partition);

    [LoggerMessage(Level = LogLevel.Error,
        Message =
            "Non-fatal Kafka error during commit for Offset {Offset} (Topic: {Topic}, Partition: {Partition}). Processing succeeded but commit failed.")]
    private static partial void LogNonFatalCommitError(ILogger logger, Exception ex, long offset, string topic,
        int partition);

    [LoggerMessage(Level = LogLevel.Error,
        Message =
            "Consume error: {Reason}. Code: {Code}, IsFatal: {IsFatal}, IsLocal: {IsLocal}, IsBroker: {IsBroker}")]
    private static partial void LogConsumeError(ILogger logger, Exception ex, string reason, ErrorCode code,
        bool isFatal, bool isLocal, bool isBroker);

    [LoggerMessage(Level = LogLevel.Critical,
        Message = "Fatal Kafka consume error encountered. Stopping processor.")]
    private static partial void LogFatalConsumeError(ILogger logger);

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "Non-fatal consume error. Delaying {DelaySeconds} seconds before next attempt...")]
    private static partial void LogNonFatalConsumeError(ILogger logger, double delaySeconds);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Consume loop cancellation requested during Consume operation.")]
    private static partial void LogConsumeLoopCancelled(ILogger logger);

    [LoggerMessage(Level = LogLevel.Warning,
        Message =
            "{ExceptionType} occured within ProcessMessageAsync for Offset {Offset} (Topic: {Topic}, Partition: {Partition}). Offset not committed.")]
    private static partial void LogProcessConcurrencyError(ILogger logger, Exception ex, string exceptionType,
        long offset, string topic, int partition);

    [LoggerMessage(Level = LogLevel.Warning,
        Message =
            "{ExceptionType} occured within ProcessMessageAsync but ConsumeResult is null. Offset not committed.")]
    private static partial void LogProcessErrorNullResult(ILogger logger, Exception ex, string exceptionType);

    [LoggerMessage(Level = LogLevel.Error,
        Message =
            "Error processing message at Offset {Offset} (Topic: {Topic}, Partition: {Partition}). Sending to DLQ topic {DlqTopicName}...")]
    private static partial void LogProcessingError(ILogger logger, Exception ex, long offset, string topic,
        int partition, string dlqTopicName);

    [LoggerMessage(Level = LogLevel.Information,
        Message =
            "Successfully sent message from Offset {Offset} (Topic: {Topic}, Partition: {Partition}) to DLQ topic {DlqTopicName}. Committing original offset.")]
    private static partial void LogDlqSuccess(ILogger logger, long offset, string topic, int partition,
        string dlqTopicName);

    [LoggerMessage(Level = LogLevel.Critical,
        Message =
            "FATAL Kafka error during commit for DLQ'd Offset {Offset} (Topic: {Topic}, Partition: {Partition}).")]
    private static partial void LogFatalDlqCommitError(ILogger logger, Exception ex, long offset, string topic,
        int partition);

    [LoggerMessage(Level = LogLevel.Error,
        Message =
            "Non-fatal Kafka error committing DLQ'd Offset {Offset} (Topic: {Topic}, Partition: {Partition}). Consumer may reprocess.")]
    private static partial void LogNonFatalDlqCommitError(ILogger logger, Exception ex, long offset, string topic,
        int partition);

    [LoggerMessage(Level = LogLevel.Error,
        Message =
            "Failed to send message from Offset {Offset} (Topic: {Topic}, Partition: {Partition}) to DLQ. **Offset will not be committed.**")]
    private static partial void LogDlqFailure(ILogger logger, long offset, string topic, int partition);

    [LoggerMessage(Level = LogLevel.Error,
        Message = "Processing error occurred but ConsumeResult was null. Cannot DLQ or commit.")]
    private static partial void LogProcessingErrorNullConsumeResult(ILogger logger, Exception ex);

    [LoggerMessage(Level = LogLevel.Critical,
        Message =
            "Received null message value at Offset {Offset} (Topic: {Topic}, Partition: {Partition}). Data loss risk — value was null in Kafka but offset will be committed.")]
    private static partial void LogNullMessage(ILogger logger, long offset, string topic, int partition);

    [LoggerMessage(Level = LogLevel.Warning,
        Message =
            "Outbox message Id {MessageId} (Topic: {Topic}, Partition: {Partition}, Offset: {Offset}) was already marked 'IsProcessed=true' in the Kafka message payload. Skipping processing and committing offset.")]
    private static partial void LogAlreadyProcessedPayload(ILogger logger, int messageId, string topic,
        int partition, long offset);

    [LoggerMessage(Level = LogLevel.Information,
        Message =
            "Starting processing for message Id {MessageId} (Topic: {Topic}, Partition: {Partition}, Offset: {Offset})...")]
    private static partial void LogStartProcessing(ILogger logger, int messageId, string topic, int partition,
        long offset);

    [LoggerMessage(Level = LogLevel.Warning,
        Message =
            "OutboxMessage Id {MessageId} not found in database for Offset {Offset} (Topic: {Topic}, Partition: {Partition}). Committing offset and skipping.")]
    private static partial void LogMessageNotFound(ILogger logger, int messageId, long offset, string topic,
        int partition);

    [LoggerMessage(Level = LogLevel.Information,
        Message =
            "Outbox message Id {MessageId} (Topic: {Topic}, Partition: {Partition}, Offset: {Offset}) was already marked 'IsProcessed=true' in the Database. Committing offset.")]
    private static partial void LogAlreadyProcessedDb(ILogger logger, int messageId, string topic, int partition,
        long offset);

    [LoggerMessage(Level = LogLevel.Warning,
        Message =
            "Concurrency conflict saving OutboxMessage ID {MessageId} (Topic: {Topic}, Partition: {Partition}, Offset: {Offset}). Rethrowing to allow retry.")]
    private static partial void LogConcurrencyConflict(ILogger logger, Exception ex, int messageId, string topic,
        int partition, long offset);

    [LoggerMessage(Level = LogLevel.Information,
        Message =
            "Successfully processed message Id {MessageId} (Topic: {Topic}, Partition: {Partition}, Offset: {Offset}).")]
    private static partial void LogProcessSuccess(ILogger logger, int messageId, string topic, int partition,
        long offset);

    [LoggerMessage(Level = LogLevel.Warning,
        Message =
            "Sending failed message from Offset {OriginalOffset} (Topic: {OriginalTopic}, Partition: {OriginalPartition}) to DLQ topic {DlqTopic} due to error: {ErrorMessage}")]
    private static partial void LogSendingToDlq(ILogger logger, long originalOffset, string originalTopic,
        int originalPartition, string dlqTopic, string errorMessage);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "Message successfully produced to DLQ topic {DlqTopic}, Partition {Partition}, Offset {Offset}")]
    private static partial void LogProducedToDlq(ILogger logger, string dlqTopic, int partition, long offset);

    [LoggerMessage(Level = LogLevel.Critical,
        Message =
            "FATAL error producing message to DLQ topic {DlqTopic} for original Offset {OriginalOffset} (Topic: {OriginalTopic}, Partition: {OriginalPartition}). Error: {Reason}")]
    private static partial void LogFatalDlqProduceError(ILogger logger, Exception ex, string dlqTopic,
        long originalOffset, string originalTopic, int originalPartition, string reason);

    [LoggerMessage(Level = LogLevel.Warning,
        Message =
            "DLQ production cancelled for original Offset {OriginalOffset} (Topic: {OriginalTopic}, Partition: {OriginalPartition}).")]
    private static partial void LogDlqCancelled(ILogger logger, long originalOffset, string originalTopic,
        int originalPartition);

    [LoggerMessage(Level = LogLevel.Error,
        Message =
            "Unexpected error sending message to DLQ topic {DlqTopic} for original Offset {OriginalOffset} (Topic: {OriginalTopic}, Partition: {OriginalPartition}).")]
    private static partial void LogDlqUnexpectedError(ILogger logger, Exception ex, string dlqTopic,
        long originalOffset, string originalTopic, int originalPartition);

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "DLQ produce attempt {Attempt}/{MaxRetries} failed for topic {DlqTopic}. Retrying...")]
    private static partial void LogDlqProduceRetry(ILogger logger, int attempt, int maxRetries, string dlqTopic);

    [LoggerMessage(Level = LogLevel.Error,
        Message = "Kafka Consumer Error: {Reason} (Code: {Code}, IsFatal: {IsFatal})")]
    private static partial void LogKafkaConsumerError(ILogger logger, string reason, ErrorCode code, bool isFatal);

    [LoggerMessage(Level = LogLevel.Information, Message = "Kafka Consumer Log: [{Facility}] {Message}")]
    private static partial void LogKafkaConsumerLog(ILogger logger, string facility, string message);

    [LoggerMessage(Level = LogLevel.Information, Message = "KafkaOutboxProcessor StopAsync called.")]
    private static partial void LogStopAsyncCalled(ILogger logger);

    [LoggerMessage(Level = LogLevel.Information, Message = "KafkaOutboxProcessor has finished stopping.")]
    private static partial void LogStopAsyncFinished(ILogger logger);

    [LoggerMessage(Level = LogLevel.Warning,
        Message =
            "Max retries ({MaxRetries}) exceeded for message ID {MessageId} at Offset {Offset} (Topic: {Topic}, Partition: {Partition}). Committing offset and skipping.")]
    private static partial void LogMaxRetriesExceeded(ILogger logger, int messageId, long offset, string topic,
        int partition, int maxRetries);

    [LoggerMessage(Level = LogLevel.Critical,
        Message =
            "Max consecutive DLQ failures ({ConsecutiveFailures}) reached at Offset {Offset} (Topic: {Topic}, Partition: {Partition}). Breaking consume loop.")]
    private static partial void LogMaxConsecutiveDlqFailures(ILogger logger, int consecutiveFailures, long offset,
        string topic, int partition);

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "Error during graceful consumer.Close(): {ErrorMessage}")]
    private static partial void LogConsumerCloseError(ILogger logger, Exception ex, string errorMessage);

    [LoggerMessage(Level = LogLevel.Warning,
        Message =
            "DLQ topic '{DlqTopic}' not verified (error: {ErrorCode}). Production failures will be routed to DLQ failure path.")]
    private static partial void LogDlqTopicNotFound(ILogger logger, string dlqTopic, string errorCode);

    [LoggerMessage(Level = LogLevel.Information,
        Message = "DLQ topic '{DlqTopic}' verified successfully.")]
    private static partial void LogDlqTopicVerified(ILogger logger, string dlqTopic);

    [LoggerMessage(Level = LogLevel.Warning,
        Message = "DLQ topic verification skipped for '{DlqTopic}' (broker unreachable): {ErrorMessage}")]
    private static partial void LogDlqTopicVerificationSkipped(ILogger logger, string dlqTopic, string errorMessage);
}

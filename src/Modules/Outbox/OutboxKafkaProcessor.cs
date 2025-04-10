using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Outbox;

public class OutboxKafkaProcessor : BackgroundService
{
    private const string TopicName = "outbox_topic.Outbox.OutboxMessages"; // Adjust topic name if needed
    private readonly ConsumerConfig _consumerConfig;
    private readonly ILogger<OutboxKafkaProcessor> _logger;

    public OutboxKafkaProcessor(ILogger<OutboxKafkaProcessor> logger)
    {
        _logger = logger;

        // Configure your consumer settings (consider moving these settings into appsettings.json)
        _consumerConfig = new ConsumerConfig
        {
            BootstrapServers = "mm.kafka:9092", // Use your Kafka bootstrap servers here
            GroupId = "outbox-consumer-group",
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false // Manual commit after processing
        };
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var consumer = new ConsumerBuilder<Ignore, string>(_consumerConfig)
            .SetErrorHandler((_, e) => _logger.LogError("Kafka error: {Reason}", e.Reason))
            .Build();

        // Subscribe to the topic. Note that Debezium typically creates topics with a pattern based on database.server.name and table names.
        consumer.Subscribe(TopicName);

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // This call blocks until a message is available or the token is canceled.
                    var consumeResult = consumer.Consume(stoppingToken);

                    if (consumeResult != null)
                    {
                        _logger.LogInformation("Consumed message at offset {Offset}: {Message}", consumeResult.Offset,
                            consumeResult.Message.Value);

                        // Commit the offset once processing is complete.
                        consumer.Commit(consumeResult);
                    }
                }
                catch (ConsumeException ex)
                {
                    _logger.LogError(ex, "Error occurred while consuming message");
                }
                catch (OperationCanceledException)
                {
                    // This exception is expected on shutdown. Just break out.
                    break;
                }
            }
        }
        finally
        {
            consumer.Close();
        }

        await Task.CompletedTask;
    }
}

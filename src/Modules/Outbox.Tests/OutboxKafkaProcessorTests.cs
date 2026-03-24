using System.Collections.Concurrent;
using System.Text.Json;
using Common.Application.EventBus;
using Common.Application.Persistence.Outbox;
using Common.Domain.Events;
using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Outbox.Persistence;
using Xunit;

#pragma warning disable CA1707 // Remove the underscores from member name

namespace Outbox.Tests;

public record TestDomainEvent(string Data) : DomainEvent;

public class SpyEventBus : IEventBus
{
    private readonly ConcurrentBag<IEvent> _publishedEvents = new();
    public IReadOnlyCollection<IEvent> PublishedEvents => _publishedEvents;
    public bool ShouldThrow { get; set; }

    public Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IEvent
    {
        if (ShouldThrow)
        {
            throw new InvalidOperationException("Simulated processing failure for DLQ.");
        }

        _publishedEvents.Add(@event);
        return Task.CompletedTask;
    }
}

public class OutboxKafkaProcessorTests : IClassFixture<OutboxTestWebAppFactory>
{
    private readonly OutboxTestWebAppFactory _factory;
    private readonly IServiceScopeFactory _scopeFactory;

    public OutboxKafkaProcessorTests(OutboxTestWebAppFactory factory)
    {
        _factory = factory;
        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
    }

    [Fact]
    public async Task Should_Process_Message_Successfully()
    {
        // 1. Arrange: Seed a message in the PostgreSQL Outbox table
        var eventData = new TestDomainEvent("IntegrationTest");
        int messageId;

        using (var scope = _scopeFactory.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<OutboxDbContext>();
            var outboxMessage = OutboxMessage.Create(DateTimeOffset.UtcNow, eventData);
            dbContext.OutboxMessages.Add(outboxMessage);
            await dbContext.SaveChangesAsync();
            messageId = outboxMessage.Id;
        }

        // 2. Act: Push the simulated unwrapped Debezium event to Kafka
        var dto = new OutboxMessageDto
        {
            Id = messageId,
            Event = JsonSerializer.Serialize((DomainEvent)eventData), // Payload string
            CreatedOn = DateTimeOffset.UtcNow,
            IsProcessed = false
        };

        var config = new ProducerConfig { BootstrapServers = _factory.KafkaBootstrapAddress };
        using var producer = new ProducerBuilder<Null, string>(config).Build();

        var message = new Message<Null, string> { Value = JsonSerializer.Serialize(dto) };

        // Simple retry for cold-start Kafka metadata readiness on CI
        for (var i = 0; i < 3; i++)
        {
            try
            {
                await producer.ProduceAsync("test-outbox-topic", message);
                break;
            }
            catch (ProduceException<Null, string>) when (i < 2)
            {
                await Task.Delay(1000);
            }
        }

        // 3. Assert: Wait for OutboxKafkaProcessor to pick it up and process it
        var isProcessed = false;
        var endTime = DateTime.UtcNow.AddSeconds(30);

        while (DateTime.UtcNow < endTime)
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<OutboxDbContext>();
            var msg = await dbContext.OutboxMessages.FirstOrDefaultAsync(x => x.Id == messageId);

            if (msg != null && msg.IsProcessed)
            {
                isProcessed = true;
                break;
            }

            await Task.Delay(500);
        }

        Assert.True(isProcessed, "The background processor should have marked the message as processed in the DB.");
    }

    [Fact]
    public async Task Should_Produce_DLQ_On_Failure()
    {
        // 0. Set the Spy to throw an exception
        var spyBus = _factory.Services.GetRequiredService<IEventBus>() as SpyEventBus;
        Assert.NotNull(spyBus);
        spyBus.ShouldThrow = true;

        // 1. Arrange: Seed a message in the PostgreSQL Outbox table
        var eventData = new TestDomainEvent("DlqTest");
        int messageId;

        using (var scope = _scopeFactory.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<OutboxDbContext>();
            var outboxMessage = OutboxMessage.Create(DateTimeOffset.UtcNow, eventData);
            dbContext.OutboxMessages.Add(outboxMessage);
            await dbContext.SaveChangesAsync();
            messageId = outboxMessage.Id;
        }

        // 2. Act: Push the simulated unwrapped Debezium event to Kafka
        var dto = new OutboxMessageDto
        {
            Id = messageId,
            Event = JsonSerializer.Serialize((DomainEvent)eventData), // Payload string
            CreatedOn = DateTimeOffset.UtcNow,
            IsProcessed = false
        };

        var config = new ProducerConfig { BootstrapServers = _factory.KafkaBootstrapAddress };
        using var producer = new ProducerBuilder<Null, string>(config).Build();

        var message = new Message<Null, string> { Value = JsonSerializer.Serialize(dto) };

        // Simple retry for cold-start Kafka metadata readiness on CI
        for (var i = 0; i < 3; i++)
        {
            try
            {
                await producer.ProduceAsync("test-outbox-topic", message);
                break;
            }
            catch (ProduceException<Null, string>) when (i < 2)
            {
                await Task.Delay(1000);
            }
        }

        // 3. Assert: Setup a Kafka consumer to read from the DLQ topic and wait for the message
        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = _factory.KafkaBootstrapAddress,
            GroupId = "test-dlq-verifier-group",
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnablePartitionEof = true
        };

        using var consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
        consumer.Subscribe("test-dlq-topic");

        var dlqReceived = false;
        var endTime = DateTime.UtcNow.AddSeconds(30);

        try
        {
            while (DateTime.UtcNow < endTime)
            {
                ConsumeResult<Ignore, string>? consumeResult;
                try
                {
                    consumeResult = consumer.Consume(TimeSpan.FromSeconds(1));
                }
                catch (ConsumeException)
                {
                    // Ignore transient consume errors from Kafka on CI
                    continue;
                }

                if (consumeResult == null || consumeResult.IsPartitionEOF)
                {
                    continue;
                }

                // If we get here, a message was produced to the DLQ topic!
                // We could optionally deserialize it and verify the original offset/topic, but receiving any message here means success for this targeted test payload.
                if (consumeResult.Message?.Value != null &&
                    consumeResult.Message.Value.Contains("Simulated processing failure for DLQ.",
                        StringComparison.Ordinal))
                {
                    dlqReceived = true;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            // Specifically catching unhandled stuff to at least delay failures if any Kafka error happens.
            Console.WriteLine($"Test crashed with: {ex.Message}");
        }
        finally
        {
            // Reset the spy for other tests
            spyBus.ShouldThrow = false;
        }

        Assert.True(dlqReceived, "The background processor should have routed the poison message to the DLQ topic.");
    }
}

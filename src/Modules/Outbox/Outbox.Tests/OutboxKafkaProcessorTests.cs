using System.Collections.Concurrent;
using System.Text.Json;
using Common.Application.EventBus;
using Common.Application.Persistence.Outbox;
using Common.Domain.Events;
using Common.Infrastructure.Persistence.ValueConverters;
using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Outbox.Persistence;
using Xunit;

#pragma warning disable CA1707 // Remove the underscores from member name

namespace Outbox.Tests;

public record TestDomainEvent(string Data) : DomainEvent;

public class SpyDomainEventDispatcher : IDomainEventDispatcher
{
    private readonly ConcurrentBag<DomainEvent> _dispatchedEvents = new();
    public IReadOnlyCollection<DomainEvent> DispatchedEvents => _dispatchedEvents;
    public bool ShouldThrow { get; set; }

    public Task DispatchAsync(DomainEvent @event, CancellationToken cancellationToken)
    {
        if (ShouldThrow)
        {
            throw new InvalidOperationException("Simulated processing failure for DLQ.");
        }

        _dispatchedEvents.Add(@event);
        return Task.CompletedTask;
    }
}

public class DomainEventSerializationTests
{
    [Fact]
    public void DomainEventConverter_RoundTrip_PreservesEventId()
    {
        var converter = new DomainEventConverter();
        var original = new TestDomainEvent("round-trip");

        var json = (string)converter.ConvertToProvider(original)!;
        var restored = (TestDomainEvent)converter.ConvertFromProvider(json)!;

        Assert.Equal(original.Id, restored.Id);
    }

    [Fact]
    public void DomainEventConverter_RoundTrip_PreservesEventData()
    {
        var converter = new DomainEventConverter();
        var original = new TestDomainEvent("hello-serialization");

        var json = (string)converter.ConvertToProvider(original)!;
        var restored = (TestDomainEvent)converter.ConvertFromProvider(json)!;

        Assert.Equal(original.Data, restored.Data);
        Assert.Equal(original.CreatedOn, restored.CreatedOn);
        Assert.Equal(original.Version, restored.Version);
        Assert.IsType<TestDomainEvent>(restored);
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
            Event = JsonSerializer.Serialize((DomainEvent)eventData),
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
        var spy = _factory.Services.GetRequiredService<IDomainEventDispatcher>() as SpyDomainEventDispatcher;
        Assert.NotNull(spy);
        spy.ShouldThrow = true;

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
            Event = JsonSerializer.Serialize((DomainEvent)eventData),
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
                    continue;
                }

                if (consumeResult == null || consumeResult.IsPartitionEOF)
                {
                    continue;
                }

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
            Console.WriteLine($"Test crashed with: {ex.Message}");
        }
        finally
        {
            spy.ShouldThrow = false;
        }

        Assert.True(dlqReceived, "The background processor should have routed the poison message to the DLQ topic.");
    }
}

using System.Collections.Concurrent;
using System.Diagnostics.Metrics;
using System.Text.Json;
using Common.Application.EventBus;
using Common.Application.Persistence.Outbox;
using Common.Domain.Events;
using Common.Infrastructure.Persistence.ValueConverters;
using Common.IntegrationEvents;
using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Outbox.Persistence;
using Xunit;

#pragma warning disable CA1707 // Remove the underscores from member name

namespace Outbox.Tests;

public record TestDomainEvent(string Data) : DomainEvent;

public sealed record TestIntegrationEvent(string Data) : IntegrationEvent;

public class SpyEventDispatcher : IEventDispatcher
{
    private readonly ConcurrentBag<IEvent> _dispatchedEvents = new();
    public IReadOnlyCollection<IEvent> DispatchedEvents => _dispatchedEvents;
    public bool ShouldThrow { get; set; }
    public bool ShouldHang { get; set; }

    public Task DispatchAsync(IEvent @event, CancellationToken cancellationToken)
    {
        if (ShouldHang)
        {
            return Task.Delay(Timeout.Infinite, cancellationToken);
        }

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
            Event = JsonSerializer.Serialize((DomainEvent)eventData, EventConverter.WriteOptions),
            CreatedOn = DateTimeOffset.UtcNow,
            IsProcessed = false,
            EventType = OutboxMessage.EventTypeDomain
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
    public async Task Should_Process_Integration_Event_Successfully()
    {
        // Arrange: Seed an integration event and verify routing through single processor
        var eventData = new TestIntegrationEvent("IntegrationEventTest");
        int messageId;

        using (var scope = _scopeFactory.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<OutboxDbContext>();
            var outboxMessage = OutboxMessage.Create(DateTimeOffset.UtcNow, eventData);
            dbContext.OutboxMessages.Add(outboxMessage);
            await dbContext.SaveChangesAsync();
            messageId = outboxMessage.Id;
        }

        // Act: Push to Kafka
        var dto = new OutboxMessageDto
        {
            Id = messageId,
            Event = JsonSerializer.Serialize((IntegrationEvent)eventData, EventConverter.WriteOptions),
            CreatedOn = DateTimeOffset.UtcNow,
            IsProcessed = false,
            EventType = OutboxMessage.EventTypeIntegration
        };

        var config = new ProducerConfig { BootstrapServers = _factory.KafkaBootstrapAddress };
        using var producer = new ProducerBuilder<Null, string>(config).Build();
        var message = new Message<Null, string> { Value = JsonSerializer.Serialize(dto) };

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

        // 3. Assert: Wait for processor to mark message as processed
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

        Assert.True(isProcessed,
            "The background processor should have marked the integration event as processed in the DB.");
    }

    [Fact]
    public async Task Should_Produce_DLQ_On_Failure()
    {
        // 0. Wire MeterListener to verify DLQ telemetry counters
        var dlqProducedCount = 0L;
        var messagesFailedCount = 0L;

        using var meterListener = new MeterListener();
        meterListener.InstrumentPublished = (instrument, listener) =>
        {
            if (instrument.Meter.Name == "ModularMonolith.Outbox")
            {
                listener.EnableMeasurementEvents(instrument);
            }
        };

        meterListener.SetMeasurementEventCallback<long>((instrument, measurement, _, _) =>
        {
            if (instrument.Name == "outbox.messages_failed.total")
            {
                Interlocked.Exchange(ref messagesFailedCount, measurement);
            }

            if (instrument.Name == "outbox.messages_dlq_produced.total")
            {
                Interlocked.Exchange(ref dlqProducedCount, measurement);
            }
        });

        meterListener.Start();

        // 1. Set the Spy to throw an exception
        var spy = _factory.Services.GetRequiredService<IEventDispatcher>() as SpyEventDispatcher;
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
            Event = JsonSerializer.Serialize((DomainEvent)eventData, EventConverter.WriteOptions),
            CreatedOn = DateTimeOffset.UtcNow,
            IsProcessed = false,
            EventType = OutboxMessage.EventTypeDomain
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

        // Give the MeterListener a moment to receive the callbacks
        await Task.Delay(500);

        Assert.True(messagesFailedCount > 0, "outbox.messages_failed.total should record the failed processing.");
        Assert.True(dlqProducedCount > 0, "outbox.messages_dlq_produced.total should record the DLQ production.");
    }

    [Fact]
    public async Task Should_Record_Telemetry_On_Successful_Process()
    {
        // Arrange: Wire MeterListener to capture telemetry from ModularMonolith.Outbox
        var processedCount = 0L;
        var lastDuration = 0.0;

        using var meterListener = new MeterListener();
        meterListener.InstrumentPublished = (instrument, listener) =>
        {
            if (instrument.Meter.Name == "ModularMonolith.Outbox")
            {
                listener.EnableMeasurementEvents(instrument);
            }
        };

        meterListener.SetMeasurementEventCallback<long>((instrument, measurement, _, _) =>
        {
            if (instrument.Name == "outbox.messages_processed.total")
            {
                Interlocked.Exchange(ref processedCount, measurement);
            }
        });

        meterListener.SetMeasurementEventCallback<double>((instrument, measurement, _, _) =>
        {
            if (instrument.Name == "outbox.processing.duration")
            {
                lastDuration = measurement;
            }
        });

        meterListener.Start();

        // 1. Seed a message in the PostgreSQL Outbox table
        var eventData = new TestDomainEvent("TelemetryTest");
        int messageId;

        using (var scope = _scopeFactory.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<OutboxDbContext>();
            var outboxMessage = OutboxMessage.Create(DateTimeOffset.UtcNow, eventData);
            dbContext.OutboxMessages.Add(outboxMessage);
            await dbContext.SaveChangesAsync();
            messageId = outboxMessage.Id;
        }

        // 2. Push the simulated Debezium event to Kafka
        var dto = new OutboxMessageDto
        {
            Id = messageId,
            Event = JsonSerializer.Serialize((DomainEvent)eventData, EventConverter.WriteOptions),
            CreatedOn = DateTimeOffset.UtcNow,
            IsProcessed = false,
            EventType = OutboxMessage.EventTypeDomain
        };

        var config = new ProducerConfig { BootstrapServers = _factory.KafkaBootstrapAddress };
        using var producer = new ProducerBuilder<Null, string>(config).Build();
        var message = new Message<Null, string> { Value = JsonSerializer.Serialize(dto) };

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

        // 3. Wait for processor to mark message as processed
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

        // Give the MeterListener a moment to receive the callback
        await Task.Delay(500);

        // 4. Assert telemetry was recorded
        Assert.True(isProcessed, "The background processor should have marked the message as processed in the DB.");
        Assert.True(processedCount > 0,
            "The outbox.messages_processed.total counter should be > 0 after successful processing.");
        Assert.True(lastDuration > 0, "The outbox.processing.duration histogram should record a duration in ms.");
    }

    [Fact]
    public async Task Should_Timeout_When_Handler_Hangs()
    {
        // Arrange: Spy hangs indefinitely, set ShouldHang
        var spy = _factory.Services.GetRequiredService<IEventDispatcher>() as SpyEventDispatcher;
        Assert.NotNull(spy);
        spy.ShouldHang = true;

        var eventData = new TestDomainEvent("TimeoutTest");
        int messageId;

        using (var scope = _scopeFactory.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<OutboxDbContext>();
            var outboxMessage = OutboxMessage.Create(DateTimeOffset.UtcNow, eventData);
            dbContext.OutboxMessages.Add(outboxMessage);
            await dbContext.SaveChangesAsync();
            messageId = outboxMessage.Id;
        }

        // Act: Push to Kafka
        var dto = new OutboxMessageDto
        {
            Id = messageId,
            Event = JsonSerializer.Serialize((DomainEvent)eventData, EventConverter.WriteOptions),
            CreatedOn = DateTimeOffset.UtcNow,
            IsProcessed = false,
            EventType = OutboxMessage.EventTypeDomain
        };

        var config = new ProducerConfig { BootstrapServers = _factory.KafkaBootstrapAddress };
        using var producer = new ProducerBuilder<Null, string>(config).Build();
        var message = new Message<Null, string> { Value = JsonSerializer.Serialize(dto) };

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

        // 3. Assert: Wait for DLQ topic to contain the timed-out message
        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = _factory.KafkaBootstrapAddress,
            GroupId = "test-timeout-verifier-group",
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnablePartitionEof = true
        };

        using var consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
        consumer.Subscribe("test-dlq-topic");

        var dlqReceived = false;
        var endTime = DateTime.UtcNow.AddSeconds(40);

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
                    consumeResult.Message.Value.Contains("Timeout", StringComparison.OrdinalIgnoreCase))
                {
                    dlqReceived = true;
                    break;
                }
            }
        }
        finally
        {
            spy.ShouldHang = false;
        }

        Assert.True(dlqReceived, "The processor should have timed out and routed the message to the DLQ.");
    }
}

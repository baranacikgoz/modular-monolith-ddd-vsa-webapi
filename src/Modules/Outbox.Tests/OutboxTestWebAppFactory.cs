using Common.Tests;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Common.Application.EventBus;
using Testcontainers.Kafka;
using Xunit;

namespace Outbox.Tests;

public class OutboxTestWebAppFactory : IntegrationTestFactory, IAsyncLifetime
{
    private readonly KafkaContainer _kafkaContainer = new KafkaBuilder()
        .WithImage("confluentinc/cp-kafka:7.4.0")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.ConfigureAppConfiguration((context, config) =>
        {
            var confDict = new Dictionary<string, string?>
            {
                { "OutboxOptions:KafkaConsumer:BootstrapServers", _kafkaContainer.GetBootstrapAddress() },
                { "OutboxOptions:KafkaConsumer:TopicName", "test-outbox-topic" },
                { "OutboxOptions:KafkaConsumer:GroupId", "test-outbox-group" },
                { "OutboxOptions:KafkaConsumer:AutoOffsetReset", "Earliest" },
                { "OutboxOptions:KafkaConsumer:EnablePartitionEof", "true" },
                { "OutboxOptions:KafkaConsumer:SessionTimeoutMs", "10000" },
                { "OutboxOptions:KafkaConsumer:HeartbeatIntervalMs", "3000" },
                { "OutboxOptions:KafkaDlqProducer:BootstrapServers", _kafkaContainer.GetBootstrapAddress() },
                { "OutboxOptions:KafkaDlqProducer:TopicName", "test-dlq-topic" },
                { "OutboxOptions:SetupRetryDelaySeconds", "1" },
                { "OutboxOptions:ConsumeErrorDelaySeconds", "1" },
                { "OutboxOptions:ProcessingErrorDelaySeconds", "0" }
            };
            config.AddInMemoryCollection(confDict);
        });

        builder.ConfigureTestServices(services =>
        {
            // Inject SpyEventBus to intercept or manipulate event publishing for DLQ testing
            services.AddSingleton<IEventBus, SpyEventBus>();
        });
    }

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        await _kafkaContainer.StartAsync();
        // Warm-up delay for Kafka metadata synchronization on CI runners
        await Task.Delay(5000);
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _kafkaContainer.StopAsync();
        // Because IntegrationTestFactory explicitly implements IAsyncLifetime.DisposeAsync privately,
        // we invoke it via reflection or cast but the easiest is just let the normal DisposeAsync pipeline clean up Postgres.
    }

    public override async ValueTask DisposeAsync()
    {
        await _kafkaContainer.DisposeAsync();
        await base.DisposeAsync();
        GC.SuppressFinalize(this);
    }

    public string KafkaBootstrapAddress => _kafkaContainer.GetBootstrapAddress();
}

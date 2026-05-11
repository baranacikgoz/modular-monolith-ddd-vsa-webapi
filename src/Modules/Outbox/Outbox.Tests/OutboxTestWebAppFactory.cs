using Common.Application.EventBus;
using Common.Tests;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.Kafka;
using Xunit;

namespace Outbox.Tests;

public class OutboxTestWebAppFactory : IntegrationTestFactory, IAsyncLifetime
{
    protected override string[] GetActiveModules() => ["Outbox", "IAM"];

    // DinD environments (DOCKER_HOST=tcp://...) expose Docker via TCP without a Unix socket.
    // On resource-constrained CI runners, Kafka can take longer than the default 120-second
    // readiness timeout to log "started (kafka.server.KafkaServer)". We override the wait
    // strategy with a 5-minute ceiling to tolerate the slower DinD startup.
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
                { "OutboxOptions:KafkaConsumer:MaxPollIntervalMs", "300000" },
                { "OutboxOptions:KafkaDlqProducer:BootstrapServers", _kafkaContainer.GetBootstrapAddress() },
                { "OutboxOptions:KafkaDlqProducer:TopicName", "test-dlq-topic" },
                { "OutboxOptions:SetupRetryDelaySeconds", "1" },
                { "OutboxOptions:ConsumeErrorDelaySeconds", "1" },
                { "OutboxOptions:ProcessingErrorDelaySeconds", "0" },
                { "OutboxOptions:ProcessingErrorMaxRetryCount", "3" },
                { "OutboxOptions:ProcessTimeoutSeconds", "3" }
            };
            config.AddInMemoryCollection(confDict);
        });

        builder.ConfigureTestServices(services =>
        {
            // Override IEventDispatcher with a spy to intercept dispatches for DLQ testing.
            services.AddSingleton<IEventDispatcher, SpyEventDispatcher>();
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
    }

    public override async ValueTask DisposeAsync()
    {
        await _kafkaContainer.DisposeAsync();
        await base.DisposeAsync();
        GC.SuppressFinalize(this);
    }

    public string KafkaBootstrapAddress => _kafkaContainer.GetBootstrapAddress();
}

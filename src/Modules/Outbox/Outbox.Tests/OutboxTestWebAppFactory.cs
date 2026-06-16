using Common.Tests;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Testcontainers.RabbitMq;
using Xunit;

namespace Outbox.Tests;

public class OutboxTestWebAppFactory : IntegrationTestFactory, IAsyncLifetime
{
    protected override string[] GetActiveModules() => ["Outbox", "IAM"];

    private readonly RabbitMqContainer _rabbitMqContainer = new RabbitMqBuilder("rabbitmq:3.13-management-alpine")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        // Outbox.Tests exercises real publishing against its own RabbitMQ container, so override the
        // base factory's in-memory transport back to RabbitMQ. UseSetting wins (registration-visible).
        builder.UseSetting("MassTransitOptions:UseInMemoryTransport", "false");

        builder.ConfigureAppConfiguration((_, config) =>
        {
            var confDict = new Dictionary<string, string?>
            {
                { "OutboxOptions:IsProcessor", "true" },
                { "OutboxOptions:PollIntervalMs", "100" },
                { "OutboxOptions:BatchSize", "10" },
                { "OutboxOptions:MaxRetryCount", "3" },
                { "OutboxOptions:BaseBackoffSeconds", "2" },
                { "OutboxOptions:MaxBackoffSeconds", "30" },
                { "RabbitMqOptions:Host", _rabbitMqContainer.Hostname },
                { "RabbitMqOptions:Port", _rabbitMqContainer.GetMappedPublicPort(5672).ToString(System.Globalization.CultureInfo.InvariantCulture) },
                { "RabbitMqOptions:Username", "guest" },
                { "RabbitMqOptions:Password", "guest" }
            };
            config.AddInMemoryCollection(confDict);
        });
    }

    public override async ValueTask InitializeAsync()
    {
        await _rabbitMqContainer.StartAsync();
        await base.InitializeAsync();
    }

    public override async ValueTask DisposeAsync()
    {
        await _rabbitMqContainer.StopAsync();
        await _rabbitMqContainer.DisposeAsync();
        await base.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}

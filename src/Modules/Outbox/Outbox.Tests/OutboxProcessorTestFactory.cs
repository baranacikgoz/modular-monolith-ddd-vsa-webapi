using Common.Tests;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace Outbox.Tests;

// Real Postgres (via the base IntegrationTestFactory Testcontainer), fake bus. OutboxOptions:IsProcessor
// stays false (the base default), so OutboxProcessor is never auto-registered as a BackgroundService —
// tests call ProcessBatchAsync directly instead, for deterministic single-batch runs with no RabbitMQ
// broker and no poll-timer races.
public class OutboxProcessorTestFactory : IntegrationTestFactory
{
    protected override string[] GetActiveModules() => ["Outbox"];

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.ConfigureTestServices(services =>
        {
            services.AddSingleton<FakePublishEndpoint>();
            services.AddSingleton<IPublishEndpoint>(sp => sp.GetRequiredService<FakePublishEndpoint>());
        });
    }
}

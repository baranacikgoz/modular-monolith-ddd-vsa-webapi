using System.Reflection;
using Common.Application.Options;
using MassTransit;
using Microsoft.Extensions.Options;

namespace Host.Infrastructure;

internal static partial class Setup
{
    public static IServiceCollection AddCustomMassTransit(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly[] moduleAssemblies)
    {
        services.Configure<RabbitMqOptions>(configuration.GetSection(nameof(RabbitMqOptions)));

        // Integration tests run with no RabbitMQ broker. Against a real broker the bus blocks while the
        // OutboxProcessor publishes inside an open transaction holding "FOR UPDATE" locks on OutboxMessages,
        // so Respawn's between-test DELETE deadlocks and times out. The in-memory transport delivers in-process,
        // keeping publish/consume real (no mocking) while removing the broker dependency entirely.
        var useInMemoryTransport = configuration.GetValue<bool>("MassTransitOptions:UseInMemoryTransport");

        services.AddMassTransit(x =>
        {
            x.AddConsumers(moduleAssemblies);

            // MassTransit auto-registers a "masstransit-bus" health check tagged "ready" by default.
            // Drop the "ready" tag so it cannot gate the readiness probe: broker readiness is owned by
            // ConditionalRabbitMqHealthCheck (skippable via HealthCheckOptions.SkipRabbitMqHealthCheck),
            // which keeps test/CI environments — where no broker runs — from failing /health/ready.
            x.ConfigureHealthCheckOptions(options =>
            {
                options.Tags.Clear();
                options.Tags.Add("masstransit");
            });

            if (useInMemoryTransport)
            {
                x.UsingInMemory((ctx, cfg) => cfg.ConfigureEndpoints(ctx));
                return;
            }

            x.UsingRabbitMq((ctx, cfg) =>
            {
                var opts = ctx.GetRequiredService<IOptions<RabbitMqOptions>>().Value;

                cfg.Host(opts.Host, (ushort)opts.Port, opts.VirtualHost, h =>
                {
                    h.Username(opts.Username);
                    h.Password(opts.Password);
                    h.PublisherConfirmation = true;
                });

                cfg.ConfigureEndpoints(ctx);
            });
        });

        return services;
    }
}

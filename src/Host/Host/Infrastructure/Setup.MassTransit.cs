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

            // MassTransit auto-registers a "masstransit-bus" health check that probes the *already-open*
            // bus connection — no fresh TCP/AMQP handshake per call. Keep it on the "ready" tag so it,
            // and not a per-probe connection dial, owns broker readiness. Under the in-memory transport
            // (tests/CI, no broker) this check reports Healthy, so /health/ready passes without any skip flag.
            x.ConfigureHealthCheckOptions(options =>
            {
                options.Tags.Clear();
                options.Tags.Add("ready");
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

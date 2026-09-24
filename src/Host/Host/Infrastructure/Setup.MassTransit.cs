using System.Reflection;
using Common.Application.EventBus;
using Common.Application.Options;
using Common.InterModuleRequests.Contracts;
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

        // Request/response handlers get their own receive endpoint policy (no retry, own concurrency, handler
        // timeout) through InterModuleRequestHandlerDefinition. AddConsumers would register them without a
        // definition and a later AddConsumer for the same type is ignored, so they are excluded from the scan
        // and registered one by one with the closed definition type.
        var requestHandlerTypes = moduleAssemblies
            .SelectMany(GetLoadableTypes)
            .Where(IsInterModuleRequestHandler)
            .ToArray();

        services.AddMassTransit(x =>
        {
            x.AddConsumers(type => !IsInterModuleRequestHandler(type), moduleAssemblies);

            foreach (var handlerType in requestHandlerTypes)
            {
                x.AddConsumer(handlerType, typeof(InterModuleRequestHandlerDefinition<>).MakeGenericType(handlerType));
            }

            // MassTransit auto-registers a "masstransit-bus" health check that probes the *already-open*
            // bus connection: no fresh TCP/AMQP handshake per call. Keep it on the "ready" tag so it,
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

                // Transient consumer failures (DB blips, pool timeouts) retry with backoff instead of
                // faulting straight to the _error queue. Consumers are idempotent (IntegrationEventHandlerBase),
                // so redelivery is safe.
                cfg.UseMessageRetry(r => r.Exponential(
                    retryLimit: opts.RetryLimit,
                    minInterval: TimeSpan.FromMilliseconds(opts.RetryMinIntervalMs),
                    maxInterval: TimeSpan.FromMilliseconds(opts.RetryMaxIntervalMs),
                    intervalDelta: TimeSpan.FromMilliseconds(opts.RetryIntervalDeltaMs)));

                cfg.SetConcurrency(opts.DefaultPrefetchCount, opts.DefaultConcurrentMessageLimit);

                cfg.ConfigureEndpoints(ctx);
            });
        });

        return services;
    }

    private static bool IsInterModuleRequestHandler(Type type)
    {
        if (type.IsAbstract || type.IsInterface || type.IsGenericTypeDefinition)
        {
            return false;
        }

        for (var current = type.BaseType; current is not null; current = current.BaseType)
        {
            if (current.IsGenericType && current.GetGenericTypeDefinition() == typeof(InterModuleRequestHandler<,>))
            {
                return true;
            }
        }

        return false;
    }
}

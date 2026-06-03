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

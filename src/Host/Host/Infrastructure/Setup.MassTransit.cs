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

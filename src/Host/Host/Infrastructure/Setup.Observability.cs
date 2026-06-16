using System.Diagnostics;
using Common.Application.Options;
using Common.Infrastructure.Modules;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetryBuilder = OpenTelemetry.OpenTelemetryBuilder;

namespace Host.Infrastructure;

internal static partial class Setup
{
    public static IServiceCollection AddObservability(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment env)
    {
        Activity.DefaultIdFormat = ActivityIdFormat.W3C;

        var options = configuration
                          .GetSection(nameof(ObservabilityOptions))
                          .Get<ObservabilityOptions>()
                      ?? throw new InvalidOperationException("ObservabilityOptions is null.");

        if (!options.EnableMetrics && !options.EnableTracing)
        {
            return services;
        }

        var activeModules = GetRegisteredModules(services);

        if (options.EnableTracing && options.EnableMetrics)
        {
            services
                .AddOpenTelemetry()
                .ConfigureResource(ConfigureResource(options, env))
                .ConfigureMetrics(options, activeModules)
                .ConfigureTracing(options, activeModules);

            return services;
        }

        if (options.EnableMetrics)
        {
            services
                .AddOpenTelemetry()
                .ConfigureResource(ConfigureResource(options, env))
                .ConfigureMetrics(options, activeModules);

            return services;
        }

        // EnableTracing only
        services
            .AddOpenTelemetry()
            .ConfigureResource(ConfigureResource(options, env))
            .ConfigureTracing(options, activeModules);

        return services;
    }

    private static IReadOnlyList<IModule> GetRegisteredModules(IServiceCollection services)
    {
        var registry =
            services.LastOrDefault(d => d.ServiceType == typeof(ModuleRegistry))?.ImplementationInstance as
                ModuleRegistry;

        return registry?.OrderedModules ?? [];
    }

    private static OpenTelemetryBuilder ConfigureMetrics(this OpenTelemetryBuilder builder,
        ObservabilityOptions options,
        IReadOnlyList<IModule> activeModules)
    {
        return builder.WithMetrics(x =>
        {
            x
                .AddRuntimeInstrumentation()
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddProcessInstrumentation()
                .AddView("http.server.request.duration",
                    new ExplicitBucketHistogramConfiguration
                    {
                        Boundaries = [0, 0.005, 0.01, 0.025, 0.05, 0.075, 0.1, 0.25, 0.5, 0.75, 1, 2.5, 5, 7.5, 10]
                    })
                .AddOtlpExporter(o =>
                {
                    o.Endpoint = new Uri(options.OtlpEndpoint!);
                    o.Protocol = options.OtlpProtocol!.ToOtlpExportProtocol();
                });

            foreach (var module in activeModules)
            {
                foreach (var meterName in module.MeterNames)
                {
                    x.AddMeter(meterName);
                }
            }

            x.AddMeter("ModularMonolith.FeatureManagement");
        });
    }

    private static void ConfigureTracing(
        this OpenTelemetryBuilder builder,
        ObservabilityOptions options,
        IReadOnlyList<IModule> activeModules)
    {
        builder.WithTracing(x =>
        {
            x
                .AddAspNetCoreInstrumentation(cfg =>
                {
                    cfg.Filter = httpContext =>
                        !httpContext.Request.Path.StartsWithSegments("/health", StringComparison.OrdinalIgnoreCase);
                })
                .AddHttpClientInstrumentation()
                .AddEntityFrameworkCoreInstrumentation(cfg =>
                {
                    cfg.EnrichWithIDbCommand = (activity, command) =>
                    {
                        var sql = command.CommandText?.TrimStart();
                        var firstWord = sql?.Split(' ', StringSplitOptions.RemoveEmptyEntries)[0];
                        if (firstWord is { Length: > 0 })
                        {
                            activity.DisplayName = $"{firstWord} {activity.DisplayName}";
                            activity.SetTag("db.command_type", firstWord);
                        }
                    };
                })
                .AddOtlpExporter(o =>
                {
                    o.Endpoint = new Uri(options.OtlpEndpoint!);
                    o.Protocol = options.OtlpProtocol!.ToOtlpExportProtocol();
                });

            foreach (var module in activeModules)
            {
                foreach (var sourceName in module.ActivitySourceNames)
                {
                    x.AddSource(sourceName);
                }
            }

            x.AddSource("ModularMonolith.EventBus");
            x.AddSource("ModularMonolith.FeatureManagement");
            x.AddSource("MassTransit");
        });
    }

    private static Action<ResourceBuilder> ConfigureResource(ObservabilityOptions options, IHostEnvironment env)
    {
        return cfg => cfg
            .AddAttributes([new KeyValuePair<string, object>("service.environment", env.EnvironmentName)])
            .AddService(options.AppName,
                serviceVersion: options.AppVersion,
                serviceInstanceId: Environment.MachineName);
    }
}

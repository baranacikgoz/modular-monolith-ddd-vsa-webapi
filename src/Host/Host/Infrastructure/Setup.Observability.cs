using System.Data;
using System.Diagnostics;
using Common.Application.Options;
using Common.Infrastructure.Modules;
using MassTransit.Logging;
using MassTransit.Monitoring;
using Microsoft.Extensions.Options;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Host.Infrastructure;

internal static partial class Setup
{
    public static IApplicationBuilder UseObservability(this IApplicationBuilder app)
    {
        var observabilityOptions = app
            .ApplicationServices
            .GetRequiredService<IOptions<ObservabilityOptions>>()
            .Value ?? throw new InvalidOperationException("OtlpMetricsUsePrometheusDirectly is null.");

        if (observabilityOptions.OtlpMetricsUsePrometheusDirectly)
        {
            app.UseOpenTelemetryPrometheusScrapingEndpoint();
        }

        return app;
    }

    public static IServiceCollection AddObservability(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment env,
        IEnumerable<Func<string?, IDbCommand, bool>> efCoreTracingFiltersFromModules)
    {
        Activity.DefaultIdFormat = ActivityIdFormat.W3C;

        var options = configuration
                          .GetSection(nameof(ObservabilityOptions))
                          .Get<ObservabilityOptions>()
                      ?? throw new InvalidOperationException("LoggingMonitoringOptions is null.");

        if (!options.EnableMetrics && !options.EnableTracing)
        {
            return services;
        }

        var activeModules = GetRegisteredModules(services);

        if (options.EnableTracing && options.EnableMetrics)
        {
            services
                .AddOpenTelemetry()
                .ConfigureResource(ConfigureService(options, env))
                .ConfigureMetrics(options, activeModules)
                .ConfigureTracing(options, efCoreTracingFiltersFromModules, activeModules);

            return services;
        }

        if (options.EnableMetrics)
        {
            services
                .AddOpenTelemetry()
                .ConfigureResource(ConfigureService(options, env))
                .ConfigureMetrics(options, activeModules);

            return services;
        }

        // options.EnableTracing is true
        services
            .AddOpenTelemetry()
            .ConfigureResource(ConfigureService(options, env))
            .ConfigureTracing(options, efCoreTracingFiltersFromModules, activeModules);

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
        return builder
            .WithMetrics(x =>
            {
                x
                    .AddRuntimeInstrumentation()
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddProcessInstrumentation()
                    .AddView("http.server.request.duration",
                        new ExplicitBucketHistogramConfiguration
                        {
                            Boundaries =
                                [0, 0.005, 0.01, 0.025, 0.05, 0.075, 0.1, 0.25, 0.5, 0.75, 1, 2.5, 5, 7.5, 10]
                        })
                    .AddMeter(InstrumentationOptions.MeterName);

                // Auto-register module Meters
                foreach (var module in activeModules)
                {
                    foreach (var meterName in module.MeterNames)
                    {
                        x.AddMeter(meterName);
                    }
                }

                if (options.OtlpMetricsUsePrometheusDirectly)
                {
                    x.AddPrometheusExporter();
                }
                else
                {
                    x.AddOtlpExporter(o =>
                    {
                        o.Endpoint = new Uri(options.OtlpMetricsEndpoint!);
                        o.Protocol = options.OtlpMetricsProtocol!.ToOtlpExportProtocol();
                    });
                }
            });
    }

    private static OpenTelemetryBuilder ConfigureTracing(
        this OpenTelemetryBuilder builder,
        ObservabilityOptions options,
        IEnumerable<Func<string?, IDbCommand, bool>> efCoreTracingFiltersFromModules,
        IReadOnlyList<IModule> activeModules)
    {
        return builder
            .WithTracing(x =>
            {
                x
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddEntityFrameworkCoreInstrumentation(cfg =>
                    {
                        cfg.SetDbStatementForText = true;

                        foreach (var filter in efCoreTracingFiltersFromModules)
                        {
                            cfg.Filter += filter;
                        }
                    })
                    //.AddNpgsql()
                    .AddSource(DiagnosticHeaders.DefaultListenerName);

                // Auto-register module ActivitySources
                foreach (var module in activeModules)
                {
                    foreach (var sourceName in module.ActivitySourceNames)
                    {
                        x.AddSource(sourceName);
                    }
                }

                x.AddOtlpExporter(o =>
                {
                    o.Endpoint = new Uri(options.OtlpTracingEndpoint);
                    o.Protocol = options.OtlpTracingProtocol.ToOtlpExportProtocol();
                });
            });
    }

    private static Action<ResourceBuilder> ConfigureService(ObservabilityOptions options, IHostEnvironment env)
    {
        return cfg => cfg
            .AddAttributes(
            [
                new KeyValuePair<string, object>("service.environment", env.EnvironmentName)
            ])
            .AddService(options.AppName,
                serviceVersion: options.AppVersion,
                serviceInstanceId: Environment.MachineName);
    }
}

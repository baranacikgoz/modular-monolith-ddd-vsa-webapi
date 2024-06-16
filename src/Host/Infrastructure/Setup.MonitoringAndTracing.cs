using Common.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Npgsql;
using OpenTelemetry;
using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Host.Infrastructure;

public static partial class Setup
{
    public static IServiceCollection AddMetricsAndTracing(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        var options = configuration
                        .GetSection(nameof(LoggingMonitoringOptions))
                        .Get<LoggingMonitoringOptions>()
                        ?? throw new InvalidOperationException("LoggingMonitoringOptions is null.");

        if (!options.EnableMetrics && !options.EnableTracing)
        {
            return services;
        }

        if (options.EnableTracing && options.EnableMetrics)
        {
            services
                .AddOpenTelemetry()
                    .ConfigureResource(ConfigureService(options, env))
                    .ConfigureMetrics(options)
                    .ConfigureTracing(options);

            return services;
        }

        else if (options.EnableMetrics)
        {
            services
                .AddOpenTelemetry()
                    .ConfigureResource(ConfigureService(options, env))
                    .ConfigureMetrics(options);

            return services;
        }
        else // options.EnableTracing is true
        {
            services
                .AddOpenTelemetry()
                    .ConfigureResource(ConfigureService(options, env))
                    .ConfigureTracing(options);

            return services;
        }
    }

    private static OpenTelemetryBuilder ConfigureMetrics(this OpenTelemetryBuilder builder, LoggingMonitoringOptions options)
        => builder
            .WithMetrics(x => x
                .AddRuntimeInstrumentation()
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddProcessInstrumentation()
                .AddOtlpExporter(o =>
                {
                    o.Endpoint = new Uri(options.OtlpMetricsEndpoint);
                    o.Protocol = OtlpExportProtocol.Grpc;
                })
                .AddView("http.server.request.duration",
                    new ExplicitBucketHistogramConfiguration
                    {
                        Boundaries = [0, 0.005, 0.01, 0.025, 0.05, 0.075, 0.1, 0.25, 0.5, 0.75, 1, 2.5, 5, 7.5, 10]
                    })
                );

    private static OpenTelemetryBuilder ConfigureTracing(this OpenTelemetryBuilder builder, LoggingMonitoringOptions options)
        => builder
            .WithTracing(x => x
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddEntityFrameworkCoreInstrumentation()
                .AddNpgsql()
                .AddOtlpExporter(o =>
                {
                    o.Endpoint = new Uri(options.OtlpTracingEndpoint);
                    o.Protocol = OtlpExportProtocol.HttpProtobuf;
                }));

    private static Action<ResourceBuilder> ConfigureService(LoggingMonitoringOptions options, IHostEnvironment env)
        => cfg => cfg
                    .AddAttributes(
                    [
                        new("service.environment", env.EnvironmentName),
                    ])
                    .AddService(serviceName: options.AppName,
                                serviceVersion: options.AppVersion,
                                serviceInstanceId: Environment.MachineName);

    public static IApplicationBuilder UsePrometheusScraping(this IApplicationBuilder app)
        => app.UseOpenTelemetryPrometheusScrapingEndpoint();
}

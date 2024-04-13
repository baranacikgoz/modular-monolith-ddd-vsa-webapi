using Common.Options;
using Npgsql;
using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Host.Infrastructure;

public static partial class Setup
{
    public static IServiceCollection AddMetricsAndTracing(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration
                        .GetSection(nameof(LoggingMonitoringTracingOptions))
                        .Get<LoggingMonitoringTracingOptions>()
                        ?? throw new InvalidOperationException("LoggingMonitoringTracingOptions is null.");

        if (!options.EnableMetrics && !options.EnableTracing)
        {
            return services;
        }

        if (options.EnableTracing && options.EnableMetrics)
        {
            services
                .AddOpenTelemetry()
                    .ConfigureResource(ConfigureService(options))
                    .WithTracing(x => x
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddEntityFrameworkCoreInstrumentation()
                        .AddNpgsql()
                        .AddOtlpExporter(o =>
                        {
                            o.Endpoint = new Uri(options.OtlpTracingEndpoint);
                            o.Protocol = OtlpExportProtocol.HttpProtobuf;
                        }))
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
            return services;
        }

        else if (options.EnableTracing)
        {
            services
                .AddOpenTelemetry()
                    .ConfigureResource(ConfigureService(options))
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
            return services;
        }
        else // options.EnableMetrics is true
        {
            services
                .AddOpenTelemetry()
                    .ConfigureResource(cfg => cfg.AddService(serviceName: options.AppName,
                                                             //serviceVersion: typeof(Program).Assembly.GetName().Version?.ToString() ?? "unknown",
                                                             serviceInstanceId: Environment.MachineName))
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
            return services;
        }
    }

    private static Action<ResourceBuilder> ConfigureService(LoggingMonitoringTracingOptions options)
        => cfg => cfg.AddService(serviceName: options.AppName,
                                     serviceVersion: typeof(Program).Assembly.GetName().Version?.ToString() ?? "unknown",
                                     serviceInstanceId: Environment.MachineName);

    public static IApplicationBuilder UsePrometheusScraping(this IApplicationBuilder app)
        => app.UseOpenTelemetryPrometheusScrapingEndpoint();
}

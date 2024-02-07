using Common.Options;
using Npgsql;
using OpenTelemetry.Exporter;
using OpenTelemetry.Instrumentation.AspNetCore;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Host.Infrastructure;

public static partial class Setup
{
    public static IServiceCollection AddMonitoringAndTracing(this IServiceCollection services, IConfiguration configuration)
    {
        var monitoringOptions = configuration
                                    .GetSection(nameof(MonitoringTracingOptions))
                                    .Get<MonitoringTracingOptions>()
                                    ?? throw new InvalidOperationException("MonitoringOptions is null.");

        var seqUrl = configuration
                        .GetSection(nameof(CustomLoggingOptions))
                        .Get<CustomLoggingOptions>()?.SeqUrl
                        ?? throw new InvalidOperationException("SeqUrl is null.");

        // Build a resource configuration action to set service information.
        void configureResource(ResourceBuilder r)
            => r.AddService(serviceName: monitoringOptions.ServiceName,
            serviceVersion: typeof(Program).Assembly.GetName().Version?.ToString() ?? "unknown",
            serviceInstanceId: Environment.MachineName);

        services
            .Configure<AspNetCoreTraceInstrumentationOptions>(o =>
                {
                    o.Filter += httpContext => httpContext.Request.Path != "/metrics";
                    o.RecordException = true;
                })
            .AddOpenTelemetry()
                .ConfigureResource(configureResource)
                .WithTracing(x => x
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation(o =>
                    {
                        // Filter out requests to Seq.
                        o.FilterHttpWebRequest += request => request.RequestUri != seqUrl;

                        // Filter out requests for /metrics.
                        o.FilterHttpWebRequest += request => request.RequestUri?.AbsolutePath != "/metrics";
                    })
                    .AddEntityFrameworkCoreInstrumentation()
                    .AddNpgsql()
                    .AddTracingExporter(monitoringOptions))
                .WithMetrics(x => x
                    .AddRuntimeInstrumentation()
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddProcessInstrumentation()
                    .AddMetricsExporter(monitoringOptions)
                    .AddView("http.server.request.duration",
                        new ExplicitBucketHistogramConfiguration
                        {
                            Boundaries = [0, 0.005, 0.01, 0.025, 0.05, 0.075, 0.1, 0.25, 0.5, 0.75, 1, 2.5, 5, 7.5, 10]
                        })
                    );

        return services;
    }

    public static IApplicationBuilder UseMonitoringAndTracing(this IApplicationBuilder app, IConfiguration configuration)
    {
        var monitoringOptions = configuration
                                    .GetSection(nameof(MonitoringTracingOptions))
                                    .Get<MonitoringTracingOptions>()
                                    ?? throw new InvalidOperationException("MonitoringOptions is null.");

        if (string.Equals(monitoringOptions.MetricsExporter, "prometheus", StringComparison.OrdinalIgnoreCase))
        {
            app.UseOpenTelemetryPrometheusScrapingEndpoint();
        }

        return app;
    }

    private static TracerProviderBuilder AddTracingExporter(this TracerProviderBuilder builder, MonitoringTracingOptions monitoringOptions)
    {
        if (string.Equals(monitoringOptions.TracingExporter, "otlp", StringComparison.OrdinalIgnoreCase))
        {
            builder.AddOtlpExporter(o =>
            {
                o.Endpoint = new Uri(monitoringOptions.OtlpEndpoint);
                o.Protocol = OtlpExportProtocol.Grpc;
            });
        }
        else if (string.Equals(monitoringOptions.TracingExporter, "zipkin", StringComparison.OrdinalIgnoreCase))
        {
            builder.AddZipkinExporter(o =>
            {
                o.Endpoint = new Uri(monitoringOptions.ZipkinEndpoint);
            });
        }
        else
        {
            throw new InvalidOperationException($"Unknown tracing exporter: {monitoringOptions.TracingExporter}");
        }

        return builder;
    }

    private static MeterProviderBuilder AddMetricsExporter(this MeterProviderBuilder builder, MonitoringTracingOptions monitoringOptions)
    {
        if (string.Equals(monitoringOptions.MetricsExporter, "prometheus", StringComparison.OrdinalIgnoreCase))
        {
            builder.AddPrometheusExporter();
        }
        else if (string.Equals(monitoringOptions.MetricsExporter, "otlp", StringComparison.OrdinalIgnoreCase))
        {
            builder.AddOtlpExporter(o =>
            {
                o.Endpoint = new Uri(monitoringOptions.OtlpEndpoint);
                o.Protocol = OtlpExportProtocol.Grpc;
            });
        }
        else
        {
            throw new InvalidOperationException($"Unknown metrics exporter: {monitoringOptions.MetricsExporter}");
        }

        return builder;
    }
}

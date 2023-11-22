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
                                    .GetSection(nameof(MonitoringOptions))
                                    .Get<MonitoringOptions>()
                                    ?? throw new InvalidOperationException("MonitoringOptions is null.");

        var seqUrl = configuration
                        .GetSection(nameof(CustomLoggingOptions))
                        .Get<CustomLoggingOptions>()?.SeqUrl
                        ?? throw new InvalidOperationException("SeqUrl is null.");

        services
        .Configure<AspNetCoreInstrumentationOptions>(o =>
            {
                o.Filter = httpContext => httpContext.Request.Path != "/metrics";
            })
        .AddOpenTelemetry()
            .ConfigureResource(r =>
                {
                    r.AddService(monitoringOptions.ServiceName);
                    r.AddTelemetrySdk();
                })
            .WithTracing(x => x
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation(o =>
                {
                    // Filter out requests to Seq.
                    o.FilterHttpWebRequest = request => request.RequestUri != seqUrl;
                })
                .AddEntityFrameworkCoreInstrumentation()
                .AddNpgsql()
                .AddOtlpExporter(o =>
                {
                    o.Endpoint = new Uri(monitoringOptions.OtlpEndpoint);
                    o.Protocol = OtlpExportProtocol.Grpc;
                }))
            .WithMetrics(x => x
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddRuntimeInstrumentation()
                .AddProcessInstrumentation()
                .AddView("http.server.request.duration",
                    new ExplicitBucketHistogramConfiguration
                    {
                        Boundaries = [0, 0.005, 0.01, 0.025, 0.05, 0.075, 0.1, 0.25, 0.5, 0.75, 1, 2.5, 5, 7.5, 10]
                    })
                .AddPrometheusExporter());

        return services;
    }
}

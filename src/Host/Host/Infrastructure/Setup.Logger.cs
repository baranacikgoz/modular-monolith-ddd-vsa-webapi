using System.Globalization;
using Common.Application.Options;
using Elastic.Serilog.Sinks;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Configuration;
using Serilog.Enrichers.Span;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Compact;
using Serilog.Sinks.OpenTelemetry;
using Serilog.Sinks.SystemConsole.Themes;

namespace Host.Infrastructure;

internal static partial class Setup
{
    public static IHostBuilder UseCustomizedSerilog(this IHostBuilder hostBuilder)
    {
        return hostBuilder.UseSerilog((ctx, sp, serilog) =>
        {
            var options = sp.GetRequiredService<IOptions<ObservabilityOptions>>()?.Value
                          ?? throw new InvalidOperationException($"{nameof(ObservabilityOptions)} is null.");

            serilog.ApplyConfigurations(options, ctx.HostingEnvironment);
        });
    }

    public static LoggerConfiguration ApplyConfigurations(this LoggerConfiguration serilog,
        ObservabilityOptions options, IHostEnvironment env)
    {
        serilog.MinimumLevel.ParseFrom(options.MinimumLevel);
        serilog.OverrideMinimumLevelsOf(options.MinimumLevelOverrides);
        serilog.ConfigureEnrichers(options, env);
        serilog.ConfigureWriteTos(options, env);
        return serilog;
    }

    private static void ParseFrom(this LoggerMinimumLevelConfiguration minLevel,
        string level)
    {
        if (level.IsDebug()) { minLevel.Debug(); return; }
        if (level.IsInformation()) { minLevel.Information(); return; }
        if (level.IsWarning()) { minLevel.Warning(); return; }
        throw new InvalidOperationException($"Minimum log level {level} is unknown.");
    }

    private static void OverrideMinimumLevelsOf(
        this LoggerConfiguration serilog,
        IEnumerable<KeyValuePair<string, string>> minimumLevelOverrides)
    {
        foreach (var (key, value) in minimumLevelOverrides)
        {
            if (value.IsDebug()) { serilog.MinimumLevel.Override(key, LogEventLevel.Debug); continue; }
            if (value.IsInformation()) { serilog.MinimumLevel.Override(key, LogEventLevel.Information); continue; }
            if (value.IsWarning()) { serilog.MinimumLevel.Override(key, LogEventLevel.Warning); continue; }
            throw new InvalidOperationException($"Minimum log level ({value}) is unknown.");
        }
    }

    private static void ConfigureEnrichers(this LoggerConfiguration serilog, ObservabilityOptions options,
        IHostEnvironment env)
    {
        serilog
            .Enrich.WithProperty("Application", options.AppName)
            .Enrich.WithProperty("Environment", env.EnvironmentName)
            .Enrich.WithProperty("AppVersion", options.AppVersion)
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .Enrich.WithMachineName()
            .Enrich.WithProcessId()
            .Enrich.WithThreadId()
            .Enrich.WithSpan();
    }

    private static void ConfigureWriteTos(this LoggerConfiguration serilog, ObservabilityOptions options,
        IHostEnvironment env)
    {
        if (options.WriteToConsole)
        {
            serilog.WriteTo.Async(wt => wt.Console(
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                theme: SystemConsoleTheme.Literate,
                formatProvider: CultureInfo.InvariantCulture));
        }

        if (options.WriteToFile)
        {
            serilog.WriteTo.Async(wt => wt.File(
                new CompactJsonFormatter(),
                $"logs/{options.AppName}-.logs",
                rollingInterval: RollingInterval.Day,
                rollOnFileSizeLimit: true,
                fileSizeLimitBytes: 10 * 1024 * 1024,
                retainedFileCountLimit: 10,
                restrictedToMinimumLevel: LogEventLevel.Information));
        }

        if (options.LogSink == "Seq")
        {
            serilog.WriteTo.Seq(options.SeqServerUrl!, formatProvider: CultureInfo.InvariantCulture);
        }
        else if (options.LogSink == "Elasticsearch")
        {
            serilog.WriteTo.Elasticsearch([new Uri(options.ElasticsearchUrl!)]);
        }
        else if (options.LogSink == "Otlp")
        {
            serilog.WriteTo.OpenTelemetry(cfg =>
            {
                cfg.Endpoint = options.OtlpEndpoint!;
                cfg.Protocol = options.OtlpProtocol!.Equals("Grpc", StringComparison.OrdinalIgnoreCase)
                    ? OtlpProtocol.Grpc
                    : OtlpProtocol.HttpProtobuf;
                cfg.ResourceAttributes = new Dictionary<string, object>
                {
                    ["service.name"] = options.AppName,
                    ["service.version"] = options.AppVersion,
                    ["service.instance.id"] = Environment.MachineName,
                    ["service.environment"] = env.EnvironmentName,
                    ["telemetry.sdk.language"] = "dotnet",
                    ["telemetry.sdk.name"] = "serilog",
                };
            });
        }
    }

    private static bool IsDebug(this string level)
        => string.Equals("Debug", level, StringComparison.OrdinalIgnoreCase);

    private static bool IsInformation(this string level)
        => string.Equals("Information", level, StringComparison.OrdinalIgnoreCase);

    private static bool IsWarning(this string level)
        => string.Equals("Warning", level, StringComparison.OrdinalIgnoreCase);
}

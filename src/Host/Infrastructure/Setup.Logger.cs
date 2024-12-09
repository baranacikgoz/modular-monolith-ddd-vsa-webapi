using System.Globalization;
using Common.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Configuration;
using Serilog.Enrichers.Span;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Compact;
using Serilog.Sinks.SystemConsole.Themes;

namespace Host.Infrastructure;

internal static partial class Setup
{
    public static IHostBuilder UseCustomizedSerilog(this IHostBuilder hostBuilder)
        => hostBuilder.UseSerilog((ctx, sp, serilog) =>
        {
            var options = sp.GetRequiredService<IOptions<ObservabilityOptions>>()?.Value
                ?? throw new InvalidOperationException($"{nameof(ObservabilityOptions)} is null.");

            serilog.ApplyConfigurations(options, ctx.HostingEnvironment);
        });

    public static LoggerConfiguration ApplyConfigurations(this LoggerConfiguration serilog, ObservabilityOptions options, IHostEnvironment env)
    {
        serilog.MinimumLevel.ParseFrom(options.MinimumLevel);

        serilog.OverrideMinimumLevelsOf(options.MinimumLevelOverrides);

        serilog.ConfigureEnrichers(options, env);

        serilog.ConfigureWriteTos(options);

        return serilog;
    }

    private static LoggerMinimumLevelConfiguration ParseFrom(this LoggerMinimumLevelConfiguration minLevel, string level)
    {
        if (level.IsDebug())
        {
            minLevel.Debug();
            return minLevel;
        }

        if (level.IsInformation())
        {
            minLevel.Information();
            return minLevel;
        }

        if (level.IsWarning())
        {
            minLevel.Warning();
            return minLevel;
        }

        throw new InvalidOperationException($"Minimum log level {level} is unknown.");
    }

    private static void OverrideMinimumLevelsOf(
        this LoggerConfiguration serilog,
        IEnumerable<KeyValuePair<string, string>> minimumLevelOverrides)
    {
        foreach (var (key, value) in minimumLevelOverrides)
        {
            if (value.IsDebug())
            {
                serilog.MinimumLevel.Override(key, LogEventLevel.Debug);
                continue;
            }

            if (value.IsInformation())
            {
                serilog.MinimumLevel.Override(key, LogEventLevel.Information);
                continue;
            }

            if (value.IsWarning())
            {
                serilog.MinimumLevel.Override(key, LogEventLevel.Warning);
                continue;
            }

            throw new InvalidOperationException($"Minimum log level ({value}) is unknown.");
        }
    }

    private static void ConfigureEnrichers(this LoggerConfiguration serilog, ObservabilityOptions options, IHostEnvironment env)
        => serilog
                .Enrich
                    .WithProperty("Application", options.AppName)
                .Enrich
                    .WithProperty("Environment", env.EnvironmentName)
                .Enrich
                    .WithProperty("AppVersion", options.AppVersion)
                .Enrich
                    .FromLogContext()
                .Enrich
                    .WithExceptionDetails()
                .Enrich
                    .WithMachineName()
                .Enrich
                    .WithProcessId()
                .Enrich
                    .WithThreadId()
                .Enrich
                    .WithSpan();

    private static void ConfigureWriteTos(this LoggerConfiguration serilog, ObservabilityOptions options)
    {
        if (options.WriteToConsole)
        {
            serilog
                .WriteTo
                    .Async(wt => wt.Console(
                            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                            theme: SystemConsoleTheme.Literate,
                            formatProvider: CultureInfo.InvariantCulture));
        }

        if (options.WriteToFile)
        {
            serilog
                .WriteTo
                    .Async(wt => wt.File(new CompactJsonFormatter(),
                                        $"logs/{options.AppName}-.logs",
                                        rollingInterval: RollingInterval.Day,
                                        rollOnFileSizeLimit: true,
                                        fileSizeLimitBytes: 10 * 1024 * 1024,
                                        retainedFileCountLimit: 10,
                                        restrictedToMinimumLevel: LogEventLevel.Information));
        }

        serilog
            .WriteTo
                .Async(wt => wt.OpenTelemetry(
                                    endpoint: options.OtlpLoggingEndpoint,
                                    protocol: options.OtlpLoggingProtocol.ToOtlpProtocol(),
                                    resourceAttributes: new Dictionary<string, object>() { { "service.name", options.AppName } }
                                    ));
    }

    private static bool IsDebug(this string level) => string.Equals("Debug", level, StringComparison.OrdinalIgnoreCase);
    private static bool IsInformation(this string level) => string.Equals("Information", level, StringComparison.OrdinalIgnoreCase);
    private static bool IsWarning(this string level) => string.Equals("Warning", level, StringComparison.OrdinalIgnoreCase);
}

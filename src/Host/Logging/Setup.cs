using System.Globalization;
using Common.Options;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Compact;

namespace Host.Logging;

public static class Setup
{
    public static WebApplicationBuilder UseSerilogAsLoggingProvider(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, loggerConfiguration) =>
        {
            var loggerOptions = context.Configuration.GetSection(nameof(LoggerOptions)).Get<LoggerOptions>()!;

            var appName = loggerOptions.AppName;
            var seqConnectionString = loggerOptions.SeqConnectionString;
            var writeToFile = loggerOptions.WriteToFile;
            var minimumLogLevel = loggerOptions.MinimumLogLevel;

            ConfigureEnrichers(loggerConfiguration, appName);
            ConfigureConsoleLogging(loggerConfiguration, loggerOptions.WriteToConsole);
            ConfigureWriteToFile(loggerConfiguration, writeToFile);
            ConfigureSeq(loggerConfiguration, seqConnectionString);
            SetMinimumLogLevel(loggerConfiguration, minimumLogLevel);
            OverideMinimumLogLevel(loggerConfiguration);

        });

        return builder;
    }

    private static void ConfigureEnrichers(LoggerConfiguration serilogConfig, string appName)
    {
        serilogConfig
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Application", appName)
            .Enrich.WithExceptionDetails()
            .Enrich.WithMachineName()
            .Enrich.WithProcessId()
            .Enrich.WithThreadId();
    }

    private static void ConfigureConsoleLogging(LoggerConfiguration serilogConfig, bool writeToConsole)
    {
        if (writeToConsole)
        {
            serilogConfig.WriteTo.Async(wt => wt.Console(formatProvider: CultureInfo.InvariantCulture));
        }
    }

    private static void ConfigureWriteToFile(LoggerConfiguration serilogConfig, bool writeToFile)
    {
        if (writeToFile)
        {
            serilogConfig.WriteTo.File(
             new CompactJsonFormatter(),
             "Logs/logs.json",
             restrictedToMinimumLevel: LogEventLevel.Information,
             rollingInterval: RollingInterval.Day,
             retainedFileCountLimit: 31);
        }
    }

    private static void ConfigureSeq(LoggerConfiguration serilogConfig, string seqUrl)
    {
        serilogConfig.WriteTo.Seq(seqUrl);
    }

    private static void SetMinimumLogLevel(LoggerConfiguration serilogConfig, string minLogLevel)
    {
        switch (minLogLevel.ToUpperInvariant())
        {
            case "DEBUG":
                serilogConfig.MinimumLevel.Debug();
                break;

            case "INFORMATION":
                serilogConfig.MinimumLevel.Information();
                break;

            case "WARNING":
                serilogConfig.MinimumLevel.Warning();
                break;

            default:
                serilogConfig.MinimumLevel.Information();
                break;
        }
    }

    private static void OverideMinimumLogLevel(LoggerConfiguration serilogConfig)
    {
        serilogConfig
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .MinimumLevel.Override("Hangfire", LogEventLevel.Warning)
                    .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Error);
    }
}

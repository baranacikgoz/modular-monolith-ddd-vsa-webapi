using Common.Application.Options;
using Common.Tests.Helpers;
using Host.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Json;

namespace Host.Tests;

// A DbUpdateException carries every tracked entity and, through them, the whole DbContext. Destructured by
// reflection the record grows with the tracked entities: about 200 KB for one, over 0.8 MB with a module
// context. Loki refuses an entry above 256 KB (the stack trace then never arrives) and every log call took over
// 100 ms of the request thread. The record must stay small.
public class ExceptionLoggingTests
{
    private const int RecordLimitBytes = 64 * 1024;

    private sealed class CollectingSink : ILogEventSink
    {
        public List<LogEvent> Events { get; } = [];

        public void Emit(LogEvent logEvent) => Events.Add(logEvent);
    }

    private sealed class TestHostEnvironment : IHostEnvironment
    {
        public string EnvironmentName { get; set; } = "Test";
        public string ApplicationName { get; set; } = "Host.Tests";
        public string ContentRootPath { get; set; } = AppContext.BaseDirectory;
        public IFileProvider ContentRootFileProvider { get; set; } = new NullFileProvider();
    }

    private static ObservabilityOptions Options()
    {
        return new ObservabilityOptions
        {
            AppName = "host-tests",
            AppVersion = "1.0.0",
            MinimumLevel = "Information",
            WriteToConsole = false,
            WriteToFile = false,
            ResponseTimeThresholdInMs = 1000,
            EnableMetrics = false,
            EnableTracing = false,
            TraceSamplingRatio = 1.0
        };
    }

    [Fact]
    public void Error_WithDbUpdateExceptionCarryingTrackedEntries_IsWrittenUnderTheRecordLimit()
    {
        var contextOptions = new DbContextOptionsBuilder<HelpersTestDbContext>()
            .UseNpgsql("Host=localhost;Database=unused")
            .Options;
        using var db = new HelpersTestDbContext(contextOptions);
        var entry = db.Projections.Add(new SampleProjection { SourceId = "source-1", Name = "tracked" });
#pragma warning disable EF1001 // The update entry of a tracked entity is only reachable through the infrastructure accessor.
        IUpdateEntry updateEntry = entry.GetInfrastructure();
#pragma warning restore EF1001
        var exception = new DbUpdateException(
            "An error occurred while saving the entity changes.",
            new InvalidOperationException("23505: duplicate key value violates unique constraint"),
            [updateEntry]);

        var sink = new CollectingSink();
        using var logger = new LoggerConfiguration()
            .ApplyConfigurations(Options(), new TestHostEnvironment())
            .WriteTo.Sink(sink)
            .CreateLogger();

        logger.Error(exception, "Error saving changes to the database.");

        using var writer = new StringWriter();
        new JsonFormatter().Format(Assert.Single(sink.Events), writer);
        var record = writer.ToString();
        Assert.True(record.Length < RecordLimitBytes, $"The log record is {record.Length} characters, the limit is {RecordLimitBytes}.");
        Assert.Contains("23505", record, StringComparison.Ordinal);
    }
}

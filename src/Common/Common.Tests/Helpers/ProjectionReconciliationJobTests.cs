using System.Globalization;
using Common.Application.Options;
using Common.Application.Persistence.Projections;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Xunit;

namespace Common.Tests.Helpers;

public class ProjectionReconciliationJobTests(IntegrationTestFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task RunAsync_PagesThroughSourceAndPersistsEveryPage()
    {
        await using var db = await HelpersTestDbContext.CreateAsync(Factory.ConnectionString);
        var prefix = Guid.NewGuid().ToString("N");
        var source = Enumerable.Range(1, 7).Select(i => $"{prefix}-{i}").ToList();
        var job = new SampleReconciliationJob(db, source, Options.Create(new ProjectionReconciliationOptions { PageSize = 3, MaxPages = 10 }));

        var applied = await job.RunAsync();

        Assert.Equal(7, applied);
        Assert.Equal([3, 3, 1], job.PageSizes);
        Assert.Equal(7, await db.Projections.AsNoTracking().CountAsync(p => p.SourceId.StartsWith(prefix)));
        Assert.Empty(db.ChangeTracker.Entries());
    }

    [Fact]
    public async Task RunAsync_MaxPagesReached_StopsWithoutLooping()
    {
        await using var db = await HelpersTestDbContext.CreateAsync(Factory.ConnectionString);
        var prefix = Guid.NewGuid().ToString("N");
        var source = Enumerable.Range(1, 10).Select(i => $"{prefix}-{i}").ToList();
        var job = new SampleReconciliationJob(db, source, Options.Create(new ProjectionReconciliationOptions { PageSize = 2, MaxPages = 3 }));

        var applied = await job.RunAsync();

        Assert.Equal(6, applied);
        Assert.Equal(3, job.PageSizes.Count);
    }

    [Fact]
    public async Task RunAsync_HealedRows_LogsWarningWithHealedAndReadCounts()
    {
        await using var db = await HelpersTestDbContext.CreateAsync(Factory.ConnectionString);
        var prefix = Guid.NewGuid().ToString("N");
        var source = Enumerable.Range(1, 6).Select(i => $"{prefix}-{i}").ToList();
        var logger = new CapturingLogger();
        var options = Options.Create(new ProjectionReconciliationOptions { PageSize = 3, MaxPages = 10 });
        // Two rows already current: the run finds them stale and heals only the other four.
        await db.Projections.UpsertIfNewerAsync(source[0], 1, () => new SampleProjection { SourceId = source[0] }, p => p.Name = source[0], CancellationToken.None);
        await db.Projections.UpsertIfNewerAsync(source[1], 1, () => new SampleProjection { SourceId = source[1] }, p => p.Name = source[1], CancellationToken.None);
        db.ChangeTracker.Clear();
        var job = new SampleReconciliationJob(db, source, options, logger);

        await job.RunAsync();

        var warning = Assert.Single(logger.Entries, e => e.Level == LogLevel.Warning);
        Assert.Contains("healed 4 of 6", warning.Message, StringComparison.Ordinal);
    }

    [Fact]
    public async Task RunAsync_NothingHealed_LogsNoWarning()
    {
        await using var db = await HelpersTestDbContext.CreateAsync(Factory.ConnectionString);
        var prefix = Guid.NewGuid().ToString("N");
        var source = Enumerable.Range(1, 4).Select(i => $"{prefix}-{i}").ToList();
        var options = Options.Create(new ProjectionReconciliationOptions { PageSize = 3, MaxPages = 10 });
        await new SampleReconciliationJob(db, source, options, new CapturingLogger()).RunAsync();
        var logger = new CapturingLogger();

        await new SampleReconciliationJob(db, source, options, logger).RunAsync();

        Assert.DoesNotContain(logger.Entries, e => e.Level >= LogLevel.Warning);
    }

    [Fact]
    public async Task RunAsync_MaxPagesReached_WarnsThatTheRestIsNotReconciled()
    {
        await using var db = await HelpersTestDbContext.CreateAsync(Factory.ConnectionString);
        var prefix = Guid.NewGuid().ToString("N");
        var source = Enumerable.Range(1, 10).Select(i => $"{prefix}-{i}").ToList();
        var logger = new CapturingLogger();
        var job = new SampleReconciliationJob(
            db, source, Options.Create(new ProjectionReconciliationOptions { PageSize = 2, MaxPages = 3 }), logger);

        await job.RunAsync();

        Assert.Contains(logger.Entries, e =>
            e.Level == LogLevel.Warning && e.Message.Contains("NOT reconciled", StringComparison.Ordinal));
    }

    private sealed class SampleReconciliationJob(
        HelpersTestDbContext db,
        IReadOnlyList<string> source,
        IOptions<ProjectionReconciliationOptions> options,
        ILogger? logger = null)
        : ProjectionReconciliationJob<string, string>(db, options, logger ?? NullLogger.Instance)
    {
        public List<int> PageSizes { get; } = [];

        protected override Task<(IReadOnlyList<string> Items, string? Next)> FetchPageAsync(
            string? cursor, int pageSize, CancellationToken cancellationToken)
        {
            var offset = cursor is null ? 0 : int.Parse(cursor, CultureInfo.InvariantCulture);
            var items = source.Skip(offset).Take(pageSize).ToList();
            var end = offset + items.Count;
            var next = end < source.Count ? end.ToString(CultureInfo.InvariantCulture) : null;
            return Task.FromResult<(IReadOnlyList<string>, string?)>((items, next));
        }

        protected override async Task<int> ApplyPageAsync(IReadOnlyList<string> items, CancellationToken cancellationToken)
        {
            PageSizes.Add(items.Count);
            var healed = 0;
            foreach (var item in items)
            {
                var outcome = await db.Projections.UpsertIfNewerAsync(item, 1, () => new SampleProjection { SourceId = item },
                    p => p.Name = item, cancellationToken);
                if (outcome != ProjectionUpsertOutcome.Stale)
                {
                    healed++;
                }
            }

            return healed;
        }
    }

    private sealed class CapturingLogger : ILogger
    {
        public List<(LogLevel Level, string Message)> Entries { get; } = [];

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
            Entries.Add((logLevel, formatter(state, exception)));
        }
    }
}

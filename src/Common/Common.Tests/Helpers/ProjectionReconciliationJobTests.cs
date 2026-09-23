using System.Globalization;
using Common.Application.Options;
using Common.Application.Persistence.Projections;
using Microsoft.EntityFrameworkCore;
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

    private sealed class SampleReconciliationJob(
        HelpersTestDbContext db,
        IReadOnlyList<string> source,
        IOptions<ProjectionReconciliationOptions> options)
        : ProjectionReconciliationJob<string, string>(db, options, NullLogger.Instance)
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

        protected override async Task ApplyPageAsync(IReadOnlyList<string> items, CancellationToken cancellationToken)
        {
            PageSizes.Add(items.Count);
            foreach (var item in items)
            {
                await db.Projections.UpsertIfNewerAsync(item, 1, () => new SampleProjection { SourceId = item },
                    p => p.Name = item, cancellationToken);
            }
        }
    }
}

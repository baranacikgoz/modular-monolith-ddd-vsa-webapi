using Common.Application.Persistence.Projections;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Common.Tests.Helpers;

public class ProjectionUpsertTests(IntegrationTestFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task UpsertIfNewerAsync_MissingRow_Inserts()
    {
        await using var db = await HelpersTestDbContext.CreateAsync(Factory.ConnectionString);
        var key = NewKey();

        var outcome = await db.Projections.UpsertIfNewerAsync(key, 3,
            () => new SampleProjection { SourceId = key }, p => p.Name = "v3", CancellationToken.None);

        Assert.Equal(ProjectionUpsertOutcome.Inserted, outcome);
        var row = await db.Projections.AsNoTracking().SingleAsync(p => p.SourceId == key);
        Assert.Equal("v3", row.Name);
        Assert.Equal(3, row.SourceVersion);
    }

    [Fact]
    public async Task UpsertIfNewerAsync_NewerVersion_Updates()
    {
        await using var db = await HelpersTestDbContext.CreateAsync(Factory.ConnectionString);
        var key = NewKey();
        await db.Projections.UpsertIfNewerAsync(key, 3, () => new SampleProjection { SourceId = key }, p => p.Name = "v3", CancellationToken.None);

        var outcome = await db.Projections.UpsertIfNewerAsync(key, 4,
            () => throw new InvalidOperationException("must not create"), p => p.Name = "v4", CancellationToken.None);

        Assert.Equal(ProjectionUpsertOutcome.Updated, outcome);
        var row = await db.Projections.AsNoTracking().SingleAsync(p => p.SourceId == key);
        Assert.Equal("v4", row.Name);
        Assert.Equal(4, row.SourceVersion);
    }

    [Theory]
    [InlineData(5)]
    [InlineData(2)]
    public async Task UpsertIfNewerAsync_SameOrOlderVersion_IsStaleAndDoesNotApply(long incoming)
    {
        await using var db = await HelpersTestDbContext.CreateAsync(Factory.ConnectionString);
        var key = NewKey();
        await db.Projections.UpsertIfNewerAsync(key, 5, () => new SampleProjection { SourceId = key }, p => p.Name = "v5", CancellationToken.None);
        var applied = false;

        var outcome = await db.Projections.UpsertIfNewerAsync(key, incoming,
            () => throw new InvalidOperationException("must not create"), _ => applied = true, CancellationToken.None);

        Assert.Equal(ProjectionUpsertOutcome.Stale, outcome);
        Assert.False(applied);
        var row = await db.Projections.AsNoTracking().SingleAsync(p => p.SourceId == key);
        Assert.Equal("v5", row.Name);
        Assert.Equal(5, row.SourceVersion);
    }

    [Theory]
    [InlineData(7, ProjectionUpsertOutcome.Updated, "v7")]
    [InlineData(3, ProjectionUpsertOutcome.Stale, "v5")]
    public async Task UpsertIfNewerAsync_LostInsertRace_RereadsAndResolvesByVersion(
        long incoming, ProjectionUpsertOutcome expected, string expectedName)
    {
        await using var db = await HelpersTestDbContext.CreateAsync(Factory.ConnectionString);
        await using var rival = await HelpersTestDbContext.CreateAsync(Factory.ConnectionString);
        var key = NewKey();

        // The create callback runs after the miss and before the insert: the rival lands the row in that window.
        var outcome = await db.Projections.UpsertIfNewerAsync(key, incoming,
            () =>
            {
                rival.Projections.UpsertIfNewerAsync(key, 5, () => new SampleProjection { SourceId = key },
                    p => p.Name = "v5", CancellationToken.None).GetAwaiter().GetResult();
                return new SampleProjection { SourceId = key };
            },
            p => p.Name = $"v{incoming}",
            CancellationToken.None);

        Assert.Equal(expected, outcome);
        var row = await rival.Projections.AsNoTracking().SingleAsync(p => p.SourceId == key);
        Assert.Equal(expectedName, row.Name);
        Assert.Equal(Math.Max(incoming, 5), row.SourceVersion);
    }

    private static string NewKey()
    {
        return Guid.NewGuid().ToString("N");
    }
}

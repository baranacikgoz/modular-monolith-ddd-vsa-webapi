using Common.Domain.StronglyTypedIds;
using Inventory.Application.StoreAccesses;
using Xunit;

namespace Inventory.Tests.StoreAccesses;

public class StoreAccessTests
{
    private static readonly ApplicationUserId Owner = new(DefaultIdType.CreateVersion7());

    private static StoreAccess NewRow(bool isActive = true, long version = 1)
    {
        var row = StoreAccess.Create(DefaultIdType.CreateVersion7());
        row.ApplyIfNewer(Owner, isActive, version);
        return row;
    }

    [Fact]
    public void Apply_SameValues_ReportsNoChange()
    {
        var row = NewRow();

        Assert.False(row.Apply(Owner, true));
    }

    [Fact]
    public void Apply_ClosedStore_ReportsChangeAndTakesIt()
    {
        var row = NewRow();

        Assert.True(row.Apply(Owner, false));
        Assert.False(row.IsActive);
    }

    [Theory]
    [InlineData(3)]
    [InlineData(2)]
    public void ApplyIfNewer_SameOrOlderVersion_LeavesTheRowUntouched(long version)
    {
        var row = NewRow(isActive: true, version: 3);

        Assert.False(row.ApplyIfNewer(Owner, false, version));
        Assert.True(row.IsActive);
        Assert.Equal(3, row.SourceVersion);
    }

    [Fact]
    public void ApplyIfNewer_NewerVersion_AppliesAndClaimsTheVersion()
    {
        var row = NewRow(isActive: true, version: 3);

        Assert.True(row.ApplyIfNewer(Owner, false, 4));
        Assert.False(row.IsActive);
        Assert.Equal(4, row.SourceVersion);
    }
}

using Inventory.Application.ProductSummaries;
using Xunit;

namespace Inventory.Tests.ProductSummaries;

public class ProductSummaryTests
{
    private static readonly DefaultIdType StoreId = DefaultIdType.CreateVersion7();

    private static ProductSummary NewRow(string name = "Widget", long version = 1)
    {
        var row = ProductSummary.Create(DefaultIdType.CreateVersion7());
        row.ApplyIfNewer(StoreId, name, version);
        return row;
    }

    [Fact]
    public void Apply_SameValues_ReportsNoChange()
    {
        var row = NewRow();

        Assert.False(row.Apply(StoreId, "Widget"));
    }

    [Fact]
    public void Apply_NewName_ReportsChangeAndTakesIt()
    {
        var row = NewRow();

        Assert.True(row.Apply(StoreId, "Gadget"));
        Assert.Equal("Gadget", row.Name);
    }

    [Theory]
    [InlineData(3)]
    [InlineData(2)]
    public void ApplyIfNewer_SameOrOlderVersion_LeavesTheRowUntouched(long version)
    {
        var row = NewRow(version: 3);

        Assert.False(row.ApplyIfNewer(StoreId, "Stale", version));
        Assert.Equal("Widget", row.Name);
        Assert.Equal(3, row.SourceVersion);
    }

    [Fact]
    public void ApplyIfNewer_NewerVersion_AppliesAndClaimsTheVersion()
    {
        var row = NewRow(version: 3);
        var otherStoreId = DefaultIdType.CreateVersion7();

        Assert.True(row.ApplyIfNewer(otherStoreId, "Gadget", version: 4));
        Assert.Equal("Gadget", row.Name);
        Assert.Equal(otherStoreId, row.StoreId);
        Assert.Equal(4, row.SourceVersion);
    }
}

using Common.Tests;
using Xunit;

namespace Inventory.Tests.Architecture;

/// <summary>
/// A stock movement is a ledger row: it exists because a <c>StockLevel</c> command method changed the quantity, and it is never
/// edited or removed afterwards. The type system keeps outsiders away (<c>StockMovement.Create</c> is internal, the properties have
/// no public setters); this scan keeps a new code path from quietly becoming a second writer, for example a bulk job that adds rows
/// through the DbSet or a raw SQL statement that skips the aggregate and its events.
/// </summary>
public class StockMovementWriteGuardTests
{
    private const string Aggregate = "src/Modules/Inventory/Inventory.Domain/StockLevels/StockLevel.cs";
    private const string Entity = "src/Modules/Inventory/Inventory.Domain/StockLevels/StockMovement.cs";

    private static List<(string Path, string Text)> ProductionSources()
    {
        var root = TestPaths.RepositoryRoot;

        return [.. Directory
            .EnumerateFiles(Path.Combine(root, "src"), "*.cs", SearchOption.AllDirectories)
            .Select(path => (Path: Path.GetRelativePath(root, path).Replace(Path.DirectorySeparatorChar, '/'), Text: File.ReadAllText(path)))
            .Where(file => !file.Path.Contains("/obj/", StringComparison.Ordinal)
                           && !file.Path.Contains("/bin/", StringComparison.Ordinal)
                           && !file.Path.Contains("/Migrations/", StringComparison.Ordinal)
                           && !file.Path.Contains(".Tests/", StringComparison.Ordinal))];
    }

    private static List<string> FilesContaining(string fragment)
    {
        return [.. ProductionSources().Where(file => file.Text.Contains(fragment, StringComparison.Ordinal)).Select(file => file.Path).Order(StringComparer.Ordinal)];
    }

    [Fact]
    public void StockMovement_IsCreatedOnlyByStockLevel()
    {
        Assert.Equal([Aggregate], FilesContaining("StockMovement.Create("));
        Assert.Equal([Entity], FilesContaining("new StockMovement"));
    }

    [Fact]
    public void StockMovementRows_AreNeverAddedThroughTheDbSetOrRemoved()
    {
        Assert.Empty(FilesContaining("StockMovements.Add("));
        Assert.Empty(FilesContaining("StockMovements.AddRange("));
        Assert.Empty(FilesContaining("StockMovements.Update("));
        Assert.Empty(FilesContaining("StockMovements.Remove("));
        Assert.Empty(FilesContaining("StockMovements.RemoveRange("));
        Assert.Empty(FilesContaining("StockMovements.ExecuteUpdate"));
        Assert.Empty(FilesContaining("StockMovements.ExecuteDelete"));
    }

    [Fact]
    public void StockMovement_IsNeverWrittenByRawSql()
    {
        Assert.Empty(FilesContaining("\"StockMovements\""));
    }
}

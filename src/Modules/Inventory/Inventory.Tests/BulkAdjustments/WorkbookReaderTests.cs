using Inventory.Infrastructure.BulkAdjustments;
using Inventory.Tests.Support;
using Xunit;

namespace Inventory.Tests.BulkAdjustments;

public class WorkbookReaderTests
{
    private static readonly Guid FirstProduct = Guid.Parse("0b8e3a52-9d57-4d1e-b6a2-3f0c8d6a1e11");
    private static readonly Guid SecondProduct = Guid.Parse("7f1c2d34-5a6b-4c7d-8e9f-0a1b2c3d4e5f");

    [Fact]
    public void ReadRows_SkipsTheHeaderAndBlankRowsAndKeepsExcelRowNumbers()
    {
        using var workbook = new MemoryStream(Workbooks.Build(
            [FirstProduct, 5],
            ["", ""],
            [SecondProduct, -2]));

        var rows = WorkbookReader.ReadRows(workbook).ToList();

        Assert.Equal([2, 4], rows.Select(r => r.RowNumber));
        Assert.Equal([FirstProduct.ToString(), "5"], rows[0].Cells);
        Assert.Equal([SecondProduct.ToString(), "-2"], rows[1].Cells);
    }

    [Fact]
    public void ReadRows_ReadTwiceFromTheSameStream_LeavesTheStreamOpenForTheSecondPass()
    {
        using var workbook = new MemoryStream(Workbooks.Build([FirstProduct, 5]));

        var counted = WorkbookReader.ReadRows(workbook).Count();
        workbook.Position = 0;
        var rows = WorkbookReader.ReadRows(workbook).ToList();

        Assert.Equal(1, counted);
        Assert.Single(rows);
    }

    [Fact]
    public void ReadRows_FileThatIsNotAWorkbook_Throws()
    {
        using var notAWorkbook = new MemoryStream("plain text"u8.ToArray());

        Assert.ThrowsAny<Exception>(() => WorkbookReader.ReadRows(notAWorkbook).ToList());
    }
}

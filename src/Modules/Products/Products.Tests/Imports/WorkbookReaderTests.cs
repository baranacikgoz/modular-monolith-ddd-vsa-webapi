using Products.Infrastructure.Imports;
using Products.Tests.Support;
using Xunit;

namespace Products.Tests.Imports;

public class WorkbookReaderTests
{
    [Fact]
    public void ReadRows_SkipsTheHeaderAndBlankRowsAndKeepsExcelRowNumbers()
    {
        using var workbook = new MemoryStream(Workbooks.Build(
            ["Desk lamp", "LED", 5, 49.9m],
            ["", "", "", ""],
            ["Chair", "", 2, 120m]));

        var rows = WorkbookReader.ReadRows(workbook).ToList();

        Assert.Equal([2, 4], rows.Select(r => r.RowNumber));
        Assert.Equal(["Desk lamp", "LED", "5", "49.9"], rows[0].Cells);
        Assert.Equal(["Chair", "", "2", "120"], rows[1].Cells);
    }

    [Fact]
    public void ReadRows_ExportedCsvSavedAsWorkbook_KeepsEveryColumnOfEveryRow()
    {
        using var workbook = new MemoryStream(Workbooks.FromCsv(
            "Name,Description,Quantity,Price,Sku,Id,StoreId,CreatedOn\r\n" +
            "\"Lamp, desk\",\"Says \"\"bright\"\"\",5,49.9,LAMP-1,id-1,store-1,2026-01-01\r\n"));

        var rows = WorkbookReader.ReadRows(workbook).ToList();

        var row = Assert.Single(rows);
        Assert.Equal(2, row.RowNumber);
        Assert.Equal(["Lamp, desk", "Says \"bright\"", "5", "49.9", "LAMP-1", "id-1", "store-1", "2026-01-01"], row.Cells);
    }

    [Fact]
    public void ReadRows_ReadTwiceFromTheSameStream_LeavesTheStreamOpenForTheSecondPass()
    {
        using var workbook = new MemoryStream(Workbooks.Build(["Desk lamp", "LED", 5, 49.9m]));

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

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

    // The export puts an apostrophe in front of a cell a spreadsheet would run as a formula; a spreadsheet keeps it as part of the
    // text when the CSV is opened and saved as a workbook, so the reader takes it off again.
    [Theory]
    [InlineData("'=SUM(A1)", "=SUM(A1)")]
    [InlineData("'+1 deal", "+1 deal")]
    [InlineData("'-5 degree sock", "-5 degree sock")]
    [InlineData("'@home", "@home")]
    public void ReadRows_TextCellWithTheFormulaGuardApostrophe_IsReadWithoutIt(string cell, string expected)
    {
        using var workbook = new MemoryStream(Workbooks.FromCsv($"Name,Description\r\n{cell},x\r\n"));

        var row = Assert.Single(WorkbookReader.ReadRows(workbook));

        Assert.Equal(expected, row.Cells[0]);
    }

    [Theory]
    [InlineData("'Tis the season")]
    [InlineData("'")]
    [InlineData("rock 'n' roll")]
    [InlineData("=SUM(A1)")]
    public void ReadRows_TextCellWithoutTheFormulaGuardApostrophe_IsReadAsItIs(string cell)
    {
        using var workbook = new MemoryStream(Workbooks.FromCsv($"Name,Description\r\n\"{cell}\",x\r\n"));

        var row = Assert.Single(WorkbookReader.ReadRows(workbook));

        Assert.Equal(cell, row.Cells[0]);
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

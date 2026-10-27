using ClosedXML.Excel;

namespace Inventory.Tests.Support;

internal static class Workbooks
{
    /// <summary>A one-sheet .xlsx with the bulk adjustment header (product id, quantity) and the given data rows. Strings
    /// become text cells and numbers become numeric cells, the way a spreadsheet user would type them.</summary>
    public static byte[] Build(params object[][] rows)
    {
        using var workbook = new XLWorkbook();
        var sheet = workbook.Worksheets.Add("Adjustments");

        string[] header = ["ProductId", "Quantity"];
        for (var column = 0; column < header.Length; column++)
        {
            sheet.Cell(1, column + 1).Value = header[column];
        }

        for (var row = 0; row < rows.Length; row++)
        {
            for (var column = 0; column < rows[row].Length; column++)
            {
                sheet.Cell(row + 2, column + 1).Value = rows[row][column] switch
                {
                    string text => text,
                    Guid id => id.ToString(),
                    int number => number,
                    var other => throw new NotSupportedException($"Unsupported cell value {other}.")
                };
            }
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }
}

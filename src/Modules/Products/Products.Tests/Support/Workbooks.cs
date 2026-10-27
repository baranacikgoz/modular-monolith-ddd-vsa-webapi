using ClosedXML.Excel;

namespace Products.Tests.Support;

internal static class Workbooks
{
    /// <summary>The columns the import reads, by position, which is also the header a workbook for it carries.</summary>
    public static IReadOnlyList<string> ImportColumns { get; } = ["Name", "Description", "Quantity", "Price", "Sku"];

    /// <summary>A one-sheet .xlsx with the import header (name, description, quantity, price, SKU) and the given data rows; the SKU cell of a row is optional.
    /// Strings become text cells, numbers become numeric cells, the way a spreadsheet user would type them.</summary>
    public static byte[] Build(params object[][] rows)
    {
        using var workbook = new XLWorkbook();
        var sheet = workbook.Worksheets.Add("Products");

        for (var column = 0; column < ImportColumns.Count; column++)
        {
            sheet.Cell(1, column + 1).Value = ImportColumns[column];
        }

        for (var row = 0; row < rows.Length; row++)
        {
            for (var column = 0; column < rows[row].Length; column++)
            {
                sheet.Cell(row + 2, column + 1).Value = rows[row][column] switch
                {
                    string text => text,
                    int number => number,
                    decimal number => number,
                    var other => throw new NotSupportedException($"Unsupported cell value {other}.")
                };
            }
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }
}

using System.Text;
using ClosedXML.Excel;

namespace Products.Tests.Support;

internal static class Workbooks
{
    /// <summary>The columns the import reads, by position (<c>RowParser</c>): what an export's header has to carry for its file to be a valid import input.</summary>
    public static IReadOnlyList<string> ImportColumns { get; } = ["Name", "Description", "Quantity", "Price", "Sku"];

    /// <summary>The .xlsx a spreadsheet user gets by opening an exported CSV and saving it as a workbook: the first sheet holds
    /// the CSV's rows as they are, every field a text cell, the header row included. A field that starts with an apostrophe keeps
    /// it as part of the text, as a CSV opened in a spreadsheet does.</summary>
    public static byte[] FromCsv(string csv)
    {
        using var workbook = new XLWorkbook();
        var sheet = workbook.Worksheets.Add("Products");

        var rows = ParseCsv(csv);
        for (var row = 0; row < rows.Count; row++)
        {
            for (var column = 0; column < rows[row].Count; column++)
            {
                SetText(sheet.Cell(row + 1, column + 1), rows[row][column]);
            }
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

    /// <summary>The cell setter reads a leading apostrophe as a spreadsheet's "keep this as text" marker and drops it, which is not
    /// what a text file brings in: there the apostrophe is a character like any other. A rich text run is stored as it is.</summary>
    private static void SetText(IXLCell cell, string text)
    {
        if (text.StartsWith('\''))
        {
            cell.CreateRichText().AddText(text);
            return;
        }

        cell.Value = text;
    }

    /// <summary>RFC 4180: fields separated by commas, a field with a comma, quote or line break wrapped in quotes with inner quotes doubled.</summary>
    private static List<List<string>> ParseCsv(string csv)
    {
        var rows = new List<List<string>>();
        var row = new List<string>();
        var field = new StringBuilder();
        var quoted = false;
        var position = 0;

        while (position < csv.Length)
        {
            var c = csv[position];
            var next = position + 1 < csv.Length ? csv[position + 1] : '\0';
            var step = 1;

            if (quoted && c == '"' && next == '"')
            {
                field.Append('"');
                step = 2;
            }
            else if (c == '"')
            {
                quoted = !quoted;
            }
            else if (!quoted && c == ',')
            {
                row.Add(field.ToString());
                field.Clear();
            }
            else if (!quoted && c == '\r' && next == '\n')
            {
                row.Add(field.ToString());
                field.Clear();
                rows.Add(row);
                row = [];
                step = 2;
            }
            else
            {
                field.Append(c);
            }

            position += step;
        }

        if (field.Length > 0 || row.Count > 0)
        {
            row.Add(field.ToString());
            rows.Add(row);
        }

        return rows;
    }

    /// <summary>A one-sheet .xlsx with the import header (name, description, quantity, price) and the given data rows; a row may add a fifth cell, the SKU.
    /// Strings become text cells, numbers become numeric cells, the way a spreadsheet user would type them.</summary>
    public static byte[] Build(params object[][] rows)
    {
        using var workbook = new XLWorkbook();
        var sheet = workbook.Worksheets.Add("Products");

        string[] header = ["Name", "Description", "Quantity", "Price"];
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

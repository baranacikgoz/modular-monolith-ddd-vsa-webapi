using System.Globalization;
using System.Text;
using ExcelDataReader;
using Products.Application.Imports;

namespace Products.Infrastructure.Imports;

/// <summary>One non-blank data row of the first worksheet. <paramref name="RowNumber" /> is the Excel row number, so the
/// header is row 1 and the first data row is row 2.</summary>
internal readonly record struct SheetRow(int RowNumber, IReadOnlyList<string> Cells);

/// <summary>
///     Streams the rows of an uploaded workbook (.xlsx or .xls) with ExcelDataReader. Only the first worksheet is read,
///     the header row is skipped and rows without a single non-blank cell are skipped. A workbook that cannot be opened
///     throws; the caller turns that into a failed job.
/// </summary>
internal static class WorkbookReader
{
    static WorkbookReader()
    {
        // Legacy .xls (BIFF) files carry a Windows code page; ExcelDataReader needs this provider registered once per
        // process to decode them.
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    }

    /// <summary>Lazy: the reader is open until the enumeration ends or is disposed, so the workbook stream must outlive it.</summary>
    internal static IEnumerable<SheetRow> ReadRows(Stream workbook)
    {
        // The caller reads the same stream more than once (count, then slice), so the reader must not close it.
        using var reader = ExcelReaderFactory.CreateReader(workbook, new ExcelReaderConfiguration { LeaveOpen = true });

        var rowNumber = 0;
        while (reader.Read())
        {
            rowNumber++;
            if (rowNumber == ProductImportJob.HeaderRowNumber)
            {
                continue;
            }

            var cells = new string[reader.FieldCount];
            for (var i = 0; i < cells.Length; i++)
            {
                cells[i] = FormatCell(reader.GetValue(i));
            }

            if (cells.All(string.IsNullOrWhiteSpace))
            {
                continue;
            }

            yield return new SheetRow(rowNumber, cells);
        }
    }

    private static string FormatCell(object? value)
    {
        return value switch
        {
            null => string.Empty,
            double d => d.ToString(CultureInfo.InvariantCulture),
            decimal m => m.ToString(CultureInfo.InvariantCulture),
            DateTime dt => dt.ToString("O", CultureInfo.InvariantCulture),
            _ => value.ToString() ?? string.Empty
        };
    }
}

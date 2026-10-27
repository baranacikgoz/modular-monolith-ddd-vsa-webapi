using System.Globalization;
using System.Text;
using Products.Infrastructure.Spreadsheets;

namespace Products.Infrastructure.Exports;

/// <summary>Minimal RFC 4180 CSV writer: a handful of lines of string building, so no dependency. Appends one row per call
/// into a single <see cref="StringBuilder" />.</summary>
internal static class CsvWriter
{
    private static readonly char[] CharactersThatNeedQuoting = [',', '"', '\r', '\n'];

    internal static void AppendRow(StringBuilder builder, IReadOnlyList<string?> fields)
    {
        builder.AppendJoin(',', fields.Select(Escape));
        builder.Append("\r\n");
    }

    internal static string FormatDate(DateTimeOffset value)
    {
        return value.ToString("O", CultureInfo.InvariantCulture);
    }

    private static string Escape(string? field)
    {
        // A cell a spreadsheet would run as a formula gets a leading apostrophe, the usual CSV injection mitigation: the cell is
        // read as text and never evaluated. Names and descriptions are typed by users, and the file is opened in a spreadsheet by
        // someone else. The import takes the apostrophe off again.
        field = FormulaGuard.Apply(field ?? string.Empty);

        return field.IndexOfAny(CharactersThatNeedQuoting) < 0
            ? field
            : $"\"{field.Replace("\"", "\"\"", StringComparison.Ordinal)}\"";
    }
}

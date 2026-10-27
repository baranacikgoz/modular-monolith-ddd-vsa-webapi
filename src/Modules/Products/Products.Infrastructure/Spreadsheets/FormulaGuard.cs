using System.Buffers;

namespace Products.Infrastructure.Spreadsheets;

/// <summary>
///     The export puts an apostrophe in front of a cell a spreadsheet would run as a formula (spreadsheet formula injection), and
///     the import takes it off again, so a text such as <c>-5 degree sock</c> survives the round trip. Both directions live here
///     so they cannot drift apart.
///     <para>
///         A text is a formula when it starts with <c>=</c>, <c>+</c>, <c>-</c>, <c>@</c>, a tab or a carriage return. A text
///         that already starts with apostrophes followed by one of those characters (<c>'=x</c>) is guarded too, because
///         <see cref="Strip" /> removes exactly one apostrophe from such a text and would otherwise eat its own. Every other
///         text, including one that merely starts with an apostrophe (<c>'Tis</c>), passes both ways unchanged.
///     </para>
/// </summary>
internal static class FormulaGuard
{
    private const char Apostrophe = '\'';

    private static readonly SearchValues<char> FormulaStarts = SearchValues.Create("=+-@\t\r");

    /// <summary>The text as it is written to the file.</summary>
    internal static string Apply(string text)
    {
        return StartsLikeAFormula(text) ? Apostrophe + text : text;
    }

    /// <summary>The text as it was before <see cref="Apply" />.</summary>
    internal static string Strip(string text)
    {
        return text.Length > 0 && text[0] == Apostrophe && StartsLikeAFormula(text) ? text[1..] : text;
    }

    /// <summary>True when the text, past any leading apostrophes, starts with a formula character.</summary>
    private static bool StartsLikeAFormula(ReadOnlySpan<char> text)
    {
        var rest = text.TrimStart(Apostrophe);
        return rest.Length > 0 && FormulaStarts.Contains(rest[0]);
    }
}

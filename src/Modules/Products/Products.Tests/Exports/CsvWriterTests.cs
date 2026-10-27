using System.Text;
using Products.Infrastructure.Exports;
using Xunit;

namespace Products.Tests.Exports;

public class CsvWriterTests
{
    private static string Write(params string?[] fields)
    {
        var builder = new StringBuilder();
        CsvWriter.AppendRow(builder, fields);
        return builder.ToString();
    }

    [Fact]
    public void AppendRow_PlainFields_AreJoinedWithCommasAndEndWithCrLf()
    {
        Assert.Equal("a,b,c\r\n", Write("a", "b", "c"));
    }

    [Fact]
    public void AppendRow_NullField_IsAnEmptyCell()
    {
        Assert.Equal("a,,c\r\n", Write("a", null, "c"));
    }

    [Theory]
    [InlineData("one, two", "\"one, two\"")]
    [InlineData("say \"hi\"", "\"say \"\"hi\"\"\"")]
    [InlineData("line1\nline2", "\"line1\nline2\"")]
    public void AppendRow_FieldWithSpecialCharacters_IsQuotedAndEscaped(string field, string expected)
    {
        Assert.Equal(expected + "\r\n", Write(field));
    }

    [Theory]
    [InlineData("=SUM(A1:A9)", "'=SUM(A1:A9)")]
    [InlineData("+1", "'+1")]
    [InlineData("-1", "'-1")]
    [InlineData("@cmd", "'@cmd")]
    public void AppendRow_FieldThatLooksLikeAFormula_GetsALeadingApostrophe(string field, string expected)
    {
        Assert.Equal(expected + "\r\n", Write(field));
    }

    // The import takes one apostrophe off a cell that starts with apostrophes and then a formula character, so a name that already
    // starts that way needs one more to survive.
    [Theory]
    [InlineData("'=x", "''=x")]
    [InlineData("''-5", "'''-5")]
    public void AppendRow_FieldWithApostrophesBeforeAFormulaCharacter_GetsOneMore(string field, string expected)
    {
        Assert.Equal(expected + "\r\n", Write(field));
    }

    [Theory]
    [InlineData("'Tis the season")]
    [InlineData("rock 'n' roll")]
    [InlineData("'")]
    public void AppendRow_FieldWithAnApostropheThatIsNotBeforeAFormulaCharacter_IsUnchanged(string field)
    {
        Assert.Equal(field + "\r\n", Write(field));
    }

    [Fact]
    public void AppendRow_FormulaFieldThatAlsoNeedsQuoting_IsPrefixedThenQuoted()
    {
        Assert.Equal("\"'=A1,B1\"\r\n", Write("=A1,B1"));
    }
}

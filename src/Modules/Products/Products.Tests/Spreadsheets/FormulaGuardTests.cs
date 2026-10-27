using Products.Infrastructure.Spreadsheets;
using Xunit;

namespace Products.Tests.Spreadsheets;

public class FormulaGuardTests
{
    public static TheoryData<string> Texts => new()
    {
        "",
        "Lamp",
        "-5 degree sock",
        "+1 free",
        "=SUM(A1:A9)",
        "@home",
        "\tindented",
        "\rreturn",
        "'",
        "''",
        "'Tis the season",
        "rock 'n' roll",
        "'=odd",
        "''-5",
        "'''@x",
        "5 - 3",
        "a=b"
    };

    [Theory]
    [MemberData(nameof(Texts))]
    public void Strip_AfterApply_GivesTheTextBack(string text)
    {
        Assert.Equal(text, FormulaGuard.Strip(FormulaGuard.Apply(text)));
    }

    [Theory]
    [InlineData("-5 degree sock", "'-5 degree sock")]
    [InlineData("'=odd", "''=odd")]
    [InlineData("''-5", "'''-5")]
    public void Apply_TextThatStartsLikeAFormula_GetsOneLeadingApostrophe(string text, string expected)
    {
        Assert.Equal(expected, FormulaGuard.Apply(text));
    }

    [Theory]
    [InlineData("Lamp")]
    [InlineData("'Tis the season")]
    [InlineData("'")]
    [InlineData("5 - 3")]
    [InlineData("a=b")]
    public void Apply_AnyOtherText_IsUnchanged(string text)
    {
        Assert.Equal(text, FormulaGuard.Apply(text));
    }

    [Theory]
    [InlineData("'-5 degree sock", "-5 degree sock")]
    [InlineData("''=odd", "'=odd")]
    public void Strip_TextWithTheGuardApostrophe_LosesExactlyOneApostrophe(string text, string expected)
    {
        Assert.Equal(expected, FormulaGuard.Strip(text));
    }

    [Theory]
    [InlineData("-5 degree sock")]
    [InlineData("=SUM(A1)")]
    [InlineData("'Tis the season")]
    [InlineData("'")]
    [InlineData("")]
    public void Strip_AnyOtherText_IsUnchanged(string text)
    {
        Assert.Equal(text, FormulaGuard.Strip(text));
    }
}

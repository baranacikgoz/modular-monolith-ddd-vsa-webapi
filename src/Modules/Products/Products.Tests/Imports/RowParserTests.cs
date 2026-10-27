using Products.Application.Imports;
using Products.Domain.Products;
using Products.Infrastructure.Imports;
using Xunit;

namespace Products.Tests.Imports;

public class RowParserTests
{
    private static SheetRow Row(params string[] cells) => new(2, cells);

    [Fact]
    public void Parse_ValidRow_ReturnsProductRow()
    {
        var result = RowParser.Parse(Row(" Desk lamp ", "LED, dimmable", "12", "49.90"));

        Assert.False(result.IsFailure);
        Assert.Equal(new ProductRow(2, "Desk lamp", "LED, dimmable", 12, 49.90m), result.Value);
    }

    [Fact]
    public void Parse_MissingDescriptionColumn_IsAllowed()
    {
        var result = RowParser.Parse(Row("Desk lamp", "", "1", "10"));

        Assert.False(result.IsFailure);
        Assert.Equal(string.Empty, result.Value!.Description);
    }

    [Fact]
    public void Parse_BlankName_RejectsWithMissingName()
    {
        var result = RowParser.Parse(Row("  ", "d", "1", "10"));

        Assert.Equal(ProductImportRowErrors.MissingName.Key, result.Error!.Key);
    }

    [Fact]
    public void Parse_NameOverTheLimit_RejectsWithNameTooLong()
    {
        var result = RowParser.Parse(Row(new string('n', Constants.NameMaxLength + 1), "d", "1", "10"));

        Assert.Equal(ProductImportRowErrors.NameTooLong.Key, result.Error!.Key);
    }

    [Fact]
    public void Parse_DescriptionOverTheLimit_RejectsWithDescriptionTooLong()
    {
        var result = RowParser.Parse(Row("n", new string('d', Constants.DescriptionMaxLength + 1), "1", "10"));

        Assert.Equal(ProductImportRowErrors.DescriptionTooLong.Key, result.Error!.Key);
    }

    [Theory]
    [InlineData("")]
    [InlineData("abc")]
    [InlineData("0")]
    [InlineData("2.5")]
    public void Parse_UnusableQuantity_RejectsWithInvalidQuantity(string quantity)
    {
        var result = RowParser.Parse(Row("n", "d", quantity, "10"));

        Assert.Equal(ProductImportRowErrors.InvalidQuantity.Key, result.Error!.Key);
    }

    [Theory]
    [InlineData("")]
    [InlineData("free")]
    [InlineData("0.5")]
    [InlineData("12,5")]
    public void Parse_UnusablePrice_RejectsWithInvalidPrice(string price)
    {
        var result = RowParser.Parse(Row("n", "d", "1", price));

        Assert.Equal(ProductImportRowErrors.InvalidPrice.Key, result.Error!.Key);
    }

    [Fact]
    public void Parse_RowShorterThanTheColumns_RejectsInsteadOfThrowing()
    {
        var result = RowParser.Parse(Row("only a name"));

        Assert.Equal(ProductImportRowErrors.InvalidQuantity.Key, result.Error!.Key);
    }
}

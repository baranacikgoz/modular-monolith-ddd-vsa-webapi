using Inventory.Application.BulkAdjustments;
using Inventory.Domain.StockLevels;
using Inventory.Infrastructure.BulkAdjustments;
using Xunit;

namespace Inventory.Tests.BulkAdjustments;

public class RowParserTests
{
    private const string ProductId = "0b8e3a52-9d57-4d1e-b6a2-3f0c8d6a1e11";

    private static SheetRow Row(params string[] cells) => new(2, cells);

    [Theory]
    [InlineData("12", 12)]
    [InlineData("-3", -3)]
    [InlineData("+7", 7)]
    [InlineData(" 5 ", 5)]
    public void Parse_ValidRow_ReturnsTheProductAndTheSignedQuantity(string quantity, int expected)
    {
        var result = RowParser.Parse(Row($" {ProductId} ", quantity));

        Assert.False(result.IsFailure);
        Assert.Equal(new AdjustmentRow(2, Guid.Parse(ProductId), expected), result.Value);
    }

    [Fact]
    public void Parse_BlankProductId_RejectsWithMissingProductId()
    {
        var result = RowParser.Parse(Row("  ", "1"));

        Assert.Equal(StockBulkAdjustRowErrors.MissingProductId.Key, result.Error!.Key);
    }

    [Theory]
    [InlineData("not-a-guid")]
    [InlineData("12345")]
    [InlineData("00000000-0000-0000-0000-000000000000")]
    public void Parse_ProductIdThatIsNotAnId_RejectsWithInvalidProductId(string productId)
    {
        var result = RowParser.Parse(Row(productId, "1"));

        Assert.Equal(StockBulkAdjustRowErrors.InvalidProductId.Key, result.Error!.Key);
    }

    [Theory]
    [InlineData("")]
    [InlineData("abc")]
    [InlineData("0")]
    [InlineData("2.5")]
    [InlineData("1,000")]
    public void Parse_UnusableQuantity_RejectsWithInvalidQuantity(string quantity)
    {
        var result = RowParser.Parse(Row(ProductId, quantity));

        Assert.Equal(StockBulkAdjustRowErrors.InvalidQuantity.Key, result.Error!.Key);
    }

    [Theory]
    [InlineData(Constants.QuantityMaxValue + 1)]
    [InlineData(-Constants.QuantityMaxValue - 1)]
    public void Parse_QuantityBeyondTheLimit_RejectsWithInvalidQuantity(int quantity)
    {
        var result = RowParser.Parse(Row(ProductId, quantity.ToString(System.Globalization.CultureInfo.InvariantCulture)));

        Assert.Equal(StockBulkAdjustRowErrors.InvalidQuantity.Key, result.Error!.Key);
    }

    [Fact]
    public void Parse_QuantityAtTheLimit_IsAccepted()
    {
        var result = RowParser.Parse(Row(ProductId, Constants.QuantityMaxValue.ToString(System.Globalization.CultureInfo.InvariantCulture)));

        Assert.Equal(Constants.QuantityMaxValue, result.Value!.Quantity);
    }

    [Fact]
    public void Parse_RowShorterThanTheColumns_RejectsInsteadOfThrowing()
    {
        var result = RowParser.Parse(Row(ProductId));

        Assert.Equal(StockBulkAdjustRowErrors.InvalidQuantity.Key, result.Error!.Key);
    }
}

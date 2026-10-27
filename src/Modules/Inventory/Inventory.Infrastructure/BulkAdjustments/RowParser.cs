using System.Globalization;
using Common.Domain.ResultMonad;
using Inventory.Application.BulkAdjustments;
using Inventory.Domain.StockLevels;

namespace Inventory.Infrastructure.BulkAdjustments;

internal sealed record AdjustmentRow(int RowNumber, DefaultIdType ProductId, int Quantity);

/// <summary>
///     Turns one worksheet row into an <see cref="AdjustmentRow" /> or the reason it is rejected; never throws, so one bad
///     row cannot stop the rest of the file. Columns by position: A product id, B signed quantity (plus adds stock, minus
///     removes it). Numbers are read with the invariant culture.
/// </summary>
internal static class RowParser
{
    private const int ProductIdColumn = 0;
    private const int QuantityColumn = 1;

    internal static Result<AdjustmentRow> Parse(SheetRow row)
    {
        var productIdText = Cell(row, ProductIdColumn);
        if (productIdText.Length == 0)
        {
            return StockBulkAdjustRowErrors.MissingProductId;
        }

        if (!DefaultIdType.TryParse(productIdText, CultureInfo.InvariantCulture, out var productId) || productId == DefaultIdType.Empty)
        {
            return StockBulkAdjustRowErrors.InvalidProductId;
        }

        // No thousands separator and no decimals: "2.5" and "1,000" are rejected, not rounded.
        if (!int.TryParse(Cell(row, QuantityColumn), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out var quantity)
            || quantity == 0
            || Math.Abs((long)quantity) > Constants.QuantityMaxValue)
        {
            return StockBulkAdjustRowErrors.InvalidQuantity;
        }

        return new AdjustmentRow(row.RowNumber, productId, quantity);
    }

    private static string Cell(SheetRow row, int index)
    {
        return index < row.Cells.Count ? row.Cells[index].Trim() : string.Empty;
    }
}

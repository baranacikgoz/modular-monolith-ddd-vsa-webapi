using System.Globalization;
using Common.Domain.ResultMonad;
using Products.Application.Imports;
using Products.Domain.Products;

namespace Products.Infrastructure.Imports;

internal sealed record ProductRow(int RowNumber, string Name, string Description, int Quantity, decimal Price);

/// <summary>
///     Turns one worksheet row into a <see cref="ProductRow" /> or the reason it is rejected; never throws, so one bad
///     row cannot stop the rest of the file. Columns by position: A name, B description (optional), C quantity,
///     D price. Numbers are read with the invariant culture.
/// </summary>
internal static class RowParser
{
    private const int NameColumn = 0;
    private const int DescriptionColumn = 1;
    private const int QuantityColumn = 2;
    private const int PriceColumn = 3;

    internal static Result<ProductRow> Parse(SheetRow row)
    {
        var name = Cell(row, NameColumn);
        var description = Cell(row, DescriptionColumn);

        if (name.Length == 0)
        {
            return ProductImportRowErrors.MissingName;
        }

        if (name.Length > Constants.NameMaxLength)
        {
            return ProductImportRowErrors.NameTooLong;
        }

        if (description.Length > Constants.DescriptionMaxLength)
        {
            return ProductImportRowErrors.DescriptionTooLong;
        }

        if (!int.TryParse(Cell(row, QuantityColumn), NumberStyles.Integer, CultureInfo.InvariantCulture, out var quantity)
            || quantity < Constants.QuantityGreaterThanOrEqualTo)
        {
            return ProductImportRowErrors.InvalidQuantity;
        }

        // No thousands separator: "12,5" must be rejected, not read as 125.
        if (!decimal.TryParse(Cell(row, PriceColumn), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var price)
            || price < Constants.PriceGreaterThanOrEqualTo)
        {
            return ProductImportRowErrors.InvalidPrice;
        }

        return new ProductRow(row.RowNumber, name, description, quantity, price);
    }

    private static string Cell(SheetRow row, int index)
    {
        return index < row.Cells.Count ? row.Cells[index].Trim() : string.Empty;
    }
}

namespace Inventory.Domain.StockLevels;

public static class Constants
{
    /// <summary>Free-text explanation on a ledger row (a delivery note, the reason of a manual correction).</summary>
    public const int ReasonMaxLength = 256;

    /// <summary>Longest product name search term the stock level list sends to Products.</summary>
    public const int ProductNameSearchMaxLength = 256;

    /// <summary>Upper bound on one movement's quantity and on a low stock threshold, far above any real stock.</summary>
    public const int QuantityMaxValue = 1_000_000;
}

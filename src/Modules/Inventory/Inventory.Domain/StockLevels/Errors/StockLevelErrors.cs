using Common.Domain.ResultMonad;

namespace Inventory.Domain.StockLevels.Errors;

public static class StockLevelErrors
{
    public static readonly Error QuantityMustBePositive = new() { Key = nameof(QuantityMustBePositive) };
    public static readonly Error AdjustmentCannotBeZero = new() { Key = nameof(AdjustmentCannotBeZero) };
    public static readonly Error ReasonRequired = new() { Key = nameof(ReasonRequired) };

    /// <summary>The movement would take the on-hand quantity below zero.</summary>
    public static readonly Error InsufficientStock = new() { Key = nameof(InsufficientStock) };

    public static readonly Error LowStockThresholdCannotBeNegative = new() { Key = nameof(LowStockThresholdCannotBeNegative) };
}

namespace Inventory.Domain.StockReservations;

public static class Constants
{
    /// <summary>Warehouse system's reference for a committed reservation (webhook and Commit endpoint input).</summary>
    public const int ProviderReferenceMaxLength = 256;

    /// <summary>Domain-generated <c>rel:{id}:{ticks}</c> marker: 60 characters.</summary>
    public const int ReleaseAttemptReferenceMaxLength = 64;
}

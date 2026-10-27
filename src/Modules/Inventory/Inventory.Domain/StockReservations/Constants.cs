namespace Inventory.Domain.StockReservations;

public static class Constants
{
    /// <summary>Warehouse system's reference for a committed reservation (webhook and Commit endpoint input).</summary>
    public const int ProviderReferenceMaxLength = 256;

    /// <summary>Domain-generated <c>rel:{id}:{ticks}</c> marker: 60 characters.</summary>
    public const int ReleaseAttemptReferenceMaxLength = 64;

    /// <summary>Longest raw provider status kept on the audit event of an unrecognized one; a longer word is cut, never refused.</summary>
    public const int ProviderStatusMaxLength = 64;
}

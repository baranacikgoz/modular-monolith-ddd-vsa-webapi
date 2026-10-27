namespace Inventory.Domain.WarehouseConnections;

public static class Constants
{
    public const int BaseUrlMaxLength = 512;

    /// <summary>Longest API credential accepted in clear text, before it is encrypted.</summary>
    public const int ApiCredentialMaxLength = 1024;
}

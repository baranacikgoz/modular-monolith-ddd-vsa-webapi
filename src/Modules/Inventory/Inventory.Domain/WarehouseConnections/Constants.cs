namespace Inventory.Domain.WarehouseConnections;

public static class Constants
{
    public const int BaseUrlMaxLength = 512;

    /// <summary>Longest API credential accepted in clear text, before it is encrypted.</summary>
    public const int ApiCredentialMaxLength = 1024;

    /// <summary>
    ///     The base address stored for a <see cref="WarehouseProvider.Dummy" /> connection, which has none: a reserved
    ///     <c>.invalid</c> name, so nothing can ever be reached through it.
    /// </summary>
#pragma warning disable S1075 // A fixed placeholder that can never be reached, not an address to configure.
    public const string DummyBaseUrl = "https://dummy.invalid/";
#pragma warning restore S1075

    /// <summary>The credential stored (encrypted like any other) for a <see cref="WarehouseProvider.Dummy" /> connection, which has none.</summary>
    public const string DummyApiCredential = "dummy";
}

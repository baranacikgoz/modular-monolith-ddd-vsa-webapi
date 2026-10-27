using System.Security.Cryptography;
using Common.Application.Options;
using Common.Infrastructure.Security;
using Inventory.Application.Persistence;
using Inventory.Domain.WarehouseConnections;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Inventory.Tests.Support;

/// <summary>
///     Three encryption keys for a key rotation: the one that was current before (<see cref="OldKey" />), the current one, and
///     one this system does not know (<see cref="StrangerKey" />), plus the protectors of each. <see cref="Rotating" /> is the
///     protector of the system after the rotation: it seals under the current key and still opens values of the old one.
/// </summary>
internal static class WarehouseSecretKeys
{
    public static readonly string OldKey = NewKey();
    public const string CurrentKey = SecretProtectionOptionsValidator.DevelopmentPlaceholderKey;
    public static readonly string StrangerKey = NewKey();

    /// <summary>Seals under the old key, like the system did before the rotation.</summary>
    public static readonly SecretProtector Before = Protector(OldKey);

    /// <summary>Seals under the current key and opens the old key's values: the system after the rotation.</summary>
    public static readonly SecretProtector Rotating = Protector(CurrentKey, OldKey);

    /// <summary>Seals under a key that is not configured anywhere, so nothing can open its values.</summary>
    public static readonly SecretProtector Stranger = Protector(StrangerKey);

    public static string NewKey()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(SecretProtectionOptionsValidator.KeySizeBytes));
    }

    public static SecretProtector Protector(string current, params string[] previous)
    {
        return new SecretProtector(Options.Create(new SecretProtectionOptions { EncryptionKey = current, PreviousEncryptionKeys = previous }));
    }

    /// <summary>Saves an active connection holding exactly these stored values, whatever key sealed them.</summary>
    public static async Task<WarehouseConnection> SeedConnectionAsync(
        IServiceProvider services, byte[] apiCredential, byte[] webhookSecret, bool deactivated = false)
    {
        await using var scope = services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<IInventoryDbContext>();

        var connection = WarehouseConnection.Create(
            DefaultIdType.CreateVersion7(), WarehouseProvider.JsonApi, WarehouseConnectionTestData.BaseUrl, apiCredential, webhookSecret);
        if (deactivated)
        {
            connection.Deactivate();
        }

        db.WarehouseConnections.Add(connection);
        await db.SaveChangesAsync();

        return connection;
    }
}

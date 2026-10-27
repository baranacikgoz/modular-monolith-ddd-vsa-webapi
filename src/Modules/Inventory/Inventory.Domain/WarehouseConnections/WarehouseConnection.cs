using Common.Domain.Aggregates;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;
using Inventory.Domain.WarehouseConnections.DomainEvents.v1;
using Inventory.Domain.WarehouseConnections.Errors;

namespace Inventory.Domain.WarehouseConnections;

public readonly record struct WarehouseConnectionId(DefaultIdType Value) : IStronglyTypedId
{
    public static WarehouseConnectionId New()
    {
        return new WarehouseConnectionId(DefaultIdType.CreateVersion7());
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static bool TryParse(string str, out WarehouseConnectionId id)
    {
        return StronglyTypedIdHelper.TryDeserialize(str, out id);
    }
}

/// <summary>
///     How one store reaches its warehouse system: which provider, where, and the secrets of both directions. The
///     API credential (ours to the warehouse) and the webhook secret (the warehouse's to us) arrive here already encrypted
///     and are never read back in clear by the aggregate or returned by any endpoint. A store has at most one active
///     connection; a deactivated one stays for history.
/// </summary>
public class WarehouseConnection : AggregateRoot<WarehouseConnectionId>
{
#pragma warning disable CS8618
    private WarehouseConnection() : base(new WarehouseConnectionId(DefaultIdType.Empty)) { }
#pragma warning restore CS8618

    public DefaultIdType StoreId { get; private set; }
    public WarehouseProvider Provider { get; private set; }
    public Uri BaseUrl { get; private set; }
    public WarehouseConnectionStatus Status { get; private set; }

#pragma warning disable CA1819 // EF-mapped bytea columns; a ReadOnlyMemory<byte> wrapper buys nothing here.
    /// <summary>nonce, tag and ciphertext of the API credential, as produced by <c>SecretProtector</c>. Never projected to a response.</summary>
    public byte[] EncryptedApiCredential { get; private set; } = [];

    /// <summary>nonce, tag and ciphertext of the secret the warehouse signs its webhooks with. Never projected to a response.</summary>
    public byte[] EncryptedWebhookSecret { get; private set; } = [];
#pragma warning restore CA1819

    public static WarehouseConnection Create(
        DefaultIdType storeId,
        WarehouseProvider provider,
        Uri baseUrl,
        byte[] encryptedApiCredential,
        byte[] encryptedWebhookSecret)
    {
        var id = WarehouseConnectionId.New();

        var @event = new V1WarehouseConnectionCreatedDomainEvent(id, storeId, provider.ToCreatedSnapshot(), baseUrl);

        var connection = new WarehouseConnection
        {
            Id = id,
            StoreId = storeId,
            Provider = provider,
            BaseUrl = baseUrl,
            Status = WarehouseConnectionStatus.Active,
            EncryptedApiCredential = encryptedApiCredential,
            EncryptedWebhookSecret = encryptedWebhookSecret
        };

        connection.RaiseEvent(@event);
        return connection;
    }

    public Result ReplaceCredentials(byte[] encryptedApiCredential)
    {
        if (Status == WarehouseConnectionStatus.Deactivated)
        {
            return WarehouseConnectionErrors.CannotChangeWhileDeactivated;
        }

        var @event = new V1WarehouseConnectionCredentialsReplacedDomainEvent(Id);

        EncryptedApiCredential = encryptedApiCredential;

        RaiseEvent(@event);
        return Result.Success;
    }

    /// <summary>The previous secret stops verifying the moment this is saved.</summary>
    public Result RotateWebhookSecret(byte[] encryptedWebhookSecret)
    {
        if (Status == WarehouseConnectionStatus.Deactivated)
        {
            return WarehouseConnectionErrors.CannotChangeWhileDeactivated;
        }

        var @event = new V1WarehouseConnectionWebhookSecretRotatedDomainEvent(Id);

        EncryptedWebhookSecret = encryptedWebhookSecret;

        RaiseEvent(@event);
        return Result.Success;
    }

    /// <summary>Idempotent: an already deactivated connection changes nothing and raises nothing.</summary>
    public Result Deactivate()
    {
        if (Status == WarehouseConnectionStatus.Deactivated)
        {
            return Result.Success;
        }

        var @event = new V1WarehouseConnectionDeactivatedDomainEvent(Id);

        Status = WarehouseConnectionStatus.Deactivated;

        RaiseEvent(@event);
        return Result.Success;
    }
}

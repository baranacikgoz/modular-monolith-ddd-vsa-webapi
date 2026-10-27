using Inventory.Domain.WarehouseConnections;
using Inventory.Domain.WarehouseConnections.DomainEvents.v1;
using Inventory.Domain.WarehouseConnections.Errors;
using Xunit;

namespace Inventory.Tests.WarehouseConnections;

public class WarehouseConnectionTests
{
    private static readonly Uri BaseUrl = new("https://warehouse.example-partner.local/api/");
    private static readonly byte[] Credential = [1, 2, 3];
    private static readonly byte[] WebhookSecret = [4, 5, 6];

    private static WarehouseConnection NewConnection(WarehouseProvider provider = WarehouseProvider.JsonApi)
    {
        var connection = WarehouseConnection.Create(DefaultIdType.CreateVersion7(), provider, BaseUrl, Credential, WebhookSecret);
        connection.ClearEvents();
        return connection;
    }

    [Fact]
    public void Create_StartsActiveWithTheGivenValues()
    {
        var storeId = DefaultIdType.CreateVersion7();

        var connection = WarehouseConnection.Create(storeId, WarehouseProvider.FormApi, BaseUrl, Credential, WebhookSecret);

        Assert.Equal(storeId, connection.StoreId);
        Assert.Equal(WarehouseProvider.FormApi, connection.Provider);
        Assert.Equal(BaseUrl, connection.BaseUrl);
        Assert.Equal(WarehouseConnectionStatus.Active, connection.Status);
        Assert.Equal(Credential, connection.EncryptedApiCredential);
        Assert.Equal(WebhookSecret, connection.EncryptedWebhookSecret);
    }

    [Fact]
    public void Create_RaisesOneCreatedEventWithoutAnySecret()
    {
        var storeId = DefaultIdType.CreateVersion7();

        var connection = WarehouseConnection.Create(storeId, WarehouseProvider.FormApi, BaseUrl, Credential, WebhookSecret);

        var @event = Assert.Single(connection.Events.OfType<V1WarehouseConnectionCreatedDomainEvent>());
        Assert.Equal(connection.Id, @event.WarehouseConnectionId);
        Assert.Equal(storeId, @event.StoreId);
        Assert.Equal(V1WarehouseConnectionCreatedDomainEvent.ProviderSnapshot.FormApi, @event.Provider);
        Assert.Equal(BaseUrl, @event.BaseUrl);
        Assert.Single(connection.Events);
    }

    [Fact]
    public void ReplaceCredentials_Active_SwapsOnlyTheCredentialAndRaisesTheEvent()
    {
        var connection = NewConnection();
        byte[] replacement = [9, 9, 9];

        var result = connection.ReplaceCredentials(replacement);

        Assert.False(result.IsFailure);
        Assert.Equal(replacement, connection.EncryptedApiCredential);
        Assert.Equal(WebhookSecret, connection.EncryptedWebhookSecret);
        var @event = Assert.Single(connection.Events.OfType<V1WarehouseConnectionCredentialsReplacedDomainEvent>());
        Assert.Equal(connection.Id, @event.WarehouseConnectionId);
    }

    [Fact]
    public void ReplaceCredentials_Deactivated_ReturnsAnErrorAndChangesNothing()
    {
        var connection = NewConnection();
        connection.Deactivate();
        connection.ClearEvents();

        var result = connection.ReplaceCredentials([9, 9, 9]);

        Assert.Equal(WarehouseConnectionErrors.CannotChangeWhileDeactivated.Key, result.Error!.Key);
        Assert.Equal(Credential, connection.EncryptedApiCredential);
        Assert.Empty(connection.Events);
    }

    [Fact]
    public void RotateWebhookSecret_Active_SwapsOnlyTheSecretAndRaisesTheEvent()
    {
        var connection = NewConnection();
        byte[] replacement = [7, 7, 7];

        var result = connection.RotateWebhookSecret(replacement);

        Assert.False(result.IsFailure);
        Assert.Equal(replacement, connection.EncryptedWebhookSecret);
        Assert.Equal(Credential, connection.EncryptedApiCredential);
        var @event = Assert.Single(connection.Events.OfType<V1WarehouseConnectionWebhookSecretRotatedDomainEvent>());
        Assert.Equal(connection.Id, @event.WarehouseConnectionId);
    }

    [Fact]
    public void RotateWebhookSecret_Deactivated_ReturnsAnErrorAndChangesNothing()
    {
        var connection = NewConnection();
        connection.Deactivate();
        connection.ClearEvents();

        var result = connection.RotateWebhookSecret([7, 7, 7]);

        Assert.Equal(WarehouseConnectionErrors.CannotChangeWhileDeactivated.Key, result.Error!.Key);
        Assert.Equal(WebhookSecret, connection.EncryptedWebhookSecret);
        Assert.Empty(connection.Events);
    }

    [Fact]
    public void ReprotectSecrets_BothValues_ReplacesThemAndRaisesOneEventSayingWhichWereRewritten()
    {
        var connection = NewConnection();
        byte[] apiCredential = [8, 8, 8];
        byte[] webhookSecret = [6, 6, 6];

        var result = connection.ReprotectSecrets(apiCredential, webhookSecret);

        Assert.False(result.IsFailure);
        Assert.Equal(apiCredential, connection.EncryptedApiCredential);
        Assert.Equal(webhookSecret, connection.EncryptedWebhookSecret);
        var @event = Assert.Single(connection.Events);
        var reprotected = Assert.IsType<V1WarehouseConnectionSecretsReprotectedDomainEvent>(@event);
        Assert.Equal(connection.Id, reprotected.WarehouseConnectionId);
        Assert.True(reprotected.ApiCredential);
        Assert.True(reprotected.WebhookSecret);
    }

    [Fact]
    public void ReprotectSecrets_OneValue_LeavesTheOtherAsItIs()
    {
        var connection = NewConnection();
        byte[] webhookSecret = [6, 6, 6];

        connection.ReprotectSecrets(null, webhookSecret);

        Assert.Equal(Credential, connection.EncryptedApiCredential);
        Assert.Equal(webhookSecret, connection.EncryptedWebhookSecret);
        var reprotected = Assert.Single(connection.Events.OfType<V1WarehouseConnectionSecretsReprotectedDomainEvent>());
        Assert.False(reprotected.ApiCredential);
        Assert.True(reprotected.WebhookSecret);
    }

    [Fact]
    public void ReprotectSecrets_NothingToRewrite_IsANoOpWithoutAnEvent()
    {
        var connection = NewConnection();

        var result = connection.ReprotectSecrets(null, null);

        Assert.False(result.IsFailure);
        Assert.Equal(Credential, connection.EncryptedApiCredential);
        Assert.Equal(WebhookSecret, connection.EncryptedWebhookSecret);
        Assert.Empty(connection.Events);
    }

    // An old key can only be dropped once no stored value is under it, a deactivated connection's included.
    [Fact]
    public void ReprotectSecrets_Deactivated_StillReplacesTheValues()
    {
        var connection = NewConnection();
        connection.Deactivate();
        connection.ClearEvents();
        byte[] apiCredential = [8, 8, 8];

        var result = connection.ReprotectSecrets(apiCredential, null);

        Assert.False(result.IsFailure);
        Assert.Equal(apiCredential, connection.EncryptedApiCredential);
        Assert.Single(connection.Events.OfType<V1WarehouseConnectionSecretsReprotectedDomainEvent>());
    }

    [Fact]
    public void Deactivate_Active_BecomesDeactivatedAndRaisesTheEvent()
    {
        var connection = NewConnection();

        var result = connection.Deactivate();

        Assert.False(result.IsFailure);
        Assert.Equal(WarehouseConnectionStatus.Deactivated, connection.Status);
        var @event = Assert.Single(connection.Events.OfType<V1WarehouseConnectionDeactivatedDomainEvent>());
        Assert.Equal(connection.Id, @event.WarehouseConnectionId);
    }

    [Fact]
    public void Deactivate_AlreadyDeactivated_IsANoOpWithoutAnEvent()
    {
        var connection = NewConnection();
        connection.Deactivate();
        connection.ClearEvents();

        var result = connection.Deactivate();

        Assert.False(result.IsFailure);
        Assert.Equal(WarehouseConnectionStatus.Deactivated, connection.Status);
        Assert.Empty(connection.Events);
    }
}

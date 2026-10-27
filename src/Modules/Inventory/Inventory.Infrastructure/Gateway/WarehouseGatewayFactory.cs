using Common.Domain.ResultMonad;
using Common.Infrastructure.Security;
using Inventory.Application.Gateway;
using Inventory.Domain.WarehouseConnections;
using Inventory.Domain.WarehouseConnections.Errors;

namespace Inventory.Infrastructure.Gateway;

/// <summary>
///     Picks the gateway implementation by the connection's provider. Registered as a typed client (see <see cref="Setup" />)
///     so every gateway it builds shares one pooled <see cref="HttpClient" />; what separates connections is the pipeline
///     and the address, not the client.
/// </summary>
internal sealed class WarehouseGatewayFactory(
    HttpClient httpClient, SecretProtector protector, WarehouseResiliencePipelines pipelines) : IWarehouseGatewayFactory
{
    public Result<IWarehouseGateway> Create(WarehouseConnection connection)
    {
        ArgumentNullException.ThrowIfNull(connection);

        if (connection.Status != WarehouseConnectionStatus.Active)
        {
            return WarehouseConnectionErrors.NotActive;
        }

        var apiCredential = protector.Decrypt(connection.EncryptedApiCredential);
        var pipeline = pipelines.For(connection);

        IWarehouseGateway gateway = connection.Provider switch
        {
            WarehouseProvider.JsonApi => new JsonApiWarehouseGateway(httpClient, connection.BaseUrl, apiCredential, pipeline),
            WarehouseProvider.FormApi => new FormApiWarehouseGateway(httpClient, connection.BaseUrl, apiCredential, pipeline),
            _ => throw new ArgumentOutOfRangeException(nameof(connection), connection.Provider, "Unknown WarehouseProvider.")
        };

        return Result<IWarehouseGateway>.Success(gateway);
    }
}

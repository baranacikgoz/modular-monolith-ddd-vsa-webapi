using System.Security.Cryptography;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Security;
using Inventory.Application.Gateway;
using Inventory.Domain.WarehouseConnections;
using Inventory.Domain.WarehouseConnections.Errors;
using Microsoft.Extensions.Hosting;

namespace Inventory.Infrastructure.Gateway;

/// <summary>
///     Picks the gateway implementation by the connection's provider. Registered as a typed client (see <see cref="Setup" />)
///     so every gateway it builds shares one pooled <see cref="HttpClient" />; what separates connections is the pipeline
///     and the address, not the client. The in-memory <see cref="WarehouseProvider.Dummy" /> provider uses neither the client,
///     a pipeline nor a credential, and is never built in Production
///     (<see cref="WarehouseConnectionErrors.DummyProviderNotAllowedInProduction" />).
/// </summary>
internal sealed class WarehouseGatewayFactory(
    HttpClient httpClient,
    SecretProtector protector,
    WarehouseResiliencePipelines pipelines,
    DummyWarehouseState dummyState,
    IHostEnvironment environment)
    : IWarehouseGatewayFactory
{
    public Result<IWarehouseGateway> Create(WarehouseConnection connection)
    {
        ArgumentNullException.ThrowIfNull(connection);

        if (connection.Status != WarehouseConnectionStatus.Active)
        {
            return WarehouseConnectionErrors.NotActive;
        }

        if (connection.Provider == WarehouseProvider.Dummy && environment.IsProduction())
        {
            return WarehouseConnectionErrors.DummyProviderNotAllowedInProduction;
        }

        return connection.Provider == WarehouseProvider.Dummy
            ? Result<IWarehouseGateway>.Success(new DummyWarehouseGateway(connection.Id, dummyState))
            : CreateHttpGateway(connection);
    }

    private Result<IWarehouseGateway> CreateHttpGateway(WarehouseConnection connection)
    {
        string apiCredential;
        try
        {
            apiCredential = protector.Decrypt(connection.EncryptedApiCredential);
        }
        catch (CryptographicException)
        {
            return WarehouseConnectionErrors.CredentialUnreadable;
        }

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

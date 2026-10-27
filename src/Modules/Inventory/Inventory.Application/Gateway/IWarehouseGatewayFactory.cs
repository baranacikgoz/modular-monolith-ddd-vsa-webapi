using Common.Domain.ResultMonad;
using Inventory.Domain.WarehouseConnections;

namespace Inventory.Application.Gateway;

public interface IWarehouseGatewayFactory
{
    /// <summary>
    ///     The gateway of <paramref name="connection" />'s provider, bound to the connection's base address, its decrypted API
    ///     credential and its own resilience pipeline. The credential is decrypted here and goes no further than the gateway.
    ///     A deactivated connection yields <c>WarehouseConnectionErrors.NotActive</c>.
    /// </summary>
    Result<IWarehouseGateway> Create(WarehouseConnection connection);
}

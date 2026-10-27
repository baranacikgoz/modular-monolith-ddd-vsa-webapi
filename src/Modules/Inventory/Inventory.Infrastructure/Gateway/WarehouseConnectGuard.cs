using System.Net;
using System.Net.Sockets;
using Common.Application.Options;
using Inventory.Domain.WarehouseConnections;
using Microsoft.Extensions.Options;

namespace Inventory.Infrastructure.Gateway;

/// <summary>Resolves a host name to its addresses. A seam so the guard is testable without a network.</summary>
internal interface IWarehouseHostResolver
{
    Task<IPAddress[]> ResolveAsync(string host, CancellationToken cancellationToken);
}

internal sealed class SystemWarehouseHostResolver : IWarehouseHostResolver
{
    public Task<IPAddress[]> ResolveAsync(string host, CancellationToken cancellationToken)
    {
        return Dns.GetHostAddressesAsync(host, cancellationToken);
    }
}

/// <summary>
///     The connect callback of the warehouse HTTP client. The address of a connection is caller supplied, so the create
///     request's check (<see cref="WarehouseAddressPolicy" />) is not enough alone: a host name can point at an internal
///     address by the time it is called, or change its answer between two lookups. This resolves the host itself, refuses the
///     connection when any resolved address is non public, and connects to one of the addresses it just checked, so the
///     lookup that was checked is the one that is used. Combined with the handler's disabled redirects and disabled proxy
///     (see <see cref="Setup" />), nothing the warehouse host answers can lead the server to an internal address.
///     <see cref="InventoryOptions.AllowPrivateWarehouseAddresses" /> lifts the refusal for a developer's local stub.
/// </summary>
internal sealed class WarehouseConnectGuard(IWarehouseHostResolver resolver, IOptions<InventoryOptions> options)
{
    public async ValueTask<Stream> ConnectAsync(SocketsHttpConnectionContext context, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(context);

        var endpoint = context.DnsEndPoint;
        var addresses = await ResolveAllowedAsync(endpoint.Host, cancellationToken);

        SocketException? lastFailure = null;
        foreach (var address in addresses)
        {
            // Owned by the returned stream on success and disposed on every failure path below.
#pragma warning disable CA2000
            var socket = new Socket(address.AddressFamily, SocketType.Stream, ProtocolType.Tcp) { NoDelay = true };
#pragma warning restore CA2000
            try
            {
                await socket.ConnectAsync(new IPEndPoint(address, endpoint.Port), cancellationToken);
                return new NetworkStream(socket, ownsSocket: true);
            }
            catch (SocketException ex)
            {
                socket.Dispose();
                lastFailure = ex;
            }
            catch
            {
                socket.Dispose();
                throw;
            }
        }

        throw lastFailure ?? new SocketException((int)SocketError.HostNotFound);
    }

    /// <summary>
    ///     The addresses of <paramref name="host" /> (the host itself when it is an address literal), or
    ///     <see cref="WarehouseAddressRefusedException" /> when any of them is not public. One non public answer refuses the
    ///     whole host: a name that mixes public and internal records is not trusted.
    /// </summary>
    internal async Task<IReadOnlyList<IPAddress>> ResolveAllowedAsync(string host, CancellationToken cancellationToken)
    {
        var addresses = IPAddress.TryParse(host, out var literal)
            ? [literal]
            : await resolver.ResolveAsync(host, cancellationToken);

        if (addresses.Length == 0)
        {
            throw new SocketException((int)SocketError.HostNotFound);
        }

        if (!options.Value.AllowPrivateWarehouseAddresses && addresses.Any(address => !WarehouseAddressPolicy.IsPublicAddress(address)))
        {
            throw WarehouseAddressRefusedException.ForHost(host);
        }

        return addresses;
    }
}

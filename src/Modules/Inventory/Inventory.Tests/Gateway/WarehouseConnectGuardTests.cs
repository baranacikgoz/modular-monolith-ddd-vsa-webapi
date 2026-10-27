using System.Net;
using Common.Application.Options;
using Inventory.Application.Gateway;
using Inventory.Domain.StockReservations;
using Inventory.Domain.WarehouseConnections;
using Inventory.Infrastructure.Gateway;
using Inventory.Tests.Support;
using Microsoft.Extensions.Options;
using Xunit;
using GatewaySetup = Inventory.Infrastructure.Gateway.Setup;

namespace Inventory.Tests.Gateway;

/// <summary>
///     The outbound half of the address policy: whatever the create request accepted, a host that resolves to a non public
///     address is never connected to, and a redirect never leads the server anywhere else. A resolver stands in for DNS; the
///     sockets are real, on the loopback interface.
/// </summary>
public sealed class WarehouseConnectGuardTests : IDisposable
{
    private readonly FakeResolver _resolver = new();
    private readonly List<IDisposable> _disposables = [];

    public void Dispose()
    {
        foreach (var disposable in _disposables)
        {
            disposable.Dispose();
        }

        GC.SuppressFinalize(this);
    }

    private WarehouseConnectGuard GuardOf(bool allowPrivateAddresses = false)
    {
        var options = Options.Create(TestInventoryOptions.Create(configure: o => o.AllowPrivateWarehouseAddresses = allowPrivateAddresses));
        return new WarehouseConnectGuard(_resolver, options);
    }

    private HttpClient ClientOf(WarehouseConnectGuard guard)
    {
        // The client owns the handler and is disposed with the test.
#pragma warning disable CA2000
        var client = new HttpClient(GatewaySetup.CreateHandler(guard, TimeSpan.FromMinutes(1)), disposeHandler: true)
        {
            Timeout = TimeSpan.FromSeconds(20)
        };
#pragma warning restore CA2000
        _disposables.Add(client);
        return client;
    }

    private static JsonApiWarehouseGateway GatewayOf(HttpClient client, Uri baseUrl)
    {
        var connection = TestWarehouseResilience.Connection(WarehouseProvider.JsonApi, baseUrl: baseUrl);
        return new JsonApiWarehouseGateway(client, baseUrl, "credential", TestWarehouseResilience.Pipelines().For(connection));
    }

    private LoopbackHttpServer StartServer(string statusLine = "200 OK", string? location = null)
    {
        var server = new LoopbackHttpServer(statusLine, location);
        _disposables.Add(server);
        return server;
    }

    [Theory]
    [InlineData("10.1.2.3")]
    [InlineData("127.0.0.1")]
    [InlineData("169.254.169.254")]
    [InlineData("192.168.0.9")]
    [InlineData("::1")]
    [InlineData("fd00::5")]
    [InlineData("::ffff:10.0.0.1")]
    public async Task ResolveAllowedAsync_HostResolvingToANonPublicAddress_IsRefused(string resolved)
    {
        _resolver.Answer("warehouse.example.com", IPAddress.Parse(resolved));

        await Assert.ThrowsAsync<WarehouseAddressRefusedException>(
            () => GuardOf().ResolveAllowedAsync("warehouse.example.com", CancellationToken.None));
    }

    // A name that mixes a public and an internal record is not trusted: any internal answer refuses the host.
    [Fact]
    public async Task ResolveAllowedAsync_HostResolvingToPublicAndInternalAddresses_IsRefused()
    {
        _resolver.Answer("warehouse.example.com", IPAddress.Parse("93.184.216.34"), IPAddress.Parse("10.0.0.7"));

        await Assert.ThrowsAsync<WarehouseAddressRefusedException>(
            () => GuardOf().ResolveAllowedAsync("warehouse.example.com", CancellationToken.None));
    }

    [Fact]
    public async Task ResolveAllowedAsync_HostResolvingToPublicAddresses_ReturnsThemForTheConnectToUse()
    {
        _resolver.Answer("warehouse.example.com", IPAddress.Parse("93.184.216.34"), IPAddress.Parse("2606:2800:220:1::1"));

        var addresses = await GuardOf().ResolveAllowedAsync("warehouse.example.com", CancellationToken.None);

        Assert.Equal(2, addresses.Count);
    }

    // The create request cannot see through a literal that the resolver would return as is.
    [Fact]
    public async Task ResolveAllowedAsync_NonPublicAddressLiteral_IsRefusedWithoutAskingTheResolver()
    {
        await Assert.ThrowsAsync<WarehouseAddressRefusedException>(
            () => GuardOf().ResolveAllowedAsync("169.254.169.254", CancellationToken.None));

        Assert.Equal(0, _resolver.Lookups);
    }

    [Fact]
    public async Task ResolveAllowedAsync_AllowPrivateAddresses_AcceptsAnInternalAddress()
    {
        _resolver.Answer("stub.internal.test", IPAddress.Parse("10.0.0.7"));

        var addresses = await GuardOf(allowPrivateAddresses: true).ResolveAllowedAsync("stub.internal.test", CancellationToken.None);

        Assert.Equal(IPAddress.Parse("10.0.0.7"), Assert.Single(addresses));
    }

    [Fact]
    public async Task Send_HostResolvingToALoopbackAddress_NeverConnectsAndTheGatewayAnswersAddressNotAllowed()
    {
        var server = StartServer();
        _resolver.Answer("warehouse.example.com", IPAddress.Loopback);
        var gateway = GatewayOf(ClientOf(GuardOf()), new Uri($"http://warehouse.example.com:{server.Port}/api/"));

        var result = await gateway.RequestReleaseAsync(StockReservationId.New(), "product-1", 1, CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal("WarehouseGatewayAddressNotAllowed", result.Error!.Key);
        Assert.Equal(HttpStatusCode.BadGateway, result.Error.StatusCode);
        Assert.Equal(0, server.Connections);
        Assert.Equal(1, _resolver.Lookups);
    }

    // The switch is for a developer's stub on the local machine: the same host is then connected to, at the address that was checked.
    [Fact]
    public async Task Send_AllowPrivateAddresses_ConnectsToTheResolvedAddress()
    {
        var server = StartServer();
        _resolver.Answer("stub.internal.test", IPAddress.Loopback);
        var gateway = GatewayOf(ClientOf(GuardOf(allowPrivateAddresses: true)), new Uri($"http://stub.internal.test:{server.Port}/api/"));

        var result = await gateway.RequestReleaseAsync(StockReservationId.New(), "product-1", 1, CancellationToken.None);

        Assert.Equal("WarehouseGatewayMalformedResponse", result.Error!.Key); // the stub answered 200 with no reference
        Assert.Equal(1, server.Connections);
        Assert.Equal(1, _resolver.Lookups);
    }

    // The allowed server answers a redirect to an internal address: the handler does not follow it, so the second
    // server never sees a connection and the caller gets the 302 as a failed call.
    [Fact]
    public async Task Send_RedirectToAnInternalAddress_IsNotFollowed()
    {
        var internalServer = StartServer();
        var warehouse = StartServer("302 Found", $"http://internal.test:{internalServer.Port}/secret");
        _resolver.Answer("stub.internal.test", IPAddress.Loopback);
        _resolver.Answer("internal.test", IPAddress.Loopback);
        var gateway = GatewayOf(ClientOf(GuardOf(allowPrivateAddresses: true)), new Uri($"http://stub.internal.test:{warehouse.Port}/api/"));

        var result = await gateway.RequestReleaseAsync(StockReservationId.New(), "product-1", 1, CancellationToken.None);

        Assert.Equal("WarehouseGatewayCallFailed", result.Error!.Key);
        Assert.Equal(302, result.Error.Value);
        Assert.Equal(1, warehouse.Connections);
        Assert.Equal(0, internalServer.Connections);
    }

    [Fact]
    public void CreateHandler_NeverFollowsRedirectsAndNeverUsesAProxy()
    {
        using var handler = GatewaySetup.CreateHandler(GuardOf(), TimeSpan.FromMinutes(1));

        Assert.False(handler.AllowAutoRedirect);
        Assert.False(handler.UseProxy);
        Assert.NotNull(handler.ConnectCallback);
    }

    private sealed class FakeResolver : IWarehouseHostResolver
    {
        private readonly Dictionary<string, IPAddress[]> _answers = new(StringComparer.OrdinalIgnoreCase);
        private int _lookups;

        public int Lookups => Volatile.Read(ref _lookups);

        public void Answer(string host, params IPAddress[] addresses)
        {
            _answers[host] = addresses;
        }

        public Task<IPAddress[]> ResolveAsync(string host, CancellationToken cancellationToken)
        {
            Interlocked.Increment(ref _lookups);
            return Task.FromResult(_answers.TryGetValue(host, out var addresses) ? addresses : []);
        }
    }
}

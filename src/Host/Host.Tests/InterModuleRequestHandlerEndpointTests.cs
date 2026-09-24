using System.Text.Json;
using Common.Application.Options;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Host.Tests;

// Introspects the running bus (MassTransit's probe) instead of provoking a fault: proves that every
// InterModuleRequestHandler receive endpoint carries the InterModuleRequestHandlerDefinition policy
// (no retry, handler timeout, own concurrency) and that integration event consumers do not.
[Collection("Host")]
public class InterModuleRequestHandlerEndpointTests(HostTestFactory factory)
{
    private const string RequestHandlerEndpoint = "GetProductRequestHandler";
    private const string EventConsumerEndpoint = "CreateStockLevelOnProductCreatedHandler";

    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public void RequestHandlerEndpoint_HasNoRetryHandlerTimeoutAndOwnConcurrency()
    {
        Assert.NotNull(_client);
        var options = factory.Services.GetRequiredService<IOptions<InterModuleRequestOptions>>().Value;

        using var probe = Probe();
        var endpoint = FindReceiveEndpoint(probe, RequestHandlerEndpoint);

        var retry = FindFilter(endpoint, "retry");
        Assert.NotNull(retry);
        Assert.Equal("None", retry.Value.GetProperty("retry-consumeContext").GetProperty("policy").GetString());

        var timeout = FindFilter(endpoint, "timeout");
        Assert.NotNull(timeout);
        Assert.Equal(TimeSpan.FromSeconds(options.TimeoutSeconds), TimeSpan.Parse(timeout.Value.GetProperty("timeout").GetString()!, System.Globalization.CultureInfo.InvariantCulture));

        var transport = endpoint.GetProperty("receiveTransport");
        Assert.Equal(options.HandlerPrefetchCount, transport.GetProperty("prefetchCount").GetInt32());
        Assert.Equal(options.HandlerConcurrentMessageLimit, transport.GetProperty("concurrentMessageLimit").GetInt32());
    }

    [Fact]
    public async Task RequestHandlerEndpoint_PerRequestTimeoutOverride_IsTheHandlerTimeout()
    {
        // Own host: the override must reach the handler's receive endpoint, not only the caller's request client,
        // or a handler would be cancelled at TimeoutSeconds while its caller still waits for the longer override.
        await using var overridden = new HostTestFactory()
            .WithModules("IAM,Products")
            .WithInterModuleRequestTimeout("GetProductRequest", 3);
        await overridden.InitializeAsync();

        using var probe = Probe(overridden.Services);
        var timeout = FindFilter(FindReceiveEndpoint(probe, RequestHandlerEndpoint), "timeout");

        Assert.NotNull(timeout);
        Assert.Equal(TimeSpan.FromSeconds(3), TimeSpan.Parse(timeout.Value.GetProperty("timeout").GetString()!, System.Globalization.CultureInfo.InvariantCulture));
    }

    [Fact]
    public void EventConsumerEndpoint_DoesNotCarryRequestHandlerPolicy()
    {
        Assert.NotNull(_client);

        using var probe = Probe();
        var endpoint = FindReceiveEndpoint(probe, EventConsumerEndpoint);

        Assert.Null(FindFilter(endpoint, "timeout"));
        Assert.Null(FindFilter(endpoint, "retry"));
    }

    private JsonDocument Probe() => Probe(factory.Services);

    private static JsonDocument Probe(IServiceProvider services)
    {
        var bus = services.GetRequiredService<IBus>();
        return JsonDocument.Parse(JsonSerializer.Serialize(bus.GetProbeResult().Results));
    }

    private static JsonElement FindReceiveEndpoint(JsonDocument probe, string name)
    {
        var endpoints = probe.RootElement.GetProperty("bus").GetProperty("host").GetProperty("receiveEndpoint");
        var names = new List<string>();
        foreach (var endpoint in endpoints.EnumerateArray())
        {
            var endpointName = endpoint.GetProperty("name").GetString();
            names.Add(endpointName ?? "<null>");
            if (endpointName == name)
            {
                return endpoint;
            }
        }

        throw new Xunit.Sdk.XunitException($"Receive endpoint '{name}' not found. Endpoints: {string.Join(", ", names)}");
    }

    // Depth-first search for an object whose "filterType" equals the wanted filter, anywhere under the endpoint.
    private static JsonElement? FindFilter(JsonElement node, string filterType)
    {
        switch (node.ValueKind)
        {
            case JsonValueKind.Object:
                if (node.TryGetProperty("filterType", out var type) && type.GetString() == filterType)
                {
                    return node;
                }

                foreach (var property in node.EnumerateObject())
                {
                    if (FindFilter(property.Value, filterType) is { } found)
                    {
                        return found;
                    }
                }

                return null;
            case JsonValueKind.Array:
                foreach (var item in node.EnumerateArray())
                {
                    if (FindFilter(item, filterType) is { } found)
                    {
                        return found;
                    }
                }

                return null;
            default:
                return null;
        }
    }
}

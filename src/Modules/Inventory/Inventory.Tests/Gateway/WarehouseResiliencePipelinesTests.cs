using System.Net;
using Common.Application.Options;
using Common.Infrastructure.Resiliency;
using Inventory.Domain.WarehouseConnections;
using Inventory.Infrastructure.Gateway;
using Inventory.Tests.Support;
using Microsoft.Extensions.Options;
using Xunit;

namespace Inventory.Tests.Gateway;

/// <summary>
///     One pipeline per connection, built from the profile of the connection's provider: one store's burst or outage
///     must not spend the quota or open the circuit of another store behind the same HTTP client.
/// </summary>
public class WarehouseResiliencePipelinesTests : WarehouseTransportTestBase
{

    private JsonApiWarehouseGateway GatewayOf(WarehouseResiliencePipelines pipelines, WarehouseConnection connection)
    {
        return new JsonApiWarehouseGateway(
            Client, new Uri("https://warehouse.example-partner.local/api/"), "credential", pipelines.For(connection));
    }

    [Fact]
    public void For_KeysByProviderProfileAndConnection_SameConnectionSamePipeline_OtherConnectionOwnPipeline()
    {
        var pipelines = TestWarehouseResilience.Pipelines();
        var first = TestWarehouseResilience.Connection(WarehouseProvider.JsonApi);
        var second = TestWarehouseResilience.Connection(WarehouseProvider.JsonApi);

        Assert.Equal($"WarehouseJsonApi:{first.Id}", WarehouseResiliencePipelines.KeyOf(first));
        Assert.Same(pipelines.For(first), pipelines.For(first));
        Assert.NotSame(pipelines.For(first), pipelines.For(second));
    }

    [Fact]
    public void For_ProviderWithoutAProfile_FailsLoudly()
    {
        var options = Options.Create(new ResiliencyOptions
        {
            PooledConnectionLifetimeMinutes = 15,
            TotalRequestTimeoutSeconds = 30,
            MaxRetryAttempts = 3,
            RetryDelaySeconds = 1,
            CircuitBreakerSamplingDurationSeconds = 30,
            CircuitBreakerFailureRatio = 0.1,
            CircuitBreakerMinimumThroughput = 10,
            CircuitBreakerBreakDurationSeconds = 15,
            AttemptTimeoutSeconds = 10,
            Keyed = []
        });
#pragma warning disable CA2000
        var withoutProfiles = new WarehouseResiliencePipelines(new KeyedResiliencePipelines(options), options);
#pragma warning restore CA2000

        var failure = Assert.Throws<InvalidOperationException>(
            () => withoutProfiles.For(TestWarehouseResilience.Connection(WarehouseProvider.FormApi)));

        Assert.Contains("WarehouseFormApi", failure.Message, StringComparison.Ordinal);
    }

    [Fact]
    public async Task Quota_IsPerConnection_ExhaustedConnectionIsThrottledWithoutASend_OtherConnectionStillSends()
    {
        var pipelines = TestWarehouseResilience.Pipelines(jsonApiPermits: 2);
        var exhausted = TestWarehouseResilience.Connection();
        var other = TestWarehouseResilience.Connection();
        Handler.Responder = _ => FakeWarehouseHandler.Json(HttpStatusCode.OK, new { confirmationReference = "ref" });

        for (var i = 0; i < 2; i++)
        {
            var ok = await GatewayOf(pipelines, exhausted).RequestReleaseAsync("product-1", 1, CancellationToken.None);
            Assert.False(ok.IsFailure);
        }

        var throttled = await GatewayOf(pipelines, exhausted).RequestReleaseAsync("product-1", 1, CancellationToken.None);

        Assert.Equal("WarehouseGatewayThrottled", throttled.Error!.Key);
        Assert.Equal(HttpStatusCode.TooManyRequests, throttled.Error.StatusCode);
        Assert.Equal(2, Handler.Requests.Count);

        var unaffected = await GatewayOf(pipelines, other).RequestReleaseAsync("product-1", 1, CancellationToken.None);

        Assert.False(unaffected.IsFailure);
        Assert.Equal(3, Handler.Requests.Count);
    }

    [Fact]
    public async Task Quota_ComesFromTheProvidersOwnProfile()
    {
        var pipelines = TestWarehouseResilience.Pipelines(jsonApiPermits: 1, formApiPermits: 3);
        var json = TestWarehouseResilience.Connection(WarehouseProvider.JsonApi);
        var form = TestWarehouseResilience.Connection(WarehouseProvider.FormApi);
        Handler.Responder = _ => FakeWarehouseHandler.Json(HttpStatusCode.OK, new { confirmationReference = "ref" });

        await GatewayOf(pipelines, json).RequestReleaseAsync("product-1", 1, CancellationToken.None);
        var jsonSecond = await GatewayOf(pipelines, json).RequestReleaseAsync("product-1", 1, CancellationToken.None);
        await GatewayOf(pipelines, form).RequestReleaseAsync("product-1", 1, CancellationToken.None);
        var formSecond = await GatewayOf(pipelines, form).RequestReleaseAsync("product-1", 1, CancellationToken.None);

        Assert.Equal("WarehouseGatewayThrottled", jsonSecond.Error!.Key);
        Assert.False(formSecond.IsFailure);
    }

    [Fact]
    public async Task Circuit_OpensPerConnectionAfterRepeatedFailures_AndThenRejectsWithoutASend()
    {
        var pipelines = TestWarehouseResilience.Pipelines(breakerMinimumThroughput: 2);
        var failing = TestWarehouseResilience.Connection();
        var healthy = TestWarehouseResilience.Connection();
        Handler.Responder = _ => new HttpResponseMessage(HttpStatusCode.InternalServerError);

        for (var i = 0; i < 2; i++)
        {
            var failed = await GatewayOf(pipelines, failing).RequestReleaseAsync("product-1", 1, CancellationToken.None);
            Assert.Equal("WarehouseGatewayCallFailed", failed.Error!.Key);
        }

        var rejected = await GatewayOf(pipelines, failing).RequestReleaseAsync("product-1", 1, CancellationToken.None);

        Assert.Equal("WarehouseGatewayCircuitOpen", rejected.Error!.Key);
        Assert.Equal(HttpStatusCode.ServiceUnavailable, rejected.Error.StatusCode);
        Assert.Equal(2, Handler.Requests.Count);

        Handler.Responder = _ => FakeWarehouseHandler.Json(HttpStatusCode.OK, new { confirmationReference = "ref" });
        var unaffected = await GatewayOf(pipelines, healthy).RequestReleaseAsync("product-1", 1, CancellationToken.None);

        Assert.False(unaffected.IsFailure);
        Assert.Equal(3, Handler.Requests.Count);
    }
}

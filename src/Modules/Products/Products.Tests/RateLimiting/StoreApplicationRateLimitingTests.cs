using System.Net;
using Common.Application.Options;
using Common.Tests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Products.Infrastructure.RateLimiting;
using Xunit;

namespace Products.Tests.RateLimiting;

// The caller is anonymous, so the limit is per client IP. A named policy only limits the routes that opt in, hence
// the metadata check next to the partition checks.
[Collection("IntegrationTestCollection")]
public class StoreApplicationRateLimitingTests : BaseIntegrationTest
{
    public StoreApplicationRateLimitingTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    private Policies.StoreApplicationRateLimitingPolicy Policy()
    {
        return new Policies.StoreApplicationRateLimitingPolicy(
            Scope.ServiceProvider.GetRequiredService<IOptions<CustomRateLimitingOptions>>());
    }

    private DefaultHttpContext ContextFrom(string ip)
    {
        return new DefaultHttpContext
        {
            RequestServices = Scope.ServiceProvider,
            Connection = { RemoteIpAddress = IPAddress.Parse(ip) }
        };
    }

    [Fact]
    public void GetPartition_CallersOnTheSameIp_ShareOnePartition()
    {
        var first = Policy().GetPartition(ContextFrom("1.2.3.4"));
        var second = Policy().GetPartition(ContextFrom("1.2.3.4"));

        Assert.Equal("1.2.3.4", first.PartitionKey);
        Assert.Equal(first.PartitionKey, second.PartitionKey);
    }

    [Fact]
    public void GetPartition_CallersOnDifferentIps_GetSeparatePartitions()
    {
        var first = Policy().GetPartition(ContextFrom("1.2.3.4"));
        var second = Policy().GetPartition(ContextFrom("5.6.7.8"));

        Assert.NotEqual(first.PartitionKey, second.PartitionKey);
    }

    [Fact]
    public void Apply_Endpoint_IsAnonymousAndLimitedByTheStoreApplicationPolicy()
    {
        var endpoint = Factory.Services.GetRequiredService<EndpointDataSource>().Endpoints
            .OfType<RouteEndpoint>()
            .Single(e => e.RoutePattern.RawText is { } text
                         && text.EndsWith("/stores/applications", StringComparison.Ordinal)
                         && e.Metadata.GetMetadata<HttpMethodMetadata>()!.HttpMethods.Contains("POST"));

        var policy = endpoint.Metadata.GetMetadata<EnableRateLimitingAttribute>();

        Assert.NotNull(policy);
        Assert.Equal(RateLimitingConstants.StoreApplication, policy.PolicyName);
        Assert.NotNull(endpoint.Metadata.GetMetadata<IAllowAnonymous>());
    }
}

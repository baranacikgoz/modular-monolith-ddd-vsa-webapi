using Common.Tests;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Products.Infrastructure.RateLimiting;
using Xunit;

namespace Products.Tests.RateLimiting;

// The CreateStore policy is defined and unit-tested on its own, but a named policy only limits the routes that
// opt in. This guards the opt-in: self-service store creation must stay behind it.
[Collection("IntegrationTestCollection")]
public class MyCreateRateLimitingTests : BaseIntegrationTest
{
    public MyCreateRateLimitingTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public void MyCreate_Endpoint_IsLimitedByTheCreateStorePolicy()
    {
        var endpoint = Factory.Services.GetRequiredService<EndpointDataSource>().Endpoints
            .OfType<RouteEndpoint>()
            .Single(e => e.RoutePattern.RawText is { } text
                         && text.EndsWith("/stores/my", StringComparison.Ordinal)
                         && e.Metadata.GetMetadata<HttpMethodMetadata>()!.HttpMethods.Contains("POST"));

        var policy = endpoint.Metadata.GetMetadata<EnableRateLimitingAttribute>();

        Assert.NotNull(policy);
        Assert.Equal(RateLimitingConstants.CreateStore, policy.PolicyName);
    }
}

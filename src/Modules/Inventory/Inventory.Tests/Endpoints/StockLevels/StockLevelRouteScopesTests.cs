using Common.Application.Auth;
using Common.Tests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Inventory.Tests.Endpoints.StockLevels;

/// <summary>
///     The slice tests let every authenticated caller through every scope, so they cannot see which scope a route asks for. This
///     pins it: the routes that read any store's data ask for the administration scope, and only the <c>/my</c> routes, which
///     filter by the caller's own stores, ask for the scope every authenticated user holds.
/// </summary>
[Collection("IntegrationTestCollection")]
public class StockLevelRouteScopesTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    public static TheoryData<string, string, string> Routes => new()
    {
        { "GET", "", KeycloakScopes.StockLevels.ViewAll },
        { "GET", "/{id}/movements", KeycloakScopes.StockLevels.ViewAll },
        { "GET", "/daily-totals", KeycloakScopes.StockLevels.ViewAll },
        { "GET", "/my", KeycloakScopes.StockLevels.ViewOwn },
        { "GET", "/my/{id}/movements", KeycloakScopes.StockLevels.ViewOwn },
        { "GET", "/my/daily-totals", KeycloakScopes.StockLevels.ViewOwn }
    };

    [Theory]
    [MemberData(nameof(Routes))]
    public void StockLevelReadRoute_RequiresItsScope(string method, string route, string scope)
    {
        var endpoints = Factory.Services.GetRequiredService<EndpointDataSource>().Endpoints.OfType<RouteEndpoint>();
        var pattern = $"/v{{version:apiVersion}}/stock-levels{route}";

        var endpoint = Assert.Single(endpoints, e =>
            e.RoutePattern.RawText?.TrimEnd('/') == pattern
            && e.Metadata.GetMetadata<IHttpMethodMetadata>()!.HttpMethods.Contains(method, StringComparer.Ordinal));

        var policies = endpoint.Metadata.GetOrderedMetadata<IAuthorizeData>().Select(a => a.Policy).ToList();
        Assert.Contains(KeycloakPermission.FromScope(scope).PolicyName(), policies);
    }
}

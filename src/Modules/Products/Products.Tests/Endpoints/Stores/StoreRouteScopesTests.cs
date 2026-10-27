using Common.Application.Auth;
using Common.Tests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Products.Tests.Endpoints.Stores;

/// <summary>
///     The slice tests let every authenticated caller through every scope, so they cannot see which scope a route asks for. This
///     pins it: the listing that shows a store in any status asks for the administration scope, while the public search and the
///     single store ask for the scopes every authenticated user holds.
/// </summary>
[Collection("IntegrationTestCollection")]
public class StoreRouteScopesTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    public static TheoryData<string, string, string> Routes => new()
    {
        { "GET", "/administration", KeycloakScopes.Stores.ViewAll },
        { "GET", "/applications", KeycloakScopes.Stores.Approve },
        { "GET", "/search", KeycloakScopes.Stores.Search },
        { "GET", "/{id}", KeycloakScopes.Stores.View },
        { "GET", "/my", KeycloakScopes.Stores.ViewOwn }
    };

    [Theory]
    [MemberData(nameof(Routes))]
    public void StoreReadRoute_RequiresItsScope(string method, string route, string scope)
    {
        var endpoints = Factory.Services.GetRequiredService<EndpointDataSource>().Endpoints.OfType<RouteEndpoint>();
        var pattern = $"/v{{version:apiVersion}}/stores{route}";

        var endpoint = Assert.Single(endpoints, e =>
            e.RoutePattern.RawText?.TrimEnd('/') == pattern
            && e.Metadata.GetMetadata<IHttpMethodMetadata>()!.HttpMethods.Contains(method, StringComparer.Ordinal));

        var policies = endpoint.Metadata.GetOrderedMetadata<IAuthorizeData>().Select(a => a.Policy).ToList();
        Assert.Contains(KeycloakPermission.FromScope(scope).PolicyName(), policies);
    }
}

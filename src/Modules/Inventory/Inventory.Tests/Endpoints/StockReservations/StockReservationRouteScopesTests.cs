using Common.Application.Auth;
using Common.Tests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Inventory.Tests.Endpoints.StockReservations;

/// <summary>
///     The slice tests let every authenticated caller through every scope, so they cannot see which scope a route asks for.
///     This pins it for every stock reservation route: reading any reservation, and every command, asks for an administration
///     scope; only the <c>/my</c> routes, which filter by the caller's own stores, ask for the scope every authenticated user
///     holds; the warehouse webhook is anonymous and authenticated by its signature instead.
/// </summary>
[Collection("IntegrationTestCollection")]
public class StockReservationRouteScopesTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private const string Prefix = "/v{version:apiVersion}/stock-reservations";

    private static readonly (string Method, string Route, string Scope)[] Listed =
    [
        ("POST", "", KeycloakScopes.StockReservations.Create),
        ("POST", "/series", KeycloakScopes.StockReservations.Create),
        ("GET", "/{id}", KeycloakScopes.StockReservations.ViewAll),
        ("POST", "/{id}/commit", KeycloakScopes.StockReservations.Update),
        ("POST", "/{id}/release", KeycloakScopes.StockReservations.Update),
        ("POST", "/{id}/reconcile-release", KeycloakScopes.StockReservations.Update),
        ("POST", "/{id}/resolve-read-only", KeycloakScopes.StockReservations.Update),
        ("GET", "/my", KeycloakScopes.StockReservations.ViewOwn),
        ("GET", "/my/{id}", KeycloakScopes.StockReservations.ViewOwn)
    ];

    public static TheoryData<string, string, string> Routes
    {
        get
        {
            var data = new TheoryData<string, string, string>();
            foreach (var (method, route, scope) in Listed)
            {
                data.Add(method, route, scope);
            }

            return data;
        }
    }

    private List<RouteEndpoint> ReservationEndpoints()
    {
        return [.. Factory.Services.GetRequiredService<EndpointDataSource>().Endpoints.OfType<RouteEndpoint>()
            .Where(e => e.RoutePattern.RawText is { } text && text.TrimEnd('/').StartsWith(Prefix, StringComparison.Ordinal))];
    }

    [Theory]
    [MemberData(nameof(Routes))]
    public void StockReservationRoute_RequiresItsScope(string method, string route, string scope)
    {
        var endpoint = Assert.Single(ReservationEndpoints(), e =>
            e.RoutePattern.RawText?.TrimEnd('/') == $"{Prefix}{route}"
            && e.Metadata.GetMetadata<IHttpMethodMetadata>()!.HttpMethods.Contains(method, StringComparer.Ordinal));

        // The module group adds a plain "authenticated" requirement without a policy name; the scope is the named one.
        var policies = endpoint.Metadata.GetOrderedMetadata<IAuthorizeData>().Select(a => a.Policy).OfType<string>().ToList();
        Assert.Equal([KeycloakPermission.FromScope(scope).PolicyName()], policies);
    }

    // A route added later is caught here until it is listed above: nothing but a /my route may be open to every signed-in user.
    [Fact]
    public void StockReservationRoutes_EveryOneIsListedOrAnonymous()
    {
        var unlisted = ReservationEndpoints()
            .Where(e => e.Metadata.GetMetadata<IAllowAnonymous>() is null)
            .Select(e => (Method: e.Metadata.GetMetadata<IHttpMethodMetadata>()!.HttpMethods.Single(), Route: e.RoutePattern.RawText!.TrimEnd('/')[Prefix.Length..]))
            .Where(route => !Listed.Any(listed => listed.Method == route.Method && listed.Route == route.Route))
            .Select(route => $"{route.Method} {route.Route}")
            .ToList();

        Assert.True(unlisted.Count == 0, $"Stock reservation routes with no scope pinned here: {string.Join(", ", unlisted)}");
    }

    [Fact]
    public void StockReservationRoutes_TheWebhookIsTheOnlyAnonymousOne()
    {
        var anonymous = ReservationEndpoints()
            .Where(e => e.Metadata.GetMetadata<IAllowAnonymous>() is not null)
            .Select(e => e.RoutePattern.RawText!)
            .ToList();

        var route = Assert.Single(anonymous);
        Assert.Contains("webhooks/warehouse", route, StringComparison.Ordinal);
    }
}

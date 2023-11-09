using IdentityAndAuth.Features.Tokens.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityAndAuth.Features.Tokens;

internal static class Setup
{
    public static IServiceCollection AddTokensFeature(this IServiceCollection services)
        => services
            .AddTokensServices();

    public static RouteGroupBuilder MapTokensEndpoints(this RouteGroupBuilder rootGroup)
    {
        var tokensApiGroup = rootGroup
            .MapGroup("/tokens")
            .WithTags("Tokens");

        Create.Endpoint.MapEndpoint(tokensApiGroup);
        Refresh.Endpoint.MapEndpoint(tokensApiGroup);

        return rootGroup;
    }
}

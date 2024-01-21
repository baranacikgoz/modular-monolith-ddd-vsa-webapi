using IdentityAndAuth.Features.Tokens.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityAndAuth.Features.Tokens;

internal static class Setup
{
    public static IServiceCollection AddTokensFeature(this IServiceCollection services)
        => services
            .AddTokensInfrastructure();

    public static void MapTokensEndpoints(this RouteGroupBuilder rootGroup)
    {
        var tokensApiGroup = rootGroup
            .MapGroup("/tokens")
            .WithTags("Tokens");

        UseCases.Create.Endpoint.MapEndpoint(tokensApiGroup);
        UseCases.Refresh.Endpoint.MapEndpoint(tokensApiGroup);
    }
}

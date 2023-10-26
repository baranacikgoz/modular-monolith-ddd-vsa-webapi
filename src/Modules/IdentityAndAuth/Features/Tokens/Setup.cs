using IdentityAndAuth.Features.Tokens.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityAndAuth.Features.Tokens;

public static class Setup
{
    public static IServiceCollection AddTokensFeature(this IServiceCollection services)
    {
        services.AddTransient<ITokenService, TokenService>();

        return services;
    }

    public static RouteGroupBuilder MapTokensEndpoints(this RouteGroupBuilder rootGroup)
    {
        var tokensApiGroup = rootGroup
            .MapGroup("/tokens")
            .WithTags("Tokens");

        CreateTokens.MapEndpoint(tokensApiGroup);
        RefreshToken.MapEndpoint(tokensApiGroup);

        return rootGroup;
    }
}

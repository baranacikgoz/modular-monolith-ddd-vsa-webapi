using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace IdentityAndAuth.Application.Tokens;

public static class Setup
{
    public static void MapTokensEndpoints(this RouteGroupBuilder rootGroup)
    {
        var tokensApiGroup = rootGroup
            .MapGroup("/tokens")
            .WithTags("Tokens");

        VersionNeutral.Create.Endpoint.MapEndpoint(tokensApiGroup);
        VersionNeutral.Refresh.Endpoint.MapEndpoint(tokensApiGroup);
    }
}

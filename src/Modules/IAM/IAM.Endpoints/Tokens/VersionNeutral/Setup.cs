using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Endpoint = IAM.Endpoints.Tokens.VersionNeutral.Create.Endpoint;

namespace IAM.Endpoints.Tokens.VersionNeutral;

public static class Setup
{
    public static void MapTokensEndpoints(this RouteGroupBuilder rootGroup)
    {
        var tokensApiGroup = rootGroup
            .MapGroup("/tokens")
            .WithTags("Tokens");

        Endpoint.MapEndpoint(tokensApiGroup);
        Refresh.Endpoint.MapEndpoint(tokensApiGroup);
    }
}

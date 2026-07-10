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
        Revoke.Endpoint.MapEndpoint(tokensApiGroup);

        var sessionsApiGroup = tokensApiGroup
            .MapGroup("/sessions")
            .WithTags("Sessions");

        Sessions.List.Endpoint.MapEndpoint(sessionsApiGroup);
        Sessions.Revoke.Endpoint.MapEndpoint(sessionsApiGroup);
        Sessions.RevokeAll.Endpoint.MapEndpoint(sessionsApiGroup);
    }
}

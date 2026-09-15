using Common.Application.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Endpoint = IAM.Endpoints.Tokens.VersionNeutral.Create.Endpoint;

namespace IAM.Endpoints.Tokens.VersionNeutral;

public static class Setup
{
    public static void MapTokensEndpoints(this RouteGroupBuilder rootGroup, UsernameSource usernameSource)
    {
        var tokensApiGroup = rootGroup
            .MapGroup("/tokens")
            .WithTags("Tokens");

        // CreateByEmail (email+password) stays mapped regardless of UsernameSource: staff/admin accounts
        // always sign in by email+password, independent of which identifier sellers are provisioned under.
        if (usernameSource == UsernameSource.PhoneNumber)
        {
            Endpoint.MapEndpoint(tokensApiGroup);
        }

        CreateByEmail.Endpoint.MapEndpoint(tokensApiGroup);
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

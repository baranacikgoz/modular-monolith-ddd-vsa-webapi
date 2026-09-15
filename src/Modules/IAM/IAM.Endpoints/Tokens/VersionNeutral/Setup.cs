using Common.Application.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Endpoint = IAM.Endpoints.Tokens.VersionNeutral.Create.Endpoint;

namespace IAM.Endpoints.Tokens.VersionNeutral;

public static class Setup
{
    public static void MapTokensEndpoints(this RouteGroupBuilder rootGroup, IdentityScheme identityScheme)
    {
        var tokensApiGroup = rootGroup
            .MapGroup("/tokens")
            .WithTags("Tokens");

        // CreateByEmail (email + password + email verification token) stays mapped regardless of
        // IdentityScheme: it is the only login in the Email scheme and the staff/admin login in the
        // PhoneNumber scheme. Same flow for every account type, no per-role branching.
        if (identityScheme == IdentityScheme.PhoneNumber)
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

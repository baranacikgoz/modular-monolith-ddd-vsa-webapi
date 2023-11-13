using Common.Core.Contracts;
using Common.Core.Contracts.Results;
using Common.Core.EndpointFilters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using NimbleMediator.Contracts;
using RateLimitingConstants = IdentityAndAuth.ModuleSetup.RateLimiting.Constants;

namespace IdentityAndAuth.Features.Identity.UseCases.Users.InitiatePhoneOwnershipProcess;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapPost("initiate-phone-ownership-process", InitiatePhoneOwnershipProcessAsync)
            .WithDescription("Initiate phone ownership process by sending sms otp.")
            .Produces(StatusCodes.Status200OK)
            .RequireRateLimiting(RateLimitingConstants.Sms)
            .AllowAnonymous()
            .AddEndpointFilter<ResultToResponseTransformer>();
    }

    private static ValueTask<Result> InitiatePhoneOwnershipProcessAsync(
        [FromBody] Request request,
        [FromServices] ISender mediator,
        CancellationToken cancellationToken)
        => mediator.SendAsync<Request, Result>(request, cancellationToken);
}

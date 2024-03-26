using Common.Core.Contracts.Results;
using Common.Core.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using RateLimitingConstants = IdentityAndAuth.ModuleSetup.RateLimiting.Constants;

namespace IdentityAndAuth.Features.Identity.UseCases.Users.InitiatePhoneOwnershipProcess;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapPost("initiate-phone-ownership-process", InitiatePhoneOwnershipProcessAsync)
            .WithDescription("Initiate phone ownership process by sending sms otp.")
            .Produces(StatusCodes.Status204NoContent)
            .RequireRateLimiting(RateLimitingConstants.Sms)
            .AllowAnonymous()
            .TransformResultToOkResponse();
    }

    private static async Task<Result> InitiatePhoneOwnershipProcessAsync(
#pragma warning disable S1172
        [FromBody] Request request,
#pragma warning restore S1172
        CancellationToken cancellationToken)
    {
        await Task.Delay(300, cancellationToken);
        return Result.Success;
    }
}

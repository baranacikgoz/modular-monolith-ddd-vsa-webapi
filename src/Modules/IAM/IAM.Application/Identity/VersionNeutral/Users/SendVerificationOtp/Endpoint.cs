using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace IAM.Application.Identity.VersionNeutral.Users.SendVerificationOtp;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapPost("send-verification-otp", SendVerificationOtpAsync)
            .WithDescription("Send verification otp sms.")
            .RequireRateLimiting(RateLimiting.Constants.Sms)
            .AllowAnonymous()
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> SendVerificationOtpAsync(
#pragma warning disable S1172
        [FromBody] Request request,
#pragma warning restore S1172
        CancellationToken cancellationToken)
    {
        await Task.Delay(300, cancellationToken);
        return Result.Success;
    }
}

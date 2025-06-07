using Common.Application.Caching;
using Common.Application.Extensions;
using Common.Application.Options;
using Common.Domain.ResultMonad;
using IAM.Application.Otp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;

namespace IAM.Endpoints.Otp.VersionNeutral.Send;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapPost("", SendOtp)
            .WithDescription("Send otp sms.")
            .RequireRateLimiting(Infrastructure.RateLimiting.Constants.Sms)
            .AllowAnonymous()
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> SendOtp(
        [FromBody] Request request,
        [FromServices] IOtpService otpService,
        [FromServices] IOptions<OtpOptions> otpOptionsProvider,
        [FromServices] ICacheService cache,
        CancellationToken cancellationToken)
        => await Result<string>
            .Create(() => otpService.Generate())
            .TapAsync(async otp => await cache.SetAsync(
                key: CacheKeys.For.Otp(request.PhoneNumber),
                value: otp,
                absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(otpOptionsProvider.Value.ExpirationInMinutes),
                cancellationToken: cancellationToken))
            .TapAsync(async _ =>
            {
                // Sending sms logic comes here...

                // Simulate some delay for sending sms
                await Task.Delay(100, cancellationToken);
            });
}

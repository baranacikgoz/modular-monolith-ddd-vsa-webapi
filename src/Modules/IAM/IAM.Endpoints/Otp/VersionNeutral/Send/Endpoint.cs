using Common.Application.Caching;
using Common.Application.Extensions;
using Common.Application.FeatureManagement;
using Common.Application.Options;
using Common.Domain.ResultMonad;
using IAM.Application.Captcha.Services;
using IAM.Application.Otp.Services;
using IAM.Infrastructure.RateLimiting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;
using ZiggyCreatures.Caching.Fusion;

namespace IAM.Endpoints.Otp.VersionNeutral.Send;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapPost("", SendOtp)
            .WithDescription("Send otp sms.")
            .RequireRateLimiting(Constants.Sms)
            .AllowAnonymous()
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> SendOtp(
        Request request,
        IOtpService otpService,
        ICaptchaService captchaService,
        IFeatureManager featureManager,
        IOptions<OtpOptions> otpOptionsProvider,
        IFusionCache cache,
        CancellationToken cancellationToken)
    {
        var captchaTask = await featureManager.IsEnabledAsync(FeatureFlags.IAM.Captcha)
            ? captchaService.ValidateAsync(request.CaptchaToken ?? string.Empty, cancellationToken)
            : Task.FromResult(Result.Success);

        return await captchaTask
            .BindAsync(() => otpService.Generate())
            .TapAsync(async otp => await cache.SetAsync(
                CacheKeys.For.Otp(request.PhoneNumber),
                otp,
                options: new FusionCacheEntryOptions { Duration = TimeSpan.FromMinutes(otpOptionsProvider.Value.ExpirationInMinutes) },
                token: cancellationToken))
            .TapAsync(async _ =>
            {
                // Sending sms logic comes here...

                // Simulate some delay for sending sms
                await Task.Delay(100, cancellationToken);
            });
    }
}

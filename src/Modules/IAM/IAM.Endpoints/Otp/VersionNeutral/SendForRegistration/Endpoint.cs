using System.Globalization;
using Common.Application.Extensions;
using Common.Application.FeatureManagement;
using Common.Domain.ResultMonad;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Notifications;
using IAM.Application.Captcha.Services;
using IAM.Infrastructure.RateLimiting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.FeatureManagement;

namespace IAM.Endpoints.Otp.VersionNeutral.SendForRegistration;

internal static class Endpoint
{

    internal static void MapEndpoint(RouteGroupBuilder otpApiGroup)
    {
        otpApiGroup
            .MapPost("registration", SendOtp)
            .WithDescription("Send otp sms for registration.")
            .RequireRateLimiting(Constants.Sms)
            .AllowAnonymous()
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> SendOtp(
        Request request,
        IInterModuleRequestClient<SendPhoneOtpRequest, SendPhoneOtpResponse> otpClient,
        ICaptchaService captchaService,
        IFeatureManager featureManager,
        CancellationToken cancellationToken)
    {
        var captchaTask = await featureManager.IsEnabledAsync(FeatureFlags.IAM.Captcha)
            ? captchaService.ValidateAsync(request.CaptchaToken ?? string.Empty, cancellationToken)
            : Task.FromResult(Result.Success);

        return await captchaTask
            .BindAsync(async () =>
            {
                await otpClient.SendAsync(
                    new SendPhoneOtpRequest(request.PhoneNumber, OtpPurposes.Registration,
                        Language: CultureInfo.CurrentUICulture.TwoLetterISOLanguageName),
                    cancellationToken);
                return Result.Success;
            });
    }
}

using System.Globalization;
using Common.Application.Extensions;
using Common.Application.FeatureManagement;
using Common.Domain.ResultMonad;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Notifications;
using IAM.Application.Captcha.Services;
using IAM.Endpoints.Common;
using IAM.Infrastructure.RateLimiting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.FeatureManagement;

namespace IAM.Endpoints.Otp.VersionNeutral.SendForEmail;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder otpApiGroup)
    {
        otpApiGroup
            .MapPost("email", SendOtp)
            .WithDescription("Send otp email for the login/registration verification step.")
            .RequireRateLimiting(Constants.Email)
            .AllowAnonymous()
            .Produces(StatusCodes.Status204NoContent)
            .TransformResultToNoContentResponse();
    }

    private static async Task<Result> SendOtp(
        Request request,
        IInterModuleRequestClient<SendEmailOtpRequest, SendEmailOtpResponse> otpClient,
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
                var response = await otpClient.SendAsync(
                    new SendEmailOtpRequest(EmailNormalization.Normalize(request.Email), OtpPurposes.EmailVerification,
                        Language: CultureInfo.CurrentUICulture.TwoLetterISOLanguageName),
                    cancellationToken);
                return response.Outcome.ToResult();
            });
    }
}

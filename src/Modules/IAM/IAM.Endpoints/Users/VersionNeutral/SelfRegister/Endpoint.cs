using System.Globalization;
using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.FeatureManagement;
using Common.Domain.ResultMonad;
using IAM.Application.Captcha.Services;
using IAM.Application.Extensions;
using IAM.Application.Otp.Services;
using IAM.Domain.Identity;
using IAM.Infrastructure.Telemetry;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.FeatureManagement;
using Constants = IAM.Domain.Constants;

namespace IAM.Endpoints.Users.VersionNeutral.SelfRegister;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapPost("register/self", RegisterAsync)
            .WithDescription("Register a new user.")
            .Produces<Response>()
            .AllowAnonymous()
            .RequireRateLimiting(IAM.Infrastructure.RateLimiting.Constants.Register)
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> RegisterAsync(
        Request request,
        UserManager<ApplicationUser> userManager,
        IOtpService otpService,
        ICaptchaService captchaService,
        IFeatureManager featureManager,
        CancellationToken cancellationToken)
    {
        var captchaTask = await featureManager.IsEnabledAsync(FeatureFlags.IAM.Captcha)
            ? captchaService.ValidateAsync(request.CaptchaToken ?? string.Empty, cancellationToken)
            : Task.FromResult(Result.Success);

        return await captchaTask
            .BindAsync(() => CreateUserPipelineAsync(request, userManager, otpService, cancellationToken));
    }

    private static Task<Result<Response>> CreateUserPipelineAsync(
        Request request,
        UserManager<ApplicationUser> userManager,
        IOtpService otpService,
        CancellationToken cancellationToken)
    {
        return otpService
            .VerifyThenRemoveOtpAsync(request.PhoneNumber, request.Otp, cancellationToken)
            .BindAsync(async () =>
            {
                var user = ApplicationUser.Create(
                    request.FullName,
                    request.PhoneNumber,
                    DateOnly.ParseExact(request.BirthDate, Constants.TurkishDateFormat,
                        CultureInfo.InvariantCulture));
                var identityResult = await userManager.CreateAsync(user);
                return identityResult.ToResult(user);
            })
            .BindAsync(async user =>
            {
                var identityResult = await userManager.AddToRoleAsync(user, CustomRoles.Basic);
                return identityResult.ToResult(user);
            })
            .TapAsync(_ => IamTelemetry.UsersRegistered.Add(1))
            .MapAsync(user => new Response { Id = user.Id });
    }
}

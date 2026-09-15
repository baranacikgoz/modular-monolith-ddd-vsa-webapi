using System.Globalization;
using Common.Application.Extensions;
using Common.Application.FeatureManagement;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;
using Common.Infrastructure.Extensions;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Notifications;
using IAM.Application.Captcha.Services;
using IAM.Application.Keycloak;
using IAM.Endpoints.Common;
using IAM.Endpoints.Otp;
using IAM.Endpoints.Tokens;
using IAM.Infrastructure.Telemetry;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Constants = IAM.Domain.Constants;

namespace IAM.Endpoints.Users.VersionNeutral.SelfRegisterByEmail;

/// <summary>
///     Email-scheme counterpart of <see cref="SelfRegister.Endpoint" />. Ownership of the address was already
///     proven by /otp/email/verify, which handed out the single-use token under the
///     <see cref="OtpPurposes.EmailVerifiedRegister" /> purpose; a login-purpose token is rejected here.
/// </summary>
internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapPost("register/self/email", RegisterAsync)
            .WithDescription("Register a new email + password user in Keycloak (verification token from /otp/email/verify) and sign them in.")
            .Produces<Response>()
            .AllowAnonymous()
            .RequireRateLimiting(Infrastructure.RateLimiting.Constants.Register)
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> RegisterAsync(
        Request request,
        IInterModuleRequestClient<VerifyEmailOtpRequest, VerifyEmailOtpResponse> otpClient,
        ICaptchaService captchaService,
        IFeatureManager featureManager,
        IKeycloakAdminClient adminClient,
        IKeycloakTokenClient tokenClient,
        IInterModuleRequestClient<BindDeviceSessionRequest, BindDeviceSessionResponse> deviceClient,
        ILogger<Request> logger,
        CancellationToken cancellationToken)
    {
        using var activity = IamTelemetry.ActivitySource.StartActivityForCaller();

        var email = EmailNormalization.Normalize(request.Email);

        var captchaTask = await featureManager.IsEnabledAsync(FeatureFlags.IAM.Captcha)
            ? captchaService.ValidateAsync(request.CaptchaToken ?? string.Empty, cancellationToken)
            : Task.FromResult(Result.Success);

        return await captchaTask
            .BindAsync(async () => (await otpClient.SendAsync(
                new VerifyEmailOtpRequest(email, request.EmailVerificationToken, OtpPurposes.EmailVerifiedRegister),
                cancellationToken)).ToResult())
            // Keycloak is the source of truth: a duplicate address surfaces as its 409 (username = email here).
            .BindAsync<ApplicationUserId>(() => adminClient.CreateUserAsync(
                new CreateKeycloakUser(
                    Username: email,
                    FirstName: request.FirstName.Trim(),
                    LastName: request.LastName.Trim(),
                    PhoneNumber: null,
                    BirthDate: DateOnly.ParseExact(request.BirthDate, Constants.TurkishDateFormat,
                        CultureInfo.InvariantCulture),
                    Email: email,
                    Password: request.Password),
                cancellationToken))
            .BindAsync(userId => RegistrationCompletion.AssignBasicRoleOrRollbackAsync(userId, adminClient, logger, cancellationToken))
            .TapAsync(userId => activity?.SetTag("user.id", userId.ToString()))
            .TapAsync(_ => IamTelemetry.UsersRegistered.Add(1))
            .BindAsync<ApplicationUserId, KeycloakTokens>(_ => tokenClient.TrustedLoginAsync(email, cancellationToken))
            .BindAsync(tokens => LoginCompletion.BindDeviceAsync(
                tokens, request.DeviceId, request.ClientId, request.DeviceName, request.PushToken,
                deviceClient, adminClient, logger, cancellationToken))
            .TapAsync(_ => IamTelemetry.RecordLogin(LoginMethods.Registration))
            .MapAsync(tokens => new Response
            {
                AccessToken = tokens.AccessToken,
                AccessTokenExpiresAt = tokens.AccessTokenExpiresAt,
                RefreshToken = tokens.RefreshToken,
                RefreshTokenExpiresAt = tokens.RefreshTokenExpiresAt
            })
            .TapActivityAsync(activity);
    }
}

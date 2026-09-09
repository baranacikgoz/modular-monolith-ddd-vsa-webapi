using System.Globalization;
using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.FeatureManagement;
using Common.Domain.ResultMonad;
using Common.Domain.StronglyTypedIds;
using Common.Infrastructure.Extensions;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Notifications;
using IAM.Application.Captcha.Services;
using IAM.Application.Keycloak;
using IAM.Endpoints.Otp;
using IAM.Endpoints.Tokens;
using IAM.Infrastructure.Telemetry;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Polly.CircuitBreaker;
using Polly.Timeout;
using Constants = IAM.Domain.Constants;

namespace IAM.Endpoints.Users.VersionNeutral.SelfRegister;

internal static partial class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder usersApiGroup)
    {
        usersApiGroup
            .MapPost("register/self", RegisterAsync)
            .WithDescription("Register a new phone user in Keycloak and sign them in.")
            .Produces<Response>()
            .AllowAnonymous()
            .RequireRateLimiting(Infrastructure.RateLimiting.Constants.Register)
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> RegisterAsync(
        Request request,
        IInterModuleRequestClient<VerifyPhoneOtpRequest, VerifyPhoneOtpResponse> otpClient,
        ICaptchaService captchaService,
        IFeatureManager featureManager,
        IKeycloakAdminClient adminClient,
        IKeycloakTokenClient tokenClient,
        IInterModuleRequestClient<BindDeviceSessionRequest, BindDeviceSessionResponse> deviceClient,
        ILogger<Request> logger,
        CancellationToken cancellationToken)
    {
        using var activity = IamTelemetry.ActivitySource.StartActivityForCaller();

        var captchaTask = await featureManager.IsEnabledAsync(FeatureFlags.IAM.Captcha)
            ? captchaService.ValidateAsync(request.CaptchaToken ?? string.Empty, cancellationToken)
            : Task.FromResult(Result.Success);

        return await captchaTask
            .BindAsync(async () => (await otpClient.SendAsync(
                new VerifyPhoneOtpRequest(request.PhoneNumber, request.Otp, OtpPurposes.Registration),
                cancellationToken)).ToResult())
            // Keycloak is the source of truth: a duplicate phone number surfaces as its 409 on username.
            .BindAsync<ApplicationUserId>(() => adminClient.CreateUserAsync(
                new CreateKeycloakUser(
                    Username: request.PhoneNumber,
                    FirstName: request.FirstName.Trim(),
                    LastName: request.LastName.Trim(),
                    PhoneNumber: request.PhoneNumber,
                    BirthDate: DateOnly.ParseExact(request.BirthDate, Constants.TurkishDateFormat,
                        CultureInfo.InvariantCulture)),
                cancellationToken))
            .BindAsync(userId => AssignBasicRoleOrRollbackAsync(userId, adminClient, logger, cancellationToken))
            .TapAsync(userId => activity?.SetTag("user.id", userId.ToString()))
            .TapAsync(_ => IamTelemetry.UsersRegistered.Add(1))
            .BindAsync<ApplicationUserId, KeycloakTokens>(_ => tokenClient.TrustedLoginAsync(request.PhoneNumber, cancellationToken))
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

    /// <summary>
    ///     The user already exists in Keycloak from <c>CreateUserAsync</c>; role assignment is the second of two
    ///     non-transactional Admin REST calls. On failure, delete the user so a retry sees a clean "not registered"
    ///     state instead of a roleless account that 409s forever (PR #149 #3).
    /// </summary>
    private static async Task<Result<ApplicationUserId>> AssignBasicRoleOrRollbackAsync(
        ApplicationUserId userId, IKeycloakAdminClient adminClient, ILogger logger, CancellationToken cancellationToken)
    {
        var assigned = await adminClient.AssignRealmRoleAsync(userId, KeycloakRoles.Basic, cancellationToken);
        if (!assigned.IsFailure)
        {
            return userId;
        }

        try
        {
            await adminClient.DeleteUserAsync(userId, cancellationToken);
        }
        catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException
                                       or BrokenCircuitException or TimeoutRejectedException)
        {
            LogRegistrationRollbackFailed(logger, userId, ex);
            IamTelemetry.RecordOrphanedUserLeftRoleless();
        }

        return Result<ApplicationUserId>.Failure(assigned.Error!);
    }

    [LoggerMessage(Level = LogLevel.Error,
        Message = "Role assignment failed for newly created Keycloak user {UserId} and the compensating delete also failed; the user is left roleless.")]
    private static partial void LogRegistrationRollbackFailed(ILogger logger, ApplicationUserId userId, Exception ex);
}

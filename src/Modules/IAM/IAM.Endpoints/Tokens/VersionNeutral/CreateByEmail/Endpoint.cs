using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Extensions;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Notifications;
using IAM.Application.Keycloak;
using IAM.Endpoints.Common;
using IAM.Endpoints.Otp;
using IAM.Infrastructure.Telemetry;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Constants = IAM.Infrastructure.RateLimiting.Constants;

namespace IAM.Endpoints.Tokens.VersionNeutral.CreateByEmail;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder tokensApiGroup)
    {
        tokensApiGroup
            .MapPost("email", CreateTokensByEmail)
            .WithDescription("Sign in with email + password + a verification token from /otp/email/verify and receive tokens.")
            .AllowAnonymous()
            .RequireRateLimiting(Constants.TokenCreate)
            .Produces<Response>()
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> CreateTokensByEmail(
        Request request,
        IKeycloakTokenClient tokenClient,
        IKeycloakAdminClient adminClient,
        IInterModuleRequestClient<VerifyEmailOtpRequest, VerifyEmailOtpResponse> otpClient,
        IInterModuleRequestClient<BindDeviceSessionRequest, BindDeviceSessionResponse> deviceClient,
        ILogger<Request> logger,
        CancellationToken cancellationToken)
    {
        using var activity = IamTelemetry.ActivitySource.StartActivityForCaller();

        // Password checked BEFORE the verification token is consumed: the token store deletes on a
        // correct match (single-use), so checking it first would burn it on every wrong-password retry
        // and force a fresh email round-trip. Keycloak applies brute-force protection and the password
        // policy; a wrong password, unknown email and locked account all surface as the same
        // InvalidCredentials error.
        return await tokenClient
            .PasswordLoginAsync(request.Email, request.Password, cancellationToken)
            .BindAsync(_ => VerifyEmailVerificationTokenAsync(
                request.Email, request.EmailVerificationToken, otpClient, cancellationToken))
            .BindAsync(tokens => LoginCompletion.BindDeviceAsync(
                tokens, request.DeviceId, request.ClientId, request.DeviceName, request.PushToken,
                deviceClient, adminClient, logger, cancellationToken))
            .TapAsync(tokens => activity?.SetTag("session.id", tokens.SessionId))
            .TapAsync(_ => IamTelemetry.RecordLogin(LoginMethods.EmailPassword))
            .MapAsync(tokens => new Response
            {
                AccessToken = tokens.AccessToken,
                AccessTokenExpiresAt = tokens.AccessTokenExpiresAt,
                RefreshToken = tokens.RefreshToken,
                RefreshTokenExpiresAt = tokens.RefreshTokenExpiresAt
            })
            .TapActivityAsync(activity);
    }

    private static async Task<Result> VerifyEmailVerificationTokenAsync(
        string email,
        string emailVerificationToken,
        IInterModuleRequestClient<VerifyEmailOtpRequest, VerifyEmailOtpResponse> otpClient,
        CancellationToken cancellationToken)
    {
        var response = await otpClient.SendAsync(
            new VerifyEmailOtpRequest(
                EmailNormalization.Normalize(email), emailVerificationToken, OtpPurposes.EmailVerifiedLogin),
            cancellationToken);

        return response.ToResult();
    }
}

using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Extensions;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Notifications;
using IAM.Application.Keycloak;
using IAM.Endpoints.Otp;
using IAM.Infrastructure.Telemetry;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Constants = IAM.Infrastructure.RateLimiting.Constants;

namespace IAM.Endpoints.Tokens.VersionNeutral.Create;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder tokensApiGroup)
    {
        tokensApiGroup
            .MapPost("", CreateTokens)
            .WithDescription("Sign in with phone number + SMS code and receive tokens.")
            .AllowAnonymous()
            .RequireRateLimiting(Constants.TokenCreate)
            .Produces<Response>()
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> CreateTokens(
        Request request,
        IInterModuleRequestClient<VerifyPhoneOtpRequest, VerifyPhoneOtpResponse> otpClient,
        IKeycloakTokenClient tokenClient,
        IKeycloakAdminClient adminClient,
        IInterModuleRequestClient<BindDeviceSessionRequest, BindDeviceSessionResponse> deviceClient,
        ILogger<Request> logger,
        CancellationToken cancellationToken)
    {
        using var activity = IamTelemetry.ActivitySource.StartActivityForCaller();

        var verifyOtpResponse = await otpClient.SendAsync(
            new VerifyPhoneOtpRequest(request.PhoneNumber, request.Otp, OtpPurposes.Login),
            cancellationToken);

        return await verifyOtpResponse
            .ToResult()
            .BindAsync<KeycloakTokens>(() => tokenClient.TrustedLoginAsync(request.PhoneNumber, cancellationToken))
            .BindAsync(tokens => LoginCompletion.BindDeviceAsync(
                tokens, request.DeviceId, request.ClientId, request.DeviceName, request.PushToken,
                deviceClient, adminClient, logger, cancellationToken))
            .TapAsync(tokens => activity?.SetTag("session.id", tokens.SessionId))
            .TapAsync(_ => IamTelemetry.RecordLogin(LoginMethods.PhoneOtp))
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

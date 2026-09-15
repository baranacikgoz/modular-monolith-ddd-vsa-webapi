using Common.Application.Extensions;
using Common.Domain.ResultMonad;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Notifications;
using IAM.Application.Keycloak;
using IAM.Endpoints.Common;
using IAM.Infrastructure.RateLimiting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace IAM.Endpoints.Otp.VersionNeutral.VerifyEmail;

internal static class Endpoint
{
    internal static void MapEndpoint(RouteGroupBuilder otpApiGroup)
    {
        otpApiGroup
            .MapPost("email/verify", VerifyOtp)
            .WithDescription("Verify the email otp and receive a short-lived verification token, plus whether the address is already registered.")
            .RequireRateLimiting(Constants.OtpVerify)
            .AllowAnonymous()
            .Produces<Response>()
            .TransformResultTo<Response>();
    }

    private static async Task<Result<Response>> VerifyOtp(
        Request request,
        IInterModuleRequestClient<VerifyEmailOtpRequest, VerifyEmailOtpResponse> otpClient,
        IInterModuleRequestClient<IssueVerificationTokenRequest, IssueVerificationTokenResponse> issueTokenClient,
        IKeycloakAdminClient adminClient,
        CancellationToken cancellationToken)
    {
        var email = EmailNormalization.Normalize(request.Email);

        var verifyResponse = await otpClient.SendAsync(
            new VerifyEmailOtpRequest(email, request.Otp, OtpPurposes.EmailVerification), cancellationToken);

        return await verifyResponse
            .ToResult()
            .BindAsync(async () =>
            {
                // Only the isRegistered branch this decides on ever reaches the caller: the send endpoint
                // never reveals it, so account existence leaks only after a correct code, to its own owner.
                var user = await adminClient.FindUserByEmailAsync(email, cancellationToken);
                var isRegistered = user is not null;
                var purpose = isRegistered ? OtpPurposes.EmailVerifiedLogin : OtpPurposes.EmailVerifiedRegister;

                var tokenResponse = await issueTokenClient.SendAsync(
                    new IssueVerificationTokenRequest(email, purpose), cancellationToken);

                return Result<Response>.Success(new Response
                {
                    IsRegistered = isRegistered,
                    EmailVerificationToken = tokenResponse.Token
                });
            });
    }
}

using System.Security.Cryptography;
using Common.Application.Options;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Notifications;
using Microsoft.Extensions.Options;
using Notifications.Application.Otp;

namespace Notifications.Infrastructure.InterModuleRequestHandlers;

/// <summary>
///     Generates the token inline with <see cref="RandomNumberGenerator" /> rather than through
///     <see cref="IOtpService.Generate" />: that path honors <see cref="OtpOptions.DummyCode" />, which is a
///     fixed, publicly known string in non-production config. A verification token must stay unguessable in
///     every environment, so it never goes through the same generator as the human-facing OTP code.
/// </summary>
public sealed class IssueVerificationTokenRequestHandler(
    IOtpService otpService,
    IOptions<OtpOptions> otpOptionsProvider
) : InterModuleRequestHandler<IssueVerificationTokenRequest, IssueVerificationTokenResponse>
{
    private const int TokenSizeInBytes = 32;

    public override async Task<IssueVerificationTokenResponse> HandleAsync(
        IssueVerificationTokenRequest request,
        CancellationToken cancellationToken)
    {
        var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(TokenSizeInBytes));

        await otpService.StoreAsync(
            request.Identifier,
            token,
            request.Purpose,
            TimeSpan.FromMinutes(otpOptionsProvider.Value.VerificationTokenExpirationInMinutes),
            null,
            cancellationToken);

        return new IssueVerificationTokenResponse(token);
    }
}

using Common.Core.Contracts.Results;

namespace IdentityAndAuth.Features.Users.Services.Otp;

internal interface IOtpService
{
    Task<string> GetOtpAsync(string phoneNumber, CancellationToken cancellationToken);
    Task<Result> ValidateAsync(string otp, string phoneNumber, CancellationToken cancellationToken);
}

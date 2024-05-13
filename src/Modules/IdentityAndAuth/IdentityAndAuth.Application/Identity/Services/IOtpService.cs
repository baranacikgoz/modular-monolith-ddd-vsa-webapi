using Common.Domain.ResultMonad;

namespace IdentityAndAuth.Application.Identity.Services;

public interface IOtpService
{
    Task<string> GetOtpAsync(string phoneNumber, CancellationToken cancellationToken);
    Task<Result> ValidateAsync(string otp, string phoneNumber, CancellationToken cancellationToken);
}

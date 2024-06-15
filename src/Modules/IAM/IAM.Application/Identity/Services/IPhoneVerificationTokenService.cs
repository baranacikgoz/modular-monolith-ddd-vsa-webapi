using Common.Domain.ResultMonad;

namespace IAM.Application.Identity.Services;

public interface IPhoneVerificationTokenService
{
    Task<string> GetTokenAsync(string phoneNumber, CancellationToken cancellationToken);
    Task<Result> ValidateTokenAsync(string phoneNumber, string token, CancellationToken cancellationToken);
}

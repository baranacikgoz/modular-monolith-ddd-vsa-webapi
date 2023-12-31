using Common.Core.Contracts.Results;

namespace IdentityAndAuth.Features.Identity.Domain;

public interface IPhoneVerificationTokenService
{
    Task<string> GetTokenAsync(string phoneNumber, CancellationToken cancellationToken);
    Task<Result> ValidateTokenAsync(string phoneNumber, string token, CancellationToken cancellationToken);
}

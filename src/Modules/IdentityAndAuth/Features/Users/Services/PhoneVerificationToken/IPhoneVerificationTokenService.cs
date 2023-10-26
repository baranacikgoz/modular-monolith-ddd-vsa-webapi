using Common.Core.Contracts.Results;

namespace IdentityAndAuth.Features.Users.Services.PhoneVerificationToken;

public interface IPhoneVerificationTokenService
{
    Task<string> GetTokenAsync(string phoneNumber, CancellationToken cancellationToken);
    Task<Result> ValidateTokenAsync(string phoneNumber, string token, CancellationToken cancellationToken);
}

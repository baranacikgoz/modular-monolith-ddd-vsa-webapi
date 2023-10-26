using Common.Core.Contracts.Results;

namespace IdentityAndAuth.Features.Users.Services.PhoneVerificationToken;

public class DummyPhoneVerificationTokenService : IPhoneVerificationTokenService
{
    private const string DummyPhoneVerificationToken = "dummyPhoneVerificationToken";
    public Task<string> GetTokenAsync(string phoneNumber, CancellationToken cancellationToken)
        => Task.FromResult(DummyPhoneVerificationToken);

    public async Task<Result> ValidateTokenAsync(string phoneNumber, string token, CancellationToken cancellationToken)
    {
        await Task.Delay(100, cancellationToken);

        if (token != DummyPhoneVerificationToken)
        {
            return new PhoneVerificationTokenValidationFailedError();
        }

        return Result.Succeeded();
    }
}

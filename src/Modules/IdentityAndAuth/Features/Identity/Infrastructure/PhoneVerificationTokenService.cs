using Common.Caching;
using Common.Core;
using Common.Core.Contracts.Results;
using Common.Options;
using IdentityAndAuth.Features.Identity.Domain;
using IdentityAndAuth.Features.Identity.Domain.Errors;
using Microsoft.Extensions.Options;

namespace IdentityAndAuth.Features.Identity.Infrastructure;

internal class PhoneVerificationTokenService(
    ICacheService cache,
    IOptions<OtpOptions> otpOptionsProvider
    ) : IPhoneVerificationTokenService
{
    private readonly int _expirationInMinutes = otpOptionsProvider.Value.ExpirationInMinutes;

    public Task<string> GetTokenAsync(string phoneNumber, CancellationToken cancellationToken)
     => cache.GetOrSetAsync(
        CacheKey(phoneNumber),
        () => Guid.NewGuid().ToString("N"),
        absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(_expirationInMinutes),
        cancellationToken: cancellationToken);

    public async Task<Result> ValidateTokenAsync(string phoneNumber, string token, CancellationToken cancellationToken)
        => await Result<string>
                .Create(
                    taskToAwaitValue: async () => await cache.GetAsync<string>(CacheKey(phoneNumber), cancellationToken),
                    ifTaskReturnsNull: PhoneVerificationTokenErrors.TokenNotFound)
                .BindAsync(cachedToken => StringExt.EnsureNotNullOrEmpty(cachedToken, ifNull: PhoneVerificationTokenErrors.TokenNotFound))
                .BindAsync(cachedToken => EnsureTokensAreMatching(cachedToken, token))
                .BindAsync(async () => await cache.RemoveAsync(CacheKey(phoneNumber), cancellationToken));
    private static Result<string> EnsureTokensAreMatching(string cachedToken, string token)
    {
        var boolResult = string.Equals(cachedToken, token, StringComparison.Ordinal);

        if (boolResult)
        {
            return cachedToken;
        }

        return PhoneVerificationTokenErrors.NotMatching;
    }

    private static string CacheKey(string phoneNumber) => $"phone-verification-token:{phoneNumber}";
}

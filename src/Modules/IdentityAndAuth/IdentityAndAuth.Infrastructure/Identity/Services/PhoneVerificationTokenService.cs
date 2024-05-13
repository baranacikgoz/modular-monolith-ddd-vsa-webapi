using Common.Application.Caching;
using Common.Domain.Extensions;
using Common.Domain.ResultMonad;
using Common.Infrastructure.Options;
using IdentityAndAuth.Application.Identity.Services;
using IdentityAndAuth.Domain.Identity.Errors;
using Microsoft.Extensions.Options;

namespace IdentityAndAuth.Infrastructure.Identity.Services;

internal class PhoneVerificationTokenService(
    ICacheService cache,
    IOptions<OtpOptions> otpOptionsProvider
    ) : IPhoneVerificationTokenService
{
    private readonly int _expirationInMinutes = otpOptionsProvider.Value.ExpirationInMinutes;
    private const string ErrorKey = "PhoneVerificationToken";

    public Task<string> GetTokenAsync(string phoneNumber, CancellationToken cancellationToken)
     => cache.GetOrSetAsync(
        CacheKey(phoneNumber),
        () => Guid.NewGuid().ToString("N"),
        absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(_expirationInMinutes),
        cancellationToken: cancellationToken);

    /// <summary>
    /// Initially, we were removing the token from the cache at the end of this <see cref="ValidateTokenAsync"/> method.
    /// But, for the new user registrations, since we are requesting token twice;
    /// - First in <see cref="Application.Identity.VersionNeutral.Users.SelfRegister.Endpoint>
    /// - Then in <see cref="Application.Tokens.VersionNeutral.Create.Endpoint>
    /// If we remove the token from the cache after the first request, the second request will fail with <see cref="PhoneVerificationTokenErrors.PhoneVerificationTokenNotFound"/>
    /// And users will have to go back to the very first step of the registration process.
    /// </summary>
    public async Task<Result> ValidateTokenAsync(string phoneNumber, string token, CancellationToken cancellationToken)
        => await Result<string>
                .CreateAsync(
                    taskToAwaitValue: async () => await cache.GetAsync<string>(CacheKey(phoneNumber), cancellationToken),
                    errorIfValueNull: Error.NotFound(ErrorKey, phoneNumber))
                .TapAsync(cachedToken => StringExtensions.EnsureNotNullOrEmpty(cachedToken, ifNullOrEmpty: Error.NotFound(ErrorKey, phoneNumber)))
                .TapAsync(cachedToken => EnsureTokensAreMatching(cachedToken, token));
    private static Result<string> EnsureTokensAreMatching(string cachedToken, string token)
    {
        var boolResult = string.Equals(cachedToken, token, StringComparison.Ordinal);

        if (boolResult)
        {
            return cachedToken;
        }

        return PhoneVerificationTokenErrors.PhoneVerificationTokensNotMatching;
    }

    private static string CacheKey(string phoneNumber) => $"phone-verification-token:{phoneNumber}";
}

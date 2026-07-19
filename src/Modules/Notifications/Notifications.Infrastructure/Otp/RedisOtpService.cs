using System.Security.Cryptography;
using Common.Application.Caching;
using Common.Application.Options;
using Microsoft.Extensions.Options;
using Notifications.Application.Otp;
using StackExchange.Redis;

namespace Notifications.Infrastructure.Otp;

/// <summary>
///     Redis-backed OTP store. Verification is a single atomic Lua script so concurrent attempts
///     cannot bypass the failed-attempt cap or consume the same OTP twice — across all instances.
///     No automated test covers this class (no Redis container in CI); the scripts were verified manually
///     against a local Redis instance. Do not "optimize" the scripts — keep them exactly as written.
/// </summary>
internal sealed class RedisOtpService(
    IConnectionMultiplexer redis,
    IOptions<OtpOptions> otpOptionsProvider) : IOtpService
{
    private const int MaxFailedAttempts = 3;

    // KEYS[1] = otp key, ARGV[1] = provided otp, ARGV[2] = max failed attempts
    // Returns: 1 = success, 0 = invalid, 2 = too many attempts
    private const string VerifyScript =
        """
        local otp = redis.call('HGET', KEYS[1], 'otp')
        if not otp then return 0 end
        if otp == ARGV[1] then
            redis.call('DEL', KEYS[1])
            return 1
        end
        local failed = redis.call('HINCRBY', KEYS[1], 'failed', 1)
        if failed >= tonumber(ARGV[2]) then
            redis.call('DEL', KEYS[1])
            return 2
        end
        return 0
        """;

    // KEYS[1] = otp key, ARGV[1] = otp, ARGV[2] = ttl in milliseconds
    // Single atomic EVAL: HSET and PEXPIRE must not be separate round trips — a disconnect between
    // them would leave an OTP key with no TTL (a never-expiring OTP).
    private const string StoreScript =
        """
        redis.call('HSET', KEYS[1], 'otp', ARGV[1], 'failed', 0)
        redis.call('PEXPIRE', KEYS[1], ARGV[2])
        return 1
        """;

    public async Task StoreAsync(string phoneNumber, string otp, string purpose, TimeSpan duration,
        string? contextId, CancellationToken cancellationToken)
    {
        var key = CacheKeys.For.Otp(phoneNumber, purpose, contextId);
        var db = redis.GetDatabase();
        await db.ScriptEvaluateAsync(StoreScript, [(RedisKey)key], [otp, (long)duration.TotalMilliseconds]);
    }

    public async Task<OtpVerificationOutcome> VerifyThenRemoveAsync(string phoneNumber, string otp,
        string purpose, string? contextId, CancellationToken cancellationToken)
    {
        var key = CacheKeys.For.Otp(phoneNumber, purpose, contextId);
        var db = redis.GetDatabase();
        var result = (long)await db.ScriptEvaluateAsync(VerifyScript, [(RedisKey)key], [otp, MaxFailedAttempts]);

        return result switch
        {
            1 => OtpVerificationOutcome.Success,
            2 => OtpVerificationOutcome.TooManyAttempts,
            _ => OtpVerificationOutcome.InvalidOtp,
        };
    }

    public string Generate()
    {
        var length = otpOptionsProvider.Value.Length;

        if (length <= 0)
        {
            throw new ArgumentException("Length must be greater than zero.");
        }

        var otp = new char[length];
        for (var i = 0; i < length; i++)
        {
            otp[i] = (char)('0' + RandomNumberGenerator.GetInt32(0, 10));
        }

        return new string(otp);
    }
}

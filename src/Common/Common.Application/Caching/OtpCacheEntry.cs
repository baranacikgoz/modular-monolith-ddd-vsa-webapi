namespace Common.Application.Caching;

public sealed record OtpCacheEntry(string Otp, int FailedAttempts, DateTimeOffset ExpiresAt);

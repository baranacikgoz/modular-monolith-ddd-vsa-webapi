namespace IAM.Infrastructure.Identity.Services;

internal sealed record OtpCacheEntry(string Otp, int FailedAttempts);

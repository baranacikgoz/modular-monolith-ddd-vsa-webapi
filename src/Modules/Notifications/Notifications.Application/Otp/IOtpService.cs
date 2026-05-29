namespace Notifications.Application.Otp;

public enum OtpVerificationOutcome
{
    Success,
    InvalidOtp,
    TooManyAttempts
}

public interface IOtpService
{
    string Generate();

    Task StoreAsync(string phoneNumber, string otp, string purpose, TimeSpan duration, string? contextId,
        CancellationToken cancellationToken);

    Task<OtpVerificationOutcome> VerifyThenRemoveAsync(string phoneNumber, string otp, string purpose,
        string? contextId, CancellationToken cancellationToken);
}

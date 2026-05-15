using Common.Domain.ResultMonad;

namespace IAM.Application.Otp.Services;

public interface IOtpService
{
    Task StoreOtpAsync(string phoneNumber, string otp, string purpose, TimeSpan duration, CancellationToken cancellationToken);
    Task<Result> VerifyThenRemoveOtpAsync(string phoneNumber, string otp, string purpose, CancellationToken cancellationToken);
    string Generate();
}

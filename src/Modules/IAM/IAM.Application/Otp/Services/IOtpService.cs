using Common.Domain.ResultMonad;

namespace IAM.Application.Otp.Services;

public interface IOtpService
{
    Task StoreOtpAsync(string phoneNumber, string otp, TimeSpan duration, CancellationToken cancellationToken);
    Task<Result> VerifyThenRemoveOtpAsync(string phoneNumber, string otp, CancellationToken cancellationToken);
    string Generate();
}

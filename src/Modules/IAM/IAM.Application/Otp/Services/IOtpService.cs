using Common.Domain.ResultMonad;

namespace IAM.Application.Otp.Services;

public interface IOtpService
{
    Task<Result> VerifyThenRemoveOtpAsync(string phoneNumber, string otp, CancellationToken cancellationToken);
    string Generate();
}

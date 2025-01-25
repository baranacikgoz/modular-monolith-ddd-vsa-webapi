using IAM.Application.Users.Services;

namespace IAM.Infrastructure.Identity.Services;

internal class DummyOtpService : IOtpService
{
    private const string DummyOtp = "123456";

    public string Generate() => DummyOtp;
}

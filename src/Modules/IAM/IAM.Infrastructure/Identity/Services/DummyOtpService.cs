using Common.Application.Caching;

namespace IAM.Infrastructure.Identity.Services;

/// <summary>
///     Test / Development OTP service that always generates a fixed OTP code.
///     Inherits the real cache-based verification logic from <see cref="OtpServiceBase" />
///     so that behaviour stays consistent while only the generation differs.
/// </summary>
internal sealed class DummyOtpService(ICacheService cache) : OtpServiceBase(cache)
{
    private const string DummyOtp = "123456";

    public override string Generate() => DummyOtp;
}

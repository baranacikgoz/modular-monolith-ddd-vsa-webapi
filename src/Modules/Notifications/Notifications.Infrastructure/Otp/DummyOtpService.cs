using ZiggyCreatures.Caching.Fusion;

namespace Notifications.Infrastructure.Otp;

internal sealed class DummyOtpService(IFusionCache cache) : OtpServiceBase(cache)
{
    private const string DummyOtp = "123456";

    public override string Generate() => DummyOtp;
}

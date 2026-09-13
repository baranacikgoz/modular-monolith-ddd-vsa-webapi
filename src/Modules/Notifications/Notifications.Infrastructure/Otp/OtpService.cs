using Common.Application.Options;
using Microsoft.Extensions.Options;
using ZiggyCreatures.Caching.Fusion;

namespace Notifications.Infrastructure.Otp;

internal sealed class OtpService(IOptions<OtpOptions> otpOptionsProvider, IFusionCache cache)
    : OtpServiceBase(cache)
{
    public override string Generate() => OtpCodeGenerator.Generate(otpOptionsProvider.Value);
}

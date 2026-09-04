using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using Common.Application.Options;
using Common.Domain.ResultMonad;
using Microsoft.Extensions.Options;
using Notifications.Application.Sms;
using Notifications.Infrastructure.Telemetry;
using ZiggyCreatures.Caching.Fusion;

namespace Notifications.Infrastructure.Sms;

/// <summary>
/// Decorator over the real <see cref="ISmsGateway"/> enforcing a per-phone-number and a global daily send
/// cap, so a bug or an attacker driving repeated sends cannot run up an unbounded SMS bill. Registered
/// only when <see cref="SmsOptions.Provider"/> is <see cref="SmsProvider.NetGsm"/> (see <see cref="Setup"/>),
/// so every caller of <see cref="ISmsGateway"/> is covered: the cap cannot be bypassed by adding a new caller.
/// </summary>
internal sealed class ThrottledSmsGateway(
    ISmsGateway decoree,
    IFusionCache cache,
    IOptions<SmsOptions> optionsProvider
) : ISmsGateway
{
    public async Task<Result> SendAsync(SmsMessage message, CancellationToken cancellationToken)
    {
        var options = optionsProvider.Value;
        var counterTtl = TimeSpan.FromHours(options.ThrottleCounterTtlHours);
        var today = DateTimeOffset.UtcNow.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
        var phoneKey = $"sms:count:phone:{Hash(message.PhoneNumber)}:{today}";
        var globalKey = $"sms:count:global:{today}";

        var phoneCount = await cache.GetOrDefaultAsync<int>(phoneKey, token: cancellationToken);
        if (phoneCount >= options.MaxPerPhoneNumberPerDay)
        {
            NotificationsTelemetry.RecordSmsThrottled("per_phone_daily_cap");
            return SmsErrors.Throttled;
        }

        var globalCount = await cache.GetOrDefaultAsync<int>(globalKey, token: cancellationToken);
        if (globalCount >= options.MaxPerDay)
        {
            NotificationsTelemetry.RecordSmsThrottled("global_daily_cap");
            return SmsErrors.Throttled;
        }

        var result = await decoree.SendAsync(message, cancellationToken);

        if (!result.IsFailure)
        {
            // ponytail: FusionCache get+set is not atomic across replicas: a race can overshoot a cap
            // by a few sends. Move to Redis INCR/EXPIRE if the overshoot ever costs real money.
            var entryOptions = new FusionCacheEntryOptions { Duration = counterTtl };
            await cache.SetAsync(phoneKey, phoneCount + 1, entryOptions, cancellationToken);
            await cache.SetAsync(globalKey, globalCount + 1, entryOptions, cancellationToken);
        }

        return result;
    }

    private static string Hash(string phoneNumber) =>
        Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(phoneNumber)));
}

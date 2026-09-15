using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using Common.Application.Options;
using Common.Domain.ResultMonad;
using Microsoft.Extensions.Options;
using Notifications.Application.Email;
using Notifications.Infrastructure.Telemetry;
using ZiggyCreatures.Caching.Fusion;

namespace Notifications.Infrastructure.Email;

/// <summary>
/// Decorator over the real <see cref="IEmailGateway"/> enforcing a per-address and a global daily send
/// cap, so a bug or an attacker driving repeated sends cannot run up an unbounded email bill or burn the
/// sender's reputation with the provider. Registered only when <see cref="EmailOptions.Provider"/> is
/// <see cref="EmailProvider.Brevo"/> (see <see cref="Setup"/>), so every caller of <see cref="IEmailGateway"/>
/// is covered: the cap cannot be bypassed by adding a new caller.
/// </summary>
internal sealed class ThrottledEmailGateway(
    IEmailGateway decoree,
    IFusionCache cache,
    IOptions<EmailOptions> optionsProvider
) : IEmailGateway
{
    public async Task<Result> SendAsync(EmailMessage message, CancellationToken cancellationToken)
    {
        var options = optionsProvider.Value;
        var counterTtl = TimeSpan.FromHours(options.ThrottleCounterTtlHours);
        var today = DateTimeOffset.UtcNow.ToString("yyyyMMdd", CultureInfo.InvariantCulture);

        // Callers are expected to already lowercase/trim the address, but this gateway is a shared
        // caching boundary reachable from any caller: hash the normalized form ourselves so the cap
        // cannot be bypassed by casing even if a future caller forgets to normalize. CA1308 wants
        // ToUpperInvariant for security comparisons; here lowercase must match Keycloak's own casing.
#pragma warning disable CA1308
        var normalizedTo = message.To.Trim().ToLowerInvariant();
#pragma warning restore CA1308
        var addressKey = $"email:count:address:{Hash(normalizedTo)}:{today}";
        var globalKey = $"email:count:global:{today}";

        var addressCount = await cache.GetOrDefaultAsync<int>(addressKey, token: cancellationToken);
        if (addressCount >= options.MaxPerAddressPerDay)
        {
            NotificationsTelemetry.RecordEmailThrottled("per_address_daily_cap");
            return EmailErrors.Throttled;
        }

        var globalCount = await cache.GetOrDefaultAsync<int>(globalKey, token: cancellationToken);
        if (globalCount >= options.MaxPerDay)
        {
            NotificationsTelemetry.RecordEmailThrottled("global_daily_cap");
            return EmailErrors.Throttled;
        }

        var result = await decoree.SendAsync(message, cancellationToken);

        if (!result.IsFailure)
        {
            // ponytail: FusionCache get+set is not atomic across replicas: a race can overshoot a cap
            // by a few sends. Move to Redis INCR/EXPIRE if the overshoot ever costs real money.
            var entryOptions = new FusionCacheEntryOptions { Duration = counterTtl };
            await cache.SetAsync(addressKey, addressCount + 1, entryOptions, cancellationToken);
            await cache.SetAsync(globalKey, globalCount + 1, entryOptions, cancellationToken);
        }

        return result;
    }

    private static string Hash(string normalizedAddress) =>
        Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(normalizedAddress)));
}

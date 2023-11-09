using Common.Options;
using Microsoft.AspNetCore.RateLimiting;

namespace Appointments.ModuleSetup.RateLimiting;

public static class Policies
{
    public static IEnumerable<Action<RateLimiterOptions, CustomRateLimitingOptions>> Get()
    {
        yield return SearchAppointmentsPolicy;
    }

    private static void SearchAppointmentsPolicy(RateLimiterOptions rateLimiter, CustomRateLimitingOptions options)
        => rateLimiter
            .AddFixedWindowLimiter(RateLimiting.Constants.SearchAppointments, opt =>
            {
                var searchAppointmentsRateLimiting = options.SearchAppointments ?? throw new InvalidOperationException("Sms rate limiting is null.");
                var permitLimit = searchAppointmentsRateLimiting.Limit;
                var periodInMs = searchAppointmentsRateLimiting.PeriodInMs;

                opt.PermitLimit = permitLimit;
                opt.Window = TimeSpan.FromMilliseconds(periodInMs);

                if (searchAppointmentsRateLimiting.HasQueueLimit)
                {
                    opt.QueueLimit = searchAppointmentsRateLimiting.QueueLimit!.Value;
                }
            });
}

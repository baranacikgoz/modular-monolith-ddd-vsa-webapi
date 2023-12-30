using System.Net;
using System.Threading.RateLimiting;
using Common.Core.Interfaces;
using Common.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace Appointments.ModuleSetup.RateLimiting;

public static class Policies
{
    public static IEnumerable<Action<RateLimiterOptions, CustomRateLimitingOptions>> Get()
    {
        yield return BookAppointmentConcurrencyPolicy;
    }

    private static void BookAppointmentConcurrencyPolicy(RateLimiterOptions rateLimiter, CustomRateLimitingOptions options)
        => rateLimiter
            .AddPolicy<string, BookAppointmentConcurrencyRateLimitingPolicy>(RateLimiting.RateLimitingConstants.BookAppointmentConcurrency);

    private sealed class BookAppointmentConcurrencyRateLimitingPolicy(
        IProblemDetailsFactory problemDetailsFactory,
        IStringLocalizer<BookAppointmentConcurrencyRateLimitingPolicy> localizer
        ) : IRateLimiterPolicy<string>
    {
        public Func<OnRejectedContext, CancellationToken, ValueTask>? OnRejected => async (context, cancellationToken) =>
        {
            var problemDetails = problemDetailsFactory.Create(
                status: StatusCodes.Status409Conflict,
                title: localizer["Aynı randevuyu şu anda başka bir kullanıcı almayı deniyor. Lütfen biraz sonra tekrar deneyin."],
                type: nameof(BookAppointmentConcurrencyRateLimitingPolicy),
                instance: context.HttpContext.Request.Path,
                requestId: context.HttpContext.TraceIdentifier,
                errors: Enumerable.Empty<string>()
            );

            await problemDetails.ExecuteAsync(context.HttpContext);
        };

        public RateLimitPartition<string> GetPartition(HttpContext httpContext)
        {
            var venueId = httpContext.GetRouteValue("venueId")?.ToString()
                ?? throw new InvalidOperationException("venueId is not found in route values.");

            var appointmentDate = httpContext.GetRouteValue("appointmentDate")?.ToString()
                ?? throw new InvalidOperationException("appointmentDate is not found in route values.");

            var key = $"{venueId}-{appointmentDate}";

            return RateLimitPartition.GetConcurrencyLimiter(key, _ => new ConcurrencyLimiterOptions
            {
                PermitLimit = 1,
                QueueLimit = 0
            });
        }
    }
}

using System.Net;
using System.Threading.RateLimiting;
using Common.Infrastructure.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Localization;
using Common.Infrastructure.Extensions;
using Common.Application.Localization;
using Common.Application.Extensions;

namespace Host.Middlewares;

internal static class RateLimitingMiddleware
{
    public static IServiceCollection AddCustomRateLimiting(
        this IServiceCollection services,
        IConfiguration configuration,
        params IEnumerable<Action<RateLimiterOptions, CustomRateLimitingOptions>>[] rateLimitingPoliciesPerModule)
        => services
            .AddRateLimiter(opt =>
            {
                var customRateLimitingOptions = GetCustomRateLimitingOptions(configuration);

                // all individual policies should have their own OnRejected, I guess this is fallback only ??? don't know at the moment.
                opt.OnRejected = WriteTooManyRequestsToResponse();

                opt.GlobalLimiter = GlobalRateLimiter(customRateLimitingOptions);

                // allow each module register their rate limit needs in a decoupled way
                foreach (var policy in rateLimitingPoliciesPerModule.SelectMany(x => x))
                {
                    policy(opt, customRateLimitingOptions);
                }
            });

    private static CustomRateLimitingOptions GetCustomRateLimitingOptions(IConfiguration configuration)
        => configuration
            .GetSection(nameof(CustomRateLimitingOptions))
            .Get<CustomRateLimitingOptions>()
            ?? throw new InvalidOperationException("Custom rate limiting options are null.");
    private static PartitionedRateLimiter<HttpContext> GlobalRateLimiter(CustomRateLimitingOptions rateLimitingOptions)
    {
        return PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: httpContext.GetIpAddress() ?? "N/A",
                        factory: _ =>
                        {
                            var globalRateLimiting = rateLimitingOptions.Global ?? throw new InvalidOperationException("Global rate limiting is null.");
                            var permitLimit = globalRateLimiting.Limit;
                            var periodInMs = globalRateLimiting.PeriodInMs;

                            return new FixedWindowRateLimiterOptions()
                            {
                                PermitLimit = permitLimit,
                                Window = TimeSpan.FromMilliseconds(periodInMs),
                                QueueLimit = globalRateLimiting.QueueLimit
                            };
                        }
                ));
    }

    private static Func<OnRejectedContext, CancellationToken, ValueTask> WriteTooManyRequestsToResponse()
    {
        return (context, _) =>
        {
            var localizer = context.HttpContext.RequestServices.GetRequiredService<IStringLocalizer<ResxLocalizer>>();

            var problemDetailsService = context.HttpContext.RequestServices.GetRequiredService<IProblemDetailsService>();
            var problemDetails = new ProblemDetails()
            {
                Status = (int)HttpStatusCode.TooManyRequests,
                Title = localizer[nameof(HttpStatusCode.TooManyRequests)],
            };

            if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
            {
                problemDetails.Detail = localizer[nameof(MetadataName.RetryAfter), retryAfter];
            }

            problemDetails.AddErrorKey(nameof(HttpStatusCode.TooManyRequests));

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
            return problemDetailsService.WriteAsync(new ProblemDetailsContext()
            {
                HttpContext = context.HttpContext,
                ProblemDetails = problemDetails
            });
        };
    }
}

using System.Net;
using System.Threading.RateLimiting;
using Common.Application.Extensions;
using Common.Application.Localization.Resources;
using Common.Application.Options;
using Common.Infrastructure.Extensions;
using Common.Infrastructure.RateLimiting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Host.Middlewares;

internal static class RateLimitingMiddleware
{
    public static IServiceCollection AddCustomRateLimiting(
        this IServiceCollection services,
        IConfiguration configuration,
        params IEnumerable<Action<RateLimiterOptions, CustomRateLimitingOptions>>[] rateLimitingPoliciesPerModule)
    {
        return services
            .AddRateLimiter(opt =>
            {
                var customRateLimitingOptions = GetCustomRateLimitingOptions(configuration);

                opt.OnRejected = WriteTooManyRequestsToResponse();

                opt.GlobalLimiter = GlobalRateLimiter(customRateLimitingOptions);

                // allow each module register their rate limit needs in a decoupled way
                foreach (var policy in rateLimitingPoliciesPerModule.SelectMany(x => x))
                {
                    policy(opt, customRateLimitingOptions);
                }
            });
    }

    private static CustomRateLimitingOptions GetCustomRateLimitingOptions(IConfiguration configuration)
    {
        return configuration
                   .GetSection(nameof(CustomRateLimitingOptions))
                   .Get<CustomRateLimitingOptions>()
               ?? throw new InvalidOperationException("Custom rate limiting options are null.");
    }

    private static PartitionedRateLimiter<HttpContext> GlobalRateLimiter(CustomRateLimitingOptions rateLimitingOptions)
    {
        return PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        {
            // Authenticated callers get their own bucket (JWT sub), anonymous traffic shares the IP's. Partitioning
            // by IP alone put every user of a server-rendered frontend (one egress IP) into a single bucket.
            var partitionKey = httpContext.GetUserIdOrIpAddress();

            if (IsExempt(httpContext.Request.Path, rateLimitingOptions.ExemptPathPrefixes))
            {
                return RateLimitPartition.GetNoLimiter(partitionKey);
            }

            var globalRateLimiting = rateLimitingOptions.Global ??
                                      throw new InvalidOperationException("Global rate limiting is null.");

            return RateLimitPartitions.FixedWindow(httpContext, "Global", partitionKey, globalRateLimiting);
        });
    }

    private static bool IsExempt(PathString path, IReadOnlyList<string> exemptPathPrefixes)
    {
        for (var i = 0; i < exemptPathPrefixes.Count; i++)
        {
            if (path.StartsWithSegments(exemptPathPrefixes[i], StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }

    // Shape matches ResultToResponseTransformer (Common.Application/EndpointFilters) so every 429 in the
    // API, whether raised here by the ASP.NET rate limiter or from a Result-returning handler that maps
    // its own throttling into the same "TooManyRequests" error key, looks identical on the wire.
    private static Func<OnRejectedContext, CancellationToken, ValueTask> WriteTooManyRequestsToResponse()
    {
        return (context, _) =>
        {
            var httpContext = context.HttpContext;
            var localizer = httpContext.RequestServices.GetRequiredService<IResxLocalizer>();
            var env = httpContext.RequestServices.GetRequiredService<IWebHostEnvironment>();
            var problemDetailsService = httpContext.RequestServices.GetRequiredService<IProblemDetailsService>();

            var problemDetails = new ProblemDetails
            {
                Status = (int)HttpStatusCode.TooManyRequests,
                Title = localizer.TooManyRequests,
                Instance = $"{httpContext.Request.Method} {httpContext.Request.Path.Value}"
            };

            if (httpContext.Response.SetRetryAfterHeader(context.Lease) is { } retryAfter)
            {
                problemDetails.Detail = string.Format(System.Globalization.CultureInfo.CurrentCulture,
                    localizer.RetryAfter, retryAfter);
            }

            problemDetails.AddErrorKey(nameof(HttpStatusCode.TooManyRequests));
            problemDetails.AddErrors(parameterName: null, []);
            problemDetails.Extensions.TryAdd("traceId", httpContext.TraceIdentifier);
            problemDetails.Extensions.TryAdd("environment", env.EnvironmentName);

            httpContext.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
            return problemDetailsService.WriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext, ProblemDetails = problemDetails
            });
        };
    }
}

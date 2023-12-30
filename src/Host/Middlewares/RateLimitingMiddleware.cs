using System.Threading.RateLimiting;
using Common.Core.Contracts;
using Common.Core.Extensions;
using Common.Core.Interfaces;
using Common.Options;
using IdentityAndAuth.Features.Auth.Extensions;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Localization;

namespace Host.Middlewares;

internal static class RateLimitingMiddleware
{
    public static IServiceCollection AddRateLimiting(
        this IServiceCollection services,
        IConfiguration configuration,
        params IEnumerable<Action<RateLimiterOptions, CustomRateLimitingOptions>>[] rateLimitingPoliciesPerModule)

        => services
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
                                QueueLimit = globalRateLimiting.HasQueueLimit ? globalRateLimiting.QueueLimit!.Value : 0
                            };
                        }
                ));
    }

    private static Func<OnRejectedContext, CancellationToken, ValueTask> WriteTooManyRequestsToResponse()
    {
        return (context, _) =>
        {
            var localizer = context.HttpContext.RequestServices.GetRequiredService<IStringLocalizer<Program>>();

            string[]? errors = null;

            if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
            {
                errors = [localizer["{0} sonra tekrar deneyiniz.", retryAfter]];
            }

            var problemDetailsFactory = context.HttpContext.RequestServices.GetRequiredService<IProblemDetailsFactory>();
            var tooManyRequestResult = problemDetailsFactory.Create(
                status: StatusCodes.Status429TooManyRequests,
                title: localizer["Aşırı istek."],
                type: "TooManyRequests",
                instance: context.HttpContext.Request.Path,
                requestId: context.HttpContext.TraceIdentifier,
                errors: errors ?? Enumerable.Empty<string>()
            );

            return new ValueTask(tooManyRequestResult.ExecuteAsync(context.HttpContext));
        };
    }
}

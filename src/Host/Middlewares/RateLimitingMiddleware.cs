using System.Threading.RateLimiting;
using Common.Core.Contracts;
using Common.Options;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Localization;

namespace Host.Middlewares;

public static class RateLimitingMiddleware
{
    public static IServiceCollection AddRateLimiting(
        this IServiceCollection services,
        IConfiguration configuration)

        => services
            .AddRateLimiter(opt =>
            {
                var customRateLimitingOptions = GetCustomRateLimitingOptions(configuration);

                opt.OnRejected = WriteTooManyRequestsToResponse();

                opt.GlobalLimiter = GlobalRateLimiter(customRateLimitingOptions);

                var policies = new[]{
                    IdentityAndAuth.ModuleSetup.RateLimiting.Policies.Get(),
                    Appointments.ModuleSetup.RateLimiting.Policies.Get(),
                }.SelectMany(x => x);

                // allow each module register their rate limit needs in a decoupled way
                foreach (var policy in policies)
                {
                    policy(opt, customRateLimitingOptions);
                }
            });

    private static CustomRateLimitingOptions GetCustomRateLimitingOptions(IConfiguration configuration)
        => configuration
            .GetSection(nameof(CustomRateLimitingOptions))
            .Get<CustomRateLimitingOptions>()
            ?? throw new InvalidOperationException("Custom rate limiting options are null.");

    private static string? GetIp(HttpContext httpContext) // Our app will be running behind a reverse proxy.
        => httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();

    private static PartitionedRateLimiter<HttpContext> GlobalRateLimiter(CustomRateLimitingOptions rateLimitingOptions)
    {
        return PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: GetIp(httpContext) ?? "N/A",
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
        return async (context, _) =>
        {
            var localizer = context.HttpContext.RequestServices.GetRequiredService<IStringLocalizer<Program>>();

            string[]? errors = null;

            if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
            {
                errors = [localizer["{0} sonra tekrar deneyiniz.", retryAfter]];
            }

            var tooManyRequestResult = new CustomProblemDetails()
            {
                Status = StatusCodes.Status429TooManyRequests,
                Title = localizer["Aşırı istek."],
                Type = "TooManyRequests",
                Instance = context.HttpContext.Request.Path,
                RequestId = context.HttpContext.TraceIdentifier,
                Errors = errors ?? Enumerable.Empty<string>()
            };

            await tooManyRequestResult.ExecuteAsync(context.HttpContext);
        };
    }
}

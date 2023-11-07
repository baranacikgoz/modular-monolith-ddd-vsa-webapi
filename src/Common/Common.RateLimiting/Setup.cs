using System.Threading.RateLimiting;
using Common.Core.Contracts;
using Common.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace Common.RateLimiting;

public static partial class Setup
{
    public static IServiceCollection AddRateLimiting(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddRateLimiter(opt =>
            {
                var rateLimitingOptions = GetRateLimitingOptions(configuration);

                opt.OnRejected = WriteTooManyRequestsToResponse();

                opt.GlobalLimiter = GlobalRateLimiter(rateLimitingOptions);

                opt.ApplySmsPolicy(rateLimitingOptions);
            });

    private static RateLimitingOptions GetRateLimitingOptions(IConfiguration configuration)
    {
        return configuration
                .GetSection(nameof(RateLimitingOptions))
                .Get<RateLimitingOptions>()
                ?? throw new InvalidOperationException("RateLimitingOptions is null.");
    }

    private static string? GetIp(HttpContext httpContext) // Our app will be running behind a reverse proxy.
        => httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();

    private static PartitionedRateLimiter<HttpContext> GlobalRateLimiter(RateLimitingOptions rateLimitingOptions)
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
            var localizer = context.HttpContext.RequestServices.GetRequiredService<IStringLocalizer<IDummyInterfaceForStringLocalizer>>();

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

    // String localizer requires an interface as its generic type parameter.
    // If it was injected from a class's constructor, we could have used that class as the generic type parameter.
    // Since we are injecting it from a static class here, I had to create this dummy interface.
    private interface IDummyInterfaceForStringLocalizer { }

}

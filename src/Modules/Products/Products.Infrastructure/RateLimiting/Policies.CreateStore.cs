using System.Net;
using System.Threading.RateLimiting;
using Common.Application.Extensions;
using Common.Application.Options;
using Common.Application.Localization.Resources;
using Common.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Options;

namespace Products.Infrastructure.RateLimiting;

public static partial class Policies
{
    private static void CreateStorePolicy(RateLimiterOptions rateLimiter, CustomRateLimitingOptions options)
    {
        rateLimiter
            .AddPolicy<string, CreateStoreRateLimitingPolicy>(RateLimitingConstants.CreateStore);
    }

    // internal (not private): unit-tested directly from Products.Tests via InternalsVisibleTo.
    internal sealed class CreateStoreRateLimitingPolicy(
        IProblemDetailsService problemDetailsService,
        IResxLocalizer localizer,
        IOptions<CustomRateLimitingOptions> rateLimitingOptionsProvider
    ) : IRateLimiterPolicy<string>
    {
        public Func<OnRejectedContext, CancellationToken, ValueTask>? OnRejected => (context, cancellationToken) =>
        {
            var localizedMessage = context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter)
                ? LocalizedMessage(retryAfter)
                : LocalizedMessage(TimeSpan.FromMilliseconds(rateLimitingOptionsProvider.Value.CreateStore.PeriodInMs));

            var problemDetails = new ProblemDetails
            {
                Status = (int)HttpStatusCode.TooManyRequests,
                Title = localizedMessage,
                Instance = context.HttpContext.Request.Path
            };

            problemDetails.AddErrorKey(nameof(HttpStatusCode.TooManyRequests));

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
            return problemDetailsService.WriteAsync(new ProblemDetailsContext
            {
                HttpContext = context.HttpContext, ProblemDetails = problemDetails
            });
        };

        public RateLimitPartition<string> GetPartition(HttpContext httpContext)
        {
            var partitionKey = httpContext.GetUserIdOrIpAddress();

            return RateLimitPartition.GetFixedWindowLimiter(partitionKey, opt =>
                new FixedWindowRateLimiterOptions
                {
                    Window = TimeSpan.FromMilliseconds(rateLimitingOptionsProvider.Value.CreateStore.PeriodInMs),
                    PermitLimit = rateLimitingOptionsProvider.Value.CreateStore.Limit,
                    QueueLimit = rateLimitingOptionsProvider.Value.CreateStore.QueueLimit
                });
        }

        private string LocalizedMessage(TimeSpan retryAfter)
        {
            return string.Format(System.Globalization.CultureInfo.CurrentCulture,
                localizer.Stores_v1_Create_WaitTime, retryAfter);
        }
    }
}

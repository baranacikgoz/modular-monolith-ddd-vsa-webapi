using System.Net;
using System.Threading.RateLimiting;
using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Application.Options;
using Common.Application.Localization.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Products.Infrastructure.RateLimiting;

public static partial class Policies
{
    private static void CreateStorePolicy(RateLimiterOptions rateLimiter, CustomRateLimitingOptions options)
    {
        rateLimiter
            .AddPolicy<string, CreateStoreRateLimitingPolicy>(RateLimitingConstants.CreateStore);
    }

    private sealed class CreateStoreRateLimitingPolicy(
        IProblemDetailsService problemDetailsService,
        IResxLocalizer localizer,
        IOptions<CustomRateLimitingOptions> rateLimitingOptionsProvider
    ) : IRateLimiterPolicy<string>
    {
        public Func<OnRejectedContext, CancellationToken, ValueTask>? OnRejected => (context, cancellationToken) =>
        {
            var localizedMessage = context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter)
                ? LocalizedMessage(retryAfter)
                : LocalizedMessage(TimeSpan.FromMilliseconds(rateLimitingOptionsProvider.Value.CreateStore!.PeriodInMs!));

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
            var userId = httpContext
                             .RequestServices
                             .GetRequiredService<ICurrentUser>()
                             .IdAsString
                         ?? throw new InvalidOperationException("User is not authenticated.");

            return RateLimitPartition.GetFixedWindowLimiter(userId, opt =>
                new FixedWindowRateLimiterOptions
                {
                    Window = TimeSpan.FromMilliseconds(rateLimitingOptionsProvider.Value.CreateStore!.PeriodInMs!),
                    PermitLimit = rateLimitingOptionsProvider.Value.CreateStore!.Limit!,
                    QueueLimit = rateLimitingOptionsProvider.Value.CreateStore!.QueueLimit!
                });
        }

        private string LocalizedMessage(TimeSpan retryAfter)
        {
            return string.Format(System.Globalization.CultureInfo.CurrentCulture,
                localizer.Stores_v1_Create_WaitTime, retryAfter);
        }
    }
}

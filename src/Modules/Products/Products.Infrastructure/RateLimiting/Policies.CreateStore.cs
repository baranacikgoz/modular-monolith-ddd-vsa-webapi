using System.Net;
using System.Threading.RateLimiting;
using Common.Application.Auth;
using Common.Application.Extensions;
using Common.Infrastructure.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace Products.Infrastructure.RateLimiting;

public static partial class Policies
{
    private static void CreateStorePolicy(RateLimiterOptions rateLimiter, CustomRateLimitingOptions options)
        => rateLimiter
            .AddPolicy<string, CreateStoreRateLimitingPolicy>(RateLimitingConstants.CreateStore);

    private sealed class CreateStoreRateLimitingPolicy(
        IProblemDetailsService problemDetailsService,
        IStringLocalizer<CreateStoreRateLimitingPolicy> localizer,
        IOptions<CustomRateLimitingOptions> rateLimitingOptionsProvider
        ) : IRateLimiterPolicy<string>
    {
        private readonly CustomRateLimitingOptions _rateLimitingOptions = rateLimitingOptionsProvider.Value;
        public Func<OnRejectedContext, CancellationToken, ValueTask>? OnRejected => (context, cancellationToken) =>
        {
            var localizedMessage = context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter)
                ? LocalizedMessage(retryAfter)
                : LocalizedMessage(TimeSpan.FromMilliseconds(_rateLimitingOptions.CreateStore!.PeriodInMs!));

            var problemDetails = new ProblemDetails()
            {
                Status = (int)HttpStatusCode.TooManyRequests,
                Title = localizedMessage,
                Instance = context.HttpContext.Request.Path,
            };

            problemDetails.AddErrorKey(nameof(HttpStatusCode.TooManyRequests));

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
            return problemDetailsService.WriteAsync(new ProblemDetailsContext()
            {
                HttpContext = context.HttpContext,
                ProblemDetails = problemDetails,
            });
        };

        private LocalizedString LocalizedMessage(TimeSpan retryAfter)
            => localizer["En az {0} sonra yeni bir mağaza oluşturabilirsiniz.", retryAfter];
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
                    Window = TimeSpan.FromMilliseconds(_rateLimitingOptions.CreateStore!.PeriodInMs!),
                    PermitLimit = _rateLimitingOptions.CreateStore!.Limit!,
                    QueueLimit = _rateLimitingOptions.CreateStore!.QueueLimit!
                });
        }
    }
}

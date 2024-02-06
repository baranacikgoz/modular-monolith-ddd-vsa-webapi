using System.Net;
using System.Threading.RateLimiting;
using Common.Core.Auth;
using Common.Core.Interfaces;
using Common.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace Sales.ModuleSetup.RateLimiting;

public static partial class Policies
{
    private static void CreateStorePolicy(RateLimiterOptions rateLimiter, CustomRateLimitingOptions options)
        => rateLimiter
            .AddPolicy<string, CreateStoreRateLimitingPolicy>(RateLimiting.RateLimitingConstants.CreateStore);

    private sealed class CreateStoreRateLimitingPolicy(
        IProblemDetailsFactory problemDetailsFactory,
        IStringLocalizer<CreateStoreRateLimitingPolicy> localizer,
        IOptions<CustomRateLimitingOptions> rateLimitingOptionsProvider
        ) : IRateLimiterPolicy<string>
    {
        private readonly CustomRateLimitingOptions _rateLimitingOptions = rateLimitingOptionsProvider.Value;
        public Func<OnRejectedContext, CancellationToken, ValueTask>? OnRejected => async (context, cancellationToken) =>
        {
            var localizedMessage = context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter)
                ? LocalizedMessage(retryAfter)
                : LocalizedMessage(TimeSpan.FromMilliseconds(_rateLimitingOptions.CreateStore!.PeriodInMs!));

            var problemDetails = problemDetailsFactory.Create(
                status: (int)HttpStatusCode.TooManyRequests,
                title: localizedMessage,
                type: nameof(CreateStoreRateLimitingPolicy),
                instance: context.HttpContext.Request.Path,
                requestId: context.HttpContext.TraceIdentifier,
                errors: Enumerable.Empty<string>()
            );

            await problemDetails.ExecuteAsync(context.HttpContext);
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

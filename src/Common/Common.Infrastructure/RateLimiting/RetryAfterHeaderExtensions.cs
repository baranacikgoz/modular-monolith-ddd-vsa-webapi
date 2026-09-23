using System.Globalization;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Http;

namespace Common.Infrastructure.RateLimiting;

public static class RetryAfterHeaderExtensions
{
    /// <summary>
    ///     Writes the HTTP <c>Retry-After</c> header (whole seconds, rounded up, never below 1) from the rejected
    ///     lease's <see cref="MetadataName.RetryAfter" /> and returns that value so the caller can reuse it in the
    ///     problem-details text. Both the in-process <see cref="FixedWindowRateLimiter" /> and
    ///     <see cref="RedisFixedWindowRateLimiter" /> expose the metadata on a rejected lease; the Redis fail-closed
    ///     lease does not, so the return is null and no header is written: callers keep their own fallback text.
    ///     A machine-readable header lets a server-rendered client wait exactly the window instead of guessing.
    /// </summary>
    public static TimeSpan? SetRetryAfterHeader(this HttpResponse response, RateLimitLease lease)
    {
        if (!lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
        {
            return null;
        }

        var seconds = Math.Max(1L, (long)Math.Ceiling(retryAfter.TotalSeconds));
        response.Headers.RetryAfter = seconds.ToString(CultureInfo.InvariantCulture);
        return retryAfter;
    }
}

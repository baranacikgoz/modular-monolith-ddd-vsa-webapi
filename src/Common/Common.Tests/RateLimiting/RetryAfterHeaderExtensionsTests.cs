using System.Threading.RateLimiting;
using Common.Infrastructure.RateLimiting;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace Common.Tests.RateLimiting;

public class RetryAfterHeaderExtensionsTests
{
    [Theory]
    [InlineData(0.2, "1")]
    [InlineData(1.0, "1")]
    [InlineData(1.001, "2")]
    [InlineData(59.5, "60")]
    public void SetRetryAfterHeader_LeaseCarriesRetryAfter_WritesWholeSecondsRoundedUp(double seconds, string expectedHeader)
    {
        var response = new DefaultHttpContext().Response;
        var retryAfter = TimeSpan.FromSeconds(seconds);
        using var lease = new TestLease(retryAfter);

        var returned = response.SetRetryAfterHeader(lease);

        Assert.Equal(retryAfter, returned);
        Assert.Equal(expectedHeader, response.Headers.RetryAfter.ToString());
    }

    [Fact]
    public void SetRetryAfterHeader_LeaseWithoutMetadata_WritesNothingAndReturnsNull()
    {
        var response = new DefaultHttpContext().Response;
        using var lease = new TestLease(retryAfter: null);

        var returned = response.SetRetryAfterHeader(lease);

        Assert.Null(returned);
        Assert.False(response.Headers.ContainsKey("Retry-After"));
    }

    // Mirrors RedisFixedWindowRateLimiter.FixedWindowLease: a rejected lease with, or without (fail-closed), RetryAfter.
    private sealed class TestLease(TimeSpan? retryAfter) : RateLimitLease
    {
        public override bool IsAcquired => false;

        public override IEnumerable<string> MetadataNames => retryAfter is null ? [] : [MetadataName.RetryAfter.Name];

        public override bool TryGetMetadata(string metadataName, out object? metadata)
        {
            if (retryAfter is not null && metadataName == MetadataName.RetryAfter.Name)
            {
                metadata = retryAfter.Value;
                return true;
            }

            metadata = null;
            return false;
        }
    }
}

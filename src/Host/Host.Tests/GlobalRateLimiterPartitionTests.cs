using System.Net;
using System.Net.Http.Headers;

namespace Host.Tests;

// Boots its own host (tight global limit), so it lives in the "Host" collection like DynamicModuleTests:
// a second factory booting in parallel with the shared one corrupts global Serilog/OTel state.
[Collection("Host")]
public class GlobalRateLimiterPartitionTests
{
    [Fact]
    public async Task GlobalLimiter_PartitionsByAuthenticatedUser_NotBySharedIp()
    {
        await using var factory = new HostTestFactory().WithModules("IAM").WithGlobalRateLimit(limit: 2, periodInMs: 60_000);
        await factory.InitializeAsync();

        var userA = ClientFor(factory, Guid.NewGuid());
        var userB = ClientFor(factory, Guid.NewGuid());
        var probe = new Uri("/health/live", UriKind.Relative);

        // Both clients share the test server's single "IP": only a per-user partition keeps B unaffected by A.
        Assert.Equal(HttpStatusCode.OK, (await userA.GetAsync(probe)).StatusCode);
        Assert.Equal(HttpStatusCode.OK, (await userA.GetAsync(probe)).StatusCode);
        using var rejected = await userA.GetAsync(probe);
        Assert.Equal(HttpStatusCode.TooManyRequests, rejected.StatusCode);

        // Machine-readable wait for clients (a server-rendered frontend retries a GET after exactly this), not only
        // the localized ProblemDetails text. The in-process FixedWindowRateLimiter reports the remaining window.
        var retryAfter = rejected.Headers.RetryAfter?.Delta;
        Assert.NotNull(retryAfter);
        Assert.InRange(retryAfter.Value, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(60));

        Assert.Equal(HttpStatusCode.OK, (await userB.GetAsync(probe)).StatusCode);
    }

    private static HttpClient ClientFor(HostTestFactory factory, Guid userId)
    {
        var client = factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "test");
        client.DefaultRequestHeaders.Add("X-Test-User-Id", userId.ToString());
        return client;
    }
}

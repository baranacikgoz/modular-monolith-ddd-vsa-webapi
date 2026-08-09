using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Bogus;
using Common.Application.Auth;
using Common.Application.Caching;
using Common.Domain.StronglyTypedIds;
using Common.Tests;
using IAM.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using ZiggyCreatures.Caching.Fusion;
using Constants = IAM.Domain.Constants;
using CreateTokenRequest = IAM.Endpoints.Tokens.VersionNeutral.Create.Request;
using SelfRegisterRequest = IAM.Endpoints.Users.VersionNeutral.SelfRegister.Request;

namespace IAM.Tests.Endpoints.Tokens;

// Exercises the real JwtBearer pipeline (no TestAuthHandler) end to end, proving OnTokenValidated
// actually rejects a revoked session, including when Redis is unreachable (see issue #133).
[Collection("IamRealAuthCollection")]
public class SessionRevocationTests : BaseIntegrationTest
{
    private readonly Faker _faker = new("tr");
    private readonly IamRealAuthTestFactory _factory;

    public SessionRevocationTests(IamRealAuthTestFactory factory) : base(factory)
    {
        _factory = factory;
    }

    private static async Task EnsureBasicRoleExistsAsync(IServiceScope scope)
    {
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<ApplicationUserId>>>();
        if (!await roleManager.RoleExistsAsync(CustomRoles.Basic))
        {
            await roleManager.CreateAsync(new IdentityRole<ApplicationUserId>(CustomRoles.Basic)
            {
                NormalizedName = CustomRoles.Basic.ToUpperInvariant()
            });
        }
    }

    private string NewPhoneNumber()
    {
        return "905" + _faker.Random.Number(100000000, 999999999).ToString(CultureInfo.InvariantCulture);
    }

    private static async Task SeedOtpAsync(IFusionCache cache, string phoneNumber, string purpose, string otp)
    {
        await cache.SetAsync(
            CacheKeys.For.Otp(phoneNumber, purpose),
            new OtpCacheEntry(otp, 0, DateTimeOffset.UtcNow.AddMinutes(5)),
            new FusionCacheEntryOptions { Duration = TimeSpan.FromMinutes(5) });
    }

    private async Task<string> RegisterAsync(HttpClient client, IFusionCache cache, string phoneNumber, Guid deviceId)
    {
        const string otp = "123456";
        await SeedOtpAsync(cache, phoneNumber, "registration", otp);

        var response = await client.PostAsJsonAsync(new Uri("/users/register/self", UriKind.Relative),
            new SelfRegisterRequest
            {
                PhoneNumber = phoneNumber,
                Otp = otp,
                FullName = _faker.Name.FullName() + " Yılmaz",
                BirthDate = _faker.Date.Past(30).ToString(Constants.TurkishDateFormat, CultureInfo.InvariantCulture),
                CaptchaToken = "dummyToken",
                DeviceId = deviceId,
                ClientId = "mobile-app-1"
            });

        if (!response.IsSuccessStatusCode)
        {
            var err = await response.Content.ReadAsStringAsync();
            Assert.Fail($"Registration failed. Status: {response.StatusCode}. Error: {err}");
        }

        return await ReadAccessTokenAsync(response);
    }

    private async Task<string> LogInAsync(HttpClient client, IFusionCache cache, string phoneNumber, Guid deviceId)
    {
        var otp = _faker.Random.Number(100000, 999999).ToString(CultureInfo.InvariantCulture);
        await SeedOtpAsync(cache, phoneNumber, "login", otp);

        var response = await client.PostAsJsonAsync(new Uri("/tokens", UriKind.Relative), new CreateTokenRequest
        {
            PhoneNumber = phoneNumber,
            Otp = otp,
            DeviceId = deviceId,
            ClientId = "mobile-app-1"
        });

        if (!response.IsSuccessStatusCode)
        {
            var err = await response.Content.ReadAsStringAsync();
            Assert.Fail($"Login failed. Status: {response.StatusCode}. Error: {err}");
        }

        return await ReadAccessTokenAsync(response);
    }

    private static async Task<string> ReadAccessTokenAsync(HttpResponseMessage response)
    {
        var rawJson = await response.Content.ReadAsStringAsync();
        using var doc = System.Text.Json.JsonDocument.Parse(rawJson);
        return doc.RootElement.GetProperty("accessToken").GetString()
               ?? throw new InvalidOperationException("Response had no accessToken.");
    }

    private static void AuthorizedClient(HttpClient client, string accessToken)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
    }

    private static Task<HttpResponseMessage> GetMySessionsAsync(HttpClient client)
    {
        return client.GetAsync(new Uri("/tokens/sessions", UriKind.Relative));
    }

    private static Task<HttpResponseMessage> RevokeOwnSessionAsync(HttpClient client)
    {
        return client.PostAsync(new Uri("/tokens/revoke", UriKind.Relative), null);
    }

    private static async Task<Guid> GetFirstSessionIdAsync(HttpClient client)
    {
        var response = await GetMySessionsAsync(client);
        var rawJson = await response.Content.ReadAsStringAsync();
        using var doc = System.Text.Json.JsonDocument.Parse(rawJson);
        return doc.RootElement.EnumerateArray().First().GetProperty("id").GetGuid();
    }

    [Fact]
    public async Task RevokedSession_WithRealJwt_ReturnsUnauthorized()
    {
        using var scope = Factory.Services.CreateScope();
        await EnsureBasicRoleExistsAsync(scope);
        var cache = scope.ServiceProvider.GetRequiredService<IFusionCache>();

        var client = Factory.CreateClient();
        var accessToken = await RegisterAsync(client, cache, NewPhoneNumber(), Guid.NewGuid());
        AuthorizedClient(client, accessToken);

        Assert.Equal(HttpStatusCode.OK, (await GetMySessionsAsync(client)).StatusCode);

        Assert.Equal(HttpStatusCode.NoContent, (await RevokeOwnSessionAsync(client)).StatusCode);

        var afterRevoke = await GetMySessionsAsync(client);
        Assert.Equal(HttpStatusCode.Unauthorized, afterRevoke.StatusCode);
    }

    [Fact]
    public async Task RevokedSession_WhenRedisUnavailable_StillReturnsUnauthorized()
    {
        using var scope = Factory.Services.CreateScope();
        await EnsureBasicRoleExistsAsync(scope);
        var cache = scope.ServiceProvider.GetRequiredService<IFusionCache>();

        var client = Factory.CreateClient();
        var accessToken = await RegisterAsync(client, cache, NewPhoneNumber(), Guid.NewGuid());
        AuthorizedClient(client, accessToken);

        // Revoke while Redis is healthy: the invalidation handler runs, so no stale cache entry
        // survives. Nothing has read /tokens/sessions yet, so no positive entry was ever cached either.
        Assert.Equal(HttpStatusCode.NoContent, (await RevokeOwnSessionAsync(client)).StatusCode);

        try
        {
            await _factory.StopRedisAsync();

            // L1 has no entry for this session (never read before) and L2 is unreachable: the
            // factory must fall through to Postgres and correctly see the session as revoked,
            // instead of the pre-fix behavior of reading the miss as "not revoked" (issue #133).
            var response = await GetMySessionsAsync(client);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
        finally
        {
            await _factory.StartRedisAsync();
        }
    }

    [Fact]
    public async Task ReactivatedSession_AfterRevoke_IsAcceptedAgain()
    {
        using var scope = Factory.Services.CreateScope();
        await EnsureBasicRoleExistsAsync(scope);
        var cache = scope.ServiceProvider.GetRequiredService<IFusionCache>();

        var phoneNumber = NewPhoneNumber();
        var deviceId = Guid.NewGuid();

        var client = Factory.CreateClient();
        var firstToken = await RegisterAsync(client, cache, phoneNumber, deviceId);
        AuthorizedClient(client, firstToken);

        Assert.Equal(HttpStatusCode.NoContent, (await RevokeOwnSessionAsync(client)).StatusCode);
        Assert.Equal(HttpStatusCode.Unauthorized, (await GetMySessionsAsync(client)).StatusCode);

        // Same (DeviceId, ClientId): reuses and reactivates the same Session row, which clears
        // RevokedAt. Without the V1SessionRefreshedDomainEvent invalidation, the cached "revoked"
        // verdict from the check above would keep rejecting this session until its TTL expires.
        var secondClient = Factory.CreateClient();
        var secondToken = await LogInAsync(secondClient, cache, phoneNumber, deviceId);
        AuthorizedClient(secondClient, secondToken);

        Assert.Equal(HttpStatusCode.OK, (await GetMySessionsAsync(secondClient)).StatusCode);
    }

    [Fact]
    public async Task RevokeAllSessions_InvalidatesEveryCachedSession()
    {
        using var scope = Factory.Services.CreateScope();
        await EnsureBasicRoleExistsAsync(scope);
        var cache = scope.ServiceProvider.GetRequiredService<IFusionCache>();

        var phoneNumber = NewPhoneNumber();

        var clientA = Factory.CreateClient();
        var tokenA = await RegisterAsync(clientA, cache, phoneNumber, Guid.NewGuid());
        AuthorizedClient(clientA, tokenA);

        var clientB = Factory.CreateClient();
        var tokenB = await LogInAsync(clientB, cache, phoneNumber, Guid.NewGuid());
        AuthorizedClient(clientB, tokenB);

        // Populate the cache with a positive verdict for both sessions before revoking either.
        Assert.Equal(HttpStatusCode.OK, (await GetMySessionsAsync(clientA)).StatusCode);
        Assert.Equal(HttpStatusCode.OK, (await GetMySessionsAsync(clientB)).StatusCode);

        Assert.Equal(HttpStatusCode.NoContent,
            (await clientA.DeleteAsync(new Uri("/tokens/sessions", UriKind.Relative))).StatusCode);

        Assert.Equal(HttpStatusCode.Unauthorized, (await GetMySessionsAsync(clientA)).StatusCode);
        Assert.Equal(HttpStatusCode.Unauthorized, (await GetMySessionsAsync(clientB)).StatusCode);
    }

    [Fact]
    public async Task RevokeSession_WithRealJwtAndBasicRole_ReturnsNoContent()
    {
        using var scope = Factory.Services.CreateScope();
        await EnsureBasicRoleExistsAsync(scope);
        var cache = scope.ServiceProvider.GetRequiredService<IFusionCache>();

        var client = Factory.CreateClient();
        var accessToken = await RegisterAsync(client, cache, NewPhoneNumber(), Guid.NewGuid());
        AuthorizedClient(client, accessToken);

        var sessionId = await GetFirstSessionIdAsync(client);

        var response = await client.DeleteAsync(new Uri($"/tokens/sessions/{sessionId}", UriKind.Relative));

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task RevokeAllSessions_WithRealJwtAndBasicRole_ReturnsNoContent()
    {
        using var scope = Factory.Services.CreateScope();
        await EnsureBasicRoleExistsAsync(scope);
        var cache = scope.ServiceProvider.GetRequiredService<IFusionCache>();

        var client = Factory.CreateClient();
        var accessToken = await RegisterAsync(client, cache, NewPhoneNumber(), Guid.NewGuid());
        AuthorizedClient(client, accessToken);

        var response = await client.DeleteAsync(new Uri("/tokens/sessions", UriKind.Relative));

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
}

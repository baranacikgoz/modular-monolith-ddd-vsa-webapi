using System.Globalization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Bogus;
using Common.Application.Caching;
using Common.Tests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;
using Xunit;
using ZiggyCreatures.Caching.Fusion;
using CreateByEmailRequest = IAM.Endpoints.Tokens.VersionNeutral.CreateByEmail.Request;
using CreateRequest = IAM.Endpoints.Tokens.VersionNeutral.Create.Request;

namespace IAM.Tests;

/// <summary>Seed identities from keycloak/realm-modular-monolith.json.</summary>
internal static class SeedUsers
{
    public const string AdminPhone = "901111111111";
    public const string AdminEmail = "admin@modular-monolith.local";
    public const string AdminPassword = "SystemAdmin-Dev-Password-1";
    public const string StaffEmail = "staff@modular-monolith.local";
    public const string StaffPassword = "Staff-Dev-Password-1";
    public const string BasicPhone = "901111111112";
    public const string BasicPhone2 = "901111111113";
    public const string BasicFirstName = "John";
}

internal sealed record TokenPair(
    string AccessToken,
    DateTimeOffset AccessTokenExpiresAt,
    string RefreshToken,
    DateTimeOffset RefreshTokenExpiresAt)
{
    public string SessionId => new JsonWebToken(AccessToken).GetClaim("sid").Value;
    public string Jti => new JsonWebToken(AccessToken).GetClaim("jti").Value;
    public string Subject => new JsonWebToken(AccessToken).Subject;
}

/// <summary>Login helpers shared by the IAM endpoint tests. Every call goes through the real API + real Keycloak.</summary>
internal static class IamTestClient
{
    public const string DefaultClientId = "mobile-app-1";
    private static readonly Faker Faker = new();
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

    public static string NewPhoneNumber()
    {
        return "905" + Faker.Random.Number(100000000, 999999999).ToString(CultureInfo.InvariantCulture);
    }

    public static async Task SeedOtpAsync(IntegrationTestFactory factory, string phoneNumber, string purpose,
        string otp = InProcessSendOtpClient.DummyOtp)
    {
        using var scope = factory.Services.CreateScope();
        var cache = scope.ServiceProvider.GetRequiredService<IFusionCache>();
        await cache.SetAsync(
            CacheKeys.For.Otp(phoneNumber, purpose),
            new OtpCacheEntry(otp, 0, DateTimeOffset.UtcNow.AddMinutes(5)),
            new FusionCacheEntryOptions { Duration = TimeSpan.FromMinutes(5) });
    }

    public static async Task<HttpResponseMessage> LoginByPhoneRawAsync(IntegrationTestFactory factory,
        string phoneNumber, Guid? deviceId = null, string clientId = DefaultClientId, string? deviceName = null,
        string? pushToken = null, string otp = InProcessSendOtpClient.DummyOtp)
    {
        await SeedOtpAsync(factory, phoneNumber, "login");
        var client = factory.CreateClient();
        return await client.PostAsJsonAsync(new Uri("/tokens", UriKind.Relative), new CreateRequest
        {
            PhoneNumber = phoneNumber,
            Otp = otp,
            DeviceId = deviceId ?? Guid.NewGuid(),
            ClientId = clientId,
            DeviceName = deviceName,
            PushToken = pushToken
        });
    }

    public static async Task<TokenPair> LoginByPhoneAsync(IntegrationTestFactory factory, string phoneNumber,
        Guid? deviceId = null, string clientId = DefaultClientId, string? deviceName = null)
    {
        using var response = await LoginByPhoneRawAsync(factory, phoneNumber, deviceId, clientId, deviceName);
        return await ReadTokensAsync(response);
    }

    public static async Task<HttpResponseMessage> LoginByEmailRawAsync(IntegrationTestFactory factory,
        string email, string password, Guid? deviceId = null, string clientId = "web-app-1")
    {
        var client = factory.CreateClient();
        return await client.PostAsJsonAsync(new Uri("/tokens/email", UriKind.Relative), new CreateByEmailRequest
        {
            Email = email,
            Password = password,
            DeviceId = deviceId ?? Guid.NewGuid(),
            ClientId = clientId
        });
    }

    public static async Task<TokenPair> LoginByEmailAsync(IntegrationTestFactory factory, string email, string password)
    {
        using var response = await LoginByEmailRawAsync(factory, email, password);
        return await ReadTokensAsync(response);
    }

    public static async Task<TokenPair> ReadTokensAsync(HttpResponseMessage response)
    {
        var body = await response.Content.ReadAsStringAsync();
        Assert.True(response.IsSuccessStatusCode, $"Status: {response.StatusCode}. Body: {body}");

        return JsonSerializer.Deserialize<TokenPair>(body, JsonOptions)
               ?? throw new InvalidOperationException($"Token response deserialized to null. Body: {body}");
    }

    public static HttpClient Authorized(IntegrationTestFactory factory, TokenPair tokens)
    {
        return Authorized(factory, tokens.AccessToken);
    }

    public static HttpClient Authorized(IntegrationTestFactory factory, string accessToken)
    {
        var client = factory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        return client;
    }
}

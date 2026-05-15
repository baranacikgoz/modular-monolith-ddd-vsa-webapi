using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Bogus;
using Common.Application.Auth;
using Common.Application.Caching;
using Common.Domain.StronglyTypedIds;
using Common.Infrastructure.Persistence.Outbox;
using Common.IntegrationEvents;
using Common.Tests;
using IAM.Application.Persistence;
using IAM.Domain.Identity;
using IAM.Endpoints.Users.VersionNeutral.SelfRegister;
using IAM.Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using ZiggyCreatures.Caching.Fusion;
using Constants = IAM.Domain.Constants;

namespace IAM.Tests.Endpoints.Users;

[Collection("IntegrationTestCollection")]
public class SelfRegisterTests : BaseIntegrationTest
{
    private readonly Faker _faker = new("tr");

    public SelfRegisterTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
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

    [Fact]
    public async Task RegisterAsync_WithValidPayload_ReturnsOkAndCreatesUser()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var cache = scope.ServiceProvider.GetRequiredService<IFusionCache>();
        var db = scope.ServiceProvider.GetRequiredService<IIAMDbContext>();
        await EnsureBasicRoleExistsAsync(scope);

        var phoneNumber = "905" + _faker.Random.Number(100000000, 999999999).ToString(CultureInfo.InvariantCulture);
        var otp = "123456";

        // Pre-seed cache to bypass SMS OTP check
        var cacheKey = CacheKeys.For.Otp(phoneNumber, "registration");
        await cache.SetAsync(cacheKey, new OtpCacheEntry(otp, 0),
            new FusionCacheEntryOptions { Duration = TimeSpan.FromMinutes(5) });

        var client = Factory.CreateClient();
        var request = new Request
        {
            PhoneNumber = phoneNumber,
            Otp = otp,
            FullName = _faker.Name.FullName() + " Yılmaz",
            BirthDate = _faker.Date.Past(30).ToString(Constants.TurkishDateFormat, CultureInfo.InvariantCulture),
            CaptchaToken = "dummyToken"
        };

        // Act
        var response = await client.PostAsJsonAsync(new Uri("/users/register/self", UriKind.Relative), request);

        // Assert
        if (!response.IsSuccessStatusCode)
        {
            var err = await response.Content.ReadAsStringAsync();
            Assert.Fail($"Status: {response.StatusCode}. Error: {err}");
        }

        response.EnsureSuccessStatusCode();

        var rawJson = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(rawJson);
        var root = doc.RootElement;

        Assert.True(root.TryGetProperty("accessToken", out var accessToken));
        Assert.False(string.IsNullOrWhiteSpace(accessToken.GetString()));

        Assert.True(root.TryGetProperty("refreshToken", out var refreshToken));
        Assert.False(string.IsNullOrWhiteSpace(refreshToken.GetString()));

        // Verify Database Side-Effect
        var createdUser = await db.Users.FirstOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber);

        Assert.NotNull(createdUser);
        Assert.Equal(request.PhoneNumber, createdUser.PhoneNumber);
        Assert.Equal(request.FullName.Trim(), createdUser.FullName);

        // Verify Outbox Message was inserted
        using var outboxScope = Factory.Services.CreateScope();
        var outboxDb = outboxScope.ServiceProvider.GetRequiredService<IOutboxDbContext>();
        var outboxMessages = await outboxDb.OutboxMessages
            .TagWith(nameof(SelfRegisterTests))
            .Where(m => !m.IsProcessed)
            .ToListAsync();
        Assert.NotEmpty(outboxMessages);
        Assert.Contains(outboxMessages, m => m.Event is UserRegisteredIntegrationEvent);
    }

    [Fact]
    public async Task SelfRegister_WithDuplicatePhoneNumber_ReturnsError()
    {
        // Arrange — register a user first via userManager so NormalizedUserName is set,
        // then try to register with the same phone via the endpoint.
        using var scope = Factory.Services.CreateScope();
        var cache = scope.ServiceProvider.GetRequiredService<IFusionCache>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        await EnsureBasicRoleExistsAsync(scope);

        var phoneNumber = "905" + _faker.Random.Number(100000000, 999999999).ToString(CultureInfo.InvariantCulture);

        // Seed an existing user via Identity so NormalizedUserName is properly set
        // — the duplicate check relies on the Identity unique index on NormalizedUserName.
        var existingUser = ApplicationUser.Create(
            _faker.Name.FullName(),
            phoneNumber,
            DateOnly.FromDateTime(_faker.Date.Past(30))
        );
        await userManager.CreateAsync(existingUser);

        const string otp = "123456";
        var cacheKey = CacheKeys.For.Otp(phoneNumber, "registration");
        await cache.SetAsync(cacheKey, new OtpCacheEntry(otp, 0),
            new FusionCacheEntryOptions { Duration = TimeSpan.FromMinutes(5) });

        var client = Factory.CreateClient();
        var request = new Request
        {
            PhoneNumber = phoneNumber,
            Otp = otp,
            FullName = _faker.Name.FullName() + " Yılmaz",
            BirthDate = _faker.Date.Past(30).ToString(Constants.TurkishDateFormat, CultureInfo.InvariantCulture),
            CaptchaToken = "dummyToken"
        };

        // Act
        var response = await client.PostAsJsonAsync(new Uri("/users/register/self", UriKind.Relative), request);

        // Assert — duplicate phone must be rejected with a domain-friendly error
        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task SelfRegister_WithInvalidOtp_ReturnsBadRequest()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var cache = scope.ServiceProvider.GetRequiredService<IFusionCache>();
        await EnsureBasicRoleExistsAsync(scope);

        var phoneNumber = "905" + _faker.Random.Number(100000000, 999999999).ToString(CultureInfo.InvariantCulture);
        const string correctOtp = "123456";

        // Seed the CORRECT otp but send a WRONG one
        var cacheKey = CacheKeys.For.Otp(phoneNumber, "registration");
        await cache.SetAsync(cacheKey, new OtpCacheEntry(correctOtp, 0),
            new FusionCacheEntryOptions { Duration = TimeSpan.FromMinutes(5) });

        var client = Factory.CreateClient();
        var request = new Request
        {
            PhoneNumber = phoneNumber,
            Otp = "999999", // wrong OTP
            FullName = _faker.Name.FullName() + " Yılmaz",
            BirthDate = _faker.Date.Past(30).ToString(Constants.TurkishDateFormat, CultureInfo.InvariantCulture),
            CaptchaToken = "dummyToken"
        };

        // Act
        var response = await client.PostAsJsonAsync(new Uri("/users/register/self", UriKind.Relative), request);

        // Assert
        Assert.NotEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task SelfRegister_WithEmptyCaptcha_ReturnsBadRequest()
    {
        // Arrange
        var client = Factory.CreateClient();
        var request = new Request
        {
            PhoneNumber = "905" + _faker.Random.Number(100000000, 999999999).ToString(CultureInfo.InvariantCulture),
            Otp = "123456",
            FullName = _faker.Name.FullName() + " Yılmaz",
            BirthDate = _faker.Date.Past(30).ToString(Constants.TurkishDateFormat, CultureInfo.InvariantCulture),
            CaptchaToken = string.Empty
        };

        // Act
        var response = await client.PostAsJsonAsync(new Uri("/users/register/self", UriKind.Relative), request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task SelfRegister_WithInvalidCaptcha_ReturnsBadRequest()
    {
        // Arrange
        var client = Factory.CreateClient();
        var request = new Request
        {
            PhoneNumber = "905" + _faker.Random.Number(100000000, 999999999).ToString(CultureInfo.InvariantCulture),
            Otp = "123456",
            FullName = _faker.Name.FullName() + " Yılmaz",
            BirthDate = _faker.Date.Past(30).ToString(Constants.TurkishDateFormat, CultureInfo.InvariantCulture),
            CaptchaToken = "invalid-token"
        };

        // Act
        var response = await client.PostAsJsonAsync(new Uri("/users/register/self", UriKind.Relative), request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var rawJson = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(rawJson);
        Assert.Equal("NotHuman", doc.RootElement.GetProperty("errorKey").GetString());
    }
}

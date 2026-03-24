using System.Net.Http.Json;
using Bogus;
using Common.Application.Caching;
using Common.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace IAM.Tests.Endpoints.Users;

[Collection("IntegrationTestCollection")]
public class SelfRegisterTests : BaseIntegrationTest
{
    private readonly Faker _faker = new("tr");

    public SelfRegisterTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task RegisterAsync_WithValidPayload_ReturnsOkAndCreatesUser()
    {

        // Arrange
        using var scope = Factory.Services.CreateScope();
        var cache = scope.ServiceProvider.GetRequiredService<ICacheService>();
        var db = scope.ServiceProvider.GetRequiredService<IAM.Application.Persistence.IIAMDbContext>();
        var roleManager = scope.ServiceProvider.GetRequiredService<Microsoft.AspNetCore.Identity.RoleManager<Microsoft.AspNetCore.Identity.IdentityRole<Common.Domain.StronglyTypedIds.ApplicationUserId>>>();

        if (!await roleManager.RoleExistsAsync(IAM.Application.Auth.CustomRoles.Basic))
        {
            await roleManager.CreateAsync(new Microsoft.AspNetCore.Identity.IdentityRole<Common.Domain.StronglyTypedIds.ApplicationUserId>(IAM.Application.Auth.CustomRoles.Basic) { NormalizedName = IAM.Application.Auth.CustomRoles.Basic.ToUpperInvariant() });
        }

        var phoneNumber = "905" + _faker.Random.Number(100000000, 999999999).ToString(System.Globalization.CultureInfo.InvariantCulture);
        var otp = "123456";

        // Pre-seed cache to bypass SMS OTP check
        var cacheKey = Common.Application.Caching.CacheKeys.For.Otp(phoneNumber);
        await cache.SetAsync(cacheKey, otp, absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(5));

        var client = Factory.CreateClient();
        var request = new IAM.Endpoints.Users.VersionNeutral.SelfRegister.Request
        {
            PhoneNumber = phoneNumber,
            Otp = otp,
            Name = _faker.Name.FirstName(),
            LastName = _faker.Name.LastName(),
            NationalIdentityNumber = _faker.Random.Long(10000000000L, 99999999999L).ToString(System.Globalization.CultureInfo.InvariantCulture),
            BirthDate = _faker.Date.Past(30).ToString(IAM.Domain.Constants.TurkishDateFormat, System.Globalization.CultureInfo.InvariantCulture)
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
        using var doc = System.Text.Json.JsonDocument.Parse(rawJson);
        var root = doc.RootElement;

        Assert.True(root.TryGetProperty("id", out var userIdElement));
        var userIdString = userIdElement.GetString();
        Assert.False(string.IsNullOrWhiteSpace(userIdString));

        // Verify Database Side-Effect
        var parsedGuid = Guid.Parse(userIdString);
        var parsedId = new Common.Domain.StronglyTypedIds.ApplicationUserId(parsedGuid);
        var createdUser = await db.Users.FirstOrDefaultAsync(u => u.Id == parsedId);

        Assert.NotNull(createdUser);
        Assert.Equal(request.PhoneNumber, createdUser.PhoneNumber);
        Assert.Equal(request.NationalIdentityNumber, createdUser.NationalIdentityNumber);
    }
}

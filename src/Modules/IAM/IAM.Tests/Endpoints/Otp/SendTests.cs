using System.Net;
using System.Net.Http.Json;
using Bogus;
using Common.Application.Caching;
using Common.Tests;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace IAM.Tests.Endpoints.Otp;

[Collection("IntegrationTestCollection")]
public class SendTests : BaseIntegrationTest
{
    private readonly Faker _faker = new();

    public SendTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task SendOtp_WithValidPhoneNumber_ReturnsNoContentAndCachesOtp()
    {

        // Arrange
        using var scope = Factory.Services.CreateScope();
        var cache = scope.ServiceProvider.GetRequiredService<ICacheService>();

        var phoneNumber = "905" + _faker.Random.Number(100000000, 999999999).ToString(System.Globalization.CultureInfo.InvariantCulture);

        var client = Factory.CreateClient();
        var request = new IAM.Endpoints.Otp.VersionNeutral.Send.Request
        {
            PhoneNumber = phoneNumber
        };

        // Act
        var response = await client.PostAsJsonAsync(new Uri("/otp", UriKind.Relative), request);

        // Assert
        if (!response.IsSuccessStatusCode)
        {
            var err = await response.Content.ReadAsStringAsync();
            Assert.Fail($"Status: {response.StatusCode}. Error: {err}");
        }
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        // Verify cache Side-Effect
        var cacheKey = Common.Application.Caching.CacheKeys.For.Otp(phoneNumber);
        var cachedOtp = await cache.GetAsync<string>(cacheKey, default);

        Assert.False(string.IsNullOrWhiteSpace(cachedOtp));
    }
}

using System.Net;
using System.Net.Http.Json;
using Bogus;
using Common.Tests;
using Xunit;

namespace IAM.Tests.Endpoints.Otp;

[Collection("IntegrationTestCollection")]
public class SendForEmailTests : BaseIntegrationTest
{
    private readonly Faker _faker = new();

    public SendForEmailTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task SendOtpForEmail_WithValidEmail_ReturnsNoContent()
    {
        var client = Factory.CreateClient();
        var request = new IAM.Endpoints.Otp.VersionNeutral.SendForEmail.Request
        {
            Email = _faker.Internet.Email(),
            CaptchaToken = "dummyToken"
        };

        var response = await client.PostAsJsonAsync(new Uri("/otp/email", UriKind.Relative), request);

        if (!response.IsSuccessStatusCode)
        {
            var err = await response.Content.ReadAsStringAsync();
            Assert.Fail($"Status: {response.StatusCode}. Error: {err}");
        }
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task SendOtpForEmail_UnknownAddress_ReturnsNoContent()
    {
        // No enumeration: send never reveals whether the address is registered, only verify does.
        var client = Factory.CreateClient();
        var request = new IAM.Endpoints.Otp.VersionNeutral.SendForEmail.Request
        {
            Email = "definitely-nobody@modular-monolith.local",
            CaptchaToken = "dummyToken"
        };

        var response = await client.PostAsJsonAsync(new Uri("/otp/email", UriKind.Relative), request);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task SendOtpForEmail_WithInvalidEmailFormat_ReturnsBadRequest()
    {
        var client = Factory.CreateClient();
        var request = new IAM.Endpoints.Otp.VersionNeutral.SendForEmail.Request
        {
            Email = "not-an-email",
            CaptchaToken = "dummyToken"
        };

        var response = await client.PostAsJsonAsync(new Uri("/otp/email", UriKind.Relative), request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task SendOtpForEmail_WithInvalidCaptcha_ReturnsBadRequest()
    {
        var client = Factory.CreateClient();
        var request = new IAM.Endpoints.Otp.VersionNeutral.SendForEmail.Request
        {
            Email = _faker.Internet.Email(),
            CaptchaToken = "invalid-token"
        };

        var response = await client.PostAsJsonAsync(new Uri("/otp/email", UriKind.Relative), request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var rawJson = await response.Content.ReadAsStringAsync();
        using var doc = System.Text.Json.JsonDocument.Parse(rawJson);
        Assert.Equal("NotHuman", doc.RootElement.GetProperty("errorKey").GetString());
    }
}

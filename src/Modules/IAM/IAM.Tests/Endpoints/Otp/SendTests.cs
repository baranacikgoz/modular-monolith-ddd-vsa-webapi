using System.Net;
using System.Net.Http.Json;
using Bogus;
using Common.Tests;
using Xunit;

namespace IAM.Tests.Endpoints.Otp;

[Collection("IntegrationTestCollection")]
public class SendForLoginTests : BaseIntegrationTest
{
    private readonly Faker _faker = new();

    public SendForLoginTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task SendOtpForLogin_WithValidPhoneNumber_ReturnsNoContent()
    {
        var phoneNumber = "905" + _faker.Random.Number(100000000, 999999999).ToString(System.Globalization.CultureInfo.InvariantCulture);

        var client = Factory.CreateClient();
        var request = new IAM.Endpoints.Otp.VersionNeutral.SendForLogin.Request
        {
            PhoneNumber = phoneNumber,
            CaptchaToken = "dummyToken"
        };

        var response = await client.PostAsJsonAsync(new Uri("/otp/login", UriKind.Relative), request);

        if (!response.IsSuccessStatusCode)
        {
            var err = await response.Content.ReadAsStringAsync();
            Assert.Fail($"Status: {response.StatusCode}. Error: {err}");
        }
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task SendOtpForLogin_WithInvalidPhoneFormat_ReturnsBadRequest()
    {
        var client = Factory.CreateClient();
        var request = new IAM.Endpoints.Otp.VersionNeutral.SendForLogin.Request
        {
            PhoneNumber = "123",
            CaptchaToken = "dummyToken"
        };

        var response = await client.PostAsJsonAsync(new Uri("/otp/login", UriKind.Relative), request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task SendOtpForLogin_WithEmptyCaptcha_ReturnsBadRequest()
    {
        var client = Factory.CreateClient();
        var request = new IAM.Endpoints.Otp.VersionNeutral.SendForLogin.Request
        {
            PhoneNumber = "905" + _faker.Random.Number(100000000, 999999999).ToString(System.Globalization.CultureInfo.InvariantCulture),
            CaptchaToken = string.Empty
        };

        var response = await client.PostAsJsonAsync(new Uri("/otp/login", UriKind.Relative), request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task SendOtpForLogin_WithInvalidCaptcha_ReturnsBadRequest()
    {
        var client = Factory.CreateClient();
        var request = new IAM.Endpoints.Otp.VersionNeutral.SendForLogin.Request
        {
            PhoneNumber = "905" + _faker.Random.Number(100000000, 999999999).ToString(System.Globalization.CultureInfo.InvariantCulture),
            CaptchaToken = "invalid-token"
        };

        var response = await client.PostAsJsonAsync(new Uri("/otp/login", UriKind.Relative), request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var rawJson = await response.Content.ReadAsStringAsync();
        using var doc = System.Text.Json.JsonDocument.Parse(rawJson);
        Assert.Equal("NotHuman", doc.RootElement.GetProperty("errorKey").GetString());
    }
}

[Collection("IntegrationTestCollection")]
public class SendForRegistrationTests : BaseIntegrationTest
{
    private readonly Faker _faker = new();

    public SendForRegistrationTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task SendOtpForRegistration_WithValidPhoneNumber_ReturnsNoContent()
    {
        var phoneNumber = "905" + _faker.Random.Number(100000000, 999999999).ToString(System.Globalization.CultureInfo.InvariantCulture);

        var client = Factory.CreateClient();
        var request = new IAM.Endpoints.Otp.VersionNeutral.SendForRegistration.Request
        {
            PhoneNumber = phoneNumber,
            CaptchaToken = "dummyToken"
        };

        var response = await client.PostAsJsonAsync(new Uri("/otp/registration", UriKind.Relative), request);

        if (!response.IsSuccessStatusCode)
        {
            var err = await response.Content.ReadAsStringAsync();
            Assert.Fail($"Status: {response.StatusCode}. Error: {err}");
        }
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task SendOtpForRegistration_WithInvalidPhoneFormat_ReturnsBadRequest()
    {
        var client = Factory.CreateClient();
        var request = new IAM.Endpoints.Otp.VersionNeutral.SendForRegistration.Request
        {
            PhoneNumber = "123",
            CaptchaToken = "dummyToken"
        };

        var response = await client.PostAsJsonAsync(new Uri("/otp/registration", UriKind.Relative), request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task SendOtpForRegistration_WithInvalidCaptcha_ReturnsBadRequest()
    {
        var client = Factory.CreateClient();
        var request = new IAM.Endpoints.Otp.VersionNeutral.SendForRegistration.Request
        {
            PhoneNumber = "905" + _faker.Random.Number(100000000, 999999999).ToString(System.Globalization.CultureInfo.InvariantCulture),
            CaptchaToken = "invalid-token"
        };

        var response = await client.PostAsJsonAsync(new Uri("/otp/registration", UriKind.Relative), request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var rawJson = await response.Content.ReadAsStringAsync();
        using var doc = System.Text.Json.JsonDocument.Parse(rawJson);
        Assert.Equal("NotHuman", doc.RootElement.GetProperty("errorKey").GetString());
    }
}

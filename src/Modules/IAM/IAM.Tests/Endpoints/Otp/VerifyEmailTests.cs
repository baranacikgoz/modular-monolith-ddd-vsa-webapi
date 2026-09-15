using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Bogus;
using Common.Tests;
using Xunit;

namespace IAM.Tests.Endpoints.Otp;

[Collection("IntegrationTestCollection")]
public class VerifyEmailTests : BaseIntegrationTest
{
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };
    private readonly Faker _faker = new();

    public VerifyEmailTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    private sealed record VerifyResponseDto(bool IsRegistered, string EmailVerificationToken);

    // Bogus's Internet.Email() is not guaranteed lowercase; the endpoint normalizes (trim + lowercase)
    // before touching the cache, so tests that seed the cache directly must match that exactly.
#pragma warning disable CA1308
    private string NewEmail() => _faker.Internet.Email().Trim().ToLowerInvariant();
#pragma warning restore CA1308

    [Fact]
    public async Task VerifyEmailOtp_CorrectCodeAndUnknownEmail_ReturnsIsRegisteredFalseWithToken()
    {
        var email = NewEmail();
        await IamTestClient.SeedOtpAsync(Factory, email, "email_verification");

        var client = Factory.CreateClient();
        var response = await client.PostAsJsonAsync(new Uri("/otp/email/verify", UriKind.Relative),
            new IAM.Endpoints.Otp.VersionNeutral.VerifyEmail.Request { Email = email, Otp = InProcessSendOtpClient.DummyOtp });

        var body = await response.Content.ReadAsStringAsync();
        Assert.True(response.IsSuccessStatusCode, $"Status: {response.StatusCode}. Body: {body}");
        var dto = JsonSerializer.Deserialize<VerifyResponseDto>(body, JsonOptions)!;
        Assert.False(dto.IsRegistered);
        Assert.False(string.IsNullOrWhiteSpace(dto.EmailVerificationToken));
    }

    [Fact]
    public async Task VerifyEmailOtp_CorrectCodeAndRegisteredEmail_ReturnsIsRegisteredTrueWithToken()
    {
        await IamTestClient.SeedOtpAsync(Factory, SeedUsers.StaffEmail, "email_verification");

        var client = Factory.CreateClient();
        var response = await client.PostAsJsonAsync(new Uri("/otp/email/verify", UriKind.Relative),
            new IAM.Endpoints.Otp.VersionNeutral.VerifyEmail.Request
            {
                Email = SeedUsers.StaffEmail,
                Otp = InProcessSendOtpClient.DummyOtp
            });

        var body = await response.Content.ReadAsStringAsync();
        Assert.True(response.IsSuccessStatusCode, $"Status: {response.StatusCode}. Body: {body}");
        var dto = JsonSerializer.Deserialize<VerifyResponseDto>(body, JsonOptions)!;
        Assert.True(dto.IsRegistered);
        Assert.False(string.IsNullOrWhiteSpace(dto.EmailVerificationToken));
    }

    [Fact]
    public async Task VerifyEmailOtp_MixedCaseEmail_MatchesLowercasedRegisteredUser()
    {
        var mixedCase = "STAFF@Modular-Monolith.Local";
#pragma warning disable CA1308
        var normalized = mixedCase.Trim().ToLowerInvariant();
#pragma warning restore CA1308
        await IamTestClient.SeedOtpAsync(Factory, normalized, "email_verification");

        var client = Factory.CreateClient();
        var response = await client.PostAsJsonAsync(new Uri("/otp/email/verify", UriKind.Relative),
            new IAM.Endpoints.Otp.VersionNeutral.VerifyEmail.Request { Email = mixedCase, Otp = InProcessSendOtpClient.DummyOtp });

        var body = await response.Content.ReadAsStringAsync();
        Assert.True(response.IsSuccessStatusCode, $"Status: {response.StatusCode}. Body: {body}");
        var dto = JsonSerializer.Deserialize<VerifyResponseDto>(body, JsonOptions)!;
        Assert.True(dto.IsRegistered);
    }

    [Fact]
    public async Task VerifyEmailOtp_WrongCode_Returns400()
    {
        var email = NewEmail();
        await IamTestClient.SeedOtpAsync(Factory, email, "email_verification");

        var client = Factory.CreateClient();
        var response = await client.PostAsJsonAsync(new Uri("/otp/email/verify", UriKind.Relative),
            new IAM.Endpoints.Otp.VersionNeutral.VerifyEmail.Request { Email = email, Otp = "000000" });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task VerifyEmailOtp_SameCodeTwice_SecondAttemptFails()
    {
        var email = NewEmail();
        await IamTestClient.SeedOtpAsync(Factory, email, "email_verification");
        var request = new IAM.Endpoints.Otp.VersionNeutral.VerifyEmail.Request
        {
            Email = email,
            Otp = InProcessSendOtpClient.DummyOtp
        };

        var client = Factory.CreateClient();
        using var first = await client.PostAsJsonAsync(new Uri("/otp/email/verify", UriKind.Relative), request);
        Assert.True(first.IsSuccessStatusCode);

        using var second = await client.PostAsJsonAsync(new Uri("/otp/email/verify", UriKind.Relative), request);
        Assert.Equal(HttpStatusCode.BadRequest, second.StatusCode);
    }
}

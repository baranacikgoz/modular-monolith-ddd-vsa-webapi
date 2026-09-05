using System.Net;
using System.Net.Http.Json;
using Common.Application.Auth;
using Common.Tests;
using IAM.Application.Keycloak;
using IAM.Endpoints.Users.VersionNeutral.SelfRegister;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace IAM.Tests.Endpoints.Users;

[Collection("IntegrationTestCollection")]
public class SelfRegisterTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    internal static async Task<HttpResponseMessage> RegisterRawAsync(IntegrationTestFactory factory, string phone,
        string firstName = "Ayşe", string lastName = "Yılmaz", string birthDate = "20-06-2001",
        string otp = InProcessSendOtpClient.DummyOtp, string clientId = "mobile-app-1")
    {
        await IamTestClient.SeedOtpAsync(factory, phone, "registration");
        var client = factory.CreateClient();
        return await client.PostAsJsonAsync(new Uri("/users/register/self", UriKind.Relative), new Request
        {
            PhoneNumber = phone,
            Otp = otp,
            FirstName = firstName,
            LastName = lastName,
            BirthDate = birthDate,
            CaptchaToken = "dummyToken",
            DeviceId = Guid.NewGuid(),
            ClientId = clientId,
            DeviceName = "Test device"
        });
    }

    [Fact]
    public async Task Register_ValidRequest_CreatesKeycloakUserWithBasicRoleAndSignsIn()
    {
        var phone = IamTestClient.NewPhoneNumber();

        using var response = await RegisterRawAsync(Factory, phone, firstName: "Mehmet", lastName: "Kaya");
        var tokens = await IamTestClient.ReadTokensAsync(response);

        var adminClient = Scope.ServiceProvider.GetRequiredService<IKeycloakAdminClient>();
        var user = await adminClient.FindUserByUsernameAsync(phone, CancellationToken.None);
        Assert.NotNull(user);
        Assert.Equal("Mehmet", user.FirstName);
        Assert.Equal("Kaya", user.LastName);
        Assert.Equal(phone, user.PhoneNumber);
        Assert.Equal(new DateOnly(2001, 6, 20), user.BirthDate);
        Assert.Equal(user.Id.Value.ToString(), tokens.Subject);

        var me = await IamTestClient.Authorized(Factory, tokens)
            .GetFromJsonAsync<MeGetTests.MeResponse>(new Uri("/users/me", UriKind.Relative));
        Assert.NotNull(me);
        Assert.Contains(KeycloakRoles.Basic, me.Roles);
        Assert.Contains(KeycloakScopes.Stores.CreateOwn, me.Permissions);
    }

    [Fact]
    public async Task Register_PhoneAlreadyRegistered_Returns409()
    {
        using var response = await RegisterRawAsync(Factory, SeedUsers.BasicPhone);

        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }

    [Fact]
    public async Task Register_WrongOtp_Returns400()
    {
        using var response = await RegisterRawAsync(Factory, IamTestClient.NewPhoneNumber(), otp: "999999");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [InlineData("", "Yılmaz", "20-06-2001")]
    [InlineData("Ayşe", "", "20-06-2001")]
    [InlineData("Ayşe", "Yılmaz", "2001-06-20")]
    [InlineData("Ay5e", "Yılmaz", "20-06-2001")]
    public async Task Register_InvalidNamesOrBirthDate_Returns400(string firstName, string lastName, string birthDate)
    {
        using var response = await RegisterRawAsync(Factory, IamTestClient.NewPhoneNumber(), firstName, lastName, birthDate);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}

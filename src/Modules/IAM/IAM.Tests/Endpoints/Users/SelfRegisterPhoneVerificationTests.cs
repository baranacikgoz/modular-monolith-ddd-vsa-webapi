using Common.Tests;
using IAM.Application.Keycloak;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace IAM.Tests.Endpoints.Users;

#pragma warning disable CA1707 // Remove the underscores from member name

[Collection("IntegrationTestCollection")]
public class SelfRegisterPhoneVerificationTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task Register_ByPhoneOtp_MarksThePhoneNumberVerified()
    {
        var phone = IamTestClient.NewPhoneNumber();

        using var response = await SelfRegisterTests.RegisterRawAsync(Factory, phone);
        await IamTestClient.ReadTokensAsync(response);

        var adminClient = Scope.ServiceProvider.GetRequiredService<IKeycloakAdminClient>();
        var user = await adminClient.FindUserByUsernameAsync(phone, CancellationToken.None);
        Assert.Equal("true", await KeycloakUserAttributeReader.PhoneNumberVerifiedAsync(factory, Scope.ServiceProvider, user!.Id));
    }
}

#pragma warning restore CA1707

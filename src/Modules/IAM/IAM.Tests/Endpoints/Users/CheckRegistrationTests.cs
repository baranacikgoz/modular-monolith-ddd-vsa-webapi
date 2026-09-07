using System.Net.Http.Json;
using Common.Tests;
using Xunit;

namespace IAM.Tests.Endpoints.Users;

[Collection("IntegrationTestCollection")]
public class CheckRegistrationTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private sealed record Response(bool IsRegistered);

    [Fact]
    public async Task CheckRegistration_RegisteredPhone_ReturnsTrue()
    {
        var response = await Factory.CreateClient()
            .GetFromJsonAsync<Response>(new Uri($"/users/check-registration?phoneNumber={SeedUsers.BasicPhone}", UriKind.Relative));

        Assert.NotNull(response);
        Assert.True(response.IsRegistered);
    }

    [Fact]
    public async Task CheckRegistration_UnknownPhone_ReturnsFalse()
    {
        var response = await Factory.CreateClient()
            .GetFromJsonAsync<Response>(new Uri($"/users/check-registration?phoneNumber={IamTestClient.NewPhoneNumber()}", UriKind.Relative));

        Assert.NotNull(response);
        Assert.False(response.IsRegistered);
    }
}

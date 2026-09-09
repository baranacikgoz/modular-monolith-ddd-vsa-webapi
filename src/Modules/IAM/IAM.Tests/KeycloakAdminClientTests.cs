using System.Net;
using Common.Tests;
using IAM.Application.Keycloak;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace IAM.Tests;

#pragma warning disable CA1707 // Remove the underscores from member name

/// <summary>
///     <see cref="IKeycloakAdminClient.CreateUserAsync" /> against the real Keycloak Testcontainer, exercising the
///     email + password path no endpoint uses yet (a fork wires it up, e.g. an admin-approval registration flow).
/// </summary>
[Collection("IntegrationTestCollection")]
public class KeycloakAdminClientTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task CreateUserAsync_WithEmailAndPassword_CreatesUserThatCanLoginByEmail()
    {
        var adminClient = Scope.ServiceProvider.GetRequiredService<IKeycloakAdminClient>();
        var phone = IamTestClient.NewPhoneNumber();
        var email = $"{Guid.NewGuid():N}@example.com";
        const string password = "A-Valid-Password-1";

        var result = await adminClient.CreateUserAsync(
            new CreateKeycloakUser(
                Username: phone,
                FirstName: "Ayşe",
                LastName: "Yılmaz",
                PhoneNumber: phone,
                BirthDate: new DateOnly(2001, 6, 20),
                Email: email,
                Password: password),
            CancellationToken.None);

        Assert.False(result.IsFailure);

        var tokens = await IamTestClient.LoginByEmailAsync(Factory, email, password);
        Assert.Equal(result.Value.ToString(), tokens.Subject);
    }

    [Fact]
    public async Task CreateUserAsync_PasswordViolatesPolicy_ReturnsRejectedUser()
    {
        var adminClient = Scope.ServiceProvider.GetRequiredService<IKeycloakAdminClient>();
        var phone = IamTestClient.NewPhoneNumber();

        var result = await adminClient.CreateUserAsync(
            new CreateKeycloakUser(
                Username: phone,
                FirstName: "Ayşe",
                LastName: "Yılmaz",
                PhoneNumber: phone,
                BirthDate: new DateOnly(2001, 6, 20),
                Email: $"{Guid.NewGuid():N}@example.com",
                Password: "short"),
            CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.NotNull(result.Error);
        Assert.Equal(HttpStatusCode.BadRequest, result.Error.StatusCode);
    }
}

#pragma warning restore CA1707

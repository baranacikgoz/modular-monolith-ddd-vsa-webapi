using Common.Application.Auth;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.IAM;
using Common.Tests;
using IAM.Application.Keycloak;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace IAM.Tests.InterModuleRequestHandlers;

#pragma warning disable CA1707 // Remove the underscores from member name

[Collection("IntegrationTestCollection")]
public class ProvisionUserRequestHandlerTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private const string ValidPassword = "A-Valid-Password-1";

    [Fact]
    public async Task ProvisionUser_ValidRequest_CreatesUserWithOnlyTheBasicRole()
    {
        var phone = IamTestClient.NewPhoneNumber();
        var email = $"{Guid.NewGuid():N}@example.com";

        var response = await SendAsync(new ProvisionUserRequest(email, phone, ValidPassword, "Ada", "Lovelace"));

        Assert.Equal(ProvisionUserFailureReason.None, response.FailureReason);
        Assert.NotNull(response.UserId);
        var adminClient = Scope.ServiceProvider.GetRequiredService<IKeycloakAdminClient>();
        var user = await adminClient.FindUserByUsernameAsync(phone, CancellationToken.None);
        Assert.NotNull(user);
        Assert.Equal(response.UserId, user.Id);
        Assert.Equal(email, user.Email);
        Assert.Contains(user.Id, await adminClient.GetUserIdsInRoleAsync(KeycloakRoles.Basic, 1000, CancellationToken.None));
        Assert.DoesNotContain(user.Id, await adminClient.GetUserIdsInRoleAsync(KeycloakRoles.Staff, 1000, CancellationToken.None));
    }

    [Fact]
    public async Task ProvisionUser_PhoneNumberAlreadyRegistered_ReportsThePhoneNumber()
    {
        var response = await SendAsync(new ProvisionUserRequest(
            $"{Guid.NewGuid():N}@example.com", SeedUsers.BasicPhone, ValidPassword, "Ada", "Lovelace"));

        Assert.Equal(ProvisionUserFailureReason.PhoneNumberAlreadyRegistered, response.FailureReason);
        Assert.Null(response.UserId);
    }

    [Fact]
    public async Task ProvisionUser_EmailAlreadyRegistered_ReportsTheEmail()
    {
        var email = $"{Guid.NewGuid():N}@example.com";
        await SendAsync(new ProvisionUserRequest(email, IamTestClient.NewPhoneNumber(), ValidPassword, "Ada", "Lovelace"));

        var response = await SendAsync(new ProvisionUserRequest(
            email, IamTestClient.NewPhoneNumber(), ValidPassword, "Grace", "Hopper"));

        Assert.Equal(ProvisionUserFailureReason.EmailAlreadyRegistered, response.FailureReason);
        Assert.Null(response.UserId);
    }

    [Fact]
    public async Task ProvisionUser_PasswordViolatesPolicy_ReportsRejectedWithTheIdentityProviderMessage()
    {
        var response = await SendAsync(new ProvisionUserRequest(
            $"{Guid.NewGuid():N}@example.com", IamTestClient.NewPhoneNumber(), "short", "Ada", "Lovelace"));

        Assert.Equal(ProvisionUserFailureReason.Rejected, response.FailureReason);
        Assert.Null(response.UserId);
        Assert.False(string.IsNullOrWhiteSpace(response.RejectionMessage));
    }

    [Fact]
    public async Task ProvisionUser_RoleAssignmentFails_ReportsUnavailableAndLeavesNoUserBehind()
    {
        var phone = IamTestClient.NewPhoneNumber();
        FaultInjectingKeycloakAdminClient.FailNextRoleAssignmentFor(phone);

        var response = await SendAsync(new ProvisionUserRequest(
            $"{Guid.NewGuid():N}@example.com", phone, ValidPassword, "Ada", "Lovelace"));

        Assert.Equal(ProvisionUserFailureReason.IdentityProviderUnavailable, response.FailureReason);
        Assert.Null(response.UserId);
        var adminClient = Scope.ServiceProvider.GetRequiredService<IKeycloakAdminClient>();
        Assert.Null(await adminClient.FindUserByUsernameAsync(phone, CancellationToken.None));
    }

    private Task<ProvisionUserResponse> SendAsync(ProvisionUserRequest request)
    {
        var client = Scope.ServiceProvider
            .GetRequiredService<IInterModuleRequestClient<ProvisionUserRequest, ProvisionUserResponse>>();
        return client.SendAsync(request, CancellationToken.None);
    }
}

#pragma warning restore CA1707

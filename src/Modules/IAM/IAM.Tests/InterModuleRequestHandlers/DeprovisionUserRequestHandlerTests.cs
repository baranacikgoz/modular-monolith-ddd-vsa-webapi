using Common.Domain.StronglyTypedIds;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.IAM;
using Common.Tests;
using IAM.Application.Keycloak;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace IAM.Tests.InterModuleRequestHandlers;

#pragma warning disable CA1707 // Remove the underscores from member name

[Collection("IntegrationTestCollection")]
public class DeprovisionUserRequestHandlerTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task DeprovisionUser_ExistingUser_DeletesTheKeycloakUser()
    {
        var phone = IamTestClient.NewPhoneNumber();
        var userId = await ProvisionAsync(phone);

        await DeprovisionAsync(userId);

        var adminClient = Scope.ServiceProvider.GetRequiredService<IKeycloakAdminClient>();
        Assert.Null(await adminClient.FindUserByUsernameAsync(phone, CancellationToken.None));
    }

    [Fact]
    public async Task DeprovisionUser_UserAlreadyDeleted_IsSuccessful()
    {
        var userId = await ProvisionAsync(IamTestClient.NewPhoneNumber());
        await DeprovisionAsync(userId);

        var exception = await Record.ExceptionAsync(() => DeprovisionAsync(userId));

        Assert.Null(exception);
    }

    [Fact]
    public async Task DeprovisionUser_UserNeverExisted_IsSuccessful()
    {
        var exception = await Record.ExceptionAsync(() => DeprovisionAsync(new ApplicationUserId(Guid.NewGuid())));

        Assert.Null(exception);
    }

    private async Task<ApplicationUserId> ProvisionAsync(string phone)
    {
        var client = Scope.ServiceProvider
            .GetRequiredService<IInterModuleRequestClient<ProvisionUserRequest, ProvisionUserResponse>>();
        var provisioned = await client.SendAsync(
            ProvisionUserTestRequests.Create(Scope.ServiceProvider, $"{Guid.NewGuid():N}@example.com", phone, "A-Valid-Password-1"),
            CancellationToken.None);

        return provisioned.UserId!.Value;
    }

    private Task<DeprovisionUserResponse> DeprovisionAsync(ApplicationUserId userId)
    {
        var client = Scope.ServiceProvider
            .GetRequiredService<IInterModuleRequestClient<DeprovisionUserRequest, DeprovisionUserResponse>>();
        return client.SendAsync(new DeprovisionUserRequest(userId), CancellationToken.None);
    }
}

#pragma warning restore CA1707

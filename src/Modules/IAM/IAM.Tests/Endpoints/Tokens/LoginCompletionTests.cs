using Common.Domain.StronglyTypedIds;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Notifications;
using IAM.Application.Keycloak;
using IAM.Endpoints.Tokens;
using MassTransit;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace IAM.Tests.Endpoints.Tokens;

public class LoginCompletionTests
{
    [Fact]
    public async Task BindDeviceAsync_DeviceRegistryUnavailable_ReturnsTokensWithoutRevokingAnything()
    {
        var now = DateTimeOffset.UtcNow;
        var tokens = new KeycloakTokens("access", now.AddMinutes(5), "refresh", now.AddDays(1), ApplicationUserId.New(), "sid-1");
        var deviceClient = Substitute.For<IInterModuleRequestClient<BindDeviceSessionRequest, BindDeviceSessionResponse>>();
        deviceClient.SendAsync(Arg.Any<BindDeviceSessionRequest>(), Arg.Any<CancellationToken>())
            .ThrowsAsync(new RequestTimeoutException("bind-device"));
        var adminClient = Substitute.For<IKeycloakAdminClient>();

        var result = await LoginCompletion.BindDeviceAsync(
            tokens, Guid.NewGuid(), "mobile-app-1", deviceName: null, pushToken: null,
            deviceClient, adminClient, NullLogger.Instance, CancellationToken.None);

        Assert.False(result.IsFailure);
        Assert.Same(tokens, result.Value);
        await adminClient.DidNotReceiveWithAnyArgs().DeleteSessionAsync(default!, default);
    }

    [Fact]
    public async Task BindDeviceAsync_DeviceRegistryFaulted_ReturnsTokens()
    {
        var now = DateTimeOffset.UtcNow;
        var tokens = new KeycloakTokens("access", now.AddMinutes(5), "refresh", now.AddDays(1), ApplicationUserId.New(), "sid-2");
        var deviceClient = Substitute.For<IInterModuleRequestClient<BindDeviceSessionRequest, BindDeviceSessionResponse>>();
        deviceClient.SendAsync(Arg.Any<BindDeviceSessionRequest>(), Arg.Any<CancellationToken>())
            .ThrowsAsync(new RequestException("handler faulted"));

        var result = await LoginCompletion.BindDeviceAsync(
            tokens, Guid.NewGuid(), "mobile-app-1", deviceName: null, pushToken: null,
            deviceClient, Substitute.For<IKeycloakAdminClient>(), NullLogger.Instance, CancellationToken.None);

        Assert.Same(tokens, result.Value);
    }
}

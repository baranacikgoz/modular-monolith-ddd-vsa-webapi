using System.Globalization;
using Common.Application.Options;
using Common.Domain.StronglyTypedIds;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Notifications;
using Common.Tests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Notifications.Application.Push;
using NSubstitute;
using Xunit;

namespace Notifications.Tests.InterModuleRequestHandlers;

[Collection("IntegrationTestCollection")]
public sealed class SendSecurityAlertRequestHandlerTests(NotificationsTestFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task SendSecurityAlert_UserHasAnActivePushToken_SendsOneMulticastPush()
    {
        // IPushGateway is a singleton substitute shared by every test in this collection: clear its call
        // log first so an earlier test's send does not leak into this assertion.
        var pushGateway = Scope.ServiceProvider.GetRequiredService<IPushGateway>();
        pushGateway.ClearReceivedCalls();

        var userId = ApplicationUserId.New();
        var bindClient = Scope.ServiceProvider
            .GetRequiredService<IInterModuleRequestClient<BindDeviceSessionRequest, BindDeviceSessionResponse>>();
        await bindClient.SendAsync(
            new BindDeviceSessionRequest(userId, "sid-1", Guid.NewGuid(), "mobile-app-1", "Pixel", "fcm-1"),
            CancellationToken.None);

        var alertClient = Scope.ServiceProvider
            .GetRequiredService<IInterModuleRequestClient<SendSecurityAlertRequest, SendSecurityAlertResponse>>();
        await alertClient.SendAsync(
            new SendSecurityAlertRequest(userId, SecurityAlertType.SessionRevokedTokenReuse), CancellationToken.None);

        await pushGateway.Received(1).SendAsync(
            Arg.Is<PushMessage>(m => m.Tokens.Count == 1 && m.Tokens[0] == "fcm-1"),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task SendSecurityAlert_UserHasAnActivePushToken_UsesTheTemplateOfTheConfiguredDefaultCulture()
    {
        var pushGateway = Scope.ServiceProvider.GetRequiredService<IPushGateway>();
        pushGateway.ClearReceivedCalls();
        var defaultCulture = Scope.ServiceProvider.GetRequiredService<IOptions<ResxLocalizationOptions>>().Value.DefaultCulture;
        var expected = Scope.ServiceProvider.GetRequiredService<IOptions<PushOptions>>().Value.Templates.SecurityAlert[
            new CultureInfo(defaultCulture).TwoLetterISOLanguageName];

        var userId = ApplicationUserId.New();
        var bindClient = Scope.ServiceProvider
            .GetRequiredService<IInterModuleRequestClient<BindDeviceSessionRequest, BindDeviceSessionResponse>>();
        await bindClient.SendAsync(
            new BindDeviceSessionRequest(userId, "sid-2", Guid.NewGuid(), "mobile-app-2", "Pixel", "fcm-2"),
            CancellationToken.None);
        var alertClient = Scope.ServiceProvider
            .GetRequiredService<IInterModuleRequestClient<SendSecurityAlertRequest, SendSecurityAlertResponse>>();

        await alertClient.SendAsync(
            new SendSecurityAlertRequest(userId, SecurityAlertType.SessionRevokedTokenReuse), CancellationToken.None);

        await pushGateway.Received(1).SendAsync(
            Arg.Is<PushMessage>(m => m.Title == expected.Title && m.Body == expected.Body),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task SendSecurityAlert_UserHasNoActiveDevices_SendsNoPush()
    {
        var pushGateway = Scope.ServiceProvider.GetRequiredService<IPushGateway>();
        pushGateway.ClearReceivedCalls();

        var alertClient = Scope.ServiceProvider
            .GetRequiredService<IInterModuleRequestClient<SendSecurityAlertRequest, SendSecurityAlertResponse>>();

        await alertClient.SendAsync(
            new SendSecurityAlertRequest(ApplicationUserId.New(), SecurityAlertType.SessionRevokedTokenReuse),
            CancellationToken.None);

        await pushGateway.DidNotReceive().SendAsync(Arg.Any<PushMessage>(), Arg.Any<CancellationToken>());
    }
}

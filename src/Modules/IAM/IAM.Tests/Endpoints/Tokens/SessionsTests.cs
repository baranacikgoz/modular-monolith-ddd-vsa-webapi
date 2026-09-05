using System.Net;
using System.Net.Http.Json;
using Common.Domain.StronglyTypedIds;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Notifications;
using Common.Tests;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using RefreshRequest = IAM.Endpoints.Tokens.VersionNeutral.Refresh.Request;

namespace IAM.Tests.Endpoints.Tokens;

[Collection("IntegrationTestCollection")]
public class SessionsTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    private sealed record SessionResponse(
        string Id, string? ClientId, string? DeviceName, string? IpAddress,
        DateTimeOffset StartedAt, DateTimeOffset LastAccessAt, bool IsCurrent);

    [Fact]
    public async Task List_TwoDevices_ReturnsBothWithDeviceMetadataAndCurrentFlag()
    {
        var phone = await RegisterFreshUserAsync();
        var phoneLogin = await IamTestClient.LoginByPhoneAsync(Factory, phone, deviceName: "Pixel 9");
        var tabletLogin = await IamTestClient.LoginByPhoneAsync(Factory, phone, clientId: "mobile-app-2", deviceName: "iPad");

        var sessions = await ListAsync(tabletLogin);

        Assert.Equal(2, sessions.Count);
        var current = Assert.Single(sessions, s => s.IsCurrent);
        Assert.Equal(tabletLogin.SessionId, current.Id);
        Assert.Equal("mobile-app-2", current.ClientId);
        Assert.Equal("iPad", current.DeviceName);
        var other = Assert.Single(sessions, s => !s.IsCurrent);
        Assert.Equal(phoneLogin.SessionId, other.Id);
        Assert.Equal("Pixel 9", other.DeviceName);
    }

    [Fact]
    public async Task List_TwoRegistrationsOnOneSession_Returns200WithSingleEntry()
    {
        var phone = await RegisterFreshUserAsync();
        var login = await IamTestClient.LoginByPhoneAsync(Factory, phone, deviceName: "Pixel 9");

        // The registry is unique per (user, device, client app), not per session, so a second device can end up
        // bound to the same Keycloak session id. The list must degrade, not 500 on a duplicate dictionary key.
        var bindClient = Scope.ServiceProvider
            .GetRequiredService<IInterModuleRequestClient<BindDeviceSessionRequest, BindDeviceSessionResponse>>();
        await bindClient.SendAsync(
            new BindDeviceSessionRequest(new ApplicationUserId(Guid.Parse(login.Subject)), login.SessionId,
                Guid.NewGuid(), "mobile-app-2", "Clone", PushToken: null),
            CancellationToken.None);

        var sessions = await ListAsync(login);

        var single = Assert.Single(sessions);
        Assert.Equal(login.SessionId, single.Id);
    }

    [Fact]
    public async Task Login_SameDeviceAgain_SupersedesPreviousSession()
    {
        var phone = await RegisterFreshUserAsync();
        var deviceId = Guid.NewGuid();
        var first = await IamTestClient.LoginByPhoneAsync(Factory, phone, deviceId);
        var second = await IamTestClient.LoginByPhoneAsync(Factory, phone, deviceId);

        var sessions = await ListAsync(second);

        var single = Assert.Single(sessions);
        Assert.Equal(second.SessionId, single.Id);

        using var refreshOld = await Factory.CreateClient()
            .PostAsJsonAsync(new Uri("/tokens/refresh", UriKind.Relative), new RefreshRequest { RefreshToken = first.RefreshToken });
        Assert.Equal(HttpStatusCode.Unauthorized, refreshOld.StatusCode);
    }

    [Fact]
    public async Task Revoke_OtherOwnSession_RemovesOnlyThatSession()
    {
        var phone = await RegisterFreshUserAsync();
        var phoneLogin = await IamTestClient.LoginByPhoneAsync(Factory, phone);
        var tabletLogin = await IamTestClient.LoginByPhoneAsync(Factory, phone, clientId: "mobile-app-2");
        var client = IamTestClient.Authorized(Factory, tabletLogin);

        using var revoke = await client.DeleteAsync(new Uri($"/tokens/sessions/{phoneLogin.SessionId}", UriKind.Relative));
        Assert.Equal(HttpStatusCode.NoContent, revoke.StatusCode);

        var sessions = await ListAsync(tabletLogin);
        var remaining = Assert.Single(sessions);
        Assert.Equal(tabletLogin.SessionId, remaining.Id);
    }

    [Fact]
    public async Task Revoke_SessionOfAnotherUser_Returns404()
    {
        var victim = await IamTestClient.LoginByPhoneAsync(Factory, SeedUsers.BasicPhone2);
        var attacker = await IamTestClient.LoginByPhoneAsync(Factory, SeedUsers.BasicPhone);
        var client = IamTestClient.Authorized(Factory, attacker);

        using var response = await client.DeleteAsync(new Uri($"/tokens/sessions/{victim.SessionId}", UriKind.Relative));

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        var victimSessions = await ListAsync(victim);
        Assert.Contains(victimSessions, s => s.Id == victim.SessionId);
    }

    [Fact]
    public async Task RevokeAll_KillsEverySession()
    {
        var phone = await RegisterFreshUserAsync();
        var phoneLogin = await IamTestClient.LoginByPhoneAsync(Factory, phone);
        var tabletLogin = await IamTestClient.LoginByPhoneAsync(Factory, phone, clientId: "mobile-app-2");
        var client = IamTestClient.Authorized(Factory, tabletLogin);

        using var revokeAll = await client.PostAsync(new Uri("/tokens/sessions/revoke-all", UriKind.Relative), content: null);
        Assert.Equal(HttpStatusCode.NoContent, revokeAll.StatusCode);

        foreach (var refreshToken in new[] { phoneLogin.RefreshToken, tabletLogin.RefreshToken })
        {
            using var refresh = await Factory.CreateClient()
                .PostAsJsonAsync(new Uri("/tokens/refresh", UriKind.Relative), new RefreshRequest { RefreshToken = refreshToken });
            Assert.Equal(HttpStatusCode.Unauthorized, refresh.StatusCode);
        }
    }

    private async Task<List<SessionResponse>> ListAsync(TokenPair tokens)
    {
        var client = IamTestClient.Authorized(Factory, tokens);
        var sessions = await client.GetFromJsonAsync<List<SessionResponse>>(new Uri("/tokens/sessions", UriKind.Relative));
        Assert.NotNull(sessions);
        return sessions;
    }

    private Task<string> RegisterFreshUserAsync()
    {
        return IamTestClient.RegisterFreshUserAsync(Factory);
    }
}

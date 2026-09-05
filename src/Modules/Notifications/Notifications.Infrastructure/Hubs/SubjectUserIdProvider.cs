using Common.Application.Auth;
using Microsoft.AspNetCore.SignalR;

namespace Notifications.Infrastructure.Hubs;

/// <summary>
///     SignalR's default provider reads <c>ClaimTypes.NameIdentifier</c>; Keycloak tokens are consumed with
///     <c>MapInboundClaims = false</c>, so the user id lives in the raw <c>sub</c> claim.
/// </summary>
internal sealed class SubjectUserIdProvider : IUserIdProvider
{
    public string? GetUserId(HubConnectionContext connection)
    {
        return connection.User?.FindFirst(JwtClaimNames.Subject)?.Value;
    }
}

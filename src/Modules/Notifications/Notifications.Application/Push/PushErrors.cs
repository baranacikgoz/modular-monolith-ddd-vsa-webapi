using System.Net;
using Common.Domain.ResultMonad;

namespace Notifications.Application.Push;

public static class PushErrors
{
    /// <summary>The gateway is unreachable, timed out, or returned malformed data.</summary>
    public static readonly Error ProviderUnavailable =
        new() { Key = nameof(ProviderUnavailable), StatusCode = HttpStatusCode.BadGateway };

    /// <summary>The gateway rejected every token in the request (bad credentials or malformed payload).</summary>
    public static readonly Error Rejected =
        new() { Key = nameof(Rejected), StatusCode = HttpStatusCode.BadGateway };
}

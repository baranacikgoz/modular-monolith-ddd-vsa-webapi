using System.Net;
using Common.Domain.ResultMonad;

namespace Notifications.Application.Sms;

public static class SmsErrors
{
    /// <summary>The gateway is unreachable, timed out, or returned malformed data.</summary>
    public static readonly Error ProviderUnavailable =
        new() { Key = nameof(ProviderUnavailable), StatusCode = HttpStatusCode.BadGateway };

    /// <summary>The gateway rejected the request (bad credentials, header, content, or account state).</summary>
    public static readonly Error Rejected =
        new() { Key = nameof(Rejected), StatusCode = HttpStatusCode.BadGateway };

    /// <summary>A send-rate cap (ours or the provider's) was hit.</summary>
    public static readonly Error Throttled =
        new() { Key = nameof(Throttled), StatusCode = HttpStatusCode.ServiceUnavailable };
}

using Common.Domain.ResultMonad;

namespace Notifications.Application.Push;

/// <summary>Multicast by design: one gateway call for every device token that should receive this
/// notification, not one call per token.</summary>
public sealed record PushMessage(
    IReadOnlyList<string> Tokens,
    string Title,
    string Body,
    IReadOnlyDictionary<string, string>? Data = null);

public interface IPushGateway
{
    Task<Result> SendAsync(PushMessage message, CancellationToken cancellationToken);
}

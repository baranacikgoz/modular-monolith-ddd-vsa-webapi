using Common.Domain.ResultMonad;

namespace Notifications.Application.Email;

public sealed record EmailMessage(string To, string Subject, string HtmlBody, string? TextBody = null);

public interface IEmailGateway
{
    Task<Result> SendAsync(EmailMessage message, CancellationToken cancellationToken);
}

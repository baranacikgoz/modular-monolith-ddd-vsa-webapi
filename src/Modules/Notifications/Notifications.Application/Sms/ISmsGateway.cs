using Common.Domain.ResultMonad;

namespace Notifications.Application.Sms;

/// <summary>İYS (İleti Yönetim Sistemi) content category — decides whether the message needs consent.</summary>
public enum SmsCategory
{
    /// <summary>OTPs, booking confirmations — not subject to İYS consent.</summary>
    Transactional,

    /// <summary>Marketing content sent to an individual consumer — requires İYS consent.</summary>
    CommercialIndividual,

    /// <summary>Marketing content sent to a merchant/business contact — requires İYS consent.</summary>
    CommercialMerchant
}

public sealed record SmsMessage(string PhoneNumber, string Text, SmsCategory Category);

public interface ISmsGateway
{
    Task<Result> SendAsync(SmsMessage message, CancellationToken cancellationToken);
}

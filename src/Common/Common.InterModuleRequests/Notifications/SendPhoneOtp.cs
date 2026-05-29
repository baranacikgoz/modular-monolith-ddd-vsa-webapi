using Common.InterModuleRequests.Contracts;

namespace Common.InterModuleRequests.Notifications;

public sealed record SendPhoneOtpRequest(
    string PhoneNumber,
    string Purpose,
    string? Language = null,
    string? ContextId = null
) : IInterModuleRequest<SendPhoneOtpResponse>;

public sealed record SendPhoneOtpResponse;

using System.Globalization;
using Common.Application.Options;
using Common.InterModuleRequests.Contracts;
using Common.InterModuleRequests.Notifications;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Notifications.Application.Otp;
using Notifications.Application.Sms;

namespace Notifications.Infrastructure.InterModuleRequestHandlers;

public sealed class SendPhoneOtpRequestHandler(
    IOtpService otpService,
    ISmsGateway smsGateway,
    IOptions<OtpOptions> otpOptionsProvider,
    IOptions<RequestLocalizationOptions> localizationOptionsProvider
) : InterModuleRequestHandler<SendPhoneOtpRequest, SendPhoneOtpResponse>
{
    public override async Task<SendPhoneOtpResponse> HandleAsync(
        SendPhoneOtpRequest request,
        CancellationToken cancellationToken)
    {
        var opts = otpOptionsProvider.Value;
        var language = request.Language
                       ?? localizationOptionsProvider.Value.DefaultRequestCulture.UICulture.TwoLetterISOLanguageName;

        if (!opts.SmsTemplates.TryGetValue(language, out var template))
        {
            var fallback = localizationOptionsProvider.Value.DefaultRequestCulture.UICulture.TwoLetterISOLanguageName;
            if (!opts.SmsTemplates.TryGetValue(fallback, out template))
            {
                throw new InvalidOperationException(
                    $"No SmsTemplate configured for language '{language}' or default language '{fallback}'. Add an entry to OtpOptions:SmsTemplates.");
            }
        }

        var otp = otpService.Generate();
        await otpService.StoreAsync(
            request.PhoneNumber,
            otp,
            request.Purpose,
            TimeSpan.FromMinutes(opts.ExpirationInMinutes),
            request.ContextId,
            cancellationToken);
        var message = string.Format(CultureInfo.InvariantCulture, template, otp);
        await smsGateway.SendAsync(request.PhoneNumber, message, cancellationToken);
        return new SendPhoneOtpResponse();
    }
}

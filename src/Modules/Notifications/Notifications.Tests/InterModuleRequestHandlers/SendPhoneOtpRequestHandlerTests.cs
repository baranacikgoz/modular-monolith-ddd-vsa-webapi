using Common.Application.Options;
using Common.Domain.ResultMonad;
using Common.InterModuleRequests.Notifications;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Notifications.Application.Otp;
using Notifications.Application.Sms;
using Notifications.Infrastructure.InterModuleRequestHandlers;
using NSubstitute;
using Xunit;
using ZiggyCreatures.Caching.Fusion;

namespace Notifications.Tests.InterModuleRequestHandlers;

public sealed class SendPhoneOtpRequestHandlerTests : IDisposable
{
    private const string PhoneNumber = "905551112233";
    private const int MaxSendsPerPhonePerWindow = 3;

    private readonly IOtpService _otpService = Substitute.For<IOtpService>();
    private readonly ISmsGateway _smsGateway = Substitute.For<ISmsGateway>();
    private readonly FusionCache _cache;
    private readonly SendPhoneOtpRequestHandler _handler;

    public SendPhoneOtpRequestHandlerTests()
    {
        var otpOptions = Options.Create(new OtpOptions
        {
            Length = 6,
            ExpirationInMinutes = 5,
            ResendIntervalSeconds = 60,
            MaxSendsPerPhonePerWindow = MaxSendsPerPhonePerWindow,
            PhoneQuotaWindowMinutes = 60,
        });
        var smsOptions = Options.Create(new SmsOptions
        {
            Provider = SmsProvider.Dummy,
            ThrottleCounterTtlHours = 25,
            Templates = new SmsTemplatesOptions { Otp = { ["en"] = "Your code is: {0}" } },
        });
        var localizationOptions = Options.Create(new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture("en"),
        });
        _otpService.Generate().Returns("111111");
        _smsGateway.SendAsync(Arg.Any<SmsMessage>(), Arg.Any<CancellationToken>()).Returns(Result.Success);
        _cache = new FusionCache(new FusionCacheOptions());
        _handler = new SendPhoneOtpRequestHandler(_otpService, _smsGateway, _cache, otpOptions, smsOptions, localizationOptions);
    }

    public void Dispose()
    {
        _cache.Dispose();
        GC.SuppressFinalize(this);
    }

    [Fact]
    public async Task HandleAsync_FirstSend_StoresOtpAndCallsGateway()
    {
        var response = await _handler.HandleAsync(new SendPhoneOtpRequest(PhoneNumber, "login"), CancellationToken.None);

        Assert.Equal(SmsOtpDispatchOutcome.Sent, response.Outcome);
        await _otpService.Received(1).StoreAsync(
            PhoneNumber, "111111", "login", Arg.Any<TimeSpan>(), null, Arg.Any<CancellationToken>());
        await _smsGateway.Received(1).SendAsync(
            Arg.Is<SmsMessage>(m => m.PhoneNumber == PhoneNumber && m.Text == "Your code is: 111111"),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_SecondCallWithinResendInterval_ReturnsThrottledWithoutCallingGateway()
    {
        var request = new SendPhoneOtpRequest(PhoneNumber, "login");

        var first = await _handler.HandleAsync(request, CancellationToken.None);
        var second = await _handler.HandleAsync(request, CancellationToken.None);

        Assert.Equal(SmsOtpDispatchOutcome.Sent, first.Outcome);
        Assert.Equal(SmsOtpDispatchOutcome.Throttled, second.Outcome);
        await _smsGateway.Received(1).SendAsync(Arg.Any<SmsMessage>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_PhoneQuotaExceeded_ThrottlesEvenWithFreshContextIdEachTime()
    {
        // The per-context resend guard does not block these (each ContextId is new). The phone-only
        // quota is the separate cap that still applies regardless of how many ContextIds are used.
        for (var i = 0; i < MaxSendsPerPhonePerWindow; i++)
        {
            var allowed = await _handler.HandleAsync(
                new SendPhoneOtpRequest(PhoneNumber, "login", ContextId: $"ctx-{i}"), CancellationToken.None);
            Assert.Equal(SmsOtpDispatchOutcome.Sent, allowed.Outcome);
        }

        var overCap = await _handler.HandleAsync(
            new SendPhoneOtpRequest(PhoneNumber, "login", ContextId: "ctx-over-cap"), CancellationToken.None);

        Assert.Equal(SmsOtpDispatchOutcome.Throttled, overCap.Outcome);
        await _smsGateway.Received(MaxSendsPerPhonePerWindow).SendAsync(
            Arg.Any<SmsMessage>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_GatewayUnavailable_ReturnsProviderUnavailableAndDoesNotConsumeGuardOrQuota()
    {
        _smsGateway.SendAsync(Arg.Any<SmsMessage>(), Arg.Any<CancellationToken>())
            .Returns(SmsErrors.ProviderUnavailable);
        var request = new SendPhoneOtpRequest(PhoneNumber, "login");

        for (var i = 0; i < MaxSendsPerPhonePerWindow + 1; i++)
        {
            var response = await _handler.HandleAsync(request, CancellationToken.None);
            Assert.Equal(SmsOtpDispatchOutcome.ProviderUnavailable, response.Outcome);
        }

        // A failed send must not lock the user out of a retry, nor count against the phone quota.
        _smsGateway.SendAsync(Arg.Any<SmsMessage>(), Arg.Any<CancellationToken>()).Returns(Result.Success);
        var retry = await _handler.HandleAsync(request, CancellationToken.None);
        Assert.Equal(SmsOtpDispatchOutcome.Sent, retry.Outcome);
    }

    [Fact]
    public async Task HandleAsync_GatewayThrottled_ReturnsThrottled()
    {
        _smsGateway.SendAsync(Arg.Any<SmsMessage>(), Arg.Any<CancellationToken>())
            .Returns(SmsErrors.Throttled);

        var response = await _handler.HandleAsync(new SendPhoneOtpRequest(PhoneNumber, "login"), CancellationToken.None);

        Assert.Equal(SmsOtpDispatchOutcome.Throttled, response.Outcome);
    }

    [Fact]
    public async Task HandleAsync_UnsupportedLanguage_FallsBackToDefaultCultureTemplate()
    {
        var response = await _handler.HandleAsync(
            new SendPhoneOtpRequest(PhoneNumber, "login", Language: "fr"), CancellationToken.None);

        Assert.Equal(SmsOtpDispatchOutcome.Sent, response.Outcome);
        await _smsGateway.Received(1).SendAsync(
            Arg.Is<SmsMessage>(m => m.Text == "Your code is: 111111"), Arg.Any<CancellationToken>());
    }
}

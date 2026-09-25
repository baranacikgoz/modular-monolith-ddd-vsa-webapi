using Common.Application.Options;
using Common.Domain.ResultMonad;
using Common.InterModuleRequests.Notifications;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Notifications.Application.Email;
using Notifications.Application.Otp;
using Notifications.Infrastructure.InterModuleRequestHandlers;
using NSubstitute;
using Xunit;
using ZiggyCreatures.Caching.Fusion;

namespace Notifications.Tests.InterModuleRequestHandlers;

public sealed class SendEmailOtpRequestHandlerTests : IDisposable
{
    private const string Email = "someone@example.com";
    private const int MaxSendsPerEmailPerWindow = 3;

    private readonly IOtpService _otpService = Substitute.For<IOtpService>();
    private readonly IEmailGateway _emailGateway = Substitute.For<IEmailGateway>();
    private readonly FusionCache _cache;
    private readonly SendEmailOtpRequestHandler _handler;

    public SendEmailOtpRequestHandlerTests()
    {
        var otpOptions = Options.Create(new OtpOptions
        {
            Length = 6,
            ExpirationInMinutes = 5,
            ResendIntervalSeconds = 60,
            MaxSendsPerPhonePerWindow = 5,
            PhoneQuotaWindowMinutes = 60,
            MaxSendsPerEmailPerWindow = MaxSendsPerEmailPerWindow,
            EmailQuotaWindowMinutes = 60,
            VerificationTokenExpirationInMinutes = 30,
        });
        var emailOptions = Options.Create(new EmailOptions
        {
            Provider = EmailProvider.Dummy,
            AttemptTimeoutSeconds = 0,
            TotalRequestTimeoutSeconds = 0,
            MaxRetryAttempts = 0,
            MaxPerAddressPerDay = 0,
            MaxPerDay = 0,
            ThrottleCounterTtlHours = 25,
            Templates = new EmailTemplatesOptions
            {
                Otp =
                {
                    ["en"] = new EmailTemplate
                    {
                        Subject = "Your code: {0}",
                        // Braces on purpose: an HTML body with inline CSS must not be run through string.Format.
                        HtmlBody = "<style>p{font-weight:bold}</style><p>{0}</p>",
                        TextBody = "Code {0}",
                    },
                    ["tr"] = new EmailTemplate { Subject = "Kodunuz: {0}", HtmlBody = "<p>{0}</p>" },
                },
            },
        });
        var localizationOptions = Options.Create(new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture("en"),
        });
        _otpService.Generate().Returns("111111");
        _emailGateway.SendAsync(Arg.Any<EmailMessage>(), Arg.Any<CancellationToken>()).Returns(Result.Success);
        _cache = new FusionCache(new FusionCacheOptions());
        _handler = new SendEmailOtpRequestHandler(_otpService, _emailGateway, _cache, otpOptions, emailOptions, localizationOptions);
    }

    public void Dispose()
    {
        _cache.Dispose();
        GC.SuppressFinalize(this);
    }

    [Fact]
    public async Task HandleAsync_FirstSend_StoresOtpAndCallsGatewayWithRenderedTemplate()
    {
        var response = await _handler.HandleAsync(new SendEmailOtpRequest(Email, "email_verification"), CancellationToken.None);

        Assert.Equal(EmailOtpDispatchOutcome.Sent, response.Outcome);
        await _otpService.Received(1).StoreAsync(
            Email, "111111", "email_verification", Arg.Any<TimeSpan>(), null, Arg.Any<CancellationToken>());
        await _emailGateway.Received(1).SendAsync(
            Arg.Is<EmailMessage>(m =>
                m.To == Email
                && m.Subject == "Your code: 111111"
                && m.HtmlBody == "<style>p{font-weight:bold}</style><p>111111</p>"
                && m.TextBody == "Code 111111"),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_TemplateWithoutTextBody_SendsNullTextBody()
    {
        var response = await _handler.HandleAsync(
            new SendEmailOtpRequest(Email, "email_verification", Language: "tr"), CancellationToken.None);

        Assert.Equal(EmailOtpDispatchOutcome.Sent, response.Outcome);
        await _emailGateway.Received(1).SendAsync(
            Arg.Is<EmailMessage>(m => m.Subject == "Kodunuz: 111111" && m.TextBody == null),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_SecondCallWithinResendInterval_ReturnsThrottledWithoutCallingGateway()
    {
        var request = new SendEmailOtpRequest(Email, "email_verification");

        var first = await _handler.HandleAsync(request, CancellationToken.None);
        var second = await _handler.HandleAsync(request, CancellationToken.None);

        Assert.Equal(EmailOtpDispatchOutcome.Sent, first.Outcome);
        Assert.Equal(EmailOtpDispatchOutcome.Throttled, second.Outcome);
        await _emailGateway.Received(1).SendAsync(Arg.Any<EmailMessage>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_EmailQuotaExceeded_ThrottlesEvenWithFreshContextIdEachTime()
    {
        for (var i = 0; i < MaxSendsPerEmailPerWindow; i++)
        {
            var allowed = await _handler.HandleAsync(
                new SendEmailOtpRequest(Email, "email_verification", ContextId: $"ctx-{i}"), CancellationToken.None);
            Assert.Equal(EmailOtpDispatchOutcome.Sent, allowed.Outcome);
        }

        var overCap = await _handler.HandleAsync(
            new SendEmailOtpRequest(Email, "email_verification", ContextId: "ctx-over-cap"), CancellationToken.None);

        Assert.Equal(EmailOtpDispatchOutcome.Throttled, overCap.Outcome);
        await _emailGateway.Received(MaxSendsPerEmailPerWindow).SendAsync(
            Arg.Any<EmailMessage>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_GatewayUnavailable_ReturnsProviderUnavailableAndDoesNotConsumeGuardOrQuota()
    {
        _emailGateway.SendAsync(Arg.Any<EmailMessage>(), Arg.Any<CancellationToken>())
            .Returns(EmailErrors.ProviderUnavailable);
        var request = new SendEmailOtpRequest(Email, "email_verification");

        for (var i = 0; i < MaxSendsPerEmailPerWindow + 1; i++)
        {
            var response = await _handler.HandleAsync(request, CancellationToken.None);
            Assert.Equal(EmailOtpDispatchOutcome.ProviderUnavailable, response.Outcome);
        }

        _emailGateway.SendAsync(Arg.Any<EmailMessage>(), Arg.Any<CancellationToken>()).Returns(Result.Success);
        var retry = await _handler.HandleAsync(request, CancellationToken.None);
        Assert.Equal(EmailOtpDispatchOutcome.Sent, retry.Outcome);
    }

    [Fact]
    public async Task HandleAsync_GatewayThrottled_ReturnsThrottled()
    {
        _emailGateway.SendAsync(Arg.Any<EmailMessage>(), Arg.Any<CancellationToken>())
            .Returns(EmailErrors.Throttled);

        var response = await _handler.HandleAsync(new SendEmailOtpRequest(Email, "email_verification"), CancellationToken.None);

        Assert.Equal(EmailOtpDispatchOutcome.Throttled, response.Outcome);
    }

    [Fact]
    public async Task HandleAsync_UnsupportedLanguage_FallsBackToDefaultCultureTemplate()
    {
        var response = await _handler.HandleAsync(
            new SendEmailOtpRequest(Email, "email_verification", Language: "fr"), CancellationToken.None);

        Assert.Equal(EmailOtpDispatchOutcome.Sent, response.Outcome);
        await _emailGateway.Received(1).SendAsync(
            Arg.Is<EmailMessage>(m => m.Subject == "Your code: 111111"), Arg.Any<CancellationToken>());
    }
}

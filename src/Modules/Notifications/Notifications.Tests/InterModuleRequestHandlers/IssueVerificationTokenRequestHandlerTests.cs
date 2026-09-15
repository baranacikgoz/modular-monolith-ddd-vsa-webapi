using Common.Application.Options;
using Common.InterModuleRequests.Notifications;
using Microsoft.Extensions.Options;
using Notifications.Application.Otp;
using Notifications.Infrastructure.InterModuleRequestHandlers;
using NSubstitute;
using Xunit;

namespace Notifications.Tests.InterModuleRequestHandlers;

public sealed class IssueVerificationTokenRequestHandlerTests
{
    private readonly IOtpService _otpService = Substitute.For<IOtpService>();

    private IssueVerificationTokenRequestHandler CreateHandler(string? dummyCode = null) =>
        new(_otpService, Options.Create(new OtpOptions
        {
            Length = 6,
            ExpirationInMinutes = 5,
            ResendIntervalSeconds = 60,
            MaxSendsPerPhonePerWindow = 5,
            PhoneQuotaWindowMinutes = 60,
            MaxSendsPerEmailPerWindow = 5,
            EmailQuotaWindowMinutes = 60,
            VerificationTokenExpirationInMinutes = 30,
            DummyCode = dummyCode,
        }));

    /// <summary>
    ///     Regression guard: the handler must generate the token itself (RandomNumberGenerator), never through
    ///     IOtpService.Generate(), which honors OtpOptions.DummyCode. A verification token routed through that
    ///     path would be the same publicly known string in every non-production environment.
    /// </summary>
    [Fact]
    public async Task HandleAsync_DummyCodeConfigured_IssuedTokenIsNotTheDummyCode()
    {
        const string dummyCode = "123456";
        _otpService.Generate().Returns(dummyCode);
        var handler = CreateHandler(dummyCode);

        var response = await handler.HandleAsync(
            new IssueVerificationTokenRequest("someone@example.com", "email_verified_register"), CancellationToken.None);

        Assert.NotEqual(dummyCode, response.Token);
        Assert.True(response.Token.Length > dummyCode.Length);
        _otpService.DidNotReceive().Generate();
    }

    [Fact]
    public async Task HandleAsync_StoresTokenUnderIdentifierAndPurpose()
    {
        var handler = CreateHandler();

        var response = await handler.HandleAsync(
            new IssueVerificationTokenRequest("someone@example.com", "email_verified_login"), CancellationToken.None);

        await _otpService.Received(1).StoreAsync(
            "someone@example.com", response.Token, "email_verified_login",
            TimeSpan.FromMinutes(30), null, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_CalledTwice_ReturnsDifferentTokens()
    {
        var handler = CreateHandler();

        var first = await handler.HandleAsync(
            new IssueVerificationTokenRequest("a@example.com", "email_verified_login"), CancellationToken.None);
        var second = await handler.HandleAsync(
            new IssueVerificationTokenRequest("a@example.com", "email_verified_login"), CancellationToken.None);

        Assert.NotEqual(first.Token, second.Token);
    }
}

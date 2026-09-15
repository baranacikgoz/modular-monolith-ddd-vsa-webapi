using Common.Application.Options;
using Common.Domain.ResultMonad;
using Microsoft.Extensions.Options;
using Notifications.Application.Email;
using Notifications.Infrastructure.Email;
using Xunit;
using ZiggyCreatures.Caching.Fusion;

namespace Notifications.Tests.Email;

public sealed class ThrottledEmailGatewayTests : IDisposable
{
    private readonly FusionCache _cache = new(new FusionCacheOptions());

    public void Dispose()
    {
        _cache.Dispose();
        GC.SuppressFinalize(this);
    }

    private static IOptions<EmailOptions> BuildOptions(int maxPerAddressPerDay, int maxPerDay) =>
        Options.Create(new EmailOptions
        {
            Provider = EmailProvider.Brevo,
            MaxPerAddressPerDay = maxPerAddressPerDay,
            MaxPerDay = maxPerDay,
            ThrottleCounterTtlHours = 25,
            Templates = new EmailTemplatesOptions
            {
                Otp = { ["en"] = new EmailTemplate { Subject = "code {0}", HtmlBody = "<p>{0}</p>" } },
            },
        });

    private static EmailMessage Message(string to) => new(to, "subject", "<p>body</p>");

    private sealed class RecordingGateway : IEmailGateway
    {
        public int CallCount { get; private set; }
        public Result NextResult { get; set; } = Result.Success;

        public Task<Result> SendAsync(EmailMessage message, CancellationToken cancellationToken)
        {
            CallCount++;
            return Task.FromResult(NextResult);
        }
    }

    [Fact]
    public async Task SendAsync_UnderCap_DelegatesToInnerGateway()
    {
        var inner = new RecordingGateway();
        var sut = new ThrottledEmailGateway(inner, _cache, BuildOptions(maxPerAddressPerDay: 10, maxPerDay: 100));

        var result = await sut.SendAsync(Message("a@example.com"), CancellationToken.None);

        Assert.False(result.IsFailure);
        Assert.Equal(1, inner.CallCount);
    }

    [Fact]
    public async Task SendAsync_OverPerAddressCap_ReturnsThrottledWithoutCallingInnerGateway()
    {
        var inner = new RecordingGateway();
        var sut = new ThrottledEmailGateway(inner, _cache, BuildOptions(maxPerAddressPerDay: 1, maxPerDay: 100));

        await sut.SendAsync(Message("a@example.com"), CancellationToken.None);
        var result = await sut.SendAsync(Message("a@example.com"), CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal(EmailErrors.Throttled.Key, result.Error!.Key);
        Assert.Equal(1, inner.CallCount);
    }

    [Fact]
    public async Task SendAsync_SameMailboxDifferentCasing_SharesOneBucket()
    {
        var inner = new RecordingGateway();
        var sut = new ThrottledEmailGateway(inner, _cache, BuildOptions(maxPerAddressPerDay: 1, maxPerDay: 100));

        await sut.SendAsync(Message("a@example.com"), CancellationToken.None);
        var result = await sut.SendAsync(Message("  A@Example.COM "), CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal(1, inner.CallCount);
    }

    [Fact]
    public async Task SendAsync_OverGlobalCap_ReturnsThrottledWithoutCallingInnerGateway()
    {
        var inner = new RecordingGateway();
        var sut = new ThrottledEmailGateway(inner, _cache, BuildOptions(maxPerAddressPerDay: 100, maxPerDay: 1));

        await sut.SendAsync(Message("a@example.com"), CancellationToken.None);
        var result = await sut.SendAsync(Message("b@example.com"), CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal(EmailErrors.Throttled.Key, result.Error!.Key);
        Assert.Equal(1, inner.CallCount);
    }

    [Fact]
    public async Task SendAsync_InnerGatewayFails_DoesNotCountTowardCap()
    {
        var inner = new RecordingGateway { NextResult = EmailErrors.ProviderUnavailable };
        var sut = new ThrottledEmailGateway(inner, _cache, BuildOptions(maxPerAddressPerDay: 1, maxPerDay: 100));

        await sut.SendAsync(Message("a@example.com"), CancellationToken.None);
        inner.NextResult = Result.Success;
        var result = await sut.SendAsync(Message("a@example.com"), CancellationToken.None);

        Assert.False(result.IsFailure);
        Assert.Equal(2, inner.CallCount);
    }

    [Fact]
    public async Task SendAsync_PerAddressCapHitForOneAddress_DoesNotThrottleAnother()
    {
        var inner = new RecordingGateway();
        var sut = new ThrottledEmailGateway(inner, _cache, BuildOptions(maxPerAddressPerDay: 1, maxPerDay: 100));

        var firstA = await sut.SendAsync(Message("a@example.com"), CancellationToken.None);
        var secondA = await sut.SendAsync(Message("a@example.com"), CancellationToken.None);
        var firstB = await sut.SendAsync(Message("b@example.com"), CancellationToken.None);

        Assert.False(firstA.IsFailure);
        Assert.True(secondA.IsFailure);
        Assert.False(firstB.IsFailure);
        Assert.Equal(2, inner.CallCount);
    }
}

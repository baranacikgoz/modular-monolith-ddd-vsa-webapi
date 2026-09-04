using Common.Application.Options;
using Common.Domain.ResultMonad;
using Microsoft.Extensions.Options;
using Notifications.Application.Sms;
using Notifications.Infrastructure.Sms;
using Xunit;
using ZiggyCreatures.Caching.Fusion;

namespace Notifications.Tests.Sms;

public sealed class ThrottledSmsGatewayTests : IDisposable
{
    private readonly FusionCache _cache;

    public ThrottledSmsGatewayTests()
    {
        _cache = new FusionCache(new FusionCacheOptions());
    }

    public void Dispose()
    {
        _cache.Dispose();
        GC.SuppressFinalize(this);
    }

    private static IOptions<SmsOptions> BuildOptions(int maxPerPhoneNumberPerDay, int maxPerDay) =>
        Options.Create(new SmsOptions
        {
            Provider = SmsProvider.NetGsm,
            MaxPerPhoneNumberPerDay = maxPerPhoneNumberPerDay,
            MaxPerDay = maxPerDay,
            ThrottleCounterTtlHours = 25,
            Templates = new SmsTemplatesOptions { Otp = { ["en"] = "code {0}" } },
        });

    private sealed class RecordingGateway : ISmsGateway
    {
        public int CallCount { get; private set; }
        public Result NextResult { get; set; } = Result.Success;

        public Task<Result> SendAsync(SmsMessage message, CancellationToken cancellationToken)
        {
            CallCount++;
            return Task.FromResult(NextResult);
        }
    }

    [Fact]
    public async Task SendAsync_UnderCap_DelegatesToInnerGateway()
    {
        var inner = new RecordingGateway();
        var sut = new ThrottledSmsGateway(inner, _cache, BuildOptions(maxPerPhoneNumberPerDay: 10, maxPerDay: 100));

        var result = await sut.SendAsync(new SmsMessage("905380718209", "text", SmsCategory.Transactional), CancellationToken.None);

        Assert.False(result.IsFailure);
        Assert.Equal(1, inner.CallCount);
    }

    [Fact]
    public async Task SendAsync_OverPerPhoneCap_ReturnsThrottledWithoutCallingInnerGateway()
    {
        var inner = new RecordingGateway();
        var sut = new ThrottledSmsGateway(inner, _cache, BuildOptions(maxPerPhoneNumberPerDay: 1, maxPerDay: 100));
        var message = new SmsMessage("905380718209", "text", SmsCategory.Transactional);

        await sut.SendAsync(message, CancellationToken.None);
        var result = await sut.SendAsync(message, CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal(SmsErrors.Throttled.Key, result.Error!.Key);
        Assert.Equal(1, inner.CallCount);
    }

    [Fact]
    public async Task SendAsync_OverGlobalCap_ReturnsThrottledWithoutCallingInnerGateway()
    {
        var inner = new RecordingGateway();
        var sut = new ThrottledSmsGateway(inner, _cache, BuildOptions(maxPerPhoneNumberPerDay: 100, maxPerDay: 1));

        await sut.SendAsync(new SmsMessage("905380718209", "text", SmsCategory.Transactional), CancellationToken.None);
        var result = await sut.SendAsync(new SmsMessage("905380718210", "text", SmsCategory.Transactional), CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal(SmsErrors.Throttled.Key, result.Error!.Key);
        Assert.Equal(1, inner.CallCount);
    }

    [Fact]
    public async Task SendAsync_InnerGatewayFails_DoesNotCountTowardCap()
    {
        var inner = new RecordingGateway { NextResult = SmsErrors.ProviderUnavailable };
        var sut = new ThrottledSmsGateway(inner, _cache, BuildOptions(maxPerPhoneNumberPerDay: 1, maxPerDay: 100));
        var message = new SmsMessage("905380718209", "text", SmsCategory.Transactional);

        await sut.SendAsync(message, CancellationToken.None);
        inner.NextResult = Result.Success;
        var result = await sut.SendAsync(message, CancellationToken.None);

        Assert.False(result.IsFailure);
        Assert.Equal(2, inner.CallCount);
    }

    [Fact]
    public async Task SendAsync_PerPhoneCapHitForOnePhone_DoesNotThrottleAnotherPhone()
    {
        var inner = new RecordingGateway();
        var sut = new ThrottledSmsGateway(inner, _cache, BuildOptions(maxPerPhoneNumberPerDay: 1, maxPerDay: 100));

        var firstPhoneFirstSend = await sut.SendAsync(new SmsMessage("905380718209", "text", SmsCategory.Transactional), CancellationToken.None);
        var firstPhoneSecondSend = await sut.SendAsync(new SmsMessage("905380718209", "text", SmsCategory.Transactional), CancellationToken.None);
        var secondPhoneFirstSend = await sut.SendAsync(new SmsMessage("905380718210", "text", SmsCategory.Transactional), CancellationToken.None);

        Assert.False(firstPhoneFirstSend.IsFailure);
        Assert.True(firstPhoneSecondSend.IsFailure);
        Assert.False(secondPhoneFirstSend.IsFailure);
        Assert.Equal(2, inner.CallCount);
    }
}

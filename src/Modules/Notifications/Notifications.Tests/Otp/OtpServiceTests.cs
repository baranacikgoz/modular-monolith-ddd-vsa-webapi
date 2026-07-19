using Common.Application.Options;
using Microsoft.Extensions.Options;
using Notifications.Application.Otp;
using Notifications.Infrastructure.Otp;
using Xunit;
using ZiggyCreatures.Caching.Fusion;

namespace Notifications.Tests.Otp;

public sealed class OtpServiceTests : IDisposable
{
    private const string PhoneNumber = "1234567890";
    private const string Purpose = "login";
    private const string Otp = "654321";

    private readonly FusionCache _cache;
    private readonly OtpService _sut;

    public OtpServiceTests()
    {
        _cache = new FusionCache(new FusionCacheOptions());
        var otpOptions = Options.Create(new OtpOptions { Length = 6, ExpirationInMinutes = 5 });
        _sut = new OtpService(otpOptions, _cache);
    }

    public void Dispose()
    {
        _cache.Dispose();
        GC.SuppressFinalize(this);
    }

    [Fact]
    public async Task VerifyThenRemove_ParallelWrongAttempts_CapNotBypassed()
    {
        await _sut.StoreAsync(PhoneNumber, Otp, Purpose, TimeSpan.FromMinutes(5), null, CancellationToken.None);

        var tasks = Enumerable.Range(0, 10)
            .Select(_ => _sut.VerifyThenRemoveAsync(PhoneNumber, "000000", Purpose, null, CancellationToken.None));
        var results = await Task.WhenAll(tasks);

        Assert.True(results.Count(r => r == OtpVerificationOutcome.TooManyAttempts) <= 1);

        var finalOutcome = await _sut.VerifyThenRemoveAsync(PhoneNumber, Otp, Purpose, null, CancellationToken.None);
        Assert.Equal(OtpVerificationOutcome.InvalidOtp, finalOutcome);
    }

    [Fact]
    public async Task VerifyThenRemove_ParallelCorrectAttempts_OnlyOneSucceeds()
    {
        await _sut.StoreAsync(PhoneNumber, Otp, Purpose, TimeSpan.FromMinutes(5), null, CancellationToken.None);

        var tasks = Enumerable.Range(0, 2)
            .Select(_ => _sut.VerifyThenRemoveAsync(PhoneNumber, Otp, Purpose, null, CancellationToken.None));
        var results = await Task.WhenAll(tasks);

        Assert.Equal(1, results.Count(r => r == OtpVerificationOutcome.Success));
        Assert.Equal(1, results.Count(r => r == OtpVerificationOutcome.InvalidOtp));
    }

    [Fact]
    public async Task VerifyThenRemove_SequentialWrongAttempts_ThirdReturnsTooManyAttempts()
    {
        await _sut.StoreAsync(PhoneNumber, Otp, Purpose, TimeSpan.FromMinutes(5), null, CancellationToken.None);

        var first = await _sut.VerifyThenRemoveAsync(PhoneNumber, "000000", Purpose, null, CancellationToken.None);
        var second = await _sut.VerifyThenRemoveAsync(PhoneNumber, "000000", Purpose, null, CancellationToken.None);
        var third = await _sut.VerifyThenRemoveAsync(PhoneNumber, "000000", Purpose, null, CancellationToken.None);
        var fourth = await _sut.VerifyThenRemoveAsync(PhoneNumber, Otp, Purpose, null, CancellationToken.None);

        Assert.Equal(OtpVerificationOutcome.InvalidOtp, first);
        Assert.Equal(OtpVerificationOutcome.InvalidOtp, second);
        Assert.Equal(OtpVerificationOutcome.TooManyAttempts, third);
        Assert.Equal(OtpVerificationOutcome.InvalidOtp, fourth);
    }

    [Fact]
    public async Task VerifyThenRemove_ExpiredOtp_ReturnsInvalidOtp()
    {
        await _sut.StoreAsync(PhoneNumber, Otp, Purpose, TimeSpan.FromMilliseconds(50), null, CancellationToken.None);
        await Task.Delay(TimeSpan.FromMilliseconds(200));

        var outcome = await _sut.VerifyThenRemoveAsync(PhoneNumber, Otp, Purpose, null, CancellationToken.None);

        Assert.Equal(OtpVerificationOutcome.InvalidOtp, outcome);
    }

    [Fact]
    public async Task VerifyThenRemove_CorrectOtp_SecondUseFails()
    {
        await _sut.StoreAsync(PhoneNumber, Otp, Purpose, TimeSpan.FromMinutes(5), null, CancellationToken.None);

        var first = await _sut.VerifyThenRemoveAsync(PhoneNumber, Otp, Purpose, null, CancellationToken.None);
        var second = await _sut.VerifyThenRemoveAsync(PhoneNumber, Otp, Purpose, null, CancellationToken.None);

        Assert.Equal(OtpVerificationOutcome.Success, first);
        Assert.Equal(OtpVerificationOutcome.InvalidOtp, second);
    }
}

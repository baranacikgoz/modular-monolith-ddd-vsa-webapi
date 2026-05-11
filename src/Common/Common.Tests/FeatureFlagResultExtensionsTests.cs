using Common.Application.Auth;
using Common.Application.FeatureManagement;
using Common.Domain.ResultMonad;
using Microsoft.FeatureManagement;
using NSubstitute;
using Xunit;

namespace Common.Tests;

#pragma warning disable CA1515
#pragma warning disable CA1707
public sealed class FeatureFlagResultExtensionsTests
{
    private readonly IFeatureManager _featureManager = Substitute.For<IFeatureManager>();

    [Fact]
    public async Task TapWhenFeatureEnabledAsync_SyncTap_FlagEnabled_CallsTap()
    {
        _featureManager.IsEnabledAsync(Arg.Any<string>()).Returns(true);
        var wasCalled = false;

        var result = await Task.FromResult(Result<int>.Success(42))
            .TapWhenFeatureEnabledAsync(_featureManager, "TestFeature", value =>
            {
                wasCalled = true;
                Assert.Equal(42, value);
            });

        Assert.False(result.IsFailure);
        Assert.Equal(42, result.Value);
        Assert.True(wasCalled);
    }

    [Fact]
    public async Task TapWhenFeatureEnabledAsync_SyncTap_FlagDisabled_DoesNotCallTap()
    {
        _featureManager.IsEnabledAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(false);
        var wasCalled = false;

        var result = await Task.FromResult(Result<int>.Success(42))
            .TapWhenFeatureEnabledAsync(_featureManager, "TestFeature", _ => wasCalled = true);

        Assert.False(result.IsFailure);
        Assert.Equal(42, result.Value);
        Assert.False(wasCalled);
    }

    [Fact]
    public async Task TapWhenFeatureEnabledAsync_SyncTap_FailureResult_DoesNotCallTap()
    {
        var error = new Error { Key = "TestError" };
        var wasCalled = false;

        var result = await Task.FromResult(Result<int>.Failure(error))
            .TapWhenFeatureEnabledAsync(_featureManager, "TestFeature", _ => wasCalled = true);

        Assert.True(result.IsFailure);
        Assert.False(wasCalled);
    }

    [Fact]
    public async Task TapWhenFeatureEnabledAsync_AsyncTap_FlagEnabled_CallsTap()
    {
        _featureManager.IsEnabledAsync(Arg.Any<string>()).Returns(true);
        var wasCalled = false;

        var result = await Task.FromResult(Result<int>.Success(42))
            .TapWhenFeatureEnabledAsync(_featureManager, "TestFeature", async value =>
            {
                await Task.Yield();
                wasCalled = true;
                Assert.Equal(42, value);
            });

        Assert.False(result.IsFailure);
        Assert.Equal(42, result.Value);
        Assert.True(wasCalled);
    }

    [Fact]
    public async Task TapWhenFeatureEnabledAsync_AsyncTap_FlagDisabled_DoesNotCallTap()
    {
        _featureManager.IsEnabledAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(false);
        var wasCalled = false;

        var result = await Task.FromResult(Result<int>.Success(42))
            .TapWhenFeatureEnabledAsync(_featureManager, "TestFeature", async _ =>
            {
                await Task.Yield();
                wasCalled = true;
            });

        Assert.False(result.IsFailure);
        Assert.Equal(42, result.Value);
        Assert.False(wasCalled);
    }

    [Fact]
    public async Task TapWhenFeatureEnabledAsync_AsyncTap_FailureResult_DoesNotCallTap()
    {
        var error = new Error { Key = "TestError" };
        var wasCalled = false;

        var result = await Task.FromResult(Result<int>.Failure(error))
            .TapWhenFeatureEnabledAsync(_featureManager, "TestFeature", async _ =>
            {
                await Task.Yield();
                wasCalled = true;
            });

        Assert.True(result.IsFailure);
        Assert.False(wasCalled);
    }
}
#pragma warning restore CA1707

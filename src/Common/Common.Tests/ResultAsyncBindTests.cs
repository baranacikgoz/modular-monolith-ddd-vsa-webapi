using Common.Domain.ResultMonad;
using Xunit;

#pragma warning disable CA1515, CA1707

namespace Common.Tests;

public sealed class ResultAsyncBindTests
{
    private static readonly Error SomeError = new() { Key = "SomeError" };

    [Fact]
    public async Task BindAsync_NonGenericChain_PropagatesInnerFailure()
    {
        var result = await Task.FromResult(Result.Success)
            .BindAsync(() => Task.FromResult(Result.Failure(SomeError)));

        Assert.True(result.IsFailure);
        Assert.Same(SomeError, result.Error);
    }

    [Fact]
    public async Task BindAsync_NonGenericChain_ShortCircuitsOnFailure()
    {
        var invoked = false;

        var result = await Task.FromResult(Result.Failure(SomeError))
            .BindAsync(() =>
            {
                invoked = true;
                return Task.FromResult(Result.Success);
            });

        Assert.True(result.IsFailure);
        Assert.False(invoked);
    }

    [Fact]
    public async Task BindAsync_NonGenericChain_ContinuesIntoGenericStep()
    {
        var result = await Result.Success
            .BindAsync(() => Task.FromResult(Result.Success))
            .BindAsync<int>(() => Task.FromResult(Result<int>.Success(42)));

        Assert.False(result.IsFailure);
        Assert.Equal(42, result.Value);
    }
}

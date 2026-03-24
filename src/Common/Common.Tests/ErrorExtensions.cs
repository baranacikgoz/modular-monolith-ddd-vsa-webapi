using Common.Domain.ResultMonad;
using Xunit;

namespace Common.Tests;

internal static class ErrorExtensions
{
    public static void ShouldBe(this Error currentError, Error expectedError)
    {
        Assert.NotNull(currentError);
        Assert.Equal(expectedError.Key, currentError.Key);
        Assert.Equal(expectedError.ParameterName, currentError.ParameterName);
        Assert.Equal(expectedError.Value, currentError.Value);
        Assert.Equal(expectedError.StatusCode, currentError.StatusCode);
        Assert.Equivalent(expectedError.SubErrors, currentError.SubErrors);
    }
}

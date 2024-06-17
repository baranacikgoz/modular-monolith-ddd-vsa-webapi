using Common.Domain.ResultMonad;
using FluentAssertions;

namespace UnitTests.Common;
public static class ErrorExtensions
{
    public static void ShouldBe(this Error currentError, Error expectedError)
    {
        currentError.Should().NotBeNull();
        currentError.Key.Should().Be(expectedError.Key);
        currentError.ParameterName.Should().Be(expectedError.ParameterName);
        currentError.Value.Should().Be(expectedError.Value);
        currentError.StatusCode.Should().Be(expectedError.StatusCode);
        currentError.SubErrors.Should().BeEquivalentTo(expectedError.SubErrors);
    }
}

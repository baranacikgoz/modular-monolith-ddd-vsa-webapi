using Common.Infrastructure.Persistence.Extensions;
using Xunit;

namespace Common.Tests.Persistence;

public class LikePatternTests
{
    [Theory]
    [InlineData("E00", "%E00%")]
    [InlineData("50%_off", "%50\\%\\_off%")]
    [InlineData(null, "%%")]
    public void Contains_EscapesWildcardsAndWrapsBothSides(string? term, string expected)
    {
        Assert.Equal(expected, LikePattern.Contains(term));
    }

    [Theory]
    [InlineData("E00", "E00%")]
    [InlineData("  E00 ", "E00%")]
    [InlineData("A_B%C\\D", "A\\_B\\%C\\\\D%")]
    [InlineData(null, "%")]
    public void StartsWith_EscapesTrimsAndAnchorsAtTheStart(string? term, string expected)
    {
        Assert.Equal(expected, LikePattern.StartsWith(term));
    }
}

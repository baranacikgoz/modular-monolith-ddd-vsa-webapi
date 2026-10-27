using Common.Domain;
using Xunit;

namespace Common.Tests;

#pragma warning disable CA1707 // Identifiers should not contain underscores

public sealed class ValueObjectTests
{
    [Fact]
    public void Equals_BothNull_ReturnsTrue()
    {
        var left = Maybe(null);
        var right = Maybe(null);

        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact]
    public void Equals_NullOnTheLeft_ReturnsFalse()
    {
        var left = Maybe(null);
        var right = Maybe(new TestValue("a", 1));

        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact]
    public void Equals_NullOnTheRight_ReturnsFalse()
    {
        var left = Maybe(new TestValue("a", 1));
        var right = Maybe(null);

        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact]
    public void Equals_NullLiteralComparison_CompilesWithoutCastAndReturnsFalse()
    {
        var value = Maybe(new TestValue("a", 1));

        Assert.False(value == null);
        Assert.True(value != null);
        Assert.False(null == value);
        Assert.True(null != value);
    }

    [Fact]
    public void Equals_SameComponents_ReturnsTrue()
    {
        var left = new TestValue("a", 1);
        var right = new TestValue("a", 1);

        Assert.True(left == right);
        Assert.False(left != right);
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact]
    public void Equals_DifferentComponents_ReturnsFalse()
    {
        var left = new TestValue("a", 1);
        var right = new TestValue("a", 2);

        Assert.False(left == right);
        Assert.True(left != right);
    }

    [Fact]
    public void Equals_SameComponentsDifferentType_ReturnsFalse()
    {
        var left = new TestValue("a", 1);
        var right = new OtherTestValue("a", 1);

        Assert.False(left == right);
        Assert.True(left != right);
    }

    // Defeats flow analysis so the null comparisons are not reported as constant conditions.
    private static TestValue? Maybe(TestValue? value)
    {
        return value;
    }

    private sealed class TestValue(string name, int number) : ValueObject
    {
        public override int CompareTo(ValueObject? other)
        {
            return other is TestValue value ? number.CompareTo(value.Number) : 1;
        }

        public int Number => number;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return name;
            yield return number;
        }
    }

    private sealed class OtherTestValue(string name, int number) : ValueObject
    {
        public override int CompareTo(ValueObject? other)
        {
            return 0;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return name;
            yield return number;
        }
    }
}

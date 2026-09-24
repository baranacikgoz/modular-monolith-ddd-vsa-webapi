using Common.Application.Pagination;
using Common.Domain.StronglyTypedIds;
using Xunit;

namespace Common.Tests.Pagination;

public class PaginationCursorTests
{
    public static TheoryData<object, object> SupportedPairs => new()
    {
        { new DateTimeOffset(2026, 9, 24, 10, 30, 15, 123, TimeSpan.FromHours(3)).AddTicks(4567), Guid.NewGuid() },
        { new DateTime(2026, 9, 24, 10, 30, 15, DateTimeKind.Utc), 42L },
        { 7, "seven" },
        { 12.345m, 99 },
        { 0.1d, 2.5f },
        { "name", ApplicationUserId.New() }
    };

    [Theory]
    [MemberData(nameof(SupportedPairs))]
    public void Encode_ThenDecode_RoundTripsEverySupportedType(object sortValue, object tiebreaker)
    {
        var encoded = PaginationCursor.Encode(sortValue, tiebreaker);

        var decoded = PaginationCursor.Decode(encoded);

        Assert.False(decoded.IsFailure);
        Assert.Equal(Unwrap(sortValue), decoded.Value!.SortValue);
        Assert.Equal(Unwrap(tiebreaker), decoded.Value.Tiebreaker);
    }

    [Fact]
    public void Encode_IsUrlSafe()
    {
        var encoded = PaginationCursor.Encode(new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero), Guid.NewGuid());

        Assert.DoesNotContain('+', encoded);
        Assert.DoesNotContain('/', encoded);
        Assert.DoesNotContain('=', encoded);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("not-base64url!!")]
    [InlineData("eyJTb3J0IjpudWxsfQ")]
    [InlineData("eyJTb3J0Ijp7IlQiOiJ4IiwiViI6IjEifSwiVGllIjp7IlQiOiJpIiwiViI6IjEifX0")]
    public void Decode_MalformedCursor_FailsValidationOnAfter(string cursor)
    {
        var decoded = PaginationCursor.Decode(cursor);

        Assert.True(decoded.IsFailure);
        Assert.Equal("Validation", decoded.Error!.Key);
        Assert.Contains(PaginationCursor.ParameterName, decoded.Error.SubErrors!);
    }

    [Fact]
    public void Materialize_GuidForStronglyTypedIdTarget_RebuildsTheId()
    {
        var id = ApplicationUserId.New();

        var materialized = PaginationCursor.Materialize(id.Value, typeof(ApplicationUserId));

        Assert.Equal(id, materialized);
    }

    [Fact]
    public void Materialize_TypeMismatch_Throws()
    {
        Assert.Throws<ArgumentException>(() => PaginationCursor.Materialize("text", typeof(int)));
    }

    [Fact]
    public void Encode_UnsupportedType_Throws()
    {
        Assert.Throws<ArgumentException>(() => PaginationCursor.Encode(TimeSpan.FromHours(1), 1));
    }

    private static object Unwrap(object value)
    {
        return value is IStronglyTypedId id ? id.Value : value;
    }
}

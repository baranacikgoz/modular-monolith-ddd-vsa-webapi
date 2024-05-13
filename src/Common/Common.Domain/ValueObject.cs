namespace Common.Domain;

// We're not using records because some may use "with" keyword to create a new record from an existing one,
// which bypasses the constructor or the static create method, therefore resulting in invalid objects and unexpected behavior.
public abstract class ValueObject : IComparable<ValueObject>
{
    protected static bool EqualOperator(ValueObject left, ValueObject right)
    {
        if (left is null ^ right is null)
        {
            return false;
        }

        return left is null || left.Equals(right);
    }

    protected static bool NotEqualOperator(ValueObject left, ValueObject right)
        => !EqualOperator(left, right);

    protected abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
        {
            return false;
        }

        var other = (ValueObject)obj;
        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode()
        => GetEqualityComponents()
            .Select(x => x != null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);
    public abstract int CompareTo(ValueObject? other);
    public static bool operator ==(ValueObject left, ValueObject right)
        => EqualOperator(left, right);
    public static bool operator !=(ValueObject left, ValueObject right)
        => NotEqualOperator(left, right);
    public static bool operator <(ValueObject left, ValueObject right)
        => left.CompareTo(right) < 0;

    public static bool operator <=(ValueObject left, ValueObject right)
        => left.CompareTo(right) <= 0;

    public static bool operator >(ValueObject left, ValueObject right)
        => left.CompareTo(right) > 0;

    public static bool operator >=(ValueObject left, ValueObject right)
        => left.CompareTo(right) >= 0;
}

namespace Common.Domain;

// We're not using records because some may use "with" keyword to create a new record from an existing one,
// which bypasses the constructor or the static create method, therefore resulting in invalid objects and unexpected behavior.
public abstract class ValueObject : IComparable<ValueObject>
{
    public abstract int CompareTo(ValueObject? other);

    protected static bool EqualOperator(ValueObject left, ValueObject right)
    {
        return !(left is null ^ right is null) && (left is null || left.Equals(right));
    }

    protected static bool NotEqualOperator(ValueObject left, ValueObject right)
    {
        return !EqualOperator(left, right);
    }

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
    {
        return GetEqualityComponents()
            .Select(x => x != null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);
    }

    public static bool operator ==(ValueObject left, ValueObject right)
    {
        return EqualOperator(left, right);
    }

    public static bool operator !=(ValueObject left, ValueObject right)
    {
        return NotEqualOperator(left, right);
    }

    public static bool operator <(ValueObject left, ValueObject right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator <=(ValueObject left, ValueObject right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >(ValueObject left, ValueObject right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator >=(ValueObject left, ValueObject right)
    {
        return left.CompareTo(right) >= 0;
    }
}

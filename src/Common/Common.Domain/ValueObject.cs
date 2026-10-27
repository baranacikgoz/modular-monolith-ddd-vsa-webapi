namespace Common.Domain;

// We're not using records because some may use "with" keyword to create a new record from an existing one,
// which bypasses the constructor or the static create method, therefore resulting in invalid objects and unexpected behavior.
// Ordering follows IComparable: null sorts before every non-null value and two nulls are equal.
public abstract class ValueObject : IComparable<ValueObject>
{
    public abstract int CompareTo(ValueObject? other);

    protected static bool EqualOperator(ValueObject? left, ValueObject? right)
    {
        return left is null ? right is null : left.Equals(right);
    }

    protected static bool NotEqualOperator(ValueObject? left, ValueObject? right)
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
        var hash = new HashCode();
        foreach (var component in GetEqualityComponents())
        {
            hash.Add(component);
        }

        return hash.ToHashCode();
    }

    public static bool operator ==(ValueObject? left, ValueObject? right)
    {
        return EqualOperator(left, right);
    }

    public static bool operator !=(ValueObject? left, ValueObject? right)
    {
        return NotEqualOperator(left, right);
    }

    public static bool operator <(ValueObject? left, ValueObject? right)
    {
        return Compare(left, right) < 0;
    }

    public static bool operator <=(ValueObject? left, ValueObject? right)
    {
        return Compare(left, right) <= 0;
    }

    public static bool operator >(ValueObject? left, ValueObject? right)
    {
        return Compare(left, right) > 0;
    }

    public static bool operator >=(ValueObject? left, ValueObject? right)
    {
        return Compare(left, right) >= 0;
    }

    private static int Compare(ValueObject? left, ValueObject? right)
    {
        if (left is null)
        {
            return right is null ? 0 : -1;
        }

        return right is null ? 1 : left.CompareTo(right);
    }
}

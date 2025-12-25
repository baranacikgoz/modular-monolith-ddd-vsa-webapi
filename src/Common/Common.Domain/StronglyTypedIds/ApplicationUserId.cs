namespace Common.Domain.StronglyTypedIds;

public readonly record struct ApplicationUserId : IStronglyTypedId
{
    // Parameterless constructor for EF
    public ApplicationUserId() : this(DefaultIdType.CreateVersion7())
    {
    }

    public ApplicationUserId(DefaultIdType value) => Value = value;
    public bool IsEmpty => Value == DefaultIdType.Empty;
    public DefaultIdType Value { get; init; }

    public static ApplicationUserId New()
    {
        return new ApplicationUserId(DefaultIdType.CreateVersion7());
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public static bool TryParse(string str, out ApplicationUserId id)
    {
        return StronglyTypedIdHelper.TryDeserialize(str, out id);
    }
}

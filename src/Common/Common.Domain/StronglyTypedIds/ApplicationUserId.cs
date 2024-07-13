namespace Common.Domain.StronglyTypedIds;

public readonly record struct ApplicationUserId : IStronglyTypedId
{
    public DefaultIdType Value { get; init; }
    public bool IsEmpty => Value == DefaultIdType.Empty;

    // Parameterless constructor for EF
    public ApplicationUserId() : this(DefaultIdType.NewGuid()) { }

    public ApplicationUserId(DefaultIdType value)
    {
        Value = value;
    }

    public static ApplicationUserId New() => new(DefaultIdType.NewGuid());
    public override string ToString() => Value.ToString();
    public static bool TryParse(string str, out ApplicationUserId id) => StronglyTypedIdHelper.TryDeserialize(str, out id);
}

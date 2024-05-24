namespace Common.Domain.StronglyTypedIds;

public readonly record struct ApplicationUserId : IStronglyTypedId
{
    public Guid Value { get; init; }
    public bool IsEmpty => Value == Guid.Empty;

    // Parameterless constructor for EF
    public ApplicationUserId() : this(Guid.NewGuid()) { }

    public ApplicationUserId(Guid value)
    {
        Value = value;
    }

    public static ApplicationUserId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
    public static bool TryParse(string str, out ApplicationUserId id) => StronglyTypedIdHelper.TryDeserialize(str, out id);
}

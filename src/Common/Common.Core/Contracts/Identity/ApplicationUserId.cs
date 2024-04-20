using Common.Core.Contracts;

namespace Common.Core.Contracts.Identity;

public readonly record struct ApplicationUserId : IStronglyTypedId
{
    public Guid Value { get; init; } = Guid.NewGuid();

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

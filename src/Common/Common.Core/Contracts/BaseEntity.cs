namespace Common.Core.Contracts;

public interface IEntity<out TId>
{
    TId Id { get; }
}

public abstract class BaseEntity<TId> : IEntity<TId>
{
    protected BaseEntity(TId id)
    {
        Id = id;
    }
    public TId Id { get; }
}

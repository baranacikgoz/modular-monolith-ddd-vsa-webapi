using Common.Domain.Entities;

namespace Common.Application.Persistence.Projections;

/// <summary>
///     Read-model row kept in sync from another module's integration events. <see cref="SourceVersion" /> is the
///     version of the source aggregate the row last reflected, so a late or redelivered event cannot overwrite a
///     newer state. Rows are natural-keyed by the source id, hence the non-generic base.
/// </summary>
public abstract class ProjectionEntity : AuditableEntity
{
    public long SourceVersion { get; protected set; }

    /// <summary>
    ///     False when <paramref name="incoming" /> is not newer than what the row already reflects (a stale or
    ///     duplicate event); true, and the row now claims <paramref name="incoming" />, otherwise.
    /// </summary>
    protected internal bool AcceptVersion(long incoming)
    {
        if (incoming <= SourceVersion)
        {
            return false;
        }

        SourceVersion = incoming;
        return true;
    }
}

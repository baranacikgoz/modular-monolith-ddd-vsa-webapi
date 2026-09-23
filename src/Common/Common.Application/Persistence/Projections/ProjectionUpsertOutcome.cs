namespace Common.Application.Persistence.Projections;

public enum ProjectionUpsertOutcome
{
    Inserted,
    Updated,
    /// <summary>The event was not newer than the stored row; nothing was written.</summary>
    Stale
}

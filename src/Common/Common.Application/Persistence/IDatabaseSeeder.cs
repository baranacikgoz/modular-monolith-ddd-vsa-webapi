namespace Common.Application.Persistence;

/// <summary>
///     Contract for module database seeders.
///     Seeders are executed in <see cref="Priority"/> order (ascending) by a centralized orchestrator
///     after the application has fully started.
/// </summary>
public interface IDatabaseSeeder
{
    /// <summary>
    ///     Determines execution order. Lower values run first.
    ///     Use the module's <c>StartupPriority</c> for consistency.
    /// </summary>
    int Priority { get; }

    /// <summary>
    ///     Seeds the database. Implementations must be idempotent.
    /// </summary>
    Task SeedAsync(CancellationToken cancellationToken);
}

namespace Common.Infrastructure.Persistence;

/// <summary>
///     Marker interface that, when registered in DI, signals <see cref="MigrationGuard"/>
///     to auto-apply pending migrations instead of throwing.
///     Only registered by test infrastructure (Testcontainers) — never in production.
/// </summary>
public interface IAutoMigrateMarker;

/// <summary>
///     Default implementation. Test factories register this as a singleton.
/// </summary>
public sealed class AutoMigrateMarker : IAutoMigrateMarker;

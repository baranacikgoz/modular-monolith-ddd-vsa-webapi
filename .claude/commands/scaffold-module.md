---
description: Scaffold a new top-level module: four projects, DbContext, persistence setup, IModule, telemetry, tests, Makefile targets.
argument-hint: "<ModuleName>"
allowed-tools: Read, Edit, Write, Bash, Glob, Grep
---

Scaffold module: $ARGUMENTS

Mirror Products file for file (CLAUDE.md §2 exemplars). Layout in §1.

1. Projects: `dotnet new classlib -n {M}.{Layer} -o src/Modules/{M}/{M}.{Layer}` for Domain, Application, Endpoints, Infrastructure. Delete `Class1.cs`, add `public interface IAssemblyReference { }` to each. Reference only `Common.*` projects and the module's own lower layers.
2. `{M}.Application/Persistence/I{M}DbContext.cs`.
3. `{M}.Infrastructure/Persistence/{M}DbContext.cs`: inherit `BaseDbContext`; in `OnModelCreating` call base, then `HasDefaultSchema(nameof({M}))` (PascalCase), `ApplyConfigurationsFromAssembly(typeof({M}DbContext).Assembly)`, `Ignore<DomainEvent>()`, `ApplyConfiguration(new AuditLogEntryConfiguration())`.
4. `{M}.Infrastructure/Persistence/Setup.cs`: copy `AddPersistence` (`Seeder`, `IDatabaseSeeder`, `AddModuleDbContext`) and `UsePersistence` (`MigrationGuard`) from Products. Keep the seeder pair even with no seed data; `DatabaseSeederOrchestrator` no-ops on empty seeders.
5. `{M}.Infrastructure/Telemetry/{M}Telemetry.cs`: copy `ProductsTelemetry`.
6. `{M}.Endpoints/{M}Module.cs`: copy `ProductsModule`, pick `StartupPriority` by dependency order, drop `RateLimitingPolicies` unless needed.
7. Add `"{M}"` to `EnabledModules` in `src/Host/Host/Configurations/modules.json`.
8. Tests: `dotnet new xunit -n {M}.Tests -o src/Modules/{M}/{M}.Tests`; reference `{M}.Domain`, `Common.Tests` (supplies Testcontainers, Respawn, NSubstitute, xunit.v3, `IntegrationTestFactory`), `{M}.Infrastructure` if EF types are needed, `Outbox` if tests assert `OutboxMessages`. Add package `Bogus` only. Never reference `{M}.Endpoints` (Host wildcard already loads it). Copy `IntegrationTestWebAppFactory` (`GetActiveModules() => ["{M}", "Outbox", "IAM"]`) and `IntegrationTestCollection` from Products.
9. Makefile: add `test-{m}` and, if the module has a DbContext, `ef-add-{M}` and `ef-script-{M}` mirroring Products. Add the module schema to `SchemasToInclude` in `Common.Tests/BaseIntegrationTest.cs`. Add the module to the CI test matrix.
10. `make build`, `make test-{m}`, then `/manage-migration {M} Initial`.

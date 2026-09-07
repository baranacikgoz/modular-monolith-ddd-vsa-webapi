using Common.Tests;

namespace Products.Tests;

public class IntegrationTestWebAppFactory : IntegrationTestFactory
{
    // IAM is not activated: store seeding falls back gracefully when GetSeedUserIds has no handler
    // (Seeder.Stores.cs), and activating IAM here would require a Keycloak container per test run.
    protected override string[] GetActiveModules() => ["Products", "Outbox"];
}

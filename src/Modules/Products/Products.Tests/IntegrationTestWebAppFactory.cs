using Common.Tests;

namespace Products.Tests;

public class IntegrationTestWebAppFactory : IntegrationTestFactory
{
    protected override string[] GetActiveModules() => ["Products", "Outbox", "IAM"];
}

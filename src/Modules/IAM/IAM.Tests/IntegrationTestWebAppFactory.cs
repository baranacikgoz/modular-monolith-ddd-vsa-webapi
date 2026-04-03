using Common.Tests;

namespace IAM.Tests;

public class IntegrationTestWebAppFactory : IntegrationTestFactory
{
    protected override string[] GetActiveModules() => ["IAM", "Outbox"];
}

using Common.Tests;

namespace Inventory.Tests;

public class IntegrationTestWebAppFactory : IntegrationTestFactory
{
    // Products is active alongside Inventory so the real cross-module paths run end to end: the
    // ProductCreatedIntegrationEvent outbox message (publish side) and both InterModuleRequest
    // directions (GetProductRequest, GetStockLevelRequest). The OutboxProcessor itself stays off
    // (OutboxOptions:IsProcessor=false, set globally in IntegrationTestFactory) - slice tests only
    // assert the outbox message was written, matching the convention used everywhere else in this
    // repo's test suite (see IntegrationTestFactory's own comment on that setting).
    protected override string[] GetActiveModules() => ["Inventory", "Products", "Outbox"];
}

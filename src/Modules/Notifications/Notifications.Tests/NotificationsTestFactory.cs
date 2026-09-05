using Common.Tests;
using Xunit;

namespace Notifications.Tests;

public class NotificationsTestFactory : IntegrationTestFactory
{
    // Outbox is required by BaseDbContext's outbox insert; Notifications owns the device registry under test.
    protected override string[] GetActiveModules() => ["Notifications", "Outbox"];
}

[CollectionDefinition("IntegrationTestCollection")]
public class IntegrationTestCollection : ICollectionFixture<NotificationsTestFactory>;

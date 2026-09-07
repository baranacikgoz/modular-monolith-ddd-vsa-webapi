using System.Text.Json;
using Common.Tests;
using Xunit;

namespace Host.Tests;

// IAM has no database, so Keycloak's own event log is the only durable audit trail for identity events
// (PR #149 #5). This guards the realm file against silently losing that configuration on a future edit.
public class RealmAuditConfigurationTests
{
    [Fact]
    public void Realm_KeepsIdentityAndAdminEventsEnabledWithRetention()
    {
        using var document = JsonDocument.Parse(File.ReadAllText(TestPaths.RealmFile));
        var root = document.RootElement;

        Assert.True(root.GetProperty("eventsEnabled").GetBoolean());
        Assert.True(root.GetProperty("eventsExpiration").GetInt64() > 0);
        Assert.NotEmpty(root.GetProperty("enabledEventTypes").EnumerateArray());
        Assert.True(root.GetProperty("adminEventsEnabled").GetBoolean());
    }
}

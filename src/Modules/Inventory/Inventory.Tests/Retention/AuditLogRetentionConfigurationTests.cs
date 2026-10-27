using Common.Application.Options;
using Common.Tests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace Inventory.Tests.Retention;

/// <summary>The stock ledger's history is audit evidence: its schema keeps the AuditLog five years, longer than the global default.</summary>
[Collection("IntegrationTestCollection")]
public class AuditLogRetentionConfigurationTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public void InventorySchema_KeepsItsAuditLogLongerThanTheGlobalDefault()
    {
        var options = Scope.ServiceProvider.GetRequiredService<IOptions<AuditLogOptions>>().Value;

        Assert.Equal(1826, options.PerSchemaRetentionDays["Inventory"]);
        Assert.True(options.PerSchemaRetentionDays["Inventory"] > options.RetentionDays);
    }
}

using Host.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace Host.Tests;

public class AuditLogRetentionOverrideValidationTests
{
    private static readonly string[] DiscoveredModuleNames = ["Products", "Inventory", "IAM"];

    [Fact]
    public void ValidateAuditLogRetentionOverrides_UnknownSchemaKey_Throws()
    {
        var configuration = BuildConfiguration(new Dictionary<string, string?>
        {
            ["AuditLogOptions:PerSchemaRetentionDays:Ordres"] = "3650"
        });

        var ex = Assert.Throws<InvalidOperationException>(
            () => Setup.ValidateAuditLogRetentionOverrides(configuration, DiscoveredModuleNames));

        Assert.Contains("Ordres", ex.Message, StringComparison.Ordinal);
    }

    [Fact]
    public void ValidateAuditLogRetentionOverrides_KnownSchemaKey_DoesNotThrow()
    {
        var configuration = BuildConfiguration(new Dictionary<string, string?>
        {
            ["AuditLogOptions:PerSchemaRetentionDays:Inventory"] = "3650"
        });

        Setup.ValidateAuditLogRetentionOverrides(configuration, DiscoveredModuleNames);
    }

    [Fact]
    public void ValidateAuditLogRetentionOverrides_NoOverrides_DoesNotThrow()
    {
        var configuration = BuildConfiguration([]);

        Setup.ValidateAuditLogRetentionOverrides(configuration, DiscoveredModuleNames);
    }

    private static IConfiguration BuildConfiguration(Dictionary<string, string?> values)
    {
        return new ConfigurationBuilder().AddInMemoryCollection(values).Build();
    }
}

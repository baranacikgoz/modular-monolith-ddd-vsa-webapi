namespace Host.Configurations;

internal static class Setup
{
    public static WebApplicationBuilder AddConfigurations(this WebApplicationBuilder builder)
    {
        const string configurationsDirectory = "Configurations";
        var configuration = builder.Configuration;

        AddJsonFile(configuration, $"{configurationsDirectory}/localization");
        AddJsonFile(configuration, $"{configurationsDirectory}/jwt");
        AddJsonFile(configuration, $"{configurationsDirectory}/database");
        AddJsonFile(configuration, $"{configurationsDirectory}/otp");
        AddJsonFile(configuration, $"{configurationsDirectory}/captcha");
        AddJsonFile(configuration, $"{configurationsDirectory}/observability");
        AddJsonFile(configuration, $"{configurationsDirectory}/requestLogging");
        AddJsonFile(configuration, $"{configurationsDirectory}/rateLimiting");
        AddJsonFile(configuration, $"{configurationsDirectory}/openApi");
        AddJsonFile(configuration, $"{configurationsDirectory}/eventBus");
        AddJsonFile(configuration, $"{configurationsDirectory}/interModuleRequest");
        AddJsonFile(configuration, $"{configurationsDirectory}/outbox");
        AddJsonFile(configuration, $"{configurationsDirectory}/backgroundJobs");
        AddJsonFile(configuration, $"{configurationsDirectory}/caching");
        AddJsonFile(configuration, $"{configurationsDirectory}/auditLog");
        AddJsonFile(configuration, $"{configurationsDirectory}/modules");
        AddJsonFile(configuration, $"{configurationsDirectory}/healthCheck");
        AddJsonFile(configuration, $"{configurationsDirectory}/cors");
        AddJsonFile(configuration, $"{configurationsDirectory}/reverseProxy");
        AddJsonFile(configuration, $"{configurationsDirectory}/securityHeaders");
        AddJsonFile(configuration, $"{configurationsDirectory}/featureFlags");
        AddJsonFile(configuration, $"{configurationsDirectory}/signalR");
        AddJsonFile(configuration, $"{configurationsDirectory}/fullTextSearch");

        configuration.AddEnvironmentVariables();

        return builder;
    }

    private static void AddJsonFile(ConfigurationManager configuration, string filePath)
    {
        configuration.AddJsonFile($"{filePath}.json", false, true);

        // Intentionally disabled: this repo has no HashiCorp Vault-like integration, so these
        // settings must be materialized at deploy time using the same file names (no {environment}
        // suffix). Using per-environment overrides makes that deploy-time materialization complex
        // and confusing — one instance injected per deploy is fine.
#pragma warning disable S125 // Sections of code should not be commented out — kept deliberately to document the disabled override
        // var environment = builder.Environment.EnvironmentName;
        // configuration.AddJsonFile($"{filePath}.{environment}.json", true, true);
#pragma warning restore S125
    }
}

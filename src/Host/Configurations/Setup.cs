namespace Host.Configurations;

internal static class Setup
{
    public static WebApplicationBuilder AddConfigurations(this WebApplicationBuilder builder)
    {
        const string configurationsDirectory = "Configurations";
        var environmentName = builder.Environment.EnvironmentName;
        var configuration = builder.Configuration;

        AddJsonFile(configuration, environmentName, "appsettings");
        AddJsonFile(configuration, environmentName, $"{configurationsDirectory}/localization");
        AddJsonFile(configuration, environmentName, $"{configurationsDirectory}/jwt");
        AddJsonFile(configuration, environmentName, $"{configurationsDirectory}/database");
        AddJsonFile(configuration, environmentName, $"{configurationsDirectory}/otp");
        AddJsonFile(configuration, environmentName, $"{configurationsDirectory}/captcha");
        AddJsonFile(configuration, environmentName, $"{configurationsDirectory}/logging");
        AddJsonFile(configuration, environmentName, $"{configurationsDirectory}/rateLimiting");
        AddJsonFile(configuration, environmentName, $"{configurationsDirectory}/monitoring");

        configuration.AddEnvironmentVariables();
        return builder;
    }

    private static void AddJsonFile(ConfigurationManager configuration, string environment, string filePath)
    {
        configuration.AddJsonFile($"{filePath}.json", optional: false, reloadOnChange: true);
        configuration.AddJsonFile($"{filePath}.{environment}.json", optional: true, reloadOnChange: true);
    }
}

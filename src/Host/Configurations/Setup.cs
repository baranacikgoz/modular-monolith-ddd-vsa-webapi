namespace Host.Configurations;

public static class Setup
{
    public static WebApplicationBuilder AddConfigurations(this WebApplicationBuilder builder)
    {
        const string configurationsDirectory = "Configurations";
        var environmentName = builder.Environment.EnvironmentName;
        var configuration = builder.Configuration;

        AddJsonFile(configuration, environmentName, "appsettings.json");
        AddJsonFile(configuration, environmentName, $"{configurationsDirectory}/localization.json");
        AddJsonFile(configuration, environmentName, $"{configurationsDirectory}/rabbitmq.json");
        AddJsonFile(configuration, environmentName, $"{configurationsDirectory}/logger.json");
        AddJsonFile(configuration, environmentName, $"{configurationsDirectory}/jwt.json");
        AddJsonFile(configuration, environmentName, $"{configurationsDirectory}/database.json");
        AddJsonFile(configuration, environmentName, $"{configurationsDirectory}/otp.json");
        AddJsonFile(configuration, environmentName, $"{configurationsDirectory}/captcha.json");

        configuration.AddEnvironmentVariables();
        return builder;
    }

    private static void AddJsonFile(ConfigurationManager configuration, string environment, string filePath)
    {
        configuration.AddJsonFile(filePath, optional: false, reloadOnChange: true);
        configuration.AddJsonFile($"{filePath}.{environment}", optional: true, reloadOnChange: true);
    }
}

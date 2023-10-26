namespace Host.Configurations;

public static class Setup
{
    public static WebApplicationBuilder AddConfigurations(this WebApplicationBuilder builder)
    {
        const string configurationsDirectory = "Configurations";
        var env = builder.Environment;

        builder
            .Configuration
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"{configurationsDirectory}/localization.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{configurationsDirectory}/localization.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"{configurationsDirectory}/rabbitmq.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{configurationsDirectory}/rabbitmq.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"{configurationsDirectory}/logger.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{configurationsDirectory}/logger.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"{configurationsDirectory}/jwt.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{configurationsDirectory}/jwt.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"{configurationsDirectory}/database.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{configurationsDirectory}/database.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"{configurationsDirectory}/otp.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{configurationsDirectory}/otp.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"{configurationsDirectory}/captcha.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{configurationsDirectory}/captcha.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)

            .AddEnvironmentVariables();

        return builder;
    }
}

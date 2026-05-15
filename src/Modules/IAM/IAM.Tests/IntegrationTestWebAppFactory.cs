using Common.Tests;
using IAM.Application.Captcha.Services;
using IAM.Infrastructure.Captcha.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IAM.Tests;

public class IntegrationTestWebAppFactory : IntegrationTestFactory
{
    protected override string[] GetActiveModules()
    {
        return ["IAM", "Outbox"];
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.ConfigureAppConfiguration((_, config) =>
        {
            config.AddInMemoryCollection(new Dictionary<string, string?>
            {
                { "FeatureManagement:IAM.Captcha", "true" }
            });
        });

        builder.ConfigureTestServices(services => { services.AddSingleton<ICaptchaService, DummyCaptchaService>(); });
    }
}

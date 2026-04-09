using Common.Tests;

using IAM.Infrastructure.Captcha.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace IAM.Tests;

public class IntegrationTestWebAppFactory : IntegrationTestFactory
{
    protected override string[] GetActiveModules() => ["IAM", "Outbox"];

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.ConfigureTestServices(services =>
        {
            services.AddSingleton<IAM.Application.Captcha.Services.ICaptchaService, DummyCaptchaService>();
        });
    }
}

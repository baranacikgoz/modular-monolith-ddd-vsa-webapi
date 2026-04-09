using Common.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;
using Xunit;

namespace Common.Tests;

public class IntegrationTestFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:15-alpine")
        .WithDatabase("test_db")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    public string ConnectionString => _dbContainer.GetConnectionString();

    public virtual async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
    }

    Task IAsyncLifetime.DisposeAsync()
    {
        return _dbContainer.StopAsync();
    }

    protected virtual string[] GetActiveModules()
    {
        return ["*"];
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var overrideModule = string.Join(",", GetActiveModules());
        builder.UseSetting("TestModuleOverride", overrideModule);

        builder.ConfigureAppConfiguration((context, config) =>
        {
            var confDict = new Dictionary<string, string?>
            {
                { "ConnectionString", _dbContainer.GetConnectionString() },
                { "DatabaseOptions:ConnectionString", _dbContainer.GetConnectionString() },
                { "CustomRateLimitingOptions:Global:Limit", "5000" },
                { "CustomRateLimitingOptions:Global:PeriodInMs", "60000" },
                { "CustomRateLimitingOptions:Global:QueueLimit", "1000" },
                { "CustomRateLimitingOptions:Sms:Limit", "5000" },
                { "CustomRateLimitingOptions:Sms:PeriodInMs", "60000" },
                { "CustomRateLimitingOptions:Sms:QueueLimit", "1000" },
                { "AuditLogOptions:RetentionDays", "90" },
                { "CaptchaOptions:BaseUrl", "" }
            };
            config.AddInMemoryCollection(confDict);
        });

        builder.ConfigureTestServices(services =>
        {
            services.AddSingleton<IAutoMigrateMarker>(new AutoMigrateMarker());

            services.AddAuthentication(TestAuthHandler.AuthenticationScheme)
                .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(TestAuthHandler.AuthenticationScheme,
                    options => { });

            services.Configure<AuthenticationOptions>(options =>
            {
                options.DefaultAuthenticateScheme = TestAuthHandler.AuthenticationScheme;
                options.DefaultChallengeScheme = TestAuthHandler.AuthenticationScheme;
            });

            services.AddAuthorizationBuilder()
                .SetDefaultPolicy(new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(TestAuthHandler.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build());

            services.AddSingleton<IAuthorizationHandler, AllowAllAuthorizationHandler>();
        });
    }

    public override async ValueTask DisposeAsync()
    {
        await _dbContainer.DisposeAsync();
        await base.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}

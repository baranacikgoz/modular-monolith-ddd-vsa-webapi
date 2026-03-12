using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Testcontainers.PostgreSql;
using Xunit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.TestHost;

namespace Common.Tests;

public class IntegrationTestFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:15-alpine")
        .WithDatabase("test_db")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
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
                { "CustomRateLimitingOptions:Sms:QueueLimit", "1000" }
            };
            config.AddInMemoryCollection(confDict);
        });

        builder.ConfigureTestServices(services =>
        {
            services.AddAuthentication(TestAuthHandler.AuthenticationScheme)
                .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(TestAuthHandler.AuthenticationScheme, options => { });

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

    public virtual async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
    }

    Task IAsyncLifetime.DisposeAsync()
    {
        return _dbContainer.StopAsync();
    }

    public override async ValueTask DisposeAsync()
    {
        await _dbContainer.DisposeAsync();
        await base.DisposeAsync();
        GC.SuppressFinalize(this);
    }

    public string ConnectionString => _dbContainer.GetConnectionString();
}

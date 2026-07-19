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
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder("postgres:15-alpine")
        .WithDatabase("test_db")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    public string ConnectionString => _dbContainer.GetConnectionString();

    public virtual async ValueTask InitializeAsync()
    {
        await _dbContainer.StartAsync();
    }

    protected virtual string[] GetActiveModules()
    {
        return ["*"];
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var overrideModule = string.Join(",", GetActiveModules());
        builder.UseSetting("TestModuleOverride", overrideModule);

        // Transport is chosen at module-registration time, before ConfigureAppConfiguration's in-memory
        // overrides are merged — so it must travel via UseSetting (which IS visible at registration, like
        // TestModuleOverride). In-memory transport delivers inter-module request/response in-process so
        // tests need no RabbitMQ broker. Outbox.Tests overrides this back to RabbitMQ for its own broker.
        builder.UseSetting("MassTransitOptions:UseInMemoryTransport", "true");

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
                { "CustomRateLimitingOptions:TokenCreate:Limit", "5000" },
                { "CustomRateLimitingOptions:TokenCreate:PeriodInMs", "60000" },
                { "CustomRateLimitingOptions:TokenCreate:QueueLimit", "1000" },
                { "CustomRateLimitingOptions:CheckRegistration:Limit", "5000" },
                { "CustomRateLimitingOptions:CheckRegistration:PeriodInMs", "60000" },
                { "CustomRateLimitingOptions:CheckRegistration:QueueLimit", "1000" },
                { "CustomRateLimitingOptions:TokenRefresh:Limit", "5000" },
                { "CustomRateLimitingOptions:TokenRefresh:PeriodInMs", "60000" },
                { "CustomRateLimitingOptions:TokenRefresh:QueueLimit", "1000" },
                { "CustomRateLimitingOptions:Register:Limit", "5000" },
                { "CustomRateLimitingOptions:Register:PeriodInMs", "60000" },
                { "CustomRateLimitingOptions:Register:QueueLimit", "1000" },
                { "AuditLogOptions:RetentionDays", "90" },
                { "CaptchaOptions:BaseUrl", "" },
                // Processor OFF for slice tests: the OutboxProcessor BackgroundService opens
                // "FOR UPDATE" transactions on OutboxMessages every poll. On CPU-starved CI runners
                // that transaction stays open long enough to block Respawn's between-test DELETE,
                // which then fails with "Npgsql ... Timeout during reading attempt". Slice tests only
                // assert the message was written (IsProcessed == false), so the processor must not run.
                // Read at runtime by OutboxProcessor via IOptions, so this in-memory value applies.
                // Outbox.Tests opts back in via its own OutboxTestWebAppFactory (IsProcessor = true).
                { "OutboxOptions:IsProcessor", "false" },
                { "OutboxOptions:PollIntervalMs", "500" },
                { "OutboxOptions:BatchSize", "50" },
                { "OutboxOptions:MaxRetryCount", "3" },
                { "RabbitMqOptions:Host", "localhost" },
                { "RabbitMqOptions:Username", "guest" },
                { "RabbitMqOptions:Password", "guest" }
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

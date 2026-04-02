using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Testcontainers.PostgreSql;
using Xunit;

namespace Host.Tests;

public class HostTestFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:15-alpine")
        .WithDatabase("host_test_db")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    private string? _moduleOverride;

    public HostTestFactory WithModules(string modules)
    {
        _moduleOverride = modules;
        return this;
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _dbContainer.StopAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var connectionString = _dbContainer.GetConnectionString();
        builder.UseSetting("ConnectionString", connectionString);
        builder.UseSetting("DatabaseOptions:ConnectionString", connectionString);

        if (_moduleOverride != null)
        {
            builder.UseSetting("TestModuleOverride", _moduleOverride);
        }
        else
        {
            // Default to all if override not set, ensuring it's explicit
            builder.UseSetting("TestModuleOverride", "*");
        }

        builder.UseEnvironment("Testing");
    }

    public override async ValueTask DisposeAsync()
    {
        await _dbContainer.DisposeAsync();
        await base.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}

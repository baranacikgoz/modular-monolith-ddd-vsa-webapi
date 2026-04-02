using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace Host.Tests;

public class HostTestFactory : WebApplicationFactory<Program>
{
    private string? _moduleOverride;

    public HostTestFactory WithModules(string modules)
    {
        _moduleOverride = modules;
        return this;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // UseSetting is applied early to the builder's configuration.
        builder.UseSetting("ConnectionString", "Host=localhost;Database=dummy;Username=postgres;Password=postgres");
        builder.UseSetting("DatabaseOptions:ConnectionString", "Host=localhost;Database=dummy;Username=postgres;Password=postgres");

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
}

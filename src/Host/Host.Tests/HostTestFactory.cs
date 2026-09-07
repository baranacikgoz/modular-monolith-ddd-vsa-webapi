using Common.Tests;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Host.Tests;

public class HostTestFactory : IntegrationTestFactory
{
    private string[]? _moduleOverride;
    private string? _keycloakBaseAddress;

    public HostTestFactory WithModules(string modules)
    {
        _moduleOverride = modules.Split(',', StringSplitOptions.RemoveEmptyEntries);
        return this;
    }

    protected override string[] GetActiveModules()
    {
        return _moduleOverride ?? ["*"];
    }

    public override async ValueTask InitializeAsync()
    {
        // The IAM module's readiness check needs a reachable realm; every host boot here may include IAM.
        var keycloak = await SharedKeycloak.GetAsync();
        _keycloakBaseAddress = keycloak.GetBaseAddress().TrimEnd('/');

        await base.InitializeAsync();
        await WaitUntilReadyAsync(TimeSpan.FromSeconds(60));
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        if (_keycloakBaseAddress is not null)
        {
            // Both channels: UseSetting for registration-time reads, in-memory config for runtime IOptions
            // (the JSON config files are added after host settings and would otherwise win).
            builder.UseSetting("KeycloakOptions:BaseUrl", _keycloakBaseAddress);
            builder.ConfigureAppConfiguration((_, config) => config.AddInMemoryCollection(
                new Dictionary<string, string?> { { "KeycloakOptions:BaseUrl", _keycloakBaseAddress } }));
        }
    }

    private async Task WaitUntilReadyAsync(TimeSpan timeout)
    {
        var client = CreateClient();
        using var cts = new CancellationTokenSource(timeout);
        while (!cts.IsCancellationRequested)
        {
            try
            {
                var response = await client.GetAsync(new Uri("/health/ready", UriKind.Relative), cts.Token);
                if (response.IsSuccessStatusCode)
                {
                    return;
                }
            }
            catch (Exception ex) when (!cts.IsCancellationRequested)
            {
                _ = ex;
            }

            await Task.Delay(TimeSpan.FromMilliseconds(500), cts.Token)
                .ConfigureAwait(ConfigureAwaitOptions.SuppressThrowing);
        }
    }
}

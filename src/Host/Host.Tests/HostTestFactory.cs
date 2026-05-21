using Common.Tests;

namespace Host.Tests;

public class HostTestFactory : IntegrationTestFactory
{
    private string[]? _moduleOverride;

    public HostTestFactory WithModules(string modules)
    {
        _moduleOverride = modules.Split(',', StringSplitOptions.RemoveEmptyEntries);
        return this;
    }

    protected override string[] GetActiveModules()
    {
        return _moduleOverride ?? ["*"];
    }

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        await WaitUntilReadyAsync(TimeSpan.FromSeconds(60));
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

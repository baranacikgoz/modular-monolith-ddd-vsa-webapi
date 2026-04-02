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
        => _moduleOverride ?? ["*"];
}

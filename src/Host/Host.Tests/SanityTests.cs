namespace Host.Tests;

[Collection("Host")]
public class SanityTests(HostTestFactory factory)
{
    // Eagerly start the server during fixture setup — same pattern as HealthCheckTests.
    // A lazy CreateClient() call inside the test body races with DynamicModuleTests
    // factory disposal, which corrupts global Serilog/OTel state before StartServer() runs.
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public void Boot_WithAllModules_ShouldResolveDependencies()
    {
        Assert.NotNull(_client);
    }
}
